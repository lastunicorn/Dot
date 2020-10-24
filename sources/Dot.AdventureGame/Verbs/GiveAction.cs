using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class GiveAction : ActionBase
    {
        public override string Description => "Gives an object from your inventory to someone.";

        public override List<string> Usage => new List<string> { "<<give>> {{<object>}} <<to>> {{<person>}}" };

        public override ActionType ActionType => ActionType.Verb;

        public GiveAction()
            : base("give")
        {
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*give\s+(?'object'.+)\s+to\s+(?'person'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new[]
            {
                match.Groups["object"].Value,
                match.Groups["person"].Value
            };
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            GiveActionParameters actionParameters = new GiveActionParameters(parameters);

            if (actionParameters.TargetObject == null)
            {
                yield return new AudioText
                {
                    Text = "You cannot give an object that you don't have.",
                    AudioFileName = "give-nonexistentobject.mp3"
                };
            }
            else if (actionParameters.Receiver == null)
            {
                yield return new AudioText
                {
                    Text = "You cannot do that.",
                    AudioFileName = "give-cannotdothat.mp3"
                };
            }
            else if (!(actionParameters.TargetObject.Parent is Inventory))
            {
                yield return new AudioText
                {
                    Text = "You cannot give something that is not yours.",
                    AudioFileName = "give-notyours.mp3"
                };
            }
            else
            {
                IEnumerable results = actionParameters.Receiver.Receive(actionParameters.TargetObject);

                foreach (object result in results)
                    yield return result;
            }
        }
    }
}