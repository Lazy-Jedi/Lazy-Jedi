using UnityEngine;

namespace LazyJedi.Examples
{
    public class UseExampleSo : MonoBehaviour
    {
        #region FIELDS

        public ExampleSo ExampleSo;

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            print(ExampleSo.Name);
            print(ExampleSo.Coins);
        }

        #endregion

        #region METHODS

        #endregion
    }
}