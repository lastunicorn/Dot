using System.Collections;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame
{
    public class Inventory : ContainerObject
    {
        public override string Id { get; } = "inventory";

        public override string Name { get; } = "inventory";

        protected override void OnObjectAdding(ObjectAddingEventArgs e)
        {
            if (e.Object != null)
                e.Object.IsVisible = true;

            base.OnObjectAdding(e);
        }

        public override IEnumerable LookAt()
        {
            yield return new StoryBlock
            {
                AsciiPath = ImagePath,
                Title = "{{" + Name + "}}",
                AudioTexts = new AudioText
                {
                    Text = "This is the place where you find all the objects from your pocket."
                }
            };
        }
    }
}