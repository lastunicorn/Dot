using System;
using DustInTheWind.Dot.Application.UseCases;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.ModuleModel;
using DustInTheWind.Dot.Presentation.Presenters;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class ContinueLastGameCommand : ICommand
    {
        private readonly IUseCaseFactory useCaseFactory;
        private readonly ModuleEngine moduleEngine;
        private readonly IScreenFactory screenFactory;

        public event EventHandler CanExecuteChanges;

        public ContinueLastGameCommand(IUseCaseFactory useCaseFactory, ModuleEngine moduleEngine, IScreenFactory screenFactory)
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
            LoadGameUseCase useCase = useCaseFactory.Create<LoadGameUseCase>();
            useCase.Execute();

            moduleEngine.RequestToChangeModule("game");

            GamePresenter gamePresenter = screenFactory.Create<GamePresenter>();
            gamePresenter.Display();
        }
    }
}