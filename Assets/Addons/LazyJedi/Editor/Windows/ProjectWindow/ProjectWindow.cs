#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LazyJedi.Common.Extensions;
using LazyJedi.Editors.Common;
using LazyJedi.Editors.Globals;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LazyJedi.Editors.Windows
{
    public class ProjectWindow : EditorWindow
    {
        #region WINDOW

        [SerializeField]
        private VisualTreeAsset _visualTreeAsset;
        [SerializeField]
        private StyleSheet _styleSheet;
        [SerializeField]
        private VisualTreeAsset _folderLvItem;

        private static ProjectWindow Instance { get; set; }


        [MenuItem("Lazy-Jedi/Project Setup #&P", priority = 100)]
        public static void CreateWindow()
        {
            Instance = GetWindow<ProjectWindow>();
            Instance.titleContent = new GUIContent("Project Setup");
            Instance.minSize = Instance.maxSize = new Vector2(640, 970);
        }

        #endregion

        #region FIELDS

        private VisualElement _root;

        private ToolbarMenu _fileMenu;
        private ToolbarMenu _settingsMenu;
        private ToolbarMenu _aboutMenu;
        private ToolbarToggle _autoSaveTgl;

        private ObjectField _productIcon;
        private ObjectField _cursorIcon;

        private TextField _companyNameTF;
        private TextField _productNameTF;

        private VisualElement _tempContainer;
        private TextField _resourcesFolderTF;
        private Button _resBrowseBtn;
        private Toggle _useCustomTgl;
        private TextField _tempFolderTF;
        private Button _tempBrowseBtn;

        private Label _foldersLbl;
        private ListView _foldersLv;
        private Button _addFolderBtn;
        private Button _removeFolderBtn;
        private Button _createFoldersBtn;

        private Button _saveBtn;
        private Button _loadBtn;
        private Button _resetBtn;

        private Project _project = new Project();
        private bool _isDirty;

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            _project.Load();
        }

        private void CreateGUI()
        {
            _root = _visualTreeAsset.CloneTree();
            InitializeUI();
            SetUIValues();
            rootVisualElement.Add(_root);
        }

        #endregion

        #region SETUP METHODS

        private void InitializeUI()
        {
            #region TOOLBAR SETUP

            #region MENU TOOLBAR

            _fileMenu = _root.Q<ToolbarMenu>("fileTlb");
            _fileMenu.menu.AppendAction("New", action => { ResetSettings(); }, DropdownMenuAction.AlwaysEnabled);
            _fileMenu.menu.AppendAction("Create Folders", action => { CreateFolders(_project.Folders); }, DropdownMenuAction.AlwaysEnabled);
            _fileMenu.menu.AppendAction("Close", action =>
            {
                if (!_isDirty)
                {
                    Close();
                    return;
                }

                bool result = EditorUtility.DisplayDialog("Save", "Do you want to save the changes?", "Yes", "Cancel");
                if (!result) return;
                SaveSettings();
                Close();
            }, DropdownMenuAction.AlwaysEnabled);

            #endregion

            #region SETTINGS TOOLBAR

            _settingsMenu = _root.Q<ToolbarMenu>("settingsTlb");
            _settingsMenu.menu.AppendAction("Save", action => { SaveSettings(); }, DropdownMenuAction.AlwaysEnabled);
            _settingsMenu.menu.AppendAction("Load", action => { LoadSettings(); }, DropdownMenuAction.AlwaysEnabled);
            _settingsMenu.menu.AppendAction("Reset", action => { ResetSettings(); }, DropdownMenuAction.AlwaysEnabled);

            #endregion

            #region HELP TOOLBAR

            _aboutMenu = _root.Q<ToolbarMenu>("helpTlb");
            _aboutMenu.menu.AppendAction("Help", action =>
                {
                    bool result = EditorUtility.DisplayDialog("Help", "Visit GitHub Page", "OK");
                    if (!result) return;
                    Application.OpenURL("https://github.com/Lazy-Jedi/Lazy-Jedi");
                },
                DropdownMenuAction.AlwaysEnabled);

            _aboutMenu.menu.AppendAction("Credits", action => { CreditsWindow.CreateWindow(); }, DropdownMenuAction.AlwaysEnabled);

            #endregion

            _autoSaveTgl = _root.Q<ToolbarToggle>("autoSaveTgl");
            _autoSaveTgl.RegisterValueChangedCallback(evt =>
            {
                _isDirty = true;
                _project.AutoSave = evt.newValue;
                AutoSave();
            });

            #endregion

            #region PROJECT SETUP

            _productIcon = _root.Q<ObjectField>("iconOF");
            _productIcon.RegisterValueChangedCallback(evt =>
            {
                _productIcon.value = evt.newValue;
                PlayerSettings.SetIcons(NamedBuildTarget.Unknown, new[] { (Texture2D)evt.newValue }, IconKind.Application);
            });

            _cursorIcon = _root.Q<ObjectField>("cursorOF");
            _cursorIcon.RegisterValueChangedCallback(evt => { PlayerSettings.defaultCursor = (Texture2D)evt.newValue; });

            _companyNameTF = _root.Q<TextField>("companyTF");
            _companyNameTF.RegisterValueChangedCallback(evt => { PlayerSettings.companyName = evt.newValue; });

            _productNameTF = _root.Q<TextField>("productTF");
            _productNameTF.RegisterValueChangedCallback(evt => { PlayerSettings.productName = evt.newValue; });

            #endregion

            #region CUSTOM FOLDER SETUP

            _resourcesFolderTF = _root.Q<TextField>("resFolderTF");
            _resBrowseBtn = _root.Q<Button>("resBrowseBtn");
            _resBrowseBtn.clickable.clicked += () =>
            {
                _isDirty = true;
                _resourcesFolderTF.value = SelectFolder();
                _project.ResourcesFolder = _resourcesFolderTF.value;
                AutoSave();
            };

            _tempContainer = _root.Q<VisualElement>("tempContainer");
            _tempFolderTF = _root.Q<TextField>("tempFolderTF");
            _tempBrowseBtn = _root.Q<Button>("tempBrowseBtn");
            _tempBrowseBtn.clickable.clicked += () =>
            {
                _isDirty = true;
                _tempFolderTF.value = SelectFolder();
                Debug.unityLogger.Log(_tempFolderTF.value);
                _project.TempFolder = string.IsNullOrEmpty(_tempFolderTF.value.Trim()) ? StringGlobals.SYS_TEMP_PATH : _tempFolderTF.value;
                AutoSave();
            };

            _useCustomTgl = _root.Q<Toggle>("useCustomTgl");
            _useCustomTgl.RegisterValueChangedCallback(evt =>
            {
                _tempContainer.SetEnabled(evt.newValue);
                if (!evt.newValue)
                {
                    _project.TempFolder = _tempFolderTF.value = StringGlobals.SYS_TEMP_PATH;
                }
            });

            #endregion

            #region LIST VIEW SETUP

            _foldersLbl = _root.Q<Label>("foldersLabel");
            _foldersLbl.text = $"Folders - {_project.Folders.Count}";
            _createFoldersBtn = _root.Q<Button>("createBtn");
            _createFoldersBtn.clickable.clicked += () => CreateFolders(_project.Folders);

            _addFolderBtn = _root.Q<Button>("addFolderBtn");
            _addFolderBtn.clickable.clicked += AddFolder;
            _removeFolderBtn = _root.Q<Button>("removeFolderBtn");
            _removeFolderBtn.clickable.clicked += RemoveLastFolder;

            _foldersLv = _root.Q<ListView>("foldersLv");

            #endregion

            #region BUTTON SETUP

            _saveBtn = _root.Q<Button>("saveBtn");
            _saveBtn.clickable.clicked += SaveSettings;
            _loadBtn = _root.Q<Button>("loadBtn");
            _loadBtn.clickable.clicked += LoadSettings;
            _resetBtn = _root.Q<Button>("resetBtn");
            _resetBtn.clickable.clicked += ResetSettings;

            #endregion
        }

        private void SetUIValues()
        {
            #region TOOLBAR SETTINGS

            _autoSaveTgl.value = _project.AutoSave;

            #endregion

            #region PRODUCT SETTINGS

            Object icon = PlayerSettings.GetIcons(NamedBuildTarget.Unknown, IconKind.Application).FirstOrDefault();
            if (icon)
            {
                _productIcon.SetValueWithoutNotify(icon);
            }

            Object cursor = PlayerSettings.defaultCursor;
            if (cursor)
            {
                _cursorIcon.SetValueWithoutNotify(cursor);
            }

            _companyNameTF.SetValueWithoutNotify(PlayerSettings.companyName);
            _productNameTF.SetValueWithoutNotify(PlayerSettings.productName);

            #endregion

            #region CUSTOM SETTINGS

            _resourcesFolderTF.SetValueWithoutNotify(_project.ResourcesFolder);
            _useCustomTgl.value = _project.UseCustomTempFolder;
            _tempFolderTF.SetValueWithoutNotify(_project.TempFolder.IsNullOrEmpty() ? StringGlobals.SYS_TEMP_PATH : _project.TempFolder);

            #endregion

            #region PROJECT FOLDERS

            _foldersLbl.text = $"Folders - {_project.Folders.Count}";

            _foldersLv.itemsSource = _project.Folders;
            _foldersLv.makeItem = MakeItem;
            _foldersLv.bindItem = BindItem;
            _foldersLv.fixedItemHeight = 30;
            _foldersLv?.Rebuild();

            #endregion
        }

        #endregion

        #region METHODS

        private void CreateFolders(List<string> folders)
        {
            string basePath = Application.dataPath;
            foreach (string folderPath in folders)
            {
                string fullPath = Path.Combine(basePath, folderPath);
                if (Directory.Exists(fullPath))
                {
                    continue;
                }

                Directory.CreateDirectory(fullPath);
            }

            AssetDatabase.Refresh();
        }

        private void AutoSave()
        {
            if (!_project.AutoSave || !_isDirty)
            {
                return;
            }

            _isDirty = false;
            _project.Save();
        }

        #endregion

        #region LIST VIEW METHODS

        private VisualElement MakeItem()
        {
            return _folderLvItem.CloneTree();
        }

        private void BindItem(VisualElement element, int index)
        {
            TextField textField = element.Q<TextField>("folderPathTF");

            textField.label = Path.GetFileNameWithoutExtension(_project.Folders[index]);

            textField.value = _project.Folders[index];
            textField.RegisterValueChangedCallback(evt =>
            {
                _isDirty = true;
                _project.Folders[index] = evt.newValue;
                textField.label = Path.GetFileNameWithoutExtension(evt.newValue);
                AutoSave();
            });

            Button deleteButton = element.Q<Button>("deleteBtn");
            deleteButton.clickable.clicked += () => RemoveFolder(index);
        }

        #endregion

        #region BUTTON METHODS

        private string SelectFolder()
        {
            Debug.unityLogger.Log("Select Folder");
            string path = EditorUtility.OpenFolderPanel("Select Folder", StringGlobals.PROJECT_PATH, string.Empty);
            return string.IsNullOrEmpty(path) ? string.Empty : path;
        }

        private void SaveSettings()
        {
            Debug.unityLogger.Log("Save Settings");
            _isDirty = false;
            _project.AutoSave = _autoSaveTgl.value;
            _project.ResourcesFolder = _resourcesFolderTF.value;
            _project.UseCustomTempFolder = _useCustomTgl.value;
            _project.TempFolder = _tempFolderTF.value;
            _project.Save();
        }

        private void LoadSettings()
        {
            Debug.unityLogger.Log("Load Settings");
            _isDirty = false;
            _project.Load();
            SetUIValues();
        }

        private void ResetSettings()
        {
            Debug.unityLogger.Log("Reset Settings");
            _isDirty = false;
            _project = new Project();
            SetUIValues();
        }

        private void AddFolder()
        {
            Debug.Log("Add Item");
            _isDirty = true;
            _project.Folders.Add(string.Empty);
            _foldersLbl.text = $"Folders - {_project.Folders.Count}";
            _foldersLv?.Rebuild();
            AutoSave();
        }

        private void RemoveFolder(int index)
        {
            Debug.Log("Remove Item");
            _isDirty = true;
            _project.Folders.RemoveAt(index);
            _foldersLbl.text = $"Folders - {_project.Folders.Count}";
            _foldersLv?.Rebuild();
            AutoSave();
        }

        private void RemoveLastFolder()
        {
            if (_project.Folders.Count <= 0)
            {
                return;
            }

            Debug.Log("Remove Last Item");
            _isDirty = true;
            int lastIndex = _project.Folders.Count - 1;
            _project.Folders.RemoveAt(lastIndex);
            _foldersLbl.text = $"Folders - {_project.Folders.Count}";
            _foldersLv?.Rebuild();
            AutoSave();
        }

        #endregion
    }
}
#endif