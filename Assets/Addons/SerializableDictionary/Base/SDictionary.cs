using System;
using RotaryHeart.Lib.SerializableDictionary;

namespace SerializableDictionary
{
    [Serializable]
    public abstract class SDictionary<Key, Value> : SerializableDictionaryBase<Key, Value>
    {
    }
}