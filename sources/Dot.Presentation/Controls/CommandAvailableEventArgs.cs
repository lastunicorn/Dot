using System;

namespace DustInTheWind.Dot.Presentation.Controls
{
    public class CommandAvailableEventArgs : EventArgs
    {
        public string Command { get; }

        public CommandAvailableEventArgs(string command)
        {
            Command = command;
        }
    }
}