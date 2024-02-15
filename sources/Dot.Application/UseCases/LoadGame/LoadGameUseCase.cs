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
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Ports.ConfigAccess;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using DustInTheWind.Dot.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.Dot.Application.UseCases.LoadGame;

public class LoadGameUseCase : IRequestHandler<LoadGameRequest>
{
    private readonly ILoadGameView view;
    private readonly IGameSavingTerminal gameSavingTerminal;
    private readonly Game game;
    private readonly IGameSlotRepository gameSlotRepository;
    private readonly IGameSettings gameSettings;
    private readonly ModuleEngine moduleEngine;

    public LoadGameUseCase(ILoadGameView loadGameView, IGameSavingTerminal gameSavingTerminal, Game game,
        IGameSlotRepository gameSlotRepository, IGameSettings gameSettings, ModuleEngine moduleEngine)
    {
        view = loadGameView ?? throw new ArgumentNullException(nameof(loadGameView));
        this.gameSavingTerminal = gameSavingTerminal ?? throw new ArgumentNullException(nameof(gameSavingTerminal));
        this.game = game ?? throw new ArgumentNullException(nameof(game));
        this.gameSlotRepository = gameSlotRepository ?? throw new ArgumentNullException(nameof(gameSlotRepository));
        this.gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
        this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));
    }

    public Task Handle(LoadGameRequest request, CancellationToken cancellationToken)
    {
        if (game.IsLoaded)
        {
            if (game.IsChanged)
                SavePreviousGame();

            game.Close();
        }

        GameSlot gameSlot = ChooseGameSlot();

        game.Import(gameSlot.Data.ToExportData());
        game.Open();

        gameSettings.LastSavedGame = gameSlot.Id;

        moduleEngine.RequestToChangeModule("main-menu");

        view.AnnounceSuccess();

        return Task.CompletedTask;
    }

    private GameSlot ChooseGameSlot()
    {
        List<GameSlot> gameSlots = gameSlotRepository.GetAll().ToList();
        GameSlotId gameSlotId = view.AskToChooseGameSlot(gameSlots.Select(x => new GameSlotId(x.Id)));

        if (gameSlotId == null)
            throw new OperationCanceledException();

        return gameSlots.FirstOrDefault(x => x.Id == gameSlotId.Id);
    }

    private void SavePreviousGame()
    {
        bool savePreviousGame = view.AskToSavePreviousGame();

        if (savePreviousGame)
        {
            if (!game.IsLoaded)
                throw new GameNotRunningException();

            List<GameSlot> gameSlots = gameSlotRepository.GetAll().ToList();
            GameSlotId gameSlotId = gameSavingTerminal.SelectGameSlot(gameSlots.Select(x => new GameSlotId(x.Id)));

            if (gameSlotId == null)
                throw new OperationCanceledException();

            GameSlot gameSlot = gameSlots.FirstOrDefault(x => x.Id == gameSlotId.Id);

            gameSlot.Data = game.Export().ToStorageData();
            gameSlotRepository.AddOrReplace(gameSlot);

            gameSettings.LastSavedGame = gameSlot.Id;
        }
    }
}