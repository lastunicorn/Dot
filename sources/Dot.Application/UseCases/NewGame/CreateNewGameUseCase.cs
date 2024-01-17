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
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.Application.UseCases.NewGame;

public class CreateNewGameUseCase
{
    private readonly GameRepository gameRepository;
    private readonly IGameFactory gameFactory;
    private readonly ModuleEngine moduleEngine;

    public CreateNewGameUseCase(GameRepository gameRepository, IGameFactory gameFactory, ModuleEngine moduleEngine)
    {
        this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        this.gameFactory = gameFactory ?? throw new ArgumentNullException(nameof(gameFactory));
        this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));
    }

    public void Execute()
    {
        IGame game = gameRepository.Get();
        game?.Close();

        IGame newGame = gameFactory.Create();
        newGame.InitializeNew();
        game = newGame;
        gameRepository.Add(game);

        moduleEngine.RequestToChangeModule("game");
    }
}