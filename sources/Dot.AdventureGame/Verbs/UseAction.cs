using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class UseAction : ActionBase
    {
        public override string Description => "Uses an object from your inventory by itself or together with another one.";

        public override List<string> Usage => new List<string> { "<<use>> {{<object>}}", "<<use>> {{<object>}} <<with>> {{<object>}}" };

        public override ActionType ActionType => ActionType.Verb;

        public UseAction()
            : base("use")
        {
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*use\s+(?'object1'.+)\s+with\s+(?'object2'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline),
                new Regex(@"^\s*use\s+(?'object1'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            if (match.Groups["object2"].Success)
                return new[]
                {
                    match.Groups["object1"].Value,
                    match.Groups["object2"].Value
                };

            return new[]
            {
                match.Groups["object1"].Value
            };
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            if (parameters.Length == 0)
                return ProcessNoParametersRequest();

            if (parameters.Length == 1)
                return ProcessOneParameterRequest(parameters);

            if (parameters.Length >= 2)
                return ProcessTwoParametersRequest(parameters);

            return Enumerable.Empty<object>();
        }

        private IEnumerable ProcessNoParametersRequest()
        {
            yield return new NoObjectToUseAudioText();
        }

        private IEnumerable ProcessOneParameterRequest(IReadOnlyList<object> parameters)
        {
            switch (parameters[0])
            {
                case string _:
                    yield return new AudioText
                    {
                        Text = "That doesn't exist.",
                        AudioFileName = "use-nonexistent-1.mp3"
                    };
                    break;

                case IUsable usableObject:
                    IEnumerable results = usableObject.Use();

                    foreach (object result in results)
                        yield return result;
                    break;

                default:
                    yield return new UnusableObjectAudioText();
                    break;
            }
        }

        private IEnumerable ProcessTwoParametersRequest(IReadOnlyList<object> parameters)
        {
            if (parameters[0] is string)
            {
                yield return new AudioText
                {
                    Text = "You cannot use an object that does not exist.",
                    AudioFileName = "use-nonexistent-2.mp3"
                };
            }
            else if (parameters[1] is IReceiver receiverObject)
            {
                IObject @object = parameters[0] as IObject;

                IEnumerable results = receiverObject.Receive(@object);

                foreach (object result in results)
                    yield return result;
            }
            else
            {
                yield return new IncompatibleObjectAudioText();
            }
        }
    }
}