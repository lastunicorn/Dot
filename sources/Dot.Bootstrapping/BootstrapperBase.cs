using System;
using System.Linq;
using DustInTheWind.Dot.AdventureGame;
using DustInTheWind.Dot.Application.LoadGame;
using DustInTheWind.Dot.Application.NewGame;
using DustInTheWind.Dot.Application.SaveGame;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.ModuleModel;
using DustInTheWind.Dot.Domain.SaveModel;
using DustInTheWind.Dot.GameStorage.Binary;
using DustInTheWind.Dot.Presentation;
using DustInTheWind.Dot.Presentation.Presenters;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.Bootstrapping
{
    public abstract class BootstrapperBase
    {
        public void Run()
        {
            IServicesContainer servicesContainer = CreateServicesContainer();
            ConfigureServices(servicesContainer);

            IServiceProvider serviceProvider = servicesContainer.BuildServiceProvider();

            IGameApplication application = serviceProvider.GetService<IGameApplication>();
            application.Run();
        }

        protected abstract IServicesContainer CreateServicesContainer();

        protected virtual void ConfigureServices(IServicesContainer servicesContainer)
        {
            servicesContainer.AddSingleton<GameRepository>();
            servicesContainer.AddTransient<ResultHandlersCollection>();
            servicesContainer.AddSingleton<ModuleEngine>();

            servicesContainer.AddTransient<IGameSlotRepository, GameSlotRepository>();
            servicesContainer.AddTransient<IGameSettings, GameSettings>();

            servicesContainer.AddTransient<IUserInterface, UserInterface>();
            servicesContainer.AddTransient<ILoadGameView, LoadGameView>();
            servicesContainer.AddTransient<ISaveGameView, SaveGameView>();
            servicesContainer.AddTransient<MainMenuPresenter>();
            servicesContainer.AddTransient<MainMenuView>();
            
            servicesContainer.AddTransient<CreateNewGameUseCase>();

            Type applicationType = RetrieveApplicationType();
            if (applicationType != null)
                servicesContainer.AddSingleton(typeof(IGameApplication), applicationType);

            Type gameType = RetrieveGameType();
            if (gameType != null)
                servicesContainer.AddTransient(typeof(IGameBase), gameType);
        }

        private static Type RetrieveApplicationType()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.FullName.StartsWith("DustInTheWind.Dot"))
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(x => typeof(IGameApplication).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
        }

        private static Type RetrieveGameType()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.FullName.StartsWith("DustInTheWind.Dot"))
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(x => typeof(IGameBase).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
        }
    }
}