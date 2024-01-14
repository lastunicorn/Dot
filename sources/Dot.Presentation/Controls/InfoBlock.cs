using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.Presentation.ConsoleHelpers;

namespace DustInTheWind.Dot.Presentation.Controls
{
    public class InfoBlock
    {
        private readonly Audio audio;
        private List<string> texts = new List<string>();

        public string Text
        {
            get => texts.FirstOrDefault();
            set
            {
                if (texts.Count == 0)
                    texts.Add(value);
                else
                    texts[0] = value;
            }
        }

        public List<string> Texts
        {
            get => texts;
            set => texts = value ?? new List<string>();
        }

        public InfoBlock(Audio audio)
        {
            this.audio = audio ?? throw new ArgumentNullException(nameof(audio));
        }

        public void Display()
        {
            AudioTextBox audioTextBox = new AudioTextBox(audio)
            {
                Margins = new Margins(3, 1, 3, 2),
                Color = DefaultTheme.Instance.DefaultColor,
                ActionColor = DefaultTheme.Instance.ActionColor,
                ObjectColor = DefaultTheme.Instance.ObjectColor
            };
            audioTextBox.Display(texts);
        }
    }
}