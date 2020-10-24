using System;
using DustInTheWind.Dot.Domain.GameModel;
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

        public IGameBase Create()
        {
            return kernel.Get<Game>();
        }
    }
}