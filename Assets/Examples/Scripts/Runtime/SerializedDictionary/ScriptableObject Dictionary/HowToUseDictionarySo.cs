using System.Collections.Generic;
using System.Linq;
using LazyJedi.Examples;
using UnityEngine;

namespace LazyJedi
{
    public class HowToUseDictionarySo : MonoBehaviour
    {
        #region VARIABLES

        [Header("ScriptableObject Dictionary")]
        public DictionarySoExample DictionarySo;

        [Header("Keys and Values")]
        public List<int> Keys = new List<int>();
        public List<string> Values = new List<string>();

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
        public bool Clear = false;

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            Keys = DictionarySo.Keys.ToList();
            Values = DictionarySo.Values.ToList();

            // Check for a Key
            if (DictionarySo.ContainsKey(HasKey))
            {
                print($"Dictionary has Key - {HasKey}");
            }

            // Check for a Value
            if (DictionarySo.ContainsValue(HasValue))
            {
                print($"Dictionary has Value - {HasValue}");
            }

            // Get Value from Key
            print($"Get Value - {DictionarySo.Get(HasKey)}");

            // Update a Keys Value
            if (DictionarySo.HasUpdatedValue(ExistingKey, UpdatedValue))
            {
                print($"Updated Dictionary where Key: {ExistingKey}");
            }

            // Add new Item to Dictionary
            DictionarySo.Add(NewKey, NewValue);

            // Remove an Item from a Dictionary
            DictionarySo.Remove(KeyToRemove);

            // Clear the Dictionary
            if (Clear) DictionarySo.Clear();
        }

        #endregion

        #region METHODS

        #endregion
    }
}