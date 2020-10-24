using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class OpenAction : ActionBase
    {
        public override string Description => "Opens the specified object. It may be applied to objects like a door or an wardrobe.";

        public override List<string> Usage => new List<string> { "<<open>> {{<object>}}" };

        public override ActionType ActionType => ActionType.Verb;

        public OpenAction()
            : base("open")
        {
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*open\s+(?'object'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
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
            OpenActionParameters actionParameters = new OpenActionParameters(parameters);
            IOpenable targetObject = actionParameters.TargetObject;

            return targetObject == null
                ? ProcessUnknownTargetObject()
                : targetObject.Open();
        }

        private static IEnumerable ProcessUnknownTargetObject()
        {
            yield return new AudioText
            {
                Text = "It cannot be opened. It makes no sense.",
                AudioFileName = "open-unknownobject.mp3"
            };
        }
    }
}