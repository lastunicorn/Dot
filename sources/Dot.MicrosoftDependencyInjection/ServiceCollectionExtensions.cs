// Dot
// Copyright (C) 2020-2024 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using DustInTheWind.ConsoleTools.Modularization;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Application.UseCases.NewGame;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.GameHosting;
using DustInTheWind.Dot.GameSavesAccess;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using DustInTheWind.Dot.Ports.PresentationAccess;
using DustInTheWind.Dot.Presentation;
using DustInTheWind.Dot.Presentation.Modules;
using DustInTheWind.Dot.Presentation.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.Dot.Setup.MicrosoftDependencyInjection;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDot(this IServiceCollection servicesCollection)
    {
        servicesCollection.AddSingleton<GameRepository>();
        servicesCollection.AddTransient<ResultHandlersCollection>();
        servicesCollection.AddSingleton<ModuleEngine>();

        servicesCollection.AddTransient<IPresentation, ApplicationView>();
        servicesCollection.AddTransient<IGameSlotRepository, GameSlotRepository>();
        servicesCollection.AddTransient<IGameSettings, GameSettings>();

        servicesCollection.AddTransient<IUserInterface, UserInterface>();
        servicesCollection.AddTransient<ILoadGameView, LoadGameView>();
        servicesCollection.AddTransient<ISaveGameView, SaveGameView>();
        servicesCollection.AddTransient<MainMenuView>();

        servicesCollection.AddTransient<CreateNewGameUseCase>();

        servicesCollection.AddTransient<IModule, MenuModule>();
        servicesCollection.AddTransient<IModule, GameModule>();

        servicesCollection.AddSingleton<ModuleHost>();

        servicesCollection.AddTransient<IGameFactory, GameFactory>();
        servicesCollection.AddTransient<IUseCaseFactory, UseCaseFactory>();
        servicesCollection.AddTransient<IScreenFactory, ScreenFactory>();
        servicesCollection.AddTransient<ICommandFactory, CommandFactory>();
        servicesCollection.AddTransient<IActionResultHandlerFactory, ActionResultHandlerFactory>();

        return servicesCollection;
    }
}