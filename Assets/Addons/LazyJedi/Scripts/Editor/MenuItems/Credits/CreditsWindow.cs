#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using LazyJedi.Editors.Internal;
using UnityEngine;
using UnityEditor;

namespace LazyJedi.Editors.MenuItems
{
    public class CreditsWindow : EditorWindow
    {
        #region WINDOW

        public static CreditsWindow Window;

        [MenuItem("Lazy-Jedi/Credits", priority = 999)]
        public static void OpenWindow()
        {
            Window = GetWindow<CreditsWindow>(true, "Credits");
            Window.Show();
        }

        #endregion

        #region CREDITS VARIABLES

        private string _lazyJedi = "https://github.com/Lazy-Jedi/lazy-jedi";

        private string _rotaryHeart = "https://assetstore.unity.com/publishers/28547";
        private string _mackySoft = "https://github.com/mackysoft/Unity-SerializeReferenceExtensions";
        private string _sevenZipExtractor = "https://github.com/adoconnection/SevenZipExtractor";

        private string _kenney = "https://www.kenney.nl/assets";

        private string _starwarsIconLink = "https://www.flaticon.com/free-icons/star-wars";

        #endregion

        #region UI VARIABLES

        private GUIStyle _centeredLabelStyle;
        private GUIStyle _headerLabelStyle;

        private Font _kennyMiniSquare;

        #endregion

        #region UNITY METHODS

        public void OnGUI()
        {
            Initialize();
            EditorGUILayout.Space(12f);
            EditorGUILayout.LabelField("Credits", _centeredLabelStyle);

            EditorGUILayout.Space(16f);
            using (EditorGUILayout.VerticalScope verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.Space(4f);
                EditorGUILayout.LabelField("Lazy Jedi", _headerLabelStyle);
                DrawLink("Lazy Jedi", _lazyJedi);

                EditorGUILayout.Space(16f);
                EditorGUILayout.LabelField("Assets", _headerLabelStyle);
                DrawLink("Kenney - Fonts", _kenney);

                EditorGUILayout.Space(16f);
                EditorGUILayout.LabelField("Packages", _headerLabelStyle);
                DrawLink("Rotary Heart - Serializable Dictionary Lite", _rotaryHeart);
                DrawLink("Mackysoft - Serializable Reference", _mackySoft);
                DrawLink("Adoconnection - Seven Zip Extractor", _sevenZipExtractor);

                EditorGUILayout.Space(16f);
                EditorGUILayout.LabelField("Icons", _headerLabelStyle);
                DrawLink("FlatIcon - Star Wars Icon", _starwarsIconLink);
            }
        }

        #endregion

        #region METHODS

        private void DrawLink(string label, string link)
        {
            using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button(label))
                {
                    Application.OpenURL(link);
                }
            }
        }

        #endregion

        #region HELPER METHODS

        private void Initialize()
        {
            if (!_kennyMiniSquare)
            {
                _kennyMiniSquare = Resources.Load<Font>("Artwork/kenney-fonts/KenneyMiniSquare");
            }

            _centeredLabelStyle ??= JediStyles.CustomCenteredLabel(Color.white, Color.black, 48, font: _kennyMiniSquare);
            _headerLabelStyle ??= JediStyles.CustomCenteredLabel(Color.white, Color.black, 16, font: _kennyMiniSquare);
        }

        #endregion
    }
#endif
}