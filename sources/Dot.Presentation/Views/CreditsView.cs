using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.AudioSupport;

namespace DustInTheWind.Dot.Presentation.Views
{
    public class CreditsView : ViewBase, ICreditsView
    {
        public CreditsView(Audio audio)
            : base(audio)
        {
        }

        public void Display()
        {
            DisplayInfo("Scenarist: {{Alexandru Iuga}}");
            DisplayInfo("Programmer: {{Alexandru Iuga}}");
        }
    }
}