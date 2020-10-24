using System.Collections;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Demo.Objects
{
    internal class Acorn : ObjectBase, ITakeable
    {
        public override string Id => "acorn";

        public override string Name => "acorn";

        public override string ImagePath { get; } = "DustInTheWind.Dot.Demo.Ascii.acorn.ascii";

        public override IEnumerable LookAt()
        {
            yield return CreateDescriptionStory(new AudioText
            {
                Text = "An acorn."
            });
        }

        public IEnumerable Take()
        {
            yield return CreateDescriptionStory(new AudioText
            {
                Text = "You take the acorn and put it in your pocket."
            });

            yield return new AcquireObjectsResult
            {
                Objects = new[] { this }
            };
        }
    }
}