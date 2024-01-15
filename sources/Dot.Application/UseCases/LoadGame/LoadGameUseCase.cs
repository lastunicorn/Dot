using System;
using System.Collections.Generic;
using Dot.GameHosting;
using DustInTheWind.Dot.Application.UseCases.SaveGame;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace DustInTheWind.Dot.Application.UseCases.LoadGame
{
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
            IEnumerable<GameSlot> gameSlots = gameSlotRepository.GetAll();
            GameSlot gameSlot = view.AskToChooseGameSlot(gameSlots);

            if (gameSlot == null)
                throw new OperationCanceledException();

            return gameSlot;
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
}