using System;
using DustInTheWind.Dot.AdventureGame;
using DustInTheWind.Dot.AdventureGame.ActionModel;

namespace DustInTheWind.Dot.MicrosoftDependencyInjection
{
    public class ActionResultHandlerFactory : IActionResultHandlerFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ActionResultHandlerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IResultHandler Create(Type resultHandlerType)
        {
            return serviceProvider.GetService(resultHandlerType) as IResultHandler;
        }
    }
}