using System;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Application.UseCases.LoadGame;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class LoadCommand : ICommand
    {
        private readonly IUseCaseFactory useCaseFactory;

        public event EventHandler CanExecuteChanges;

        public LoadCommand(IUseCaseFactory useCaseFactory)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            LoadGameUseCase useCase = useCaseFactory.Create<LoadGameUseCase>();
            useCase.Execute();
        }
    }
}