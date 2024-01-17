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
using DustInTheWind.Dot.Application.UseCases.SaveGame;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using DustInTheWind.Dot.Ports.PresentationAccess;

namespace DustInTheWind.Dot.Application.UseCases.LoadGame;

public class LoadGameUseCase
{
    private readonly ILoadGameView view;
    private readonly GameRepository gameRepository;
    private readonly IGameFactory gameFactory;
    private readonly IUseCaseFactory useCaseFactory;
    private readonly IGameSlotRepository gameSlotRepository;
    private readonly IGameSettings gameSettings;
    private readonly ModuleEngine moduleEngine;

    public LoadGameUseCase(ILoadGameView loadGameView, GameRepository gameRepository, IGameFactory gameFactory,
        IUseCaseFactory useCaseFactory, IGameSlotRepository gameSlotRepository, IGameSettings gameSettings, ModuleEngine moduleEngine)
    {
        view = loadGameView ?? throw new ArgumentNullException(nameof(loadGameView));
        this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        this.gameFactory = gameFactory ?? throw new ArgumentNullException(nameof(gameFactory));
        this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        this.gameSlotRepository = gameSlotRepository ?? throw new ArgumentNullException(nameof(gameSlotRepository));
        this.gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
        this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));
    }

    public void Execute()
    {
        IGame game = gameRepository.Get();

        if (game != null)
        {
            if (game.IsChanged)
                SavePreviousGame();

            game.Close();
        }

        GameSlot gameSlot = ChooseGameSlot();

        IGame newGame = gameFactory.Create();
        newGame.Import(gameSlot.Data.ToExportData());
        game = newGame;

        gameRepository.Add(game);

        game.Open();

        gameSettings.LastSavedGame = gameSlot.Id;

        moduleEngine.RequestToChangeModule("main-menu");

        view.AnnounceSuccess();
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
            SaveGameUseCase saveGameUseCase = useCaseFactory.Create<SaveGameUseCase>();
            saveGameUseCase.Execute();
        }
    }
}