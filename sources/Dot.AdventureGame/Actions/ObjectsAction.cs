using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.Actions
{
    public class ObjectsAction : ActionBase
    {
        private readonly Game game;

        public override string Description => "Displays a list with the objects from the current location with which you can interact.";

        public override List<string> Usage => new List<string> { "<<objects>>", "<<o>>" };

        public override ActionType ActionType => ActionType.GameCommand;

        public ObjectsAction(Game game)
            : base("objects", "o")
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*(objects|o)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new string[0];
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            ILocation location = game.CurrentLocation;

            if (location == null)
            {
                yield return new StoryBlock
                {
                    AudioTexts = new AudioText
                    {
                        Text = "There is nothing around. Not event the universe.",
                        AudioFileName = "objects-no-location.mp3"
                    }
                };

                yield break;
            }

            string objectNames = location.GetChildrenNames();

            if (string.IsNullOrEmpty(objectNames))
                yield return location.Name + ": <nothing>";
            else
                yield return location.Name + ": {{" + objectNames + "}}";
        }
    }
}