using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.ModuleModel;

namespace DustInTheWind.Dot.Application.UseCases
{
    public class LoadGameUseCase
    {
        private readonly ILoadGameView view;
        private readonly GameRepository gameRepository;
        private readonly IGameFactory gameFactory;
        private readonly ModuleEngine moduleEngine;
        private readonly IUseCaseFactory useCaseFactory;
        private readonly GameSlotRepository gameSlotRepository;
        private readonly IGameSettings gameSettings;

        public LoadGameUseCase(ILoadGameView view, GameRepository gameRepository, IGameFactory gameFactory,
            ModuleEngine moduleEngine, IUseCaseFactory useCaseFactory, GameSlotRepository gameSlotRepository, IGameSettings gameSettings)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            this.gameFactory = gameFactory ?? throw new ArgumentNullException(nameof(gameFactory));
            this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));
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
            game = gameFactory.Create();
            game.Load(gameSlot.Data.Data);
            gameRepository.Add(game);

            // Open G
            game.Open();

            // todo: save the name of the last loaded game.
            //Settings.Default.LastSaveFileName = menu.SelectedItem.Value.FileName;
            //Settings.Default.Save();
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