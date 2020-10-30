using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Application.SaveGame;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Application.LoadGame
{
    public class LoadGameUseCase
    {
        private readonly ILoadGameView view;
        private readonly GameRepository gameRepository;
        private readonly IGameFactory gameFactory;
        private readonly IUseCaseFactory useCaseFactory;
        private readonly IGameSlotRepository gameSlotRepository;
        private readonly IGameSettings gameSettings;

        public LoadGameUseCase(ILoadGameView loadGameView, GameRepository gameRepository, IGameFactory gameFactory,
            IUseCaseFactory useCaseFactory, IGameSlotRepository gameSlotRepository, IGameSettings gameSettings)
        {
            view = loadGameView ?? throw new ArgumentNullException(nameof(loadGameView));
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            this.gameFactory = gameFactory ?? throw new ArgumentNullException(nameof(gameFactory));
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
            this.gameSlotRepository = gameSlotRepository ?? throw new ArgumentNullException(nameof(gameSlotRepository));
            this.gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
        }

        public void Execute()
        {
            IGameBase game = gameRepository.Get();

            if (game != null)
            {
                if (game.IsChanged)
                    SavePreviousGame();

                game.Close();
            }

            // select the slot
            GameSlot gameSlot = ChooseGameSlot();

            // create G from file
            game = gameFactory.CreateFrom(gameSlot.Data);
            gameRepository.Add(game);

            // Open G
            game.Open();

            gameSettings.LastSavedGame = gameSlot.Id;

            // display success message
            view.DisplaySuccessMessage();
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