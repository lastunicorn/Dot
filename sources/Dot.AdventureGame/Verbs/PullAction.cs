using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class PullAction : ActionBase
    {
        public override string Description => "Pulls the specified object.";

        public override List<string> Usage => new List<string> { "<<pull>> {{<object>}}" };

        public override ActionType ActionType => ActionType.Verb;

        public PullAction()
            : base("pull")
        {
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*pull\s+(?'object'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
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
            PullActionParameters actionParameters = new PullActionParameters(parameters);
            IPullable targetObject = actionParameters.TargetObject;

            return targetObject == null
                ? ProcessUnknownTargetObject()
                : targetObject.Pull();
        }

        private static IEnumerable ProcessUnknownTargetObject()
        {
            yield return new AudioText
            {
                Text = "It cannot be done.",
                AudioFileName = "pull-unknownobject.mp3"
            };
        }
    }
}