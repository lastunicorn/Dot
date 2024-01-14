using System;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;
using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace DustInTheWind.Dot.AdventureGame.LocationModel
{
    public abstract class LocationBase : ContainerObject, ILocation
    {
        protected AddOnCollection AddOns { get; } = new AddOnCollection();

        public virtual string[] AdditionalNames { get; } = new string[0];

        public abstract AudioText ResumeDescription { get; }

        public event EventHandler Entered;
        public event EventHandler Exiting;

        protected LocationBase()
        {
            IsVisible = true;
        }

        public abstract void InitializeNew();
        
        public bool HasName(string name)
        {
            return string.Equals(Name, name, StringComparison.CurrentCultureIgnoreCase) ||
                   AdditionalNames?.Any(x => string.Equals(x, name, StringComparison.CurrentCultureIgnoreCase)) == true;
        }

        public void Enter()
        {
            foreach (IAddOn addOn in AddOns)
                addOn.Start();

            OnEntered();
        }

        public void Exit()
        {
            OnExiting();

            foreach (IAddOn addOn in AddOns)
                addOn.Stop();
        }

        public override StorageNode Export()
        {
            StorageNode storageNode = base.Export();

            storageNode.Add("addons", AddOns.Export());

            return storageNode;
        }

        public override void Import(StorageNode storageNode)
        {
            base.Import(storageNode);

            StorageNode addOnsStorageNode = (StorageNode)storageNode["addons"];
            AddOns.Clear();
            AddOns.Import(addOnsStorageNode);
        }

        protected virtual void OnEntered()
        {
            Entered?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnExiting()
        {
            Exiting?.Invoke(this, EventArgs.Empty);
        }
    }
}