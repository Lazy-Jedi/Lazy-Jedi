#if UNITY_EDITOR
using LazyJedi.Globals;
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.Internal
{
    public static class LazyEditorStyles
    {
        #region FIELDS

        /// <summary>
        /// ✔ - Tick Character
        /// </summary>
        public static readonly string TICK_CHAR = "✓";

        /// <summary>
        /// ❌ - Cross Character
        /// </summary>
        public static readonly string CROSS_CHAR = "✖";

        /// <summary>
        /// ➕ - Plus Character
        /// </summary>
        public static readonly string PLUS_CHAR = "＋";

        /// <summary>
        /// ➖ - Minus Character
        /// </summary>
        public static readonly string MINUS_CHAR = "−";

        #endregion

        #region PROPERTIES

        private static Color StandardColor
        {
            get => EditorGUIUtility.isProSkin ? LazyColors.UnityFontColorLite : LazyColors.UnityFontColorDark;
        }

        #endregion

        #region LABEL METHODS

        public static GUIStyle SizeableCenteredLabel(int fontSize = 12)
        {
            return NewCustomCenteredLabel(LazyColors.UnityFontColorLite, LazyColors.UnityFontColorDark, fontSize);
        }

        public static GUIStyle CustomCenteredLabel(Color light, Color dark, int fontSize = 12, Font font = null)
        {
            return NewCustomCenteredLabel(light, dark, fontSize, font);
        }

        public static GUIStyle CustomHelpBoxLabel(Color light, Color dark, int fontSize = 12, Font font = null)
        {
            return NewHelpBoxCenteredLabel(light, dark, fontSize, font);
        }

        private static GUIStyle NewCustomCenteredLabel(Color light, Color dark, int fontSize = 12, Font font = null)
        {
            GUIStyle customLabel = new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                font      = font,
                fontSize  = fontSize
            };

            customLabel.normal.textColor = SwitchColor(light, dark);
            return customLabel;
        }

        private static GUIStyle NewHelpBoxCenteredLabel(Color light, Color dark, int fontSize = 12, Font font = null)
        {
            GUIStyle customLabel = new GUIStyle(EditorStyles.helpBox);
            customLabel.alignment        = TextAnchor.MiddleCenter;
            customLabel.font             = font;
            customLabel.fontSize         = fontSize;
            customLabel.normal.textColor = SwitchColor(light, dark);
            return customLabel;
        }

        #endregion

        #region HELPER METHODS

        /// <summary>
        /// Switch between a Light and Dark Colour depending if the Editor is in Dark or Light Mode
        /// </summary>
        /// <param name="light"></param>
        /// <param name="dark"></param>
        /// <returns></returns>
        public static Color SwitchColor(Color light, Color dark)
        {
            return EditorGUIUtility.isProSkin ? light : dark;
        }

        public static void SwitchLabelColor(GUIStyle style, Color light, Color dark)
        {
            if (style == null) return;
            style.normal.textColor = SwitchColor(light, dark);
        }

        #endregion
    }
}
#endif