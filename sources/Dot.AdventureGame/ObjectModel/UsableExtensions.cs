using System.Collections;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public static class UsableExtensions
    {
        public static StoryBlock CreateUseStory(this IUsable usableObject, IAudioText audioTexts)
        {
            return new StoryBlock
            {
                Title = "Use {{" + usableObject.Name + "}}",
                AudioTexts = audioTexts
            };
        }

        public static IEnumerable UseWhenNotYours(this IUsable usableObject)
        {
            yield return CreateUseStory(usableObject, new AudioText
            {
                Text = "You must acquire the object first and then use it.",
                AudioFileName = "use-notyours.mp3"
            });
        }
    }
}