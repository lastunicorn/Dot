using System;
using System.Collections.Generic;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Presentation.Controls;

namespace DustInTheWind.Dot.Presentation.Views
{
    public class CreditsView : ViewBase
    {
        private readonly Audio audio;

        public CreditsView(Audio audio)
            : base(audio)
        {
            this.audio = audio ?? throw new ArgumentNullException(nameof(audio));
        }

        public void Display(Credits credits)
        {
            InfoBlock infoBlock = new InfoBlock(audio)
            {
                Texts = new List<string>
                {
                    string.Format("Scenarist: {{{{{0}}}}}", credits.Scenarist),
                    string.Format("Programmer: {{{{{0}}}}}", credits.Programmer)
                }
            };
            infoBlock.Display();
        }
    }
}