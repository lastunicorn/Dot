namespace DustInTheWind.Dot.ConsoleHelpers
{
    internal interface IChipBuilder
    {
        bool Use(char c);
        ChipBuilderState State { get; set; }
    }
}