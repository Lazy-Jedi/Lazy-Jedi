#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.Project
{
    public class ProjectSetupWindow : EditorWindow
    {
        #region WINDOW

        public static ProjectSetupWindow Window;

        public static void OpenWindow()
        {
            Window = GetWindow<ProjectSetupWindow>("Project Setup");
            Window.Show();

            Window.Initialize();
        }

        #endregion

        #region VARIABLES

        private Texture2D _backgroundImage;

        private string _companyName;
        private string _productName;

        private Texture2D _productIcon;

        private Texture2D _cursor;
        private Vector2 _cursorHotspot;

        private List<string> _folders = new List<string>();

        #endregion

        #region UNITY METHODS

        #endregion

        #region GUI METHODS

        #endregion

        #region METHODS

        private void Initialize()
        {
        }

        #endregion

        #region HELPERS

        #endregion
    }
}

#endif