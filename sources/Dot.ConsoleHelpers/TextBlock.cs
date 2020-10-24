using System;
using System.Collections;
using System.Collections.Generic;

namespace DustInTheWind.Dot.ConsoleHelpers
{
    public class TextBlock : IEnumerable<TextBlockLine>
    {
        private readonly string text;
        private readonly int lineLength;
        private readonly int wordWrapPadding;

        private TextBlockLine currentLine;
        private int startIndex;

        private readonly List<ChipMarker> chipMarkers = new List<ChipMarker>
        {
            new ChipMarker
            {
                StartMarker = "{{",
                EndMarker = "}}",
                ChipType = ChipType.Object
            },
            new ChipMarker
            {
                StartMarker = "<<",
                EndMarker = ">>",
                ChipType = ChipType.Action
            }
        };

        public TextBlock(string text, int lineLength, int wordWrapPadding = 0)
        {
            this.text = text;
            this.lineLength = lineLength;
            this.wordWrapPadding = wordWrapPadding;
        }

        public IEnumerator<TextBlockLine> GetEnumerator()
        {
            currentLine = new TextBlockLine(true);

            if (string.IsNullOrEmpty(text) || lineLength <= 0)
            {
                currentLine.Add(string.Empty);
                yield return currentLine;
            }
            else
            {
                startIndex = 0;
                ChipMarker currentMarker = null;

                while (startIndex < text.Length)
                {
                    if (currentMarker == null)
                    {
                        int markerStartIndex = int.MaxValue;

                        foreach (ChipMarker chipMarker in chipMarkers)
                        {
                            int index = text.IndexOf(chipMarker.StartMarker, startIndex, StringComparison.Ordinal);

                            if (index >= 0 && index < markerStartIndex)
                            {
                                markerStartIndex = index;
                                currentMarker = chipMarker;
                            }
                        }

                        if (currentMarker == null)
                        {
                            // text until the end

                            foreach (TextBlockLine line in CreateChipsUntilIndex(text.Length, ChipType.Text))
                                yield return line;
                        }
                        else
                        {
                            foreach (TextBlockLine line in CreateChipsUntilIndex(markerStartIndex, ChipType.Text))
                                yield return line;

                            startIndex += currentMarker.StartMarker.Length;
                        }
                    }
                    else
                    {
                        int endMarkerIndex = text.IndexOf(currentMarker.EndMarker, startIndex, StringComparison.Ordinal);
                        bool foundMarker = endMarkerIndex != -1;
                        int endIndex = foundMarker ? endMarkerIndex : text.Length;

                        foreach (TextBlockLine line in CreateChipsUntilIndex(endIndex, currentMarker.ChipType))
                            yield return line;

                        startIndex = foundMarker ? endMarkerIndex + currentMarker.EndMarker.Length : text.Length;

                        currentMarker = null;
                    }
                }

                if (currentLine.TextLength > 0)
                    yield return currentLine;
            }
        }

        private IEnumerable<TextBlockLine> CreateChipsUntilIndex(int endIndex, ChipType chipType)
        {
            string textChip = text.Substring(startIndex, endIndex - startIndex);

            if (textChip.Length > 0)
            {
                IEnumerable<TextBlockLine> lines = CreateChips(textChip, chipType);

                foreach (TextBlockLine line in lines)
                    yield return line;
            }

            startIndex = endIndex;
        }

        private IEnumerable<TextBlockLine> CreateChips(string textChip, ChipType chipType)
        {
            if (!currentLine.IsFirst && currentLine.TextLength == 0)
                textChip = textChip.TrimStart();

            while (true)
            {
                if (textChip.Length == 0)
                    yield break;

                int remainingLineSpace = lineLength - currentLine.TextLength - (currentLine.IsFirst ? 0 : wordWrapPadding);

                if (textChip.Length <= remainingLineSpace)
                {
                    string padding = !currentLine.IsFirst && currentLine.TextLength == 0 ? new string(' ', wordWrapPadding) : string.Empty;

                    if (textChip.Length == remainingLineSpace)
                    {
                        currentLine.Add(padding + textChip.TrimEnd(), chipType);

                        if (currentLine.TextLength + 1 >= remainingLineSpace)
                        {
                            yield return currentLine;
                            currentLine = new TextBlockLine();
                        }
                    }
                    else
                    {
                        currentLine.Add(padding + textChip, chipType);
                    }

                    break;
                }

                // search for the last space in chip
                int spaceIndex = textChip.LastIndexOf(' ', remainingLineSpace);

                bool spaceFound = spaceIndex >= 0;
                if (spaceFound)
                {
                    // brake chip between words

                    string substring = textChip.Substring(0, spaceIndex);
                    if (substring.Length > 0)
                    {
                        string padding = !currentLine.IsFirst && currentLine.TextLength == 0 ? new string(' ', wordWrapPadding) : string.Empty;
                        currentLine.Add(padding + substring, chipType);
                    }

                    yield return currentLine;
                    currentLine = new TextBlockLine();

                    textChip = textChip.Substring(spaceIndex + 1);
                }
                else
                {
                    if (currentLine.TextLength == 0)
                    {
                        // force brake chip

                        string padding = !currentLine.IsFirst && currentLine.TextLength == 0 ? new string(' ', wordWrapPadding) : string.Empty;
                        currentLine.Add(padding + textChip.Substring(0, remainingLineSpace), chipType);
                        yield return currentLine;
                        currentLine = new TextBlockLine();

                        textChip = textChip.Substring(remainingLineSpace);
                    }
                    else
                    {
                        currentLine.TrimEnd();

                        yield return currentLine;
                        currentLine = new TextBlockLine();
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}