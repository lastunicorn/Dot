using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public static class TakeableExtensions
    {
        public static StoryBlock CreateTakeStory(this ITakeable takeableObject, IAudioTextEnumerable audioTexts)
        {
            return new StoryBlock
            {
                Title = "Take {{" + takeableObject.Name + "}}",
                AudioTexts = audioTexts
            };
        }
    }
}