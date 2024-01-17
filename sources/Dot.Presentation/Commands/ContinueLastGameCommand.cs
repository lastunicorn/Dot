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
using DustInTheWind.Dot.Application.UseCases.LoadGame;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;

namespace DustInTheWind.Dot.Presentation.Commands;

internal class ContinueLastGameCommand : ICommand
{
    private readonly IUseCaseFactory useCaseFactory;
    private readonly Game game;
    private readonly IGameSettings gameSettings;

    public event EventHandler CanExecuteChanges;

    public ContinueLastGameCommand(IUseCaseFactory useCaseFactory, Game game, IGameSettings gameSettings)
    {
        this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        this.game = game ?? throw new ArgumentNullException(nameof(game));
        this.gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
    }

    public bool CanExecute()
    {
        return !game.IsLoaded && gameSettings.LastSavedGame.HasValue;
    }

    public void Execute()
    {
        LoadGameUseCase useCase = useCaseFactory.Create<LoadGameUseCase>();
        useCase.Execute();
    }
}