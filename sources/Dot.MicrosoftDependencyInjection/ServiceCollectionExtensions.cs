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
using DustInTheWind.Dot.Application.UseCases.Credits;
using DustInTheWind.Dot.Application.UseCases.Exit;
using DustInTheWind.Dot.Application.UseCases.LoadGame;
using DustInTheWind.Dot.Application.UseCases.MainMenu;
using DustInTheWind.Dot.Application.UseCases.NewGame;
using DustInTheWind.Dot.Application.UseCases.ResumeGame;
using DustInTheWind.Dot.Application.UseCases.SaveGame;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.ConfigAccess;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.GameHosting;
using DustInTheWind.Dot.GameSavesAccess;
using DustInTheWind.Dot.GameSavesAccess.Binary;
using DustInTheWind.Dot.Ports.ConfigAccess;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using DustInTheWind.Dot.Ports.UserAccess;
using DustInTheWind.Dot.Presentation;
using DustInTheWind.Dot.Presentation.Commands;
using DustInTheWind.Dot.Presentation.Modules;
using DustInTheWind.Dot.Presentation.Presenters;
using DustInTheWind.Dot.Presentation.Views;
using DustInTheWind.Dot.UserAccess;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.Dot.Setup.MicrosoftDependencyInjection;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDot(this IServiceCollection servicesCollection)
    {
        servicesCollection.AddSingleton<IMediator, Mediator>();
        servicesCollection.AddSingleton<RequestBus>();

        servicesCollection.AddTransient<ResultHandlersCollection>();
        servicesCollection.AddSingleton<ModuleEngine>();

        servicesCollection.AddTransient<Audio>();
        servicesCollection.AddTransient<IPresentation, ApplicationView>();
        servicesCollection.AddTransient<IGameSlotRepository, GameSlotRepository>();
        servicesCollection.AddTransient<IGameSettings, GameSettings>();

        servicesCollection.AddTransient<IUserInterface, UserInterface>();
        servicesCollection.AddTransient<ILoadGameView, LoadGameView>();
        servicesCollection.AddTransient<IGameSavingTerminal, GameSavingTerminal>();

        servicesCollection.AddTransient<IModule, MenuModule>();
        servicesCollection.AddTransient<MainMenuPresenter>();
        servicesCollection.AddTransient<MainMenuView>();

        servicesCollection.AddTransient<IModule, GameModule>();
        servicesCollection.AddTransient<GamePresenter>();
        servicesCollection.AddTransient<GameView>();

        servicesCollection.AddSingleton<ModuleHost>();

        servicesCollection.AddTransient<IScreenFactory, ScreenFactory>();
        servicesCollection.AddTransient<ICommandFactory, CommandFactory>();
        servicesCollection.AddTransient<IActionResultHandlerFactory, ActionResultHandlerFactory>();

        servicesCollection.AddTransient<NewGameCommand>();
        servicesCollection.AddTransient<ResumeGameCommand>();
        servicesCollection.AddTransient<SaveCommand>();
        servicesCollection.AddTransient<LoadCommand>();
        servicesCollection.AddTransient<CreditsCommand>();
        servicesCollection.AddTransient<CreditsView>();
        servicesCollection.AddTransient<ExitCommand>();

        servicesCollection.AddTransient<IRequestHandler<MainMenuRequest>, MainMenuUseCase>();
        servicesCollection.AddTransient<IRequestHandler<CreateNewGameRequest>, CreateNewGameUseCase>();
        servicesCollection.AddTransient<IRequestHandler<ResumeGameRequest>, ResumeGameUseCase>();
        servicesCollection.AddTransient<IRequestHandler<SaveGameRequest>, SaveGameUseCase>();
        servicesCollection.AddTransient<IRequestHandler<LoadGameRequest>, LoadGameUseCase>();
        servicesCollection.AddTransient<IRequestHandler<CreditsRequest, CreditsResponse>, CreditsUseCase>();
        servicesCollection.AddTransient<IRequestHandler<ExitRequest>, ExitUseCase>();

        return servicesCollection;
    }
}