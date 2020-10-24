using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class CloseAction : ActionBase
    {
        public override string Description => "Closes the specified object. It may be applied to objects like a door or a wardrobe.";

        public override List<string> Usage => new List<string> { "<<close>> {{<object>}}" };

        public override ActionType ActionType => ActionType.Verb;

        public CloseAction()
            : base("close")
        {
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*close\s+(?'object'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
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
            CloseActionParameters actionParameters = new CloseActionParameters(parameters);

            if (actionParameters.TargetObject == null)
                return ProcessUnknownTargetObject();

            return actionParameters.TargetObject.Close();
        }

        private static IEnumerable ProcessUnknownTargetObject()
        {
            yield return new AudioText
            {
                Text = "You cannot close that. It makes no sense.",
                AudioFileName = "close-invalid.mp3"
            };
        }
    }
}