using System;

namespace DustInTheWind.Dot.Application.UseCases
{
    public class HelpUseCase
    {
        private readonly IHelpView helpView;

        public HelpUseCase(IHelpView helpView)
        {
            this.helpView = helpView ?? throw new ArgumentNullException(nameof(helpView));
        }

        public void Execute()
        {
            helpView.DisplayHelpInformation();
        }
    }
}