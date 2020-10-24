namespace DustInTheWind.Dot.ConsoleHelpers
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