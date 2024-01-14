using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public abstract class ContainerObject : ObjectBase, IObjectContainer
    {
        protected HashSet<IObject> Children { get; } = new HashSet<IObject>();

        public event EventHandler<ObjectAddingEventArgs> ObjectAdding;
        public event EventHandler<ObjectAddedEventArgs> ObjectAdded;

        public void AddObject(IObject @object)
        {
            ObjectAddingEventArgs objectAddingEventArgs = new ObjectAddingEventArgs(@object);
            OnObjectAdding(objectAddingEventArgs);

            @object.Parent = this;
            Children.Add(@object);

            ObjectAddedEventArgs objectAddedEventArgs = new ObjectAddedEventArgs(@object);
            OnObjectAdded(objectAddedEventArgs);
        }

        public void RemoveObject(IObject @object)
        {
            @object.Parent = null;
            Children.Remove(@object);
        }

        public bool Contains(IObject @object)
        {
            return Children.Contains(@object);
        }

        public bool Contains<T>()
        {
            return Children.OfType<T>().Any();
        }

        public T GetChild<T>()
        {
            return Children.OfType<T>().FirstOrDefault();
        }

        public void MakeAllChildrenVisible()
        {
            foreach (IObject @object in Children)
                @object.IsVisible = true;
        }

        public IObject FindVisibleObject(string objectName)
        {
            if (string.Equals(objectName, Name, StringComparison.CurrentCultureIgnoreCase))
                return this;

            IEnumerable<IObject> visibleChildren = Children
                .Where(x => x.IsVisible);

            foreach (IObject childObject in visibleChildren)
            {
                if (childObject.Name.Equals(objectName, StringComparison.InvariantCultureIgnoreCase))
                    return childObject;

                IObjectContainer objectContainer = childObject as IObjectContainer;

                IObject foundObject = objectContainer?.FindVisibleObject(objectName);

                if (foundObject != null)
                    return foundObject;
            }

            return null;
        }

        public T Find<T>()
        {
            return Find<T>(this);
        }

        public static T Find<T>(IObjectContainer container)
        {
            foreach (IObject child in container)
            {
                if (child is IObjectContainer childContainer)
                {
                    T o = Find<T>(childContainer);

                    if (o != null && !o.Equals(default))
                        return o;
                }

                if (child is T childOfTypeT)
                    return childOfTypeT;
            }

            return default;
        }

        public override StorageNode Export()
        {
            StorageNode storageNode = base.Export();

            IEnumerable<StorageNode> storageNodes = Children
                .Select(x => x.Export());

            foreach (StorageNode childStorageNode in storageNodes)
                storageNode.Children.Add(childStorageNode);

            return storageNode;
        }

        public override void Import(StorageNode storageNode)
        {
            base.Import(storageNode);

            foreach (StorageNode childNode in storageNode.Children)
            {
                Type childType = childNode.ObjectType;

                IObject child = Activator.CreateInstance(childType) as IObject;

                if (child != null)
                {
                    if (child is ContainerObject containerObjectChild)
                        containerObjectChild.Clear();

                    child.Import(childNode);
                    child.Parent = this;
                    Children.Add(child);
                }
            }
        }

        public void Clear()
        {
            Children.Clear();
        }

        public IEnumerator<IObject> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void OnObjectAdding(ObjectAddingEventArgs e)
        {
            ObjectAdding?.Invoke(this, e);
        }

        protected virtual void OnObjectAdded(ObjectAddedEventArgs e)
        {
            ObjectAdded?.Invoke(this, e);
        }
    }
}