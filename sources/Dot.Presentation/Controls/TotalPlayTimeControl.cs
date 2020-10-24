using System;
using DustInTheWind.Dot.AudioSupport;

namespace DustInTheWind.Dot.Presentation.Controls
{
    public class TotalPlayTimeControl
    {
        private readonly Audio audio;

        public TimeSpan Time { get; set; }

        public TotalPlayTimeControl(Audio audio)
        {
            this.audio = audio ?? throw new ArgumentNullException(nameof(audio));
        }

        public void Display()
        {
            string timeFormat = Time.Days > 0
                ? @"d\.hh\:mm\:ss"
                : @"hh\:mm\:ss";

            string playTimeText = string.Format("Total play time = {0}", Time.ToString(timeFormat));

            InfoBlock infoBlock = new InfoBlock(audio)
            {
                Text = playTimeText
            };
            infoBlock.Display();
        }
    }
}