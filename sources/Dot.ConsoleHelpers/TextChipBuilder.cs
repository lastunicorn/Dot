namespace DustInTheWind.Dot.Presentation.ConsoleHelpers
{
    internal class TextChipBuilder : IChipBuilder
    {
        public ChipBuilderState State { get; set; }

        public bool Use(char c)
        {
            return false;
        }
    }
}