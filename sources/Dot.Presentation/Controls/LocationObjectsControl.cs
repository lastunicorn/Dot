using System;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.AudioSupport;

namespace DustInTheWind.Dot.Presentation.Controls
{
    public class LocationObjectsControl
    {
        private readonly Audio audio;

        public ILocation Location { get; set; }

        public LocationObjectsControl(Audio audio)
        {
            this.audio = audio ?? throw new ArgumentNullException(nameof(audio));
        }

        public void Display()
        {
            if (Location == null)
                return;

            string objectNames = Location.GetChildrenNames();

            if (string.IsNullOrEmpty(objectNames))
                DisplayInfo(Location.Name + ": <nothing>");
            else
                DisplayInfo(Location.Name + ": {{" + objectNames + "}}");
        }

        public void DisplayInfo(string text)
        {
            InfoBlock infoBlock = new InfoBlock(audio)
            {
                Text = text
            };
            infoBlock.Display();
        }
    }
}