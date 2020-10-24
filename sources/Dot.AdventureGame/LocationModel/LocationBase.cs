using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.AdventureGame.LocationModel
{
    public abstract class LocationBase : ContainerObject, ILocation
    {
        protected HashSet<IAddOn> AddOns { get; } = new HashSet<IAddOn>();

        public virtual string[] AdditionalNames { get; } = new string[0];

        public abstract AudioText ResumeDescription { get; }

        public event EventHandler Entered;
        public event EventHandler Exiting;

        protected LocationBase()
        {
            IsVisible = true;
        }

        public void AddAddOn(IAddOn addOn)
        {
            AddOns.Add(addOn);
        }

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

            foreach (IAddOn addOn in AddOns)
            {
                StorageNode addOnStorageNode = addOn.Export();
                storageNode.Add("addon." + addOn.Id, addOnStorageNode);
            }

            return storageNode;
        }

        public override void Import(StorageNode storageNode)
        {
            base.Import(storageNode);

            IEnumerable<KeyValuePair<string, object>> storageNodes = storageNode
                .Where(x => x.Key.StartsWith("addon."));

            foreach (KeyValuePair<string, object> pair in storageNodes)
            {
                string addOnId = pair.Key.Substring("addon.".Length);
                IAddOn addOn = AddOns.Single(x => x.Id == addOnId);
                addOn.Load((StorageNode)pair.Value);
            }
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