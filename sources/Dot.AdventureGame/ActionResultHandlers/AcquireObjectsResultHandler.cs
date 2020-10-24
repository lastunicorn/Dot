using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class AcquireObjectsResultHandler : ResultHandlerBase<AcquireObjectsResult>
    {
        private readonly GameBase game;

        public AcquireObjectsResultHandler(GameBase game)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public override void Handle(AcquireObjectsResult acquireObjectsResult)
        {
            if (acquireObjectsResult.Objects != null)
            {
                foreach (IObject @object in acquireObjectsResult.Objects)
                {
                    @object.Parent?.RemoveObject(@object);
                    game.Inventory.AddObject(@object);
                }
            }
        }
    }
}