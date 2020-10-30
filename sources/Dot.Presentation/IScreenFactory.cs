namespace DustInTheWind.Dot.Presentation
{
    public interface IScreenFactory
    {
        T Create<T>();
    }
}