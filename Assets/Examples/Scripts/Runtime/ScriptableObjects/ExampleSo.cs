using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazyJedi
{
    [CreateAssetMenu(fileName = "ExampleSo", menuName = "ScriptableObjects/ExampleSo", order = 90)]
    public class ExampleSo : ScriptableObject
    {
        #region FIELDS

        public string name;
        public int coins;

        #endregion

        #region METHODS

        #endregion
    }
}