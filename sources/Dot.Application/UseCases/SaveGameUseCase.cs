using System;
using System.Collections.Generic;
using System.Reflection;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Application.UseCases
{
    public class SaveGameUseCase
    {
        private readonly ISaveGameView view;
        private readonly GameRepository gameRepository;
        private readonly GameSlotRepository gameSlotRepository;

        public SaveGameUseCase(ISaveGameView view, GameRepository gameRepository, GameSlotRepository gameSlotRepository)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            this.gameSlotRepository = gameSlotRepository ?? throw new ArgumentNullException(nameof(gameSlotRepository));
        }

        public void Execute()
        {
            IGameBase game = gameRepository.Get();

            IEnumerable<GameSlot> gameSlots = gameSlotRepository.GetAll();
            GameSlot gameSlot = view.AskToChooseGameSlot(gameSlots);

            if (gameSlot == null)
                throw new OperationCanceledException();

            gameSlot.Data = new SaveData
            {
                SaveTime = DateTime.UtcNow,
                Version = GetAssemblyVersion(),
                Data = game.Save()
            };
            gameSlotRepository.AddOrReplace(gameSlot);
        }

        private static Version GetAssemblyVersion()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();

            return assemblyName.Version;
        }
    }
}