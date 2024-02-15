using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.Domain.AudioTextModel;
using DustInTheWind.Dot.Presentation.ConsoleHelpers;

namespace DustInTheWind.Dot.Presentation.Controls
{
    public class ViewBase
    {
        private readonly Audio audio;

        public ViewBase(Audio audio)
        {
            this.audio = audio ?? throw new ArgumentNullException(nameof(audio));
        }

        protected void DisplayInfo(string text)
        {
            InfoBlock infoBlock = new InfoBlock(audio)
            {
                Text = text
            };
            infoBlock.Display();
        }

        protected void DisplayStoryTeller(IAudioText audioTexts, string title = null)
        {
            if (audioTexts == null)
                return;

            AudioTextBox audioTextBox = new AudioTextBox(audio)
            {
                Margins = new Margins(3, 2),
                Border = Border.CreateDoubleLineBorder(true, true, true, true),
                Paddings = new Paddings(2, 1),
                Color = DefaultTheme.Instance.StoryTellerColor,
                ActionColor = DefaultTheme.Instance.ActionColor,
                ObjectColor = DefaultTheme.Instance.ObjectColor
            };
            audioTextBox.Display(audioTexts, title);
        }

        protected void DisplaySuggestion(IEnumerable<string> texts)
        {
            if (texts == null)
                return;

            AudioTextBox audioTextBox = new AudioTextBox(audio)
            {
                Margins = new Margins(3, 0, 3, 2),
                Border = Border.CreateSingleLineBorder(true, false, false, false),
                Paddings = new Paddings(2, 1, 0, 1),
                Color = DefaultTheme.Instance.SuggestionColor,
                ActionColor = DefaultTheme.Instance.ActionColor,
                ObjectColor = DefaultTheme.Instance.ObjectColor
            };
            audioTextBox.Display(texts);
        }

        protected void DisplaySuggestion(string text)
        {
            if (text == null)
                return;

            AudioTextBox audioTextBox = new AudioTextBox(audio)
            {
                Margins = new Margins(3, 0, 3, 2),
                Border = Border.CreateSingleLineBorder(true, false, false, false),
                Paddings = new Paddings(2, 1, 0, 1),
                Color = DefaultTheme.Instance.SuggestionColor,
                ActionColor = DefaultTheme.Instance.ActionColor,
                ObjectColor = DefaultTheme.Instance.ObjectColor
            };
            audioTextBox.Display(text);
        }

        protected void PlayBackgroundSound(string audioFileName, Action action)
        {
#if DEBUG_AUDIO_FILE
            CustomConsole.WriteLine(audioFileName, ConsoleColor.Yellow);
#endif
            audio.PlayRepeat(audioFileName, AudioChannelType.Music);

            try
            {
                action();
            }
            finally
            {
                audio.Stop(AudioChannelType.Music);
            }
        }

        protected T PlayBackgroundSound<T>(string audioFileName, Func<T> action)
        {
#if DEBUG_AUDIO_FILE
            CustomConsole.WriteLine(audioFileName, ConsoleColor.Yellow);
#endif
            audio.PlayRepeat(audioFileName, AudioChannelType.Music);

            try
            {
                return action();
            }
            finally
            {
                audio.Stop(AudioChannelType.Music);
            }
        }

        protected void DisplayAsciiArt(string id, int marginTop = 0, int marginBottom = 0)
        {
            AsciiImageBox asciiImageBox = new AsciiImageBox
            {
                AsciiPath = id,
                ForegroundColor = DefaultTheme.Instance.DefaultColor,
                MarginTop = marginTop,
                MarginBottom = marginBottom
            };
            asciiImageBox.Display();
        }
    }
}