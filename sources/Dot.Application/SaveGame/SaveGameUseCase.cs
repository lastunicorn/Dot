using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Application.SaveGame
{
    public class SaveGameUseCase
    {
        private readonly ISaveGameView view;
        private readonly GameRepository gameRepository;
        private readonly IGameSlotRepository gameSlotRepository;
        private readonly IGameSettings gameSettings;

        public SaveGameUseCase(ISaveGameView saveGameView, GameRepository gameRepository, IGameSlotRepository gameSlotRepository, IGameSettings gameSettings)
        {
            view = saveGameView ?? throw new ArgumentNullException(nameof(saveGameView));
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            this.gameSlotRepository = gameSlotRepository ?? throw new ArgumentNullException(nameof(gameSlotRepository));
            this.gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
        }

        public void Execute()
        {
            IGameBase game = gameRepository.Get();

            if (game == null)
                throw new Exception("There is no game to be saved.");

            IEnumerable<GameSlot> gameSlots = gameSlotRepository.GetAll();
            GameSlot gameSlot = view.AskToChooseGameSlot(gameSlots);

            if (gameSlot == null)
                throw new OperationCanceledException();

            gameSlot.Data = game.Export();
            gameSlotRepository.AddOrReplace(gameSlot);

            gameSettings.LastSavedGame = gameSlot.Id;
        }
    }
}