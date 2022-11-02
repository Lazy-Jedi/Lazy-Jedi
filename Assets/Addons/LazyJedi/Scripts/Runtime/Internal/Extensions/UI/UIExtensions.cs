using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LazyJedi.Extensions
{
    public static class UIExtensions
    {
        #region METHODS

        public static void AddListener(this Button button, UnityAction buttonAction)
        {
            if (buttonAction == null) return;
            button.onClick.AddListener(buttonAction);
        }

        public static void RemoveListener(this Button button, UnityAction buttonAction)
        {
            if (buttonAction == null) return;
            button.onClick.RemoveListener(buttonAction);
        }

        public static void RemoveAllListeners(this Button button)
        {
            button.onClick.RemoveAllListeners();
        }

        public static void SetPlaceHolderText(this Graphic graphic, string text)
        {
            if (graphic is TMP_Text placeholderText)
            {
                placeholderText.SetText(text);
            }
        }

        #endregion
    }
}