namespace DustInTheWind.Dot.Presentation.Controls
{
    public class DefaultTheme : ITheme
    {
        public ConsoleColor DefaultColor { get; }
        public ConsoleColor StoryTellerColor { get; }
        public ConsoleColor ObjectColor { get; }
        public ConsoleColor ActionColor { get; }
        public ConsoleColor ChapterTitleColor { get; }
        public ConsoleColor TitleColor { get; }
        public ConsoleColor ChapterImageColor { get; }
        public ConsoleColor SuggestionColor { get; }

        public static ITheme Instance { get; } = new DefaultTheme();

        public DefaultTheme()
        {
            DefaultColor = ConsoleColor.Gray;
            StoryTellerColor = ConsoleColor.DarkGray;
            ObjectColor = ConsoleColor.Magenta;
            ActionColor = ConsoleColor.DarkGreen;
            ChapterTitleColor = ConsoleColor.White;
            TitleColor = ConsoleColor.Green;
            ChapterImageColor = ConsoleColor.Gray;
            SuggestionColor = ConsoleColor.Gray;
        }
    }
}