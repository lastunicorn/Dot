using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public struct ExitResult
    {
        public AudioText Description { get; set; }

        public bool Success { get; set; }

        public string DestinationLocationId { get; set; }
    }
}