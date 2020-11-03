using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public static class ExitWayExtensions
    {
        public static StoryBlock CreateExitStory(this IExitWay exitWayObject, IAudioText audioTexts)
        {
            return new StoryBlock
            {
                Title = "Exit {{" + exitWayObject.Name + "}}",
                AudioTexts = audioTexts
            };
        }
    }
}