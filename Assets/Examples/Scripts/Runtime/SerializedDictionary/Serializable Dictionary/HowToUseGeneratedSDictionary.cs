using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LazyJedi.Examples
{
    public class HowToUseGeneratedSDictionary : MonoBehaviour
    {
        #region VARIABLES

        [Header("Simple Dictionary")]
        public GeneratedSDictionary GeneratedDictionary = new GeneratedSDictionary();

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
            get => GeneratedDictionary.Keys;
        }

        public IEnumerable<string> Values
        {
            get => GeneratedDictionary.Values;
        }

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            DictionaryKeys = GeneratedDictionary.Keys.ToList();
            DictionaryValues = GeneratedDictionary.Values.ToList();

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
            GeneratedDictionary.Add(key, value);
        }

        public string Get(int key)
        {
            return GeneratedDictionary[key];
        }

        public void UpdateValue(int key, string value)
        {
            GeneratedDictionary[key] = value;
        }

        public bool HasUpdatedValue(int key, string value)
        {
            if (!GeneratedDictionary.ContainsKey(key)) return false;
            GeneratedDictionary[key] = value;
            return true;
        }

        public bool Remove(int key)
        {
            return GeneratedDictionary.Remove(key);
        }

        public void Clear()
        {
            GeneratedDictionary.Clear();
        }

        public bool ContainsKey(int key)
        {
            return GeneratedDictionary.ContainsKey(key);
        }

        public bool ContainsValue(string value)
        {
            return GeneratedDictionary.ContainsValue(value);
        }

        #endregion
    }
}