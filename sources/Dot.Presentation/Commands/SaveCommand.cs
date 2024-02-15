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

using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Application.UseCases.SaveGame;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;

namespace DustInTheWind.Dot.Presentation.Commands;

public class SaveCommand : ICommand
{
    private readonly RequestBus requestBus;
    private readonly Game game;

    public event EventHandler CanExecuteChanges;

    public SaveCommand(RequestBus requestBus, Game game)
    {
        this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
        this.game = game ?? throw new ArgumentNullException(nameof(game));
    }

    public bool CanExecute()
    {
        return game.IsLoaded;
    }

    public void Execute()
    {
        SaveGameRequest request = new();
        requestBus.Send(request).Wait();
    }
}