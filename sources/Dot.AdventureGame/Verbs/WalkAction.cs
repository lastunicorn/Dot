using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class WalkAction : ActionBase
    {
        public override string Description => "Exits the location using the specified exit way, like a door or a road.";

        public override List<string> Usage =>
            new List<string>
            {
                "<<walk to>> {{<exit-way>}}",
                "<<walk on>> {{<exit-way>}}",
                "<<walk>> {{<exit-way>}}",
                "<<exit>> {{<exit-way>}}",
                "<<exit through>> {{<exit-way>}}"
            };

        public override ActionType ActionType => ActionType.Verb;

        public WalkAction()
            : base("walk", "walk to", "walk on", "exit", "exit through")
        {
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*walk(\s+to|\s+on)?\s+(?'object'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline),
                new Regex(@"^\s*exit(\sthrough)?\s+(?'object'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new[]
            {
                match.Groups["object"].Value
            };
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            WalkActionParameters actionParameters = new WalkActionParameters(parameters);
            IExitWay exitWay = actionParameters.ExitWay;

            return exitWay == null
                ? ProcessUnknownExitObject()
                : exitWay.Exit();
        }

        private static IEnumerable ProcessUnknownExitObject()
        {
            yield return new AudioText
            {
                Text = "I cannot go there.",
                AudioFileName = "walk-unknownexit.mp3"
            };
        }
    }
}