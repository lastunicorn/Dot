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
using DustInTheWind.Dot.Application.UseCases.NewGame;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.GameSavesAccess;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using DustInTheWind.Dot.Presentation;
using DustInTheWind.Dot.Presentation.Views;
using Ninject;
using DustInTheWind.Dot.Presentation.Modules;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.GameHosting;
using DustInTheWind.Dot.Ports.UserAccess;
using DustInTheWind.Dot.UserAccess;
using MediatR;
using Ninject.Components;
using Ninject;
using Ninject.Infrastructure;
using Ninject.Planning.Bindings;
using Ninject.Planning.Bindings.Resolvers;
using Ninject.Modules;

namespace DustInTheWind.Dot.Setup.Ninject;

internal static class NinjectExtensions
{
    public static IKernel BindDot(this IKernel kernel)
    {
        kernel.Bind<IServiceProvider>().To<ServiceProvider>();

        kernel.Bind<IMediator>().ToMethod(context =>
        {
            Mediator mediator = context.Kernel.Get<Mediator>();
            return mediator;
        }).InSingletonScope();

        //kernel.Bind(x =>
        //    x.FromThisAssembly()
        //        .SelectAllClasses()
        //        .InheritedFrom<IRequestHandler<>>()
        //        .BindToSelf());

        kernel.Bind<RequestBus>().ToSelf().InSingletonScope();


        kernel.Bind<ResultHandlersCollection>().ToSelf();
        kernel.Bind<ModuleEngine>().ToSelf().InSingletonScope();

        kernel.Bind<IPresentation>().To<ApplicationView>();
        kernel.Bind<IGameSlotRepository>().To<GameSlotRepository>();
        kernel.Bind<IGameSettings>().To<GameSettings>();

        kernel.Bind<IUserInterface>().To<UserInterface>();
        kernel.Bind<ILoadGameView>().To<LoadGameView>();
        kernel.Bind<IGameSavingTerminal>().To<GameSavingTerminal>();
        kernel.Bind<MainMenuView>().ToSelf();

        kernel.Bind<CreateNewGameUseCase>().ToSelf();

        kernel.Bind<IModule>().To<MenuModule>();
        kernel.Bind<IModule>().To<GameModule>();

        kernel.Bind<ModuleHost>().ToSelf().InSingletonScope();

        kernel.Bind<IScreenFactory>().To<ScreenFactory>();
        kernel.Bind<ICommandFactory>().To<CommandFactory>();
        kernel.Bind<IActionResultHandlerFactory>().To<ActionResultHandlerFactory>();

        return kernel;
    }
}

internal class ServiceProvider : IServiceProvider
{
    private readonly IKernel kernel;

    public ServiceProvider(IKernel kernel)
    {
        this.kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
    }

    public object GetService(Type serviceType)
    {
        return kernel.Get(serviceType);
    }
}

//public class ContravariantBindingResolver : NinjectComponent, IBindingResolver
//{
//    /// <summary>
//    /// Returns any bindings from the specified collection that match the specified service.
//    /// </summary>
//    public IEnumerable<IBinding> Resolve(Multimap<Type, IBinding> bindings, Type service)
//    {
//        if (service.IsGenericType)
//        {
//            var genericType = service.GetGenericTypeDefinition();
//            var genericArguments = genericType.GetGenericArguments();
//            var isContravariant = genericArguments.Length == 1
//                                  && genericArguments
//                                      .Single()
//                                      .GenericParameterAttributes.HasFlag(GenericParameterAttributes.Contravariant);
//            if (isContravariant)
//            {
//                var argument = service.GetGenericArguments().Single();
//                var matches = bindings.Where(kvp => kvp.Key.IsGenericType
//                                                    && kvp.Key.GetGenericTypeDefinition() == genericType
//                                                    && kvp.Key.GetGenericArguments().Single() != argument
//                                                    && kvp.Key.GetGenericArguments().Single().IsAssignableFrom(argument))
//                    .SelectMany(kvp => kvp.Value);
//                return matches;
//            }
//        }

//        return Enumerable.Empty<IBinding>();
//    }
//}