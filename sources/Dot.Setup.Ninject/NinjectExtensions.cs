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

using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Application.UseCases.NewGame;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.GameSavesAccess;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using DustInTheWind.Dot.Ports.PresentationAccess;
using DustInTheWind.Dot.Presentation;
using DustInTheWind.Dot.Presentation.Views;
using Ninject;
using DustInTheWind.Dot.Presentation.Modules;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.GameHosting;

namespace DustInTheWind.Dot.Setup.Ninject;

internal static class NinjectExtensions
{
    public static IKernel BindDot(this IKernel kernel)
    {
        kernel.Bind<GameRepository>().ToSelf().InSingletonScope();
        kernel.Bind<ResultHandlersCollection>().ToSelf();
        kernel.Bind<ModuleEngine>().ToSelf().InSingletonScope();

        kernel.Bind<IPresentation>().To<ApplicationView>();
        kernel.Bind<IGameSlotRepository>().To<GameSlotRepository>();
        kernel.Bind<IGameSettings>().To<GameSettings>();

        kernel.Bind<IUserInterface>().To<UserInterface>();
        kernel.Bind<ILoadGameView>().To<LoadGameView>();
        kernel.Bind<ISaveGameView>().To<SaveGameView>();
        kernel.Bind<MainMenuView>().ToSelf();

        kernel.Bind<CreateNewGameUseCase>().ToSelf();

        kernel.Bind<IModule>().To<MenuModule>();
        kernel.Bind<IModule>().To<GameModule>();

        kernel.Bind<IModuleHost>().To<StandardModuleHost>().InSingletonScope();

        kernel.Bind<IGameFactory>().To<GameFactory>();
        kernel.Bind<IUseCaseFactory>().To<UseCaseFactory>();
        kernel.Bind<IScreenFactory>().To<ScreenFactory>();
        kernel.Bind<ICommandFactory>().To<CommandFactory>();
        kernel.Bind<IActionResultHandlerFactory>().To<ActionResultHandlerFactory>();

        return kernel;
    }
}