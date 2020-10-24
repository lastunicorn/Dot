using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.ModuleModel;
using DustInTheWind.Dot.Presentation;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.Demo
{
    internal class GameApplication : StandardGameApplication
    {
        public GameApplication(ApplicationView view, ModuleEngine moduleEngine, IScreenFactory screenFactory)
            : base(view, moduleEngine, screenFactory)
        {
        }
    }
}