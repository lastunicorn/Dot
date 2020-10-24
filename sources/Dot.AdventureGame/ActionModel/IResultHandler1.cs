namespace DustInTheWind.Dot.AdventureGame.ActionModel
{
    public interface IResultHandler<in T> : IResultHandler
    {
        void Handle(T result);
    }
}