using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.Actions;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.AdventureGame.Verbs;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.AdventureGame.GameModel
{
    public class GameBase : IGameBase
    {
        private readonly GameTimer gameTimer = new GameTimer();

        private ILocation lastLocation;
        private GameState state = GameState.Closed;
        private bool isNew = true;
        private bool isFinished;

        protected LocationEngine LocationEngine { get; } = new LocationEngine();

        public Inventory Inventory { get; } = new Inventory();

        public ActionSet Actions { get; } = new ActionSet();

        protected List<IAddOn> AddOns { get; } = new List<IAddOn>();

        public GameState State
        {
            get => state;
            private set
            {
                if (state == value)
                    return;

                state = value;
                OnStateChanged();
            }
        }

        public TimeSpan TotalPlayTime => gameTimer.TotalPlayTime;

        public ILocation CurrentLocation => LocationEngine.CurrentLocation;

        public bool IsChanged { get; }

        public event EventHandler StateChanged;
        public event EventHandler CurrentLocationChanged;
        public event EventHandler<GameOpenEventArgs> Opened;
        public event EventHandler Closing;
        public event EventHandler Closed;

        public GameBase()
        {
            LocationEngine.CurrentLocationChanged += HandleCurrentLocationChanged;
        }

        private void HandleCurrentLocationChanged(object sender, EventArgs e)
        {
            if (LocationEngine.CurrentLocation != null)
                lastLocation = LocationEngine.CurrentLocation;

            OnCurrentLocationChanged();
        }

        public void AddAddOn(IAddOn addOn)
        {
            AddOns.Add(addOn);
            addOn.Game = this;
        }

        protected void AddLocation(ILocation location)
        {
            LocationEngine.Locations.Add(location);
        }

        public T GetLocation<T>()
            where T : ILocation
        {
            return LocationEngine.Locations
                .OfType<T>()
                .FirstOrDefault();
        }

        public void Open()
        {
            if (State == GameState.Paused || State == GameState.Closed)
            {
                if (!isFinished)
                    gameTimer.Start();

                AddOns.ForEach(x => x.Start());

                if (lastLocation == null)
                    LocationEngine.MoveToFirst();
                else
                    LocationEngine.MoveTo(lastLocation.Id);

                State = GameState.Open;

                GameOpenEventArgs eventArgs = new GameOpenEventArgs(isNew);
                OnOpened(eventArgs);

                isNew = false;
            }
        }

        public void ChangeLocation(string newLocationId)
        {
            LocationEngine.MoveTo(newLocationId);
        }

        public void Close()
        {
            if (State != GameState.Closed)
            {
                OnClosing();

                if (!isFinished)
                {
                    gameTimer.Stop();
                    State = GameState.Closed;
                    AddOns.ForEach(x => x.Stop());
                }

                OnClosed();
            }
        }

        public void Pause()
        {
            if (State == GameState.Open)
            {
                if (!isFinished)
                {
                    gameTimer.Stop();
                    AddOns.ForEach(x => x.Stop());
                }

                State = GameState.Paused;
            }
        }

        public void Finish()
        {
            if (!isFinished)
            {
                isFinished = true;
                gameTimer.Stop();
            }
        }

        public IObject FindVisibleObject(string objectName)
        {
            IObject @object = Inventory.FindVisibleObject(objectName);

            if (@object != null)
                return @object;

            ILocation currentLocation = LocationEngine.CurrentLocation;

            if (currentLocation == null)
                return null;

            if (currentLocation.HasName(objectName))
                return currentLocation;

            return currentLocation.FindVisibleObject(objectName);
        }

        public ActionInfo? FindMatchingAction(string command)
        {
            ActionInfo? actionInfo = Actions.FindMatchingAction(command);

            if (actionInfo != null)
            {
                ResolveParameters(actionInfo.Value);
                return actionInfo;
            }

            IObject foundObject = FindVisibleObject(command);

            if (foundObject != null)
            {
                return new ActionInfo
                {
                    Action = Actions.First(x => x is LookAtAction),
                    Parameters = new object[] { foundObject }
                };
            }

            List<ActionBase> similarActions = Actions
                .Where(x => x.Names.Any(z => command == z || command.TrimStart().StartsWith(z + " ")))
                .ToList();

            if (similarActions.Count == 0)
                return null;

            return new ActionInfo
            {
                Action = Actions.First(x => x is HelpAction),
                Parameters = similarActions.Select(x => (object)x.Name).ToArray()
            };
        }

        private void ResolveParameters(ActionInfo actionInfo)
        {
            object[] parameters = actionInfo.Parameters;

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] is string stringParameter)
                {
                    IObject @object = FindVisibleObject(stringParameter);

                    if (@object != null)
                        parameters[i] = @object;
                }
            }
        }

        public virtual StorageNode Save()
        {
            // Different Properties
            StorageNode storageNode = new StorageNode
            {
                { "state", State },
                { "is-new", isNew },
                { "is-finished", isFinished },
                { "total-play-time", gameTimer.TotalPlayTime },
                { "current-location", LocationEngine.CurrentLocation?.Id },
                { "inventory", Inventory.Export() }
            };

            // Locations
            IEnumerable<string> locationTypeNames = LocationEngine.Locations
                .Where(x => x != null)
                .Select(x => x.GetType().FullName);

            storageNode.Add("locations", string.Join(";", locationTypeNames));

            foreach (ILocation location in LocationEngine.Locations)
            {
                StorageNode locationStorageNode = location.Export();
                storageNode.Add("location." + location.Id, locationStorageNode);
            }

            // Add Ons
            foreach (IAddOn addOn in AddOns)
            {
                StorageNode addOnStorageNode = addOn.Save();
                storageNode.Add("addon." + addOn.Id, addOnStorageNode);
            }

            return storageNode;
        }

        public virtual void Load(StorageNode storageNode)
        {
            // Different Properties
            State = (GameState)storageNode["state"];
            isNew = (bool)storageNode["is-new"];
            isFinished = (bool)storageNode["is-finished"];
            gameTimer.TotalPlayTime = (TimeSpan)storageNode["total-play-time"];

            // Locations
            IEnumerable<KeyValuePair<string, object>> locationNodes = storageNode
                .Where(x => x.Key.StartsWith("location."));

            foreach (KeyValuePair<string, object> pair in locationNodes)
            {
                string locationId = pair.Key.Substring("location.".Length);
                ILocation location = LocationEngine.Locations.FirstOrDefault(x => x.Id == locationId);
                location?.Import((StorageNode)pair.Value);
            }

            // Add Ons
            IEnumerable<KeyValuePair<string, object>> addOnNodes = storageNode
                .Where(x => x.Key.StartsWith("addon."));

            foreach (KeyValuePair<string, object> pair in addOnNodes)
            {
                string addOnId = pair.Key.Substring("addon.".Length);
                IAddOn addOn = AddOns.Single(x => x.Id == addOnId);
                addOn.Load((StorageNode)pair.Value);
            }

            // Current Location
            string currentLocationId = (string)storageNode["current-location"];
            LocationEngine.MoveTo(currentLocationId);

            // Inventory
            StorageNode inventoryStorageNode = (StorageNode)storageNode["inventory"];
            Inventory.Import(inventoryStorageNode);
        }

        protected virtual void OnStateChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnCurrentLocationChanged()
        {
            CurrentLocationChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnOpened(GameOpenEventArgs e)
        {
            Opened?.Invoke(this, e);
        }

        protected virtual void OnClosing()
        {
            Closing?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnClosed()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }
    }
}