using System;

namespace DustInTheWind.Dot.Application.UseCases
{
    public class CreditsUseCase
    {
        private readonly ICreditsView view;

        public CreditsUseCase(ICreditsView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Execute()
        {
            view.Display();
        }
    }
}