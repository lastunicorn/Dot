using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public static class PullableExtensions
    {
        public static StoryBlock CreatePullStory(this IPullable pullableObject, IAudioText audioTexts)
        {
            return new StoryBlock
            {
                Title = "Pull {{" + pullableObject.Name + "}}",
                AudioTexts = audioTexts
            };
        }
    }
}