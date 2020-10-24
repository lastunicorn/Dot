using DustInTheWind.Dot.Presentation.Controls;

namespace DustInTheWind.Dot.Presentation.Views
{
    internal class GameView
    {
        private readonly CommandLinePrompter prompter;

        public string PrompterText
        {
            set => prompter.Text = value;
        }

        public GameView()
        {
            prompter = new CommandLinePrompter();
        }

        public string GetUserCommand()
        {
            prompter.Display();
            return prompter.LastCommand;
        }
    }
}