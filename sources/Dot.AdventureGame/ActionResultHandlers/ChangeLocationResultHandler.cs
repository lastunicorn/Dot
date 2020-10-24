using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.GameModel;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class ChangeLocationResultHandler : ResultHandlerBase<ChangeLocationResult>
    {
        private readonly GameBase game;

        public ChangeLocationResultHandler(GameBase game)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public override void Handle(ChangeLocationResult changeLocationResult)
        {
            game.ChangeLocation(changeLocationResult.DestinationId);
        }
    }
}