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
        protected List<IAddOn> addOns;

        public virtual string[] AdditionalNames { get; } = new string[0];

        public abstract AudioText ResumeDescription { get; }

        public event EventHandler Entered;
        public event EventHandler Exiting;

        protected LocationBase()
        {
            addOns = new List<IAddOn>();

            IsVisible = true;
        }

        public void AddAddOn(IAddOn addOn)
        {
            addOns.Add(addOn);
        }

        public bool HasName(string name)
        {
            return string.Equals(Name, name, StringComparison.CurrentCultureIgnoreCase) ||
                   AdditionalNames?.Any(x => string.Equals(x, name, StringComparison.CurrentCultureIgnoreCase)) == true;
        }

        public void Enter()
        {
            addOns.ForEach(x => x.Start());

            OnEntered();
        }

        public void Exit()
        {
            OnExiting();

            addOns.ForEach(x => x.Stop());
        }

        public override StorageDataNode Export()
        {
            StorageDataNode storageDataNode = base.Export();

            foreach (IAddOn addOn in addOns)
            {
                StorageDataNode addOnStorageDataNode = addOn.Export();
                storageDataNode.Add("addon." + addOn.Id, addOnStorageDataNode);
            }

            return storageDataNode;
        }

        public override void Import(StorageDataNode storageDataNode)
        {
            base.Import(storageDataNode);

            var saveNodes = storageDataNode
                .Where(x => x.Key.StartsWith("addon."));

            foreach (KeyValuePair<string, object> pair in saveNodes)
            {
                string addOnId = pair.Key.Substring("addon.".Length);
                IAddOn addOn = addOns.Single(x => x.Id == addOnId);
                addOn.Load((StorageDataNode)pair.Value);
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