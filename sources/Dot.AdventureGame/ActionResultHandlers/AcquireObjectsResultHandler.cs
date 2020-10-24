using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.DataAccess;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class AcquireObjectsResultHandler : ResultHandlerBase<AcquireObjectsResult>
    {
        private readonly GameRepository gameRepository;

        public AcquireObjectsResultHandler(GameRepository gameRepository)
        {
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        public override void Handle(AcquireObjectsResult acquireObjectsResult)
        {
            if (acquireObjectsResult.Objects != null)
            {
                foreach (IObject @object in acquireObjectsResult.Objects)
                {
                    @object.Parent?.RemoveObject(@object);
                    GameBase game = gameRepository.Get() as GameBase;
                    game?.Inventory.AddObject(@object);
                }
            }
        }
    }
}