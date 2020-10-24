using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public abstract class ContainerObject : ObjectBase, IObjectContainer
    {
        protected HashSet<IObject> Children { get; } = new HashSet<IObject>();

        public event EventHandler<ObjectAddingEventArgs> ObjectAdding;
        public event EventHandler<ObjectAddedEventArgs> ObjectAdded;

        protected ContainerObject()
            : this(null)
        {
        }

        protected ContainerObject(IEnumerable<IObject> children)
        {
            if (children != null)
            {
                foreach (IObject child in children)
                {
                    if (child == null)
                        continue;

                    if (Children.Contains(child))
                        continue;

                    child.Parent = this;
                    Children.Add(child);
                }
            }
        }

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

            foreach (IObject childObject in Children)
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

        public override StorageNode Export()
        {
            StorageNode storageNode = base.Export();

            IEnumerable<string> childrenTypes = Children
                .Where(x => x != null)
                .Select(x => x.GetType().AssemblyQualifiedName);

            if (childrenTypes.Any())
            {
                storageNode.Add("children", string.Join(";", childrenTypes));

                foreach (IObject child in Children)
                {
                    StorageNode childStorageNode = child.Export();
                    storageNode.Add("child." + child.Id, childStorageNode);
                }
            }

            return storageNode;
        }

        public override void Import(StorageNode storageNode)
        {
            base.Import(storageNode);

            Children.Clear();

            if (storageNode.ContainsKey("children"))
            {
                string[] childrenInformation = ((string)storageNode["children"]).Split(';');

                foreach (string childTypeName in childrenInformation)
                {
                    if (childTypeName.Length == 0)
                        continue;

                    Type type = Type.GetType(childTypeName);
                    IObject childObject = (IObject)Activator.CreateInstance(type);
                    childObject.Parent = this;
                    Children.Add(childObject);
                }
            }

            IEnumerable<KeyValuePair<string, object>> storageNodes = storageNode
                .Where(x => x.Key.StartsWith("child."));

            foreach (KeyValuePair<string, object> pair in storageNodes)
            {
                string childId = pair.Key.Substring("child.".Length);
                IObject childObject = Children.Single(x => x.Id == childId);
                childObject.Import((StorageNode)pair.Value);
            }
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