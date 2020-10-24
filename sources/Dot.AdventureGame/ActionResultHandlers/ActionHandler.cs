using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class ActionHandler : ResultHandlerBase<Action>
    {
        public override void Handle(Action action)
        {
            action();
        }
    }
}