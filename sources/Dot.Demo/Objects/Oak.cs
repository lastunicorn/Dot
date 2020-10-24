using System.Collections;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Demo.Objects
{
    internal class Oak : ContainerObject
    {
        public override string Id { get; } = "oak";

        public override string Name { get; } = "oak";

        public override string ImagePath { get; } = "DustInTheWind.Dot.Demo.Ascii.oak.ascii";

        public Oak()
        {
            Acorn acorn = new Acorn();
            AddObject(acorn);
        }

        public override IEnumerable LookAt()
        {
            yield return CreateDescriptionStory(new AudioText
            {
                Text = "A tall oak. On the lowest branch there is a big {{acorn}}."
            });

            MakeAllChildrenVisible();
        }
    }
}