using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.AdventureGame.LocationModel
{
    public class LocationEngine : IEnumerable<ILocation>, IExportable
    {
        private readonly HashSet<ILocation> locations  = new HashSet<ILocation>();

        private ILocation currentLocation;

        public ILocation CurrentLocation
        {
            get => currentLocation;
            private set
            {
                OnCurrentLocationChanging();

                currentLocation?.Exit();
                currentLocation = value;
                currentLocation?.Enter();

                OnCurrentLocationChanged();
            }
        }

        public event EventHandler CurrentLocationChanging;
        public event EventHandler CurrentLocationChanged;

        public void Add(ILocation location)
        {
            locations.Add(location);
        }

        public void Clear()
        {
            locations.Clear();
        }

        public void MoveToFirst()
        {
            CurrentLocation = locations.FirstOrDefault();
        }

        public void Reset()
        {
            CurrentLocation = null;
        }

        public void MoveTo(string locationId)
        {
            CurrentLocation = locations.FirstOrDefault(x => x.Id == locationId);
        }

        public void MoveToNone()
        {
            CurrentLocation = null;
        }

        public IEnumerator<ILocation> GetEnumerator()
        {
            return locations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ExportNode Export()
        {
            ExportNode exportNode = new ExportNode
            {
                { "current-location", CurrentLocation?.Id },
            };

            exportNode.ObjectType = GetType();

            IEnumerable<ExportNode> locationStorageNodes = locations
                .Select(x => x.Export());

            foreach (ExportNode locationStorageNode in locationStorageNodes) 
                exportNode.Children.Add(locationStorageNode);

            return exportNode;
        }

        public void Import(ExportNode exportNode)
        {
            foreach (ExportNode locationNode in exportNode.Children)
            {
                Type locationType = locationNode.ObjectType;
                
                ILocation location = Activator.CreateInstance(locationType) as ILocation;
                location?.Import(locationNode);

                locations.Add(location);
            }

            string currentLocationId = (string)exportNode["current-location"];
            CurrentLocation = locations.FirstOrDefault(x => x.Id == currentLocationId);
        }

        protected virtual void OnCurrentLocationChanging()
        {
            CurrentLocationChanging?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnCurrentLocationChanged()
        {
            CurrentLocationChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}