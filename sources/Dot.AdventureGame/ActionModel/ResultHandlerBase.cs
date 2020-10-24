namespace DustInTheWind.Dot.AdventureGame.ActionModel
{
    public abstract class ResultHandlerBase<T> : IResultHandler<T>
    {
        public abstract void Handle(T result);

        public void Handle(object result)
        {
            if (result is T resultT)
                Handle(resultT);
        }
    }
}