using System;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace DustInTheWind.Dot.MicrosoftDependencyInjection
{
    public class GameFactory : IGameFactory
    {
        private readonly IServiceProvider serviceProvider;

        public GameFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IGame CreateNew()
        {
            IGame game = (IGame)serviceProvider.GetService(typeof(IGame));
            game.InitializeNew();
            return game;
        }

        public IGame CreateFrom(StorageData gameSlotData)
        {
            IGame game = (IGame)serviceProvider.GetService(typeof(IGame));
            game.Import(gameSlotData);
            return game;
        }
    }
}