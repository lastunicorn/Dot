using System;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.SaveModel;
using Ninject;

namespace DustInTheWind.Dot.Demo
{
    public class GameFactory : IGameFactory
    {
        private readonly IKernel kernel;

        public GameFactory(IKernel kernel)
        {
            this.kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }

        public IGameBase CreateNew()
        {
            IGameBase game = kernel.Get<IGameBase>();
            game.InitializeNew();
            return game;
        }

        public IGameBase CreateFrom(StorageData gameSlotData)
        {
            IGameBase game = kernel.Get<IGameBase>();
            game.Import(gameSlotData);
            return game;
        }
    }
}