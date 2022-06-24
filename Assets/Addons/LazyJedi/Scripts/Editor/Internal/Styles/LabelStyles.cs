#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace LazyJedi.Editors.Internal
{
    public static partial class JediStyles
    {
        #region LABEL VARIABLES

        private static GUIStyle _standardCenteredLabelStyle;

        #endregion

        #region PROPERTIES

        private static Color StandardColor
        {
            get => EditorGUIUtility.isProSkin ? Color.white : Color.black;
        }

        #endregion

        #region LABEL PROPERTIES

        public static GUIStyle StandardCenteredLabel
        {
            get
            {
                if (_standardCenteredLabelStyle == null)
                {
                    _standardCenteredLabelStyle = new GUIStyle()
                    {
                        alignment = TextAnchor.MiddleCenter
                    };
                    _standardCenteredLabelStyle.normal.textColor = StandardColor;
                }

                return _standardCenteredLabelStyle;
            }
        }

        #endregion

        #region LABEL METHODS

        public static GUIStyle SizeableCenteredLabel(int fontSize = 12)
        {
            return NewCustomCenteredLabel(Color.white, Color.black, fontSize);
        }

        public static GUIStyle CustomCenteredLabel(Color light, Color dark, int fontSize = 12, bool wrap = false, Font font = null)
        {
            return NewCustomCenteredLabel(Color.white, Color.black, fontSize, font);
        }

        private static GUIStyle NewCustomCenteredLabel(Color light, Color dark, int fontSize = 12, Font font = null)
        {
            GUIStyle customLabel = new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                font = font,
                fontSize = fontSize
            };

            customLabel.normal.textColor = SwitchColor(light, dark);
            return customLabel;
        }

        #endregion

        #region HELPER METHODS

        private static Color SwitchColor(Color light, Color dark)
        {
            return EditorGUIUtility.isProSkin ? light : dark;
        }

        #endregion
    }
}
#endif
#endif