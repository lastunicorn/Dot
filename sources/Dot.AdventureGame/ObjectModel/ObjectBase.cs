using System;
using System.Collections;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public abstract class ObjectBase : IObject
    {
        private bool isVisible;
        private IObjectContainer parent;

        public abstract string Id { get; }

        public abstract string Name { get; }

        public virtual string ImagePath => null;

        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                OnIsVisibleChanged();
            }
        }

        public IObjectContainer Parent
        {
            get => parent;
            set
            {
                parent = value;
                OnParentChanged();
            }
        }

        public bool IsInInventory => Parent is Inventory;

        public event EventHandler IsVisibleChanged;
        public event EventHandler ParentChanged;

        public abstract IEnumerable LookAt();

        public ILocation FindParentLocation()
        {
            IObjectContainer foundParent = parent;

            while (foundParent != null)
            {
                switch (foundParent)
                {
                    case ILocation location:
                        return location;

                    case IObject objectParent:
                        foundParent = objectParent.Parent;
                        continue;
                }

                break;
            }

            return null;
        }

        protected StoryBlock CreateDescriptionStory(IAudioTextEnumerable audioText)
        {
            return new StoryBlock
            {
                AsciiPath = ImagePath,
                Title = "{{" + Name + "}}",
                AudioTexts = audioText
            };
        }

        public virtual StorageNode Export()
        {
            StorageNode storageNode = new StorageNode
            {
                { "is-visible", isVisible }
            };

            storageNode.ObjectType = GetType();

            return storageNode;
        }

        public virtual void Import(StorageNode storageNode)
        {
            isVisible = (bool)storageNode["is-visible"];
        }

        protected virtual void OnIsVisibleChanged()
        {
            IsVisibleChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnParentChanged()
        {
            ParentChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}