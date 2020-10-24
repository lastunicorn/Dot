using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class PushAction : ActionBase
    {
        public override string Description => "Pushes the specified object.";

        public override List<string> Usage => new List<string> { "<<push>> {{<object>}}" };

        public override ActionType ActionType => ActionType.Verb;

        public PushAction()
            : base("push")
        {
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*push\s+(?'object'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
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
            PushActionParameters actionParameters = new PushActionParameters(parameters);
            IPushable targetObject = actionParameters.TargetObject;

            return targetObject == null
                ? ProcessUnknownTargetObject()
                : targetObject.Push();
        }

        private static IEnumerable ProcessUnknownTargetObject()
        {
            yield return new AudioText
            {
                Text = "It cannot be done.",
                AudioFileName = "push-unknownobject.mp3"
            };
        }
    }
}