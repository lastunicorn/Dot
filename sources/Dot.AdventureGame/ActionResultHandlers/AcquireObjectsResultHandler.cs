using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class AcquireObjectsResultHandler : ResultHandlerBase<AcquireObjectsResult>
    {
        private readonly Game game;

        public AcquireObjectsResultHandler(Game game)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public override void Handle(AcquireObjectsResult acquireObjectsResult)
        {
            if (acquireObjectsResult.Objects == null)
                return;

            IEnumerable<IObject> objects = acquireObjectsResult.Objects
                .Where(x => x != null);

            if (!game.IsLoaded)
            {
                string objectsNames = string.Join(", ", objects.Select(x => x.Name));
                throw new Exception("The objects cannot he acquired. There is no game in progress. Objects: " + objectsNames);
            }

            foreach (IObject @object in objects)
            {
                @object.Parent?.RemoveObject(@object);
                game.Inventory.AddObject(@object);
            }
        }
    }
}