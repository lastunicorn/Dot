using System;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Application.UseCases.Credits;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class CreditsCommand : ICommand
    {
        private readonly CreditsView creditsView;
        private readonly IUseCaseFactory useCaseFactory;

        public event EventHandler CanExecuteChanges;

        public CreditsCommand(CreditsView creditsView, IUseCaseFactory useCaseFactory)
        {
            this.creditsView = creditsView ?? throw new ArgumentNullException(nameof(creditsView));
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
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