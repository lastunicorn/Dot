namespace DustInTheWind.Dot.Presentation.ConsoleHelpers
{
    internal interface IChipBuilder
    {
        bool Use(char c);
        ChipBuilderState State { get; set; }
    }
}