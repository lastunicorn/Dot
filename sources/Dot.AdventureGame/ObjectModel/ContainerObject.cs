using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public class ObjectAddingEventArgs : EventArgs
    {
        public IObject Object { get; }

        public ObjectAddingEventArgs(IObject @object)
        {
            Object = @object;
        }
    }

    public class ObjectAddedEventArgs : EventArgs
    {
        public IObject Object { get; }

        public ObjectAddedEventArgs(IObject @object)
        {
            Object = @object;
        }
    }

    public abstract class ContainerObject : ObjectBase, IObjectContainer
    {
        protected readonly List<IObject> children;

        public event EventHandler<ObjectAddingEventArgs> ObjectAdding;
        public event EventHandler<ObjectAddedEventArgs> ObjectAdded;

        protected ContainerObject()
            : this(null)
        {
        }

        protected ContainerObject(IEnumerable<IObject> children)
        {
            this.children = new List<IObject>();

            if (children != null)
            {
                foreach (IObject child in children)
                {
                    child.Parent = this;
                    this.children.Add(child);
                }
            }
        }

        public void AddObject(IObject @object)
        {
            ObjectAddingEventArgs objectAddingEventArgs = new ObjectAddingEventArgs(@object);
            OnObjectAdding(objectAddingEventArgs);

            @object.Parent = this;
            children.Add(@object);

            ObjectAddedEventArgs objectAddedEventArgs = new ObjectAddedEventArgs(@object);
            OnObjectAdded(objectAddedEventArgs);
        }

        public void RemoveObject(IObject @object)
        {
            @object.Parent = null;
            children.Remove(@object);
        }

        public bool Contains(IObject @object)
        {
            return children.Contains(@object);
        }

        public bool Contains<T>()
        {
            return children.OfType<T>().Any();
        }

        public T GetChild<T>()
        {
            return children.OfType<T>().FirstOrDefault();
        }

        public void MakeAllChildrenVisible()
        {
            foreach (IObject @object in children)
                @object.IsVisible = true;
        }

        public IObject FindVisibleObject(string objectName)
        {
            if (string.Equals(objectName, Name, StringComparison.CurrentCultureIgnoreCase))
                return this;

            foreach (IObject childObject in children)
            {
                if (!childObject.IsVisible)
                    continue;

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

        public override StorageDataNode Export()
        {
            StorageDataNode storageDataNode = base.Export();

            IEnumerable<string> childrenTypes = children
                .Where(x => x != null)
                .Select(x => x.GetType().AssemblyQualifiedName);

            if (childrenTypes.Any())
            {
                storageDataNode.Add("children", string.Join(";", childrenTypes));

                foreach (IObject child in children)
                {
                    StorageDataNode childStorageDataNode = child.Export();
                    storageDataNode.Add("child." + child.Id, childStorageDataNode);
                }
            }

            return storageDataNode;
        }

        public override void Import(StorageDataNode storageDataNode)
        {
            base.Import(storageDataNode);

            children.Clear();

            if (storageDataNode.ContainsKey("children"))
            {
                string[] childrenInformation = ((string)storageDataNode["children"]).Split(';');

                foreach (string childTypeName in childrenInformation)
                {
                    if (childTypeName.Length == 0)
                        continue;

                    Type type = Type.GetType(childTypeName);
                    IObject childObject = (IObject)Activator.CreateInstance(type);
                    childObject.Parent = this;
                    children.Add(childObject);
                }
            }

            var saveNodes = storageDataNode
                .Where(x => x.Key.StartsWith("child."));

            foreach (KeyValuePair<string, object> pair in saveNodes)
            {
                string childId = pair.Key.Substring("child.".Length);
                IObject childObject = children.Single(x => x.Id == childId);
                childObject.Import((StorageDataNode)pair.Value);
            }
        }

        public IEnumerator<IObject> GetEnumerator()
        {
            return children.GetEnumerator();
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