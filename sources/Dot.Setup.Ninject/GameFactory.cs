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
using Ninject;

namespace DustInTheWind.Dot.Setup.Ninject;

internal class GameFactory : IGameFactory
{
    private readonly IKernel kernel;

    public GameFactory(IKernel kernel)
    {
        this.kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
    }

    public IGame Create()
    {
        return kernel.Get<IGame>();
    }
}