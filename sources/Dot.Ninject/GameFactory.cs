using System;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using Ninject;

namespace DustInTheWind.Dot.Ninject
{
    public class GameFactory : IGameFactory
    {
        private readonly IKernel kernel;

        public GameFactory(IKernel kernel)
        {
            this.kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }

        public IGame CreateNew()
        {
            IGame game = kernel.Get<IGame>();
            game.InitializeNew();
            return game;
        }

        public IGame CreateFrom(StorageData gameSlotData)
        {
            IGame game = kernel.Get<IGame>();
            game.Import(gameSlotData);
            return game;
        }
    }
}