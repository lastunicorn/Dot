using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.GameModel;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class ChangeLocationResultHandler : ResultHandlerBase<ChangeLocationResult>
    {
        private readonly Game game;

        public ChangeLocationResultHandler(Game game)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public override void Handle(ChangeLocationResult changeLocationResult)
        {
            if (!game.IsLoaded)
                throw new Exception("Cannot change location. There is no game in progress.");

            game.ChangeLocation(changeLocationResult.DestinationId);
        }
    }
}