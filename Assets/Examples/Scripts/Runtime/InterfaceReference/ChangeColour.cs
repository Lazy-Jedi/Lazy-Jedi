using UnityEngine;

namespace LazyJedi.Examples
{
    public class ChangeColour : MonoBehaviour
    {
        #region VARIABLES

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = Random.ColorHSV();
        }

        #endregion
    }
}