using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.Presentation.Views
{
    public class CreditsView : ViewBase
    {
        public CreditsView(Audio audio)
            : base(audio)
        {
        }

        public void Display(Credits credits)
        {
            DisplayInfo(string.Format("Scenarist: {{{0}}}", credits.Scenarist));
            DisplayInfo(string.Format("Programmer: {{{0}}}", credits.Programmer));
        }
    }
}