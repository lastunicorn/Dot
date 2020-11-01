using System;
using System.Collections.Generic;

namespace DustInTheWind.Dot.Domain.SaveModel
{
    public class StorageNode : Dictionary<string, object>
    {
        public Type ObjectType { get; set; }

        public List<StorageNode> Children { get; } = new List<StorageNode>();

        public T Get<T>(string key, T defaultValue = default)
        {
            if (!ContainsKey(key))
                return defaultValue;

            object rawValue = this[key];

            return (T)Convert.ChangeType(rawValue, typeof(T));
        }

        public void Set<T>(string key, T value)
        {
            this[key] = value;
        }
    }
}