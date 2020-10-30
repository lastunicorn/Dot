using System;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Application.NewGame;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.ModuleModel;
using DustInTheWind.Dot.Presentation.Presenters;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class NewGameCommand : ICommand
    {
        private readonly IUseCaseFactory useCaseFactory;
        private readonly ModuleEngine moduleEngine;
        private readonly IScreenFactory screenFactory;

        public event EventHandler CanExecuteChanges;

        public NewGameCommand(IUseCaseFactory useCaseFactory, ModuleEngine moduleEngine, IScreenFactory screenFactory)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
            this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));
            this.screenFactory = screenFactory ?? throw new ArgumentNullException(nameof(screenFactory));
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            CreateNewGameUseCase useCase = useCaseFactory.Create<CreateNewGameUseCase>();
            useCase.Execute();

            GamePresenter gamePresenter = screenFactory.Create<GamePresenter>();
            gamePresenter.Display();
        }
    }
}