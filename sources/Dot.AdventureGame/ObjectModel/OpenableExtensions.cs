using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public static class OpenableExtensions
    {
        public static StoryBlock CreateOpenStory(this IOpenable openableObject, IAudioTextEnumerable audioTexts)
        {
            return new StoryBlock
            {
                Title = "Open {{" + openableObject.Name + "}}",
                AudioTexts = audioTexts
            };
        }

        public static StoryBlock CreateCloseStory(this IOpenable openableObject, IAudioTextEnumerable audioTexts)
        {
            return new StoryBlock
            {
                Title = "Close {{" + openableObject.Name + "}}",
                AudioTexts = audioTexts
            };
        }
    }
}