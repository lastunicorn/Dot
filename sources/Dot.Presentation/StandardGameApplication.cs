using System;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.ModuleModel;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.Presentation
{
    public class StandardGameApplication : IGameApplication
    {
        private volatile bool closeWasRequested;

        protected ApplicationView View { get; }

        protected ModuleEngine ModuleEngine { get; }

        public StandardGameApplication(ApplicationView view, ModuleEngine moduleEngine)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
            ModuleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));

            moduleEngine.ModuleRunException += HandleModuleRunException;
        }

        protected void HandleModuleRunException(object sender, ModuleRunExceptionEventArgs e)
        {
            switch (e.Exception)
            {
                case NotImplementedException _:
                    View.DisplayFunctionalityNotImplementedInfo();
                    e.NextModule = "main-menu";
                    break;

                case OperationCanceledException _:
                    View.DisplayOperationCanceledInfo();
                    e.NextModule = "main-menu";
                    break;

                default:
                    View.DisplayError(e.Exception);
                    e.NextModule = null;
                    break;
            }
        }

        public void Run()
        {
            View.ResetConsoleWindow();
            View.DisplayApplicationHeader();

            closeWasRequested = false;

            while (!closeWasRequested)
            {
                try
                {
                    ModuleEngine.Run();
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception ex)
                {
                    CustomConsole.WriteError(ex);
                }
            }

            View.DisplayGoodByeMessage();
        }

        public void Close()
        {
            closeWasRequested = true;
            ModuleEngine.Close();
        }
    }
}