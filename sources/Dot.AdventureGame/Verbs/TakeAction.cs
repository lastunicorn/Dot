using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class TakeAction : ActionBase
    {
        public override string Description => "Takes an object from the current location and puts it in your pocket.";

        public override List<string> Usage => new List<string> { "<<take>> {{<object>}}", "<<pick up>> {{<object>}}" };

        public override ActionType ActionType => ActionType.Verb;

        public TakeAction()
            : base("take", "pick up")
        {
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*(take|pick up)(\s+the)*\s+(?'object'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
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
            if (parameters.Length == 0)
                return TakeNoObject();

            if (parameters[0] is IObject targetObject)
            {
                if (targetObject.Parent is Inventory)
                    return TakeAlreadyInInventory();

                if (parameters[0] is ITakeable targetTakeableObject)
                    return targetTakeableObject.Take();

                return TakeCannotBeTaken();
            }

            return TakeNoObject();
        }

        private static IEnumerable TakeNoObject()
        {
            yield return new AudioText
            {
                Text = "There is nothing that can be taken.",
                AudioFileName = "take-noobject.mp3"
            };
        }

        private static IEnumerable TakeAlreadyInInventory()
        {
            yield return new StoryBlock
            {
                AudioTexts = new AudioText
                {
                    Text = "It is already in your pocket.",
                    AudioFileName = "take-alreadyininventory.mp3"
                }
            };
        }

        private static IEnumerable TakeCannotBeTaken()
        {
            yield return new AudioText
            {
                Text = "It cannot be taken.",
                AudioFileName = "take-cannotbetaken.mp3"
            };
        }
    }
}