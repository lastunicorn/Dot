using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;

namespace DustInTheWind.Dot.Presentation
{
    public interface ICommandFactory
    {
        T Create<T>()
            where T : ICommand;
    }
}