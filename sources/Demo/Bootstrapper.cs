using DustInTheWind.Dot.AdventureGame;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.ModuleModel;
using DustInTheWind.Dot.Presentation;
using DustInTheWind.Dot.Presentation.Views;
using Ninject;

namespace DustInTheWind.Dot.Demo
{
    internal class Bootstrapper
    {
        public void Run()
        {
            IKernel kernel = ConfigureServices();

            IGameApplication application = kernel.Get<IGameApplication>();
            application.Run();
        }

        private static IKernel ConfigureServices()
        {
            StandardKernel kernel = new StandardKernel();

            kernel.Bind<IGameApplication>().To<GameApplication>().InSingletonScope();
            kernel.Bind<Audio>().ToSelf().InSingletonScope();
            kernel.Bind<GameRepository>().ToSelf().InSingletonScope();
            kernel.Bind<ResultHandlersCollection>().ToSelf();
            kernel.Bind<ModuleEngine>().ToSelf().InSingletonScope();
            kernel.Bind<IGameFactory>().To<GameFactory>();
            kernel.Bind<IUseCaseFactory>().To<UseCaseFactory>();
            kernel.Bind<IScreenFactory>().To<ScreenFactory>();
            kernel.Bind<ICommandFactory>().To<CommandFactory>();
            kernel.Bind<IActionResultHandlerFactory>().To<ActionResultHandlerFactory>();

            kernel.Bind<IUserInterface>().To<UserInterface>();
            kernel.Bind<ILoadGameView>().To<LoadGameView>();
            kernel.Bind<ISaveGameView>().To<SaveGameView>();

            return kernel;
        }
    }
}