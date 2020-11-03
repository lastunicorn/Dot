using System;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Application.UseCases.NewGame;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class NewGameCommand : ICommand
    {
        private readonly IUseCaseFactory useCaseFactory;

        public event EventHandler CanExecuteChanges;

        public NewGameCommand(IUseCaseFactory useCaseFactory)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            CreateNewGameUseCase useCase = useCaseFactory.Create<CreateNewGameUseCase>();
            useCase.Execute();
        }
    }
}