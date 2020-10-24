using System;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.Presentation.Controls
{
    public class LocationObjectsControl
    {
        private readonly IUserInterface userInterface;

        public ILocation Location { get; set; }

        public LocationObjectsControl(IUserInterface userInterface)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        public void Display()
        {
            if (Location == null)
                return;

            string objectNames = Location.GetChildrenNames();

            if (string.IsNullOrEmpty(objectNames))
                userInterface.DisplayInfo(Location.Name + ": <nothing>");
            else
                userInterface.DisplayInfo(Location.Name + ": {{" + objectNames + "}}");
        }
    }
}