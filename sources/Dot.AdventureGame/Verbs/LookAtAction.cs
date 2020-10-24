using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class LookAtAction : ActionBase
    {
        public override string Description => "Describes the specified object.";

        public override List<string> Usage => new List<string> { "<<look at>> {{<object>}}", "<<look>> {{<object>}}" };

        public override ActionType ActionType => ActionType.Verb;

        public LookAtAction()
            : base("look at", "look")
        {
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*look(\s+at)?\s+(?'object'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new[]
            {
                match.Groups["object"].Value.TrimEnd()
            };
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            LookAtActionParameters actionParameters = new LookAtActionParameters(parameters);

            return actionParameters.Object == null
                ? ProcessUnknownObject()
                : actionParameters.Object.LookAt();
        }

        private static IEnumerable ProcessUnknownObject()
        {
            yield return new AudioText
            {
                Text = "I don't understand. What should I look for?",
                AudioFileName = "look-unknownobject.mp3"
            };
        }
    }
}