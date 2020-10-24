namespace DustInTheWind.Dot.ConsoleHelpers
{
    //internal class TextBlock2 : IEnumerable<TextBlockLine>
    //{
    //    private enum ParsingState { Text, Braker, ActionStart, Action, ActionEnd, ObjectStart, Object, ObjectEnd }

    //    private readonly TextChipBuilder textChipBuilder = new TextChipBuilder();

    //    private readonly List<IChipBuilder> chipBuilders = new List<IChipBuilder>
    //    {
    //        new ActionChipBuilder(),
    //        new ObjectChipBuilder()
    //    };

    //    private readonly string text;
    //    private readonly int lineLength;
    //    private readonly int wordWrapPadding;

    //    private int startIndex;
    //    private int brakeIndex;
    //    private int brakeLength;
    //    private int currentLineLength;
    //    private int currentIndex;

    //    private ParsingState parsingState;
    //    private TextBlockLine currentLine;
    //    private IChipBuilder currentBuilder;

    //    public TextBlock2(string text, int lineLength, int wordWrapPadding = 0)
    //    {
    //        this.text = text;
    //        this.lineLength = lineLength;
    //        this.wordWrapPadding = wordWrapPadding;
    //    }

    //    public IEnumerator<TextBlockLine> GetEnumerator()
    //    {
    //        parsingState = ParsingState.Text;
    //        currentBuilder = null;
    //        StartLineFromIndex(0);

    //        if (string.IsNullOrEmpty(text) || lineLength <= 0)
    //        {
    //            currentLine.Add(string.Empty);
    //            yield return currentLine;
    //        }
    //        else
    //        {
    //            for (currentIndex = 0; currentIndex < text.Length; currentIndex++)
    //            {
    //                char c = text[currentIndex];

    //                // check if current line is completed

    //                if (currentLineLength >= lineLength)
    //                {
    //                    if (brakeIndex == -1 || c == ' ')
    //                    {
    //                        // force brake

    //                        brakeIndex = currentIndex;
    //                        brakeLength = 0;
    //                    }

    //                    currentLine.Add(text.Substring(startIndex, brakeIndex - startIndex));
    //                    yield return currentLine;

    //                    StartLineFromIndex(brakeIndex + brakeLength);
    //                }

    //                // analyze current char

    //                switch (c)
    //                {
    //                    case ' ':
    //                        HandleSpace();
    //                        break;

    //                    default:
    //                        if (currentBuilder == null)
    //                        {
    //                            IChipBuilder builder = TryUseBuilders(c);

    //                            if (builder == null)
    //                            {
    //                                textChipBuilder.Use(c);
    //                                currentLineLength++;
    //                                parsingState = ParsingState.Text;
    //                            }
    //                            else
    //                            {
    //                                brakeIndex = currentIndex;
    //                                brakeLength = 0;

    //                                currentLine.Add(text.Substring(startIndex, brakeIndex - startIndex));

    //                                startIndex = currentIndex;

    //                                currentBuilder = builder;

    //                                if (currentBuilder.State == ChipBuilderState.On)
    //                                    currentLineLength++;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            bool isCharUsed = currentBuilder.Use(c);

    //                            if (isCharUsed)
    //                            {
    //                                switch (currentBuilder.State)
    //                                {
    //                                    case ChipBuilderState.Off:
    //                                        isCharUsed = false;
    //                                        break;

    //                                    case ChipBuilderState.Starting:
    //                                        parsingState = ParsingState.ActionStart;
    //                                        break;

    //                                    case ChipBuilderState.On:
    //                                        currentLineLength++;
    //                                        parsingState = ParsingState.Action;
    //                                        break;

    //                                    case ChipBuilderState.Ending:
    //                                        break;

    //                                    default:
    //                                        throw new ArgumentOutOfRangeException();
    //                                }
    //                            }
    //                        }
    //                        //HandleNormalCharacter();
    //                        break;
    //                }
    //            }

    //            if (startIndex < text.Length)
    //            {
    //                currentLine.Add(text.Substring(startIndex));
    //                yield return currentLine;
    //            }
    //        }
    //    }

    //    private IChipBuilder TryUseBuilders(char c)
    //    {
    //        foreach (IChipBuilder builder in chipBuilders)
    //        {
    //            bool isCharUsed = builder.Use(c);

    //            if (!isCharUsed)
    //                continue;

    //            switch (builder.State)
    //            {
    //                case ChipBuilderState.Off:
    //                    break;

    //                case ChipBuilderState.Starting:
    //                    parsingState = ParsingState.ActionStart;
    //                    return builder;

    //                case ChipBuilderState.On:
    //                    parsingState = ParsingState.Action;
    //                    return builder;

    //                case ChipBuilderState.Ending:
    //                    break;

    //                default:
    //                    throw new ArgumentOutOfRangeException();
    //            }
    //        }

    //        return null;
    //    }

    //    //private void HandleActionStart()
    //    //{
    //    //    switch (parsingState)
    //    //    {
    //    //        case ParsingState.Text:
    //    //            parsingState = ParsingState.ActionStart;
    //    //            break;

    //    //        case ParsingState.Braker:
    //    //            parsingState = ParsingState.ActionStart;
    //    //            break;

    //    //        case ParsingState.ActionStart:
    //    //            parsingState = ParsingState.Action;
    //    //            break;

    //    //        case ParsingState.Action:
    //    //            break;

    //    //        case ParsingState.ActionEnd:
    //    //            parsingState = ParsingState.Action;
    //    //            break;

    //    //        case ParsingState.ObjectStart:
    //    //            parsingState = ParsingState.Text;
    //    //            break;

    //    //        case ParsingState.Object:
    //    //            break;

    //    //        case ParsingState.ObjectEnd:
    //    //            parsingState = ParsingState.Object;
    //    //            break;

    //    //        default:
    //    //            throw new ArgumentOutOfRangeException();
    //    //    }
    //    //}

    //    private void StartLineFromIndex(int index)
    //    {
    //        bool isFirstLine = index == 0;

    //        currentLine = new TextBlockLine(isFirstLine);

    //        startIndex = index;
    //        brakeIndex = -1;
    //        brakeLength = 0;
    //        currentLineLength = isFirstLine ? 0 : wordWrapPadding;
    //    }

    //    private void HandleSpace()
    //    {
    //        if (currentIndex == startIndex)
    //        {
    //            // Skip leading spaces.

    //            startIndex++;
    //            return;
    //        }

    //        if (parsingState == ParsingState.Braker)
    //        {
    //            // Increment the breaker length.

    //            brakeLength++;
    //        }
    //        else
    //        {
    //            // Start a breaker.

    //            parsingState = ParsingState.Braker;
    //            brakeIndex = currentIndex;
    //            brakeLength = 1;
    //        }

    //        currentLineLength++;
    //    }

    //    private void HandleNormalCharacter()
    //    {
    //        currentLineLength++;
    //        parsingState = ParsingState.Text;
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}
}