namespace DustInTheWind.Dot.Presentation.ConsoleHelpers
{
    public struct Chip
    {
        public string Text { get; set; }
        public ChipType Type { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}|{1}]", Text, Type);
        }
    }
}