#if UNITY_EDITOR
using System.Collections.Generic;
using LazyJedi.Editors.Internal;
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.MenuItems
{
    public class ProjectSetupWindow : EditorWindow
    {
        #region WINDOW

        public static ProjectSetupWindow Window;

        [MenuItem("Lazy-Jedi/Setup/Project Setup #&P", priority = 100)]
        public static void OpenWindow()
        {
            Window = GetWindow<ProjectSetupWindow>(true, "Project Setup");
            Window.minSize = new Vector2(645, 830);
            Window.Show();

            Window.LoadProjectSettings();
            Window.LoadData();
        }

        #endregion

        #region EDITOR VARIABLES

        private Texture2D _lightImage;
        private Texture2D _darkImage;
        private Rect _logoRect = Rect.zero;

        private bool _showFolders = true;
        private bool _autoSave = true;
        private bool _changeOccured = false;

        private GUIStyle _centeredLabel;

        private Vector2 _scrollPosition = Vector2.zero;

        private GUIContent _resoucesGUIContent = new GUIContent("Resources Folder:", "Select your Local Resources Folder.");

        #endregion

        #region VARIABLES

        private ProjectSetup _projectSetup;

        private string _companyName;
        private string _productName;

        private string _resourcesFolder;

        private Texture2D _productIcon;

        private Texture2D _cursor;
        private Vector2 _cursorHotspot;

        private List<string> _folders = new List<string>();
        private int _count;

        #endregion

        #region UNITY METHODS

        public void OnGUI()
        {
            Initialization();
            BackgroundLogoDrawer();

            EditorGUILayout.Space(_logoRect.height);
            using (EditorGUILayout.VerticalScope verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUI.BeginChangeCheck();
                AutoSaveDrawer();
                ProductIconDrawer();
                ProductCursor();
                ProductInfoDrawer();
                ResourcesFolderDrawer();
                FolderDrawer();
                if (EditorGUI.EndChangeCheck())
                {
                    UpdateProjectSettings();
                }
            }

            SaveSettingsButtonDrawer();
        }

        #endregion

        #region GUI METHODS

        private void BackgroundLogoDrawer()
        {
            GUI.DrawTexture(_logoRect, EditorGUIUtility.isProSkin ? _lightImage : _darkImage, ScaleMode.ScaleToFit);
        }

        private void AutoSaveDrawer()
        {
            _autoSave = EditorGUILayout.ToggleLeft("Auto Save?", _autoSave);
        }

        private void ProductIconDrawer()
        {
            EditorGUILayout.Space(8f);
            _productIcon = (Texture2D)EditorGUILayout.ObjectField("Product Icon", _productIcon, typeof(Texture2D), false);
        }

        private void ProductCursor()
        {
            _cursor = (Texture2D)EditorGUILayout.ObjectField("Cursor", _cursor, typeof(Texture2D), false);
            if (_cursor)
            {
                _cursorHotspot = EditorGUILayout.Vector2Field("Cursor Hotspot:", _cursorHotspot);
            }
        }

        private void ProductInfoDrawer()
        {
            _companyName = EditorGUILayout.TextField("Company Name:", _companyName);
            _productName = EditorGUILayout.TextField("Product Name:", _productName);
        }

        private void ResourcesFolderDrawer()
        {
            using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope())
            {
                EditorGUI.BeginChangeCheck();
                _resourcesFolder = EditorGUILayout.TextField(_resoucesGUIContent, _resourcesFolder);

                if (GUILayout.Button("Browse", GUILayout.Width(64f)))
                {
                    _resourcesFolder = EditorUtility.OpenFolderPanel("Resources Folder", Application.dataPath, "");
                    if (string.IsNullOrEmpty(_resourcesFolder))
                    {
                        Debug.Log("No Folder Selected.");
                    }
                }

                if (EditorGUI.EndChangeCheck())
                {
                    _changeOccured = true;
                }
            }
        }

        private void FolderDrawer()
        {
            EditorGUILayout.Space(4f);
            _showFolders = EditorGUILayout.ToggleLeft("Show Folders", _showFolders);

            if (!_showFolders) return;

            FolderInfo();
            FoldersDrawer();
        }

        private void FolderInfo()
        {
            EditorGUILayout.Space(8f);
            using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button(JediStyles.PLUS, GUILayout.Width(24f)))
                {
                    _count += 1;
                    _folders.Add("");
                }

                EditorGUILayout.LabelField($"Folders - {_count}", _centeredLabel);

                if (GUILayout.Button(JediStyles.MINUS, GUILayout.Width(24f)))
                {
                    _count -= 1;
                    _folders.RemoveAt(_count);
                    _changeOccured = true;
                }
            }

            EditorGUILayout.Space(4f);
            if (GUILayout.Button("Create Folders"))
            {
                CreateFolders.CreateProjectFolders(_folders);
            }

            EditorGUILayout.Space(4f);
        }

        private void FoldersDrawer()
        {
            using (EditorGUILayout.VerticalScope verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Height(280f)))
            {
                _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

                for (int i = 0; i < _count; i++)
                {
                    using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        EditorGUI.BeginChangeCheck();
                        _folders[i] = EditorGUILayout.TextField($"Folder - {i + 1}", _folders[i]);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _changeOccured = true;
                        }

                        if (GUILayout.Button(JediStyles.CROSS, GUILayout.Width(24f)))
                        {
                            _folders.RemoveAt(i);
                            _count -= 1;
                            _changeOccured = true;
                        }
                    }
                }

                EditorGUILayout.EndScrollView();

                if (_autoSave && _changeOccured)
                {
                    SaveData();
                    _changeOccured = false;
                }
            }
        }

        private void SaveSettingsButtonDrawer()
        {
            if (GUILayout.Button("Save Settings"))
            {
                SaveData();
            }
        }

        #endregion

        #region METHODS

        private void Initialization()
        {
            if (!_lightImage) _lightImage = Resources.Load<Texture2D>("Artwork/Icons/light-saber-light");
            if (!_darkImage) _darkImage = Resources.Load<Texture2D>("Artwork/Icons/light-saber-dark");

            if (_logoRect == Rect.zero)
            {
                _logoRect.width = 512;
                _logoRect.height = 192;
                _logoRect.x = (position.width / 2f) - (_logoRect.width / 2f);
                _logoRect.y = 0f;
            }

            _centeredLabel ??= JediStyles.CustomCenteredLabel(Color.white, Color.black, 12, true);
        }

        private void LoadData()
        {
            Debug.Log("Load Data");

            _projectSetup = new ProjectSetup();
            _projectSetup.LoadSettings();

            if (!string.IsNullOrEmpty(_projectSetup.CompanyName)) _companyName = _projectSetup.CompanyName;
            _folders = _projectSetup.Folders;
            _count = _folders.Count;
            _resourcesFolder = _projectSetup.ResourcesFolder;
        }

        private void SaveData()
        {
            if (_projectSetup == null) return;

            Debug.Log("Save Data");
            _projectSetup.CompanyName = _companyName;
            _projectSetup.Folders = _folders;
            _projectSetup.ResourcesFolder = _resourcesFolder;
            _projectSetup.SaveSettings();
        }

        private void LoadProjectSettings()
        {
            Debug.Log("Load Project Settings");
            if (!string.IsNullOrEmpty(PlayerSettings.companyName)) _companyName = PlayerSettings.companyName;
            if (!string.IsNullOrEmpty(PlayerSettings.productName)) _productName = PlayerSettings.productName;

            _cursor = PlayerSettings.defaultCursor;
            if (PlayerSettings.cursorHotspot != Vector2.zero) _cursorHotspot = PlayerSettings.cursorHotspot;

            _productIcon = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Unknown)[0];
        }

        private void UpdateProjectSettings()
        {
            PlayerSettings.companyName = _companyName;
            PlayerSettings.productName = _productName;

            PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, new[] { _productIcon });

            PlayerSettings.defaultCursor = _cursor;
            if (_cursor)
            {
                PlayerSettings.cursorHotspot = _cursorHotspot;
            }
        }

        #endregion
    }
}

#endif