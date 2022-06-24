using System.Collections.Generic;
using System.Linq;
using RotaryHeartAddon.Base;
using UnityEngine;

namespace LazyJedi.Examples
{
    public class GeneratedMonoBehaviourWithDictionary : MonoBehaviour
    {
        #region VARIABLES

        [Header("SimpleDictionary")]
        public SDictionary<int, string> SimpleDictionary = new SDictionary<int, string>();

        [Header("Keys and Values")]
        public List<int> DictionaryKeys = new List<int>();
        public List<string> DictionaryValues = new List<string>();

        [Header("Has Key")]
        public int HasKey = -1;

        [Header("Has Value")]
        public string HasValue = string.Empty;

        [Header("Update Existing Key Value")]
        public int ExistingKey = -1;
        public string UpdatedValue = string.Empty;

        [Header("Add New Item")]
        public int NewKey = -1;
        public string NewValue = string.Empty;

        [Header("Remove Item")]
        public int KeyToRemove = -1;

        [Header("Clear")]
        public bool ClearDictionary = false;

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

        #region UNITY METHODS

        private void Start()
        {
            DictionaryKeys = SimpleDictionary.Keys.ToList();
            DictionaryValues = SimpleDictionary.Values.ToList();

            // Check for a Key
            if (ContainsKey(HasKey))
            {
                print($"Dictionary has Key - {HasKey}");
            }

            // Check for a Value
            if (ContainsValue(HasValue))
            {
                print($"Dictionary has Value - {HasValue}");
            }

            // Get Value from Key
            print($"Get Value - {Get(HasKey)}");

            // Update a Keys Value
            if (HasUpdatedValue(ExistingKey, UpdatedValue))
            {
                print($"Updated Dictionary where Key: {ExistingKey}");
            }

            // Add new Item to Dictionary
            Add(NewKey, NewValue);

            // Remove an Item from a Dictionary
            Remove(KeyToRemove);

            // Clear the Dictionary
            if (ClearDictionary) Clear();
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