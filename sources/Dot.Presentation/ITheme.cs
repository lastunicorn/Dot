using System;

namespace DustInTheWind.Dot.Presentation
{
    public interface ITheme
    {
        ConsoleColor DefaultColor { get; }
        ConsoleColor StoryTellerColor { get; }
        ConsoleColor ObjectColor { get; }
        ConsoleColor ActionColor { get; }
        ConsoleColor ChapterTitleColor { get; }
        ConsoleColor TitleColor { get; }
        ConsoleColor ChapterImageColor { get; }
        ConsoleColor SuggestionColor { get; }
    }
}