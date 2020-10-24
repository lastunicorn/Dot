using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Domain
{
    public class StoryBlock
    {
        public string AsciiPath { get; set; }

        public string Title { get; set; }

        public IAudioTextEnumerable AudioTexts { get; set; }
    }
}