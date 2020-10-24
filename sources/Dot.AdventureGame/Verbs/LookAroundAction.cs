using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class LookAroundAction : ActionBase
    {
        private readonly GameBase game;

        public override string Description => "Describes the location where you are.";

        public override List<string> Usage => new List<string> { "<<look>>", "<<look around>>" };

        public override ActionType ActionType => ActionType.Verb;

        public LookAroundAction(GameBase game)
            : base("look around", "look")
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*look\s+around\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline),
                new Regex(@"^\s*look\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new string[0];
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            ILocation currentLocation = game.CurrentLocation;

            IEnumerable actionResults = currentLocation == null
                ? ProcessUnknownObject()
                : currentLocation.LookAt();

            foreach (object actionResult in actionResults)
                yield return actionResult;
        }

        private static IEnumerable ProcessUnknownObject()
        {
            yield return new AudioText
            {
                Text = "There is nothing around. Not even the universe.",
                AudioFileName = "look-nolocation.mp3"
            };
        }
    }
}