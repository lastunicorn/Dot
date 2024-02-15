using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.Domain.AudioTextModel;
using DustInTheWind.Dot.Presentation.ConsoleHelpers;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.ConsoleUtil;

namespace DustInTheWind.Dot.Presentation.Controls
{
    public class AudioTextBox
    {
        private readonly Audio audio;

        public Border Border { get; set; }
        public Margins Margins { get; set; }
        public Paddings Paddings { get; set; }
        public ConsoleColor Color { get; set; }
        public ConsoleColor ActionColor { get; set; }
        public ConsoleColor ObjectColor { get; set; }
        public int WordWrapPadding { get; set; }
        public bool CanSkipAudio { get; set; }

        private string leftMargin;
        private string rightMargin;
        private string leftPadding;

        private int fullWidth;
        private int outerWidth;
        private int innerWidth;
        private int textAreaWidth;

        public AudioTextBox(Audio audio)
        {
            this.audio = audio ?? throw new ArgumentNullException(nameof(audio));

            Color = ConsoleColor.Gray;
            CanSkipAudio = true;
        }

        public void Display(string text, string title = null)
        {
            Display(AudioText.FromText(text), title);
        }

        public void Display(IEnumerable<string> texts, string title = null)
        {
            AudioTextList audioMultiText = AudioTextList.FromTexts(texts);
            Display(audioMultiText, title);
        }

        public void Display(IAudioText audioTexts, string title = null)
        {
            Console.CursorVisible = false;

            try
            {
                fullWidth = Console.BufferWidth - 1;
                outerWidth = fullWidth - Margins.Left - Margins.Right;
                textAreaWidth = fullWidth - Margins.Left - Margins.Right - Paddings.Left - Paddings.Right - (Border.IsLeftVisible ? 1 : 0) - (Border.IsRightVisible ? 1 : 0);
                innerWidth = fullWidth - Margins.Left - Margins.Right - (Border.IsLeftVisible ? 1 : 0) - (Border.IsRightVisible ? 1 : 0);

                leftMargin = new string(' ', Margins.Left);
                rightMargin = new string(' ', Margins.Right);
                leftPadding = new string(' ', Paddings.Left);

                WriteTopMargin();
                WriteLineTopBorder(title);
                WriteTopPadding();
                WriteContent(audioTexts);
                WriteBottomPaddings();
                WriteBottomBorder();
                WriteBottomMargins();
            }
            finally
            {
                Console.CursorVisible = true;
            }
        }

        private void WriteTopMargin()
        {
            for (int i = 0; i < Margins.Top; i++)
                CustomConsole.WriteLine();
        }

        private void WriteLineTopBorder(string title)
        {
            if (!Border.IsTopVisible)
                return;

            CustomConsole.Write(leftMargin, Color);

            if (Border.IsLeftVisible)
                CustomConsole.Write(Border.TopLeft, Color);

            TextBlock textBlock = new TextBlock(title, int.MaxValue);
            TextBlockLine line = textBlock.First();

            if (line.TextLength == 0)
            {
                CustomConsole.Write(new string(Border.Top, innerWidth), Color);
            }
            else
            {
                CustomConsole.Write(Border.Top + " ", Color);

                foreach (Chip chip in line.Where(x => !string.IsNullOrEmpty(x.Text)))
                {
                    switch (chip.Type)
                    {
                        case ChipType.Text:
                            CustomConsole.Write(chip.Text, Color);
                            break;
                        case ChipType.Action:
                            CustomConsole.Write(chip.Text, ActionColor);
                            break;
                        case ChipType.Object:
                            CustomConsole.Write(chip.Text, ObjectColor);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                CustomConsole.Write(" " + new string(Border.Top, innerWidth - line.TextLength - 3), Color);
            }

            if (Border.IsRightVisible)
                CustomConsole.Write(Border.TopRight, Color);

            CustomConsole.Write(rightMargin, Color);

            CustomConsole.WriteLine();
        }

        private void WriteTopPadding()
        {
            for (int i = 0; i < Paddings.Top; i++)
                WriteLineEmptyLine();
        }

        private void WriteContent(IAudioText audioTexts)
        {
            bool isFirstLine = true;

            PlayBackgroundSound(audioTexts.MusicAudioFileName, () =>
            {
                foreach (AudioText audioText in audioTexts)
                {
                    if (!isFirstLine)
                        WriteLineEmptyLine();

                    string[] lines = audioText.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    foreach (string line in lines)
                        WriteLineLongText(line);

                    WriteBottomPaddings();
                    WriteBottomBorder(false);
                    WriteBottomMargins();
                    PlayVoice(audioText.AudioFileName);

                    MoveCursorAfterText();

                    isFirstLine = false;
                }
            });
        }

        public void PlayBackgroundSound(string audioFileName, Action action)
        {
            if (audioFileName == null)
            {
                action();
                return;
            }

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

        private void MoveCursorAfterText()
        {
            Console.CursorTop = Console.CursorTop - Margins.Bottom - 1 - Paddings.Bottom + 1;
            Console.CursorLeft = 0;
        }

        private void WriteBottomPaddings()
        {
            for (int i = 0; i < Paddings.Bottom; i++)
                WriteLineEmptyLine();
        }

        private void WriteBottomBorder()
        {
            if (!Border.IsBottomVisible)
                return;

            WriteBottomBorder(true);
            CustomConsole.WriteLine();
        }

        private void WriteBottomMargins()
        {
            for (int i = 0; i < Margins.Bottom; i++)
                CustomConsole.WriteLine();
        }

        private void WriteBottomBorder(bool isFinalBorder)
        {
            if (!Border.IsBottomVisible)
                return;

            CustomConsole.Write(leftMargin, Color);

            if (Border.IsLeftVisible)
                CustomConsole.Write(Border.BottomLeft, Color);

            string innerBorderLine;

            if (!isFinalBorder && CanSkipAudio)
            {
                const string skipText = "ESC >>";
                innerBorderLine = new string(Border.Bottom, innerWidth - skipText.Length - 3) + " " + skipText + " " + Border.Bottom;
            }
            else
            {
                innerBorderLine = new string(Border.Bottom, innerWidth);
            }

            CustomConsole.Write(innerBorderLine, Color);

            if (Border.IsRightVisible)
                CustomConsole.Write(Border.BottomRight, Color);

            CustomConsole.Write(rightMargin, Color);
        }

        private void WriteLineLongText(string text)
        {
            TextBlock textBlock = new TextBlock(text, textAreaWidth, WordWrapPadding);

            foreach (TextBlockLine line in textBlock)
            {
                if (!line.IsFirst)
                    CustomConsole.WriteLine();

                WriteShortText(line);
            }

            CustomConsole.WriteLine();
        }

        private void WriteShortText(TextBlockLine line)
        {
            CustomConsole.Write(leftMargin, Color);

            if (Border.IsLeftVisible)
                CustomConsole.Write(Border.Left, Color);

            CustomConsole.Write(leftPadding, Color);

            foreach (Chip chip in line)
            {
                switch (chip.Type)
                {
                    case ChipType.Text:
                        CustomConsole.Write(chip.Text, Color);
                        break;
                    case ChipType.Action:
                        CustomConsole.Write(chip.Text, ActionColor);
                        break;
                    case ChipType.Object:
                        CustomConsole.Write(chip.Text, ObjectColor);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            CustomConsole.Write(new string(' ', textAreaWidth + Paddings.Right - line.TextLength));

            if (Border.IsRightVisible)
                CustomConsole.Write(Border.Right, Color);

            CustomConsole.Write(rightMargin, Color);
        }

        private void WriteLineEmptyLine()
        {
            CustomConsole.Write(leftMargin, Color);

            if (Border.IsLeftVisible)
                CustomConsole.Write(Border.Left, Color);

            CustomConsole.Write(new string(' ', innerWidth), Color);

            if (Border.IsRightVisible)
                CustomConsole.Write(Border.Right, Color);

            CustomConsole.Write(rightMargin, Color);

            CustomConsole.WriteLine();
        }

        private void PlayVoice(string audioFileName)
        {
            if (audio == null || audioFileName == null)
                return;
#if DEBUG_AUDIO_FILE
            CustomConsole.WriteLine(audioFileName, ConsoleColor.Yellow);
#endif
            audio.Play(audioFileName, AudioChannelType.Voice);

            if (CanSkipAudio)
            {
                KeyListener keyListener = new KeyListener();
                keyListener.KeyPressed += (sender, e) =>
                {
                    audio.Stop(AudioChannelType.Voice);
                    keyListener.StopListen();
                };
                keyListener.StartListen();

                audio.WaitChannel(AudioChannelType.Voice);
                keyListener.StopListen();
            }
            else
            {
                audio.WaitChannel(AudioChannelType.Voice);
            }

            Thread.Sleep(50);
        }

        private void Pause()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            KeyListener keyListener = new KeyListener();
            keyListener.KeyPressed += (sender, e) =>
            {
                autoResetEvent.Set();
                keyListener.StopListen();
            };

            keyListener.StartListen();
            autoResetEvent.WaitOne();
            keyListener.StopListen();

            Thread.Sleep(50);
        }
    }
}