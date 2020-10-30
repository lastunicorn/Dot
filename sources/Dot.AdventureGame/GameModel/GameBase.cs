using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.Actions;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.AdventureGame.Verbs;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.AdventureGame.GameModel
{
    public abstract class GameBase : IGameBase
    {
        private readonly GameTimer gameTimer = new GameTimer();

        private ILocation lastLocation;
        private GameState state = GameState.Closed;
        private bool isNew = true;
        private bool isFinished;

        private readonly LocationEngine locationEngine = new LocationEngine();
        private readonly AddOnCollection addOns = new AddOnCollection();

        public Inventory Inventory { get; } = new Inventory();

        public ActionSet Actions { get; } = new ActionSet();

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

        public ILocation CurrentLocation => locationEngine.CurrentLocation;

        public bool IsChanged { get; }

        public event EventHandler StateChanged;
        public event EventHandler CurrentLocationChanged;
        public event EventHandler<GameOpenEventArgs> Opened;
        public event EventHandler Closing;
        public event EventHandler Closed;

        protected GameBase()
        {
            locationEngine.CurrentLocationChanged += HandleCurrentLocationChanged;
        }

        public abstract void InitializeNew();

        private void HandleCurrentLocationChanged(object sender, EventArgs e)
        {
            if (locationEngine.CurrentLocation != null)
                lastLocation = locationEngine.CurrentLocation;

            OnCurrentLocationChanged();
        }

        protected void AddLocation(ILocation location)
        {
            locationEngine.Add(location);
        }

        protected void AddAddOn(IAddOn addOn)
        {
            addOns.Add(addOn);
            addOn.Game = this;
        }

        public T GetLocation<T>()
            where T : ILocation
        {
            return locationEngine
                .OfType<T>()
                .FirstOrDefault();
        }

        public void Open()
        {
            if (State == GameState.Paused || State == GameState.Closed)
            {
                if (!isFinished)
                    gameTimer.Start();

                foreach (IAddOn addOn in addOns)
                    addOn.Start();

                if (lastLocation == null)
                    locationEngine.MoveToFirst();
                else
                    locationEngine.MoveTo(lastLocation.Id);

                State = GameState.Open;

                GameOpenEventArgs eventArgs = new GameOpenEventArgs(isNew);
                OnOpened(eventArgs);

                isNew = false;
            }
        }

        public void ChangeLocation(string newLocationId)
        {
            locationEngine.MoveTo(newLocationId);
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

                    foreach (IAddOn addOn in addOns)
                        addOn.Stop();
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

                    foreach (IAddOn addOn in addOns)
                        addOn.Stop();
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

            ILocation currentLocation = locationEngine.CurrentLocation;

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

        public virtual StorageData Export()
        {
            StorageData storageNode = new StorageData
            {
                { "state", State },
                { "is-new", isNew },
                { "is-finished", isFinished },
                { "total-play-time", gameTimer.TotalPlayTime },
                { "inventory", Inventory.Export() },
                { "locations", locationEngine.Export() },
                { "addons", addOns.Export() }
            };

            storageNode.ObjectType = GetType();
            storageNode.SaveTime = DateTime.UtcNow;
            storageNode.Version = GetAssemblyVersion();

            return storageNode;
        }

        private static Version GetAssemblyVersion()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();

            return assemblyName.Version;
        }

        public virtual void Import(StorageData storageData)
        {
            Version currentVersion = GetAssemblyVersion();
            int comparisonResult = currentVersion.CompareTo(storageData.Version, 2);

            if (comparisonResult < 0)
                throw new Exception("Storage data was created by a newer version. It cannot be imported.");

            State = (GameState)storageData["state"];
            isNew = (bool)storageData["is-new"];
            isFinished = (bool)storageData["is-finished"];
            gameTimer.TotalPlayTime = (TimeSpan)storageData["total-play-time"];

            StorageNode locationsStorageNode = (StorageNode)storageData["locations"];
            locationEngine.Clear();
            locationEngine.Import(locationsStorageNode);

            StorageNode addOnsStorageNode = (StorageNode)storageData["addons"];
            addOns.Clear();
            addOns.Import(addOnsStorageNode);

            StorageNode inventoryStorageNode = (StorageNode)storageData["inventory"];
            Inventory.Clear();
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