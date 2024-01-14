using System;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.ConsoleUtil;

namespace DustInTheWind.Dot.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Bootstrapper bootstrapper = new Bootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                CustomConsole.WriteError("Fatal error");
                CustomConsole.WriteError(ex);

                CustomConsole.Pause();
            }
        }
    }
}