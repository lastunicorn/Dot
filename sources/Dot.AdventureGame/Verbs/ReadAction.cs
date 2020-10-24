using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class ReadAction : ActionBase
    {
        public override string Description => "Read a text.";

        public override List<string> Usage => new List<string> { "<<read>> {{<object>}}" };

        public override ActionType ActionType => ActionType.Verb;

        public ReadAction()
            : base("read")
        {
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*read\s+(?'object'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
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
            ReadActionParameters actionParameters = new ReadActionParameters(parameters);

            return actionParameters.Object == null
                ? ProcessUnknownObject()
                : actionParameters.Object.Read();
        }

        private static IEnumerable ProcessUnknownObject()
        {
            yield return new AudioText
            {
                Text = "There is nothing to read.",
                AudioFileName = "read-unknownobject.mp3"
            };
        }
    }
}