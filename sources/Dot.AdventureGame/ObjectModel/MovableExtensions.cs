using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public static class MovableExtensions
    {
        public static StoryBlock CreateMoveStory(this IMovable movableObject, IAudioTextEnumerable audioTexts)
        {
            return new StoryBlock
            {
                Title = "Move {{" + movableObject.Name + "}}",
                AudioTexts = audioTexts
            };
        }
    }
}