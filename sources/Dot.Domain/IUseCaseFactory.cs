namespace DustInTheWind.Dot.Domain
{
    public interface IUseCaseFactory
    {
        T Create<T>();
    }
}