using DustInTheWind.Dot.AdventureGame;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Presentation;

namespace DustInTheWind.Dot.Bootstrapping.MicrosoftDependencyInjection
{
    public static class ServicesContainerExtensions
    {
        public static IServicesContainer ConfigureFactories(this IServicesContainer servicesContainer)
        {
            servicesContainer.AddTransient<IGameFactory, GameFactory>();
            servicesContainer.AddTransient<IUseCaseFactory, UseCaseFactory>();
            servicesContainer.AddTransient<IScreenFactory, ScreenFactory>();
            servicesContainer.AddTransient<ICommandFactory, CommandFactory>();
            servicesContainer.AddTransient<IActionResultHandlerFactory, ActionResultHandlerFactory>();

            return servicesContainer;
        }
    }
}