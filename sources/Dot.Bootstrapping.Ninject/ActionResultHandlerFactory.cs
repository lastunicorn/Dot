using System;
using DustInTheWind.Dot.AdventureGame;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using Ninject;

namespace DustInTheWind.Dot.Bootstrapping.Ninject
{
    public class ActionResultHandlerFactory : IActionResultHandlerFactory
    {
        private readonly IKernel kernel;

        public ActionResultHandlerFactory(IKernel kernel)
        {
            this.kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }

        public IResultHandler Create(Type resultHandlerType)
        {
            return kernel.Get(resultHandlerType) as IResultHandler;
        }
    }
}