namespace DustInTheWind.Dot.Application
{
    public interface IUseCaseFactory
    {
        T Create<T>()
            where T : class;
    }
}