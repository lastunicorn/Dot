// Dot
// Copyright (C) 2020-2024 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.GameHosting;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.Dot.Setup.MicrosoftDependencyInjection;

public class DotApplication
{
    private readonly ServiceCollection serviceCollection;
    private Action<Exception> exceptionHandler;

    public DotApplication()
    {
        serviceCollection = new ServiceCollection();
        serviceCollection.AddDot();
    }

    public DotApplication ConfigureServices(Action<IServiceCollection> action)
    {
        action(serviceCollection);
        return this;
    }

    public DotApplication WithGame<TGame>()
        where TGame : class, IGame
    {
        serviceCollection.AddTransient<IGame, TGame>();
        return this;
    }

    public void Run()
    {
        try
        {
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IModuleHost host = serviceProvider.GetService<IModuleHost>();
            host.Run();
        }
        catch (Exception ex)
        {
            if (exceptionHandler != null)
                exceptionHandler(ex);
            else
                throw;
        }
    }

    public DotApplication WhenUnhandledException(Action<Exception> exceptionHandler)
    {
        this.exceptionHandler = exceptionHandler;
        return this;
    }
}