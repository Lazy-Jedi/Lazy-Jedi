#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using LazyJedi.Editors.Internal;
using LazyJedi.Globals;
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
            Window.minSize = new Vector2(770, 982);
            Window.Show();

            Window.LoadPlayerSettings();
            Window.LoadProjectSetupData();
            Window.InitializeWindow();
        }

        #endregion

        #region EDITOR FIELDS

        private Texture2D _lightImage;
        private Texture2D _darkImage;
        private Rect _logoRect = Rect.zero;

        private bool _showFolders = true;
        private bool _autoSave = true;
        private bool _changeOccured = false;

        private GUIStyle _centeredLabel;
        private Font _headerFont;
        private Vector2 _scrollPosition = Vector2.zero;
        private readonly GUIContent _resourcesGUIContent = new GUIContent("Resources Folder:", "Select your Local Resources Folder.");
        private readonly GUIContent _temporaryGUIContent = new GUIContent("Temporary Folder:", "Select your Local Temporary Folder.");

        #endregion

        #region PLAYER AND PROJECT SETUP FIELDS

        private ProjectSetup _projectSetup;

        private string _companyName;
        private string _productName;

        private string _resourcesFolder;
        private bool _useProjectTemporaryFolder;
        private bool _useCustomTemporaryFolder;
        private string _temporaryFolder;

        private Texture2D _productIcon;
        private Texture2D _cursor;
        private Vector2 _cursorHotspot;

        private List<string> _folders = new List<string>();
        private int _count;

        #endregion

        #region UNITY METHODS

        public void OnGUI()
        {
            BackgroundLogoDrawer();
            AutoSaveDrawer();
            ProductInfoDrawer();
            CustomFoldersDrawer();
            ProjectFoldersDrawer();
            ButtonsDrawer();
        }

        private void OnDestroy()
        {
            Resources.UnloadAsset(_lightImage);
            Resources.UnloadAsset(_darkImage);
            Resources.UnloadAsset(_headerFont);
        }

        #endregion

        #region DRAWER METHODS

        private void BackgroundLogoDrawer()
        {
            _logoRect.x = (position.width / 2f) - (_logoRect.width / 2f);
            GUI.DrawTexture(_logoRect, EditorGUIUtility.isProSkin ? _lightImage : _darkImage, ScaleMode.ScaleToFit);
            EditorGUILayout.Space(_logoRect.height);
        }

        private void AutoSaveDrawer()
        {
            _autoSave = EditorGUILayout.ToggleLeft("Auto Save?", _autoSave);
        }

        private void ProductInfoDrawer()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.Space(8f);
            using (EditorGUILayout.VerticalScope verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Product Settings", _centeredLabel);
                _productIcon = (Texture2D)EditorGUILayout.ObjectField("Product Icon", _productIcon, typeof(Texture2D), false);
                _cursor = (Texture2D)EditorGUILayout.ObjectField("Cursor", _cursor, typeof(Texture2D), false);
                if (_cursor)
                {
                    _cursorHotspot = EditorGUILayout.Vector2Field("Cursor Hotspot:", _cursorHotspot);
                }

                _companyName = EditorGUILayout.TextField("Company Name:", _companyName);
                _productName = EditorGUILayout.TextField("Product Name:", _productName);
            }
            if (EditorGUI.EndChangeCheck())
            {
                UpdateProjectSettings();
            }

            EditorGUILayout.Space(4f);
        }

        private void CustomFoldersDrawer()
        {
            using (EditorGUILayout.VerticalScope verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Custom Folders", _centeredLabel);
                ResourcesFolderDrawerHelper();
                TemporaryFolderDrawerHelper();
            }

            EditorGUILayout.Space(4f);
        }

        private void ProjectFoldersDrawer()
        {
            using (EditorGUILayout.VerticalScope verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Project Folders", _centeredLabel);
                _showFolders = EditorGUILayout.ToggleLeft("Show Folders", _showFolders);
                if (!_showFolders) return;
                FolderInfo();
                FoldersDrawer();
            }
        }

        private void FolderInfo()
        {
            EditorGUILayout.Space(8f);
            using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button(LazyEditorStyles.MINUS_CHAR, GUILayout.Width(24f)))
                {
                    _count -= 1;
                    _folders.RemoveAt(_count);
                    _changeOccured = true;
                }

                EditorGUILayout.LabelField($"Folders - {_count}", _centeredLabel);

                if (GUILayout.Button(LazyEditorStyles.PLUS_CHAR, GUILayout.Width(24f)))
                {
                    _count += 1;
                    _folders.Add("");
                }
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

                        if (GUILayout.Button(LazyEditorStyles.CROSS_CHAR, GUILayout.Width(24f)))
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
                    SaveProjectSetupData();
                    _changeOccured = false;
                }
            }
        }

        private void ButtonsDrawer()
        {
            if (GUILayout.Button("Create Folders"))
            {
                CreateFolders.CreateProjectFolders(_folders);
            }

            if (GUILayout.Button("Save Settings"))
            {
                SaveProjectSetupData();
            }
        }

        #endregion

        #region DRAWER HELPER METHODS

        private void CustomFolderDrawerHelper(ref string customFolderPath, GUIContent label, string openFolderPanelTitle)
        {
            using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope())
            {
                EditorGUI.BeginChangeCheck();
                customFolderPath = EditorGUILayout.TextField(label, customFolderPath);

                if (GUILayout.Button("Browse", GUILayout.Width(64f)))
                {
                    customFolderPath = EditorUtility.OpenFolderPanel(openFolderPanelTitle, Application.dataPath, "");
                    if (string.IsNullOrEmpty(customFolderPath))
                    {
                        Debug.unityLogger.Log("No Folder Selected.");
                    }
                }

                if (EditorGUI.EndChangeCheck())
                {
                    _changeOccured = true;
                }

                if (_autoSave && _changeOccured)
                {
                    SaveProjectSetupData();
                    _changeOccured = false;
                }
            }
        }

        private void ResourcesFolderDrawerHelper()
        {
            CustomFolderDrawerHelper(ref _resourcesFolder, _resourcesGUIContent, "Resources Folder");
        }

        private void TemporaryFolderDrawerHelper()
        {
            EditorGUI.BeginChangeCheck();
            _useProjectTemporaryFolder = EditorGUILayout.ToggleLeft("Use Project Temporary Folder?", _useProjectTemporaryFolder);
            _temporaryFolder = !_useProjectTemporaryFolder ? LazyEditorStrings.DEFAULT_TEMPORARY_PATH : LazyEditorStrings.PROJECT_TEMPORARY_PATH;
            _useCustomTemporaryFolder = EditorGUILayout.BeginToggleGroup("Change Temporary Folder?", _useCustomTemporaryFolder);
            CustomFolderDrawerHelper(ref _temporaryFolder, _temporaryGUIContent, "Temporary Folder");
            if (string.IsNullOrEmpty(_temporaryFolder))
            {
                _temporaryFolder = LazyEditorStrings.DEFAULT_TEMPORARY_PATH;
            }
            EditorGUILayout.EndToggleGroup();
            if (EditorGUI.EndChangeCheck())
            {
                _changeOccured = true;
            }

            if (_autoSave && _changeOccured)
            {
                SaveProjectSetupData();
                _changeOccured = false;
            }
        }

        #endregion

        #region METHODS

        private void InitializeWindow()
        {
            if (!_lightImage)
            {
                _lightImage = Resources.Load<Texture2D>(LazyEditorArt.LazyJediLiteLogo);
            }
            if (!_darkImage)
            {
                _darkImage = Resources.Load<Texture2D>(LazyEditorArt.LazyJediDarkLogo);
            }

            if (_logoRect == Rect.zero)
            {
                _logoRect.width = 512;
                _logoRect.height = 192;
                _logoRect.x = (position.width / 2f) - (_logoRect.width / 2f);
                _logoRect.y = 0f;
            }

            if (!_headerFont)
            {
                _headerFont = Resources.Load<Font>(LazyEditorArt.MiniSquareFont);
            }
            _centeredLabel ??= LazyEditorStyles.CustomHelpBoxLabel(LazyColors.UnityFontColorLite, LazyColors.UnityFontColorDark, 16, _headerFont);
        }

        private void LoadProjectSetupData()
        {
            Debug.unityLogger.Log("Load Project Setup Data");
            _projectSetup = new ProjectSetup().LoadSettings();
            _folders = _projectSetup.Folders;
            _count = _folders.Count;
            _resourcesFolder = _projectSetup.ResourcesFolder;
            _useCustomTemporaryFolder = _projectSetup.UseCustomTemporaryFolder;
            _useProjectTemporaryFolder = _projectSetup.UseProjectTemporaryFolder;

            _temporaryFolder = string.IsNullOrEmpty(_projectSetup.TemporaryFolder)
                ? LazyEditorStrings.DEFAULT_TEMPORARY_PATH
                : _projectSetup.TemporaryFolder;

            if (!Directory.Exists(_temporaryFolder))
            {
                Directory.CreateDirectory(_temporaryFolder);
            }
            AssetDatabase.Refresh();
        }

        private void SaveProjectSetupData()
        {
            Debug.unityLogger.Log("Save Project Setup Data");
            _projectSetup.Folders = _folders;
            _projectSetup.ResourcesFolder = _resourcesFolder;
            _projectSetup.UseCustomTemporaryFolder = _useCustomTemporaryFolder;
            _projectSetup.UseProjectTemporaryFolder = _useProjectTemporaryFolder;

            _projectSetup.TemporaryFolder =
                !_useCustomTemporaryFolder // If not using custom temporary folder
                    ? !_useProjectTemporaryFolder // If not using project temporary folder
                        ? LazyEditorStrings.DEFAULT_TEMPORARY_PATH // Use default temporary folder, else
                        : LazyEditorStrings.PROJECT_TEMPORARY_PATH // Use project temporary folder, else
                    : _temporaryFolder; // Use custom temporary folder
            _projectSetup.SaveSettings();
        }

        private void LoadPlayerSettings()
        {
            Debug.unityLogger.Log("Load Player Settings");
            if (!string.IsNullOrEmpty(PlayerSettings.companyName))
            {
                _companyName = PlayerSettings.companyName;
            }
            if (!string.IsNullOrEmpty(PlayerSettings.productName))
            {
                _productName = PlayerSettings.productName;
            }

            _cursor = PlayerSettings.defaultCursor;
            if (PlayerSettings.cursorHotspot != Vector2.zero) _cursorHotspot = PlayerSettings.cursorHotspot;

            Texture2D[] icons = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Unknown);
            if (icons?.Length > 0) _productIcon = icons[0];
        }

        private void UpdateProjectSettings()
        {
            Debug.unityLogger.Log("Update Project Settings");
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