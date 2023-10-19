using UnityEngine;

namespace LazyJedi.Components.UI
{
    public class OnMouseHoverFade : MonoBehaviour
    {
        #region FIELDS

        [Header("UI Fader")]
        [SerializeField]
        private UIFader UIFader;

        #endregion

        #region UNITY METHODS

        public void OnMouseEnter()
        {
            UIFader.FadeIn();
        }

        public void OnMouseExit()
        {
            UIFader.FadeOut();
        }

        #endregion
    }
}