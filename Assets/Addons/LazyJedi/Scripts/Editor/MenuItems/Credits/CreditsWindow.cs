#if UNITY_EDITOR
using System;
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

        #region DRAWER FIELDS

        private GUIStyle _centeredLabelStyle;
        private GUIStyle _headerLabelStyle;

        private Font _kennyMiniSquare;

        #endregion

        #region CREDITS FIELDS

        private readonly string _lazyJedi = "https://github.com/Lazy-Jedi/lazy-jedi";
        private readonly string _blumalice = "https://github.com/BLUDRAG";
        private readonly string _rotaryHeart = "https://assetstore.unity.com/publishers/28547";
        private readonly string _mackySoft = "https://github.com/mackysoft/Unity-SerializeReferenceExtensions";
        private readonly string _kenney = "https://www.kenney.nl/assets";
        private readonly string _starwarsIcon = "https://www.flaticon.com/free-icons/star-wars";
        private readonly string _squidBox = "https://github.com/squid-box";
        private readonly string _sevenZip = "https://www.7-zip.org/";

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            Initialize();
        }

        public void OnGUI()
        {
            EditorGUILayout.Space(12f);
            EditorGUILayout.LabelField("Credits", _centeredLabelStyle);

            EditorGUILayout.Space(16f);
            using (EditorGUILayout.VerticalScope verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.Space(4f);
                EditorGUILayout.LabelField("Lazy Jedi", _headerLabelStyle);
                DrawLink("Lazy-Jedi", _lazyJedi);

                EditorGUILayout.Space(16f);
                EditorGUILayout.LabelField("Nakama", _headerLabelStyle);
                DrawLink("Kearan", _blumalice);

                EditorGUILayout.Space(16f);
                EditorGUILayout.LabelField("Assets", _headerLabelStyle);
                DrawLink("Kenney - Fonts", _kenney);

                EditorGUILayout.Space(16f);
                EditorGUILayout.LabelField("Packages", _headerLabelStyle);
                DrawLink("Rotary Heart - Serializable Dictionary Lite", _rotaryHeart);
                DrawLink("Mackysoft - Serializable Reference", _mackySoft);

                EditorGUILayout.Space(16f);
                EditorGUILayout.LabelField("Plugins", _headerLabelStyle);
                DrawLink("SquidBox (New Maintainer) - Seven Zip Sharp", _squidBox);
                DrawLink("7Zip (Igor Pavlov) - 7Zip", _sevenZip);

                EditorGUILayout.Space(16f);
                EditorGUILayout.LabelField("Icons", _headerLabelStyle);
                DrawLink("FlatIcon - Star Wars Icon", _starwarsIcon);
            }
        }

        private void OnDestroy()
        {
            Resources.UnloadAsset(_kennyMiniSquare);
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
                _kennyMiniSquare = Resources.Load<Font>(LazyEditorArt.KenneyMiniSquareFont);
            }

            _centeredLabelStyle ??= LazyEditorStyles.CustomCenteredLabel(Color.white, Color.black, 48, font: _kennyMiniSquare);
            _headerLabelStyle   ??= LazyEditorStyles.CustomCenteredLabel(Color.white, Color.black, 16, font: _kennyMiniSquare);
        }

        #endregion
    }
#endif
}