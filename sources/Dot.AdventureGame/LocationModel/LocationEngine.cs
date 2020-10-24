using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.Dot.AdventureGame.LocationModel
{
    public class LocationEngine
    {
        public List<ILocation> Locations { get; } = new List<ILocation>();
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

        public void MoveToFirst()
        {
            CurrentLocation = Locations.FirstOrDefault();
        }

        public void Reset()
        {
            CurrentLocation = null;
        }

        public void MoveTo(string locationId)
        {
            CurrentLocation = Locations.FirstOrDefault(x => x.Id == locationId);
        }

        public void MoveToNone()
        {
            CurrentLocation = null;
        }

        //public virtual StorageNode Save()
        //{
        //    StorageNode storageNode = new StorageNode();

        //    foreach (ILocation location in Locations)
        //    {
        //        StorageNode locationStorageNode = location.Save();
        //        storageNode.Add(location.Id, locationStorageNode);
        //    }


        //    IEnumerable<string> locationTypeNames = Locations
        //        .Where(x => x != null)
        //        .Select(x => x.GetType().FullName);

        //    storageNode.Add("locations", string.Join(";", locationTypeNames));

        //    foreach (ILocation location in Locations)
        //    {
        //        StorageNode locationStorageNode = location.Save();
        //        storageNode.Add("location." + location.Id, locationStorageNode);
        //    }

        //    return storageNode;
        //}

        //public virtual void Load(StorageNode storageNode)
        //{
        //    State = (GameState)storageNode["state"];
        //    TotalPlayTime = (TimeSpan)storageNode["total-play-time"];

        //    var saveNodes = storageNode
        //        .Where(x => x.Key.StartsWith("location."));

        //    foreach (KeyValuePair<string, object> pair in saveNodes)
        //    {
        //        string locationId = pair.Key.Substring("location.".Length);
        //        ILocation location = locationEngine.Locations.Single(x => x.Id == locationId);
        //        location.Load((StorageNode)pair.Value);
        //    }

        //    string currentLocationId = (string)storageNode["current-location"];
        //    locationEngine.MoveTo(currentLocationId);

        //    StorageNode inventoryStorageNode = (StorageNode)storageNode["inventory"];
        //    Inventory.Load(inventoryStorageNode);
        //}

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