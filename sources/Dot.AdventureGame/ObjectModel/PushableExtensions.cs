using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public static class PushableExtensions
    {
        public static StoryBlock CreatePushStory(this IPushable pushableObject, IAudioTextEnumerable audioTexts)
        {
            return new StoryBlock
            {
                Title = "Push {{" + pushableObject.Name + "}}",
                AudioTexts = audioTexts
            };
        }
    }
}