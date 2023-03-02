using UnityEngine;

namespace LazyJedi
{
    [CreateAssetMenu(fileName = "ExampleSo", menuName = "ScriptableObjects/ExampleSo", order = 90)]
    public class ExampleSo : ScriptableObject
    {
        #region FIELDS

        public int Coins;

        [SerializeField]
        private string _name;

        #endregion

        #region Properties

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        #endregion
    }
}