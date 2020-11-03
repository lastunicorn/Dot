using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.ConsoleHelpers;
using DustInTheWind.Dot.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;
using DustInTheWind.Dot.Presentation.Controls;

namespace DustInTheWind.Dot.Presentation
{
    public class UserInterface : IUserInterface
    {
        private readonly Audio audio;

        public UserInterface(Audio audio)
        {
            this.audio = audio ?? throw new ArgumentNullException(nameof(audio));
        }

        public void Display(List<Tuple<string, string, string>> list, int minColumn1Width, int topMargin, int bottomMargin)
        {
            for (int i = 0; i < topMargin; i++)
                CustomConsole.WriteLine();

            int column1Width = list
                .Select(x => new TextBlock(x.Item1, int.MaxValue))
                .Max(x => x.First().TextLength);

            if (column1Width < minColumn1Width)
                column1Width = minColumn1Width;

            int column2Width = list
                .Select(x => new TextBlock(x.Item2, int.MaxValue))
                .Max(x => x.First().TextLength);

            int column3Width = Console.BufferWidth - column1Width - 1 - column2Width - 1 - 1;

            foreach ((string item1, string item2, string item3) in list)
            {
                TextBlock textBlock1 = new TextBlock(item1, column1Width);
                TextBlock textBlock2 = new TextBlock(item2, column2Width);
                TextBlock textBlock3 = new TextBlock(item3, column3Width);

                using (IEnumerator<TextBlockLine> enumerator1 = textBlock1.GetEnumerator())
                using (IEnumerator<TextBlockLine> enumerator2 = textBlock2.GetEnumerator())
                using (IEnumerator<TextBlockLine> enumerator3 = textBlock3.GetEnumerator())
                {
                    while (true)
                    {
                        bool moveNext1 = enumerator1.MoveNext();
                        bool moveNext2 = enumerator2.MoveNext();
                        bool moveNext3 = enumerator3.MoveNext();

                        if (!moveNext1 && !moveNext2 && !moveNext3)
                            break;

                        if (moveNext1)
                        {
                            WriteShortText(enumerator1.Current);
                            CustomConsole.Write(new string(' ', column1Width - enumerator1.Current.TextLength));
                        }
                        else
                        {
                            CustomConsole.Write(new string(' ', column1Width));
                        }

                        CustomConsole.Write(" ");

                        if (moveNext2)
                        {
                            WriteShortText(enumerator2.Current);
                            CustomConsole.Write(new string(' ', column2Width - enumerator2.Current.TextLength));
                        }
                        else
                        {
                            CustomConsole.Write(new string(' ', column2Width));
                        }

                        CustomConsole.Write(" ");

                        if (moveNext3)
                        {
                            WriteShortText(enumerator3.Current);
                            CustomConsole.Write(new string(' ', column3Width - enumerator3.Current.TextLength));
                        }
                        else
                        {
                            CustomConsole.Write(new string(' ', column3Width));
                        }

                        CustomConsole.WriteLine();
                    }
                }
            }

            for (int i = 0; i < bottomMargin; i++)
                CustomConsole.WriteLine();
        }

        private void WriteShortText(TextBlockLine line)
        {
            foreach (Chip chip in line)
            {
                switch (chip.Type)
                {
                    case ChipType.Text:
                        CustomConsole.Write(chip.Text, DefaultTheme.Instance.DefaultColor);
                        break;

                    case ChipType.Action:
                        CustomConsole.Write(chip.Text, DefaultTheme.Instance.ActionColor);
                        break;

                    case ChipType.Object:
                        CustomConsole.Write(chip.Text, DefaultTheme.Instance.ObjectColor);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void DisplayInfoBlock(IEnumerable<string> texts)
        {
            InfoBlock infoBlock = new InfoBlock(audio)
            {
                Texts = texts.ToList()
            };
            infoBlock.Display();
        }

        public string Question(string text)
        {
            return CommandLinePrompter.QuickDisplay(text);
        }

        public void DisplayInfo(string text)
        {
            InfoBlock infoBlock = new InfoBlock(audio)
            {
                Text = text
            };
            infoBlock.Display();

            //if (text == null)
            //    return;

            //AudioTextBox audioTextBox = new AudioTextBox(audio)
            //{
            //    Margins = new Margins(3, 1, 3, 2),
            //    Color = DefaultTheme.Instance.DefaultColor,
            //    ActionColor = DefaultTheme.Instance.ActionColor,
            //    ObjectColor = DefaultTheme.Instance.ObjectColor
            //};
            //audioTextBox.Display(text);
        }

        public void DisplayInfo(IEnumerable<string> texts)
        {
            if (texts == null)
                return;

            AudioTextBox audioTextBox = new AudioTextBox(audio)
            {
                Margins = new Margins(3, 1, 3, 2),
                Color = DefaultTheme.Instance.DefaultColor,
                ActionColor = DefaultTheme.Instance.ActionColor,
                ObjectColor = DefaultTheme.Instance.ObjectColor
            };
            audioTextBox.Display(texts);
        }

        public void DisplayInfo(IAudioText audioTexts)
        {
            if (audioTexts == null)
                return;

            AudioTextBox audioTextBox = new AudioTextBox(audio)
            {
                Margins = new Margins(3, 1, 3, 2),
                Color = DefaultTheme.Instance.DefaultColor,
                ActionColor = DefaultTheme.Instance.ActionColor,
                ObjectColor = DefaultTheme.Instance.ObjectColor
            };
            audioTextBox.Display(audioTexts);
        }

        public void DisplayStoryTeller(IAudioText audioTexts, string title = null)
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

        public void DisplaySuggestion(IEnumerable<string> texts, int wordWrapPadding = 0)
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
                ObjectColor = DefaultTheme.Instance.ObjectColor,
                WordWrapPadding = wordWrapPadding
            };
            audioTextBox.Display(texts);
        }

        public void DisplaySuggestion(string text)
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

        public void DisplaySuggestion(IEnumerable<string> texts)
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

        public void DisplaySuggestion(SuggestionBlock suggestionBlock)
        {
            if (suggestionBlock == null)
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
            audioTextBox.Display(suggestionBlock.Texts);
        }

        public void PlayBackgroundSound(string audioFileName, Action action)
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

        public T PlayBackgroundSound<T>(string audioFileName, Func<T> action)
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

        public void DisplayAsciiArt(string id, int marginTop = 0, int marginBottom = 0)
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