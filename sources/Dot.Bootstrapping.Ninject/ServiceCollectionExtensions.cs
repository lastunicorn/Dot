using DustInTheWind.Dot.AdventureGame;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Presentation;

namespace DustInTheWind.Dot.Bootstrapping.Ninject
{
    public static class ServiceCollectionExtensions
    {
        public static IServicesContainer ConfigureFactories(this IServicesContainer serviceProviderBuilder)
        {
            serviceProviderBuilder.AddTransient<IGameFactory, GameFactory>();
            serviceProviderBuilder.AddTransient<IUseCaseFactory, UseCaseFactory>();
            serviceProviderBuilder.AddTransient<IScreenFactory, ScreenFactory>();
            serviceProviderBuilder.AddTransient<ICommandFactory, CommandFactory>();
            serviceProviderBuilder.AddTransient<IActionResultHandlerFactory, ActionResultHandlerFactory>();

            return serviceProviderBuilder;
        }
    }
}