using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.Domain.DataAccess;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class ChangeLocationResultHandler : ResultHandlerBase<ChangeLocationResult>
    {
        private readonly GameRepository gameRepository;

        public ChangeLocationResultHandler(GameRepository gameRepository)
        {
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        public override void Handle(ChangeLocationResult changeLocationResult)
        {
            GameBase game = gameRepository.Get() as GameBase;
            game?.ChangeLocation(changeLocationResult.DestinationId);
        }
    }
}