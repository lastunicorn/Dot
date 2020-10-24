using System;
using DustInTheWind.Dot.Application.Credits;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class CreditsCommand : ICommand
    {
        private readonly IUseCaseFactory useCaseFactory;
        private readonly CreditsView creditsView;

        public event EventHandler CanExecuteChanges;

        public CreditsCommand(IUseCaseFactory useCaseFactory, CreditsView creditsView)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
            this.creditsView = creditsView ?? throw new ArgumentNullException(nameof(creditsView));
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            CreditsUseCase useCase = useCaseFactory.Create<CreditsUseCase>();
            Credits credits = useCase.Execute();

            creditsView.Display(credits);
        }
    }
}