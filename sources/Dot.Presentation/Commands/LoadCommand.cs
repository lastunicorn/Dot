using System;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Application.LoadGame;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Presentation.Presenters;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class LoadCommand : ICommand
    {
        private readonly IUseCaseFactory useCaseFactory;
        private readonly IScreenFactory screenFactory;

        public event EventHandler CanExecuteChanges;

        public LoadCommand(IUseCaseFactory useCaseFactory, IScreenFactory screenFactory)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
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

            GamePresenter gamePresenter = screenFactory.Create<GamePresenter>();
            gamePresenter.Display();
        }
    }
}