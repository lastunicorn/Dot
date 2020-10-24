using System;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.ModuleModel;
using DustInTheWind.Dot.Presentation.Presenters;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.Presentation
{
    public class StandardGameApplication : IGameApplication
    {
        private readonly IScreenFactory screenFactory;
        protected ApplicationView View { get; }

        protected ModuleEngine ModuleEngine { get; }

        public StandardGameApplication(ApplicationView view, ModuleEngine moduleEngine, IScreenFactory screenFactory)
        {
            this.screenFactory = screenFactory ?? throw new ArgumentNullException(nameof(screenFactory));
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

            MainMenuPresenter presenter = screenFactory.Create<MainMenuPresenter>();
            presenter.Display();
            //ModuleEngine.Run();

            View.DisplayGoodByeMessage();
        }

        public void Close()
        {
            ModuleEngine.Close();
        }
    }
}