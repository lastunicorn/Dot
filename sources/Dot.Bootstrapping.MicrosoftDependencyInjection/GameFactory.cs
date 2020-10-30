using System;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Bootstrapping.MicrosoftDependencyInjection
{
    public class GameFactory : IGameFactory
    {
        private readonly IServiceProvider serviceProvider;

        public GameFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IGameBase CreateNew()
        {
            IGameBase game = (IGameBase)serviceProvider.GetService(typeof(IGameBase));
            game.InitializeNew();
            return game;
        }

        public IGameBase CreateFrom(StorageData gameSlotData)
        {
            IGameBase game = (IGameBase)serviceProvider.GetService(typeof(IGameBase));
            game.Import(gameSlotData);
            return game;
        }
    }
}