using System;

namespace DustInTheWind.Dot.AdventureGame.ActionModel
{
    public interface IActionResultHandlerFactory
    {
        IResultHandler Create(Type resultHandlerType);
    }
}