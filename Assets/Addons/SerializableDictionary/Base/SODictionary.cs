using UnityEngine;

namespace SerializableDictionary
{
    public abstract class SODictionary<Key, Value> : ScriptableObject
    {
        public abstract void Add(Key key, Value value);

        public abstract Value Get(Key key);

        public abstract void UpdateValue(Key key, Value value);

        public abstract bool Remove(Key key);

        public abstract void Clear();

        public abstract bool ContainsKey(Key key);
    }
}