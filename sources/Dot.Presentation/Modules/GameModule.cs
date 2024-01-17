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

using DustInTheWind.ConsoleTools.Modularization;
using DustInTheWind.Dot.Presentation.Presenters;

namespace DustInTheWind.Dot.Presentation.Modules;

public class GameModule : IModule
{
    private readonly IScreenFactory screenFactory;
    private GamePresenter gamePresenter;

    public ModuleId Id { get; } = "game";

    public ModuleEngine ModuleEngine { get; set; }

    public GameModule(IScreenFactory screenFactory)
    {
        this.screenFactory = screenFactory ?? throw new ArgumentNullException(nameof(screenFactory));
    }

    public void Run()
    {
        gamePresenter = screenFactory.Create<GamePresenter>();
        gamePresenter.Display();
    }

    public void RequestExit()
    {
        gamePresenter.RequestExit();
    }
}