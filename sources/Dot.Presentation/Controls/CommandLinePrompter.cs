using System;
using DustInTheWind.Dot.ConsoleHelpers.ConsoleUtil;

namespace DustInTheWind.Dot.Presentation.Controls
{
    public class CommandLinePrompter
    {
        public string Text { get; set; }

        public string LastCommand { get; private set; }

        public event EventHandler<CommandAvailableEventArgs> CommandAvailable;

        public void Display()
        {
            string command = null;

            while (string.IsNullOrEmpty(command))
                command = ReadCommand();

            LastCommand = command;

            CommandAvailableEventArgs eventArgs = new CommandAvailableEventArgs(command);
            OnCommandAvailable(eventArgs);
        }

        private string ReadCommand()
        {
            string prompterText = BuildPrompterText();


            string fullText = string.IsNullOrEmpty(prompterText)
                ? "> "
                : prompterText + " > ";

            CustomConsole.WriteEmphasies(fullText);

            return CustomConsole.ReadAction();
        }

        protected virtual string BuildPrompterText()
        {
            return Text;
        }

        public static string QuickDisplay(string text)
        {
            CommandLinePrompter commandLinePrompter = new CommandLinePrompter
            {
                Text = text
            };
            commandLinePrompter.Display();

            return commandLinePrompter.LastCommand;
        }

        protected virtual void OnCommandAvailable(CommandAvailableEventArgs e)
        {
            CommandAvailable?.Invoke(this, e);
        }
    }
}