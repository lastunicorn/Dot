using System;
using System.Collections.Generic;
using System.Linq;
using Dot.GameHosting;
using DustInTheWind.Dot.AdventureGame;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Application.UseCases.LoadGame;
using DustInTheWind.Dot.Application.UseCases.NewGame;
using DustInTheWind.Dot.Application.UseCases.SaveGame;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.GameSavesAccess;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using DustInTheWind.Dot.Presentation;
using DustInTheWind.Dot.Presentation.Modules;
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

            IModuleHost host = serviceProvider.GetService<IModuleHost>();
            host.Run();
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
            servicesContainer.AddTransient<MainMenuView>();

            servicesContainer.AddTransient<CreateNewGameUseCase>();

            servicesContainer.AddTransient<IModule, MenuModule>();
            servicesContainer.AddTransient<IModule, GameModule>();

            Type applicationType = RetrieveApplicationType();
            if (applicationType != null)
                servicesContainer.AddSingleton(typeof(IModuleHost), applicationType);

            Type gameType = RetrieveGameType();
            if (gameType != null)
                servicesContainer.AddTransient(typeof(IGame), gameType);
        }

        private static Type RetrieveApplicationType()
        {
            return GetAllClientTypes()
                .FirstOrDefault(x => typeof(IModuleHost).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
        }

        private static Type RetrieveGameType()
        {
            return GetAllClientTypes()
                .FirstOrDefault(x => typeof(IGame).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
        }

        private static IEnumerable<Type> GetAllClientTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith("DustInTheWind.Dot.Demo") || !x.FullName.StartsWith("DustInTheWind.Dot"))
                .SelectMany(x => x.GetTypes());
        }
    }
}