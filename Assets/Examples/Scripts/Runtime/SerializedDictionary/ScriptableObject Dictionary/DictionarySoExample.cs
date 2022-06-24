using System.Collections.Generic;
using RotaryHeartAddon.Base;
using UnityEngine;

namespace LazyJedi.Examples
{
    [CreateAssetMenu(fileName = "DictionarySoExample", menuName = "ScriptableObjects/DictionarySoExample", order = 81)]
    public class DictionarySoExample : ScriptableObject
    {
        #region VARIABLES

        [Header("SimpleDictionary")]
        public SDictionary<int, string> SimpleDictionary = new SDictionary<int, string>();

        #endregion
        
        #region PROPERTIES
        
        public IEnumerable<int> Keys
        {
            get => SimpleDictionary.Keys;
        }
    
        public IEnumerable<string> Values
        {
            get => SimpleDictionary.Values;
        }
        
        #endregion
        
        #region METHODS

        public void Add(int key, string value)
        {
            SimpleDictionary.Add(key, value);
        }

        public string Get(int key)
        {
            return SimpleDictionary[key];
        }

        public void UpdateValue(int key, string value)
        {
            SimpleDictionary[key] = value;
        }
        
        public bool HasUpdatedValue(int key, string value)
        {
            if (!SimpleDictionary.ContainsKey(key)) return false;
            SimpleDictionary[key] = value;
            return true;
        }

        public bool Remove(int key)
        {
            return SimpleDictionary.Remove(key);
        }
        
        public void Clear()
        {
            SimpleDictionary.Clear();
        }

        public bool ContainsKey(int key)
        {
            return SimpleDictionary.ContainsKey(key);
        }
        
        public bool ContainsValue(string value)
        {
            return SimpleDictionary.ContainsValue(value);
        }
        
        #endregion
    }
}