using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public static class LockableExtensions
    {
        public static StoryBlock CreateUnlockStory(this ILockable lockableObject, IAudioTextEnumerable audioTexts)
        {
            return new StoryBlock
            {
                Title = "Unlock {{" + lockableObject.Name + "}}",
                AudioTexts = audioTexts
            };
        }
    }
}