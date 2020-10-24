using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;

namespace DustInTheWind.Dot.AdventureGame
{
    public interface IActionResultHandlerFactory
    {
        IResultHandler Create(Type resultHandlerType);
    }
}