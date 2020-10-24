namespace DustInTheWind.Dot.Domain
{
    public interface IScreenFactory
    {
        T Create<T>();
    }
}