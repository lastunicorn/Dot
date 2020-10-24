using DustInTheWind.Dot.ConsoleHelpers.UIControls;

namespace DustInTheWind.Dot.Presentation
{
    public interface ICommandFactory
    {
        T Create<T>()
            where T : ICommand;
    }
}