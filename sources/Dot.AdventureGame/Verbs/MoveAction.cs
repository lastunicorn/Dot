using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    public class MoveAction : ActionBase
    {
        public override string Description => "Moves the specified object from its place.";

        public override List<string> Usage => new List<string> { "<<move>> {{<object>}}" };

        public override ActionType ActionType => ActionType.Verb;

        public MoveAction()
            : base("move")
        {
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*move\s+(?'object'.+)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
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
            MoveActionParameters actionParameters = new MoveActionParameters(parameters);
            IMovable targetObject = actionParameters.TargetObject;

            if (!actionParameters.TargetObjectIsSpecified)
                return ProcessUnspecifiedTargetObject();

            if (!actionParameters.TargetObjectIsRecognized)
                return ProcessUnknownTargetObject();

            if (!actionParameters.TargetObjectIsMovable)
                return ProcessUnmovableTargetObject();

            return targetObject.Move();
        }

        private static IEnumerable ProcessUnspecifiedTargetObject()
        {
            yield return new AudioText
            {
                Text = "Move... what?.",
                AudioFileName = "move-unspecifiedobject.mp3"
            };
        }

        private static IEnumerable ProcessUnknownTargetObject()
        {
            yield return new AudioText
            {
                Text = "I don't know what is that object you want to move.",
                AudioFileName = "move-unknownobject.mp3"
            };
        }

        private static IEnumerable ProcessUnmovableTargetObject()
        {
            yield return new AudioText
            {
                Text = "Unfortunately, that cannot be moved.",
                AudioFileName = "move-unmovableobject.mp3"
            };
        }
    }
}