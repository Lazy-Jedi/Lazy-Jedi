#if UNITY_EDITOR
using System.IO;
using RotaryHeartAddon.Internal;
using UnityEngine;
using UnityEditor;
using Directory = UnityEngine.Windows.Directory;

namespace RotaryHeartAddon.Editors
{
    public class DictionaryCreatorWindow : EditorWindow
    {
        #region WINDOW

        public static DictionaryCreatorWindow Window;

        [MenuItem("Lazy-Jedi/Create/Serializable Dictionary", priority = 300)]
        public static void OpenWindow()
        {
            Window = GetWindow<DictionaryCreatorWindow>("S-Dictionary Creator");
            Window.minSize = new Vector2(768, 384);
            Window.Show();
        }

        [MenuItem("Assets/Create/Serializable Dictionary", priority = 81)]
        public static void CreateSDictionary()
        {
            OpenWindow();
            Object selectedObject = Selection.activeObject;
            if (selectedObject)
            {
                Window._selectedPath = !Directory.Exists(AssetDatabase.GetAssetPath(selectedObject))
                    ? Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedObject))
                    : AssetDatabase.GetAssetPath(selectedObject);
            }

            Debug.Log(Window._selectedPath);
        }

        #endregion

        #region VARIABLES

        private string _selectedPath = string.Empty;

        private bool _sDictionaryTab = true;
        private bool _monoBehaviourTab;
        private bool _scriptableObjectTab;

        private bool _useRootNamespace = false;
        private string _namespace;
        private string _className;
        private string _sDictionaryName;

        private string _key;
        private int _keyIndex = 1;
        private string _value;
        private int _valueIndex = 1;

        private bool _useNamespace = false;
        private string _notification = string.Empty;

        private string _tick = "✓";
        private string _cross = "✖";


        private readonly string[] _types = UsableTypes.TypesList.ToArray();

        private GUIStyle _centerLabelStyle;
        private GUIStyle _centerErrorTextStyle;

        #endregion

        #region UNITY METHODS

        public void OnGUI()
        {
            InitializeStyles();
            ButtonTabsDrawer();
            NamespaceDrawer();
            ClassNameDrawer();
            SDictionaryNameDrawer();
            KeyValuePairDrawer();
            CreateButtonDrawer();
            NotificationDrawer();
        }

        #endregion

        #region METHODS

        private void ButtonTabsDrawer()
        {
            EditorGUILayout.LabelField("Choose your Template", _centerLabelStyle);
            using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                if (GUILayout.Button($"Create Serializable-Dictionary {(_sDictionaryTab ? _tick : _cross)}", EditorStyles.miniButtonLeft))
                {
                    _sDictionaryTab = true;
                    _monoBehaviourTab = false;
                    _scriptableObjectTab = false;
                }
                else if (GUILayout.Button($"Create MonoBehaviour + S-Dictionary {(_monoBehaviourTab ? _tick : _cross)}", EditorStyles.miniButtonMid))
                {
                    _sDictionaryTab = false;
                    _monoBehaviourTab = true;
                    _scriptableObjectTab = false;
                }
                else if (GUILayout.Button($"Create ScriptableObject + S-Dictionary {(_scriptableObjectTab ? _tick : _cross)}", EditorStyles.miniButtonRight))
                {
                    _sDictionaryTab = false;
                    _monoBehaviourTab = false;
                    _scriptableObjectTab = true;
                }
            }
        }

        private void NamespaceDrawer()
        {
            EditorGUILayout.Space(16f);
            _useNamespace = EditorGUILayout.ToggleLeft("Use Namespace", _useNamespace);

            if (!_useNamespace) return;

            EditorGUI.BeginChangeCheck();
            _useRootNamespace = EditorGUILayout.ToggleLeft($"Use Root Namespace ({EditorSettings.projectGenerationRootNamespace}):", _useRootNamespace);
            if (EditorGUI.EndChangeCheck())
            {
                _namespace = _useRootNamespace ? EditorSettings.projectGenerationRootNamespace : string.Empty;
            }

            TextFieldDrawerHelper(
                "Namespace",
                ref _namespace,
                "Leave Blank to Use the Editor Root Namespace.\nIf there is no Editor Root Namespace, no Namespace will be added to the class.",
                false
            );
        }

        private void ClassNameDrawer()
        {
            if (_sDictionaryTab) return;
            TextFieldDrawerHelper("Class Name", ref _className);
        }

        private void SDictionaryNameDrawer()
        {
            TextFieldDrawerHelper("S-Dictionary Class Name", ref _sDictionaryName, "Serializable Dictionary Class Name.");
        }


        private void KeyValuePairDrawer()
        {
            EditorGUILayout.Space(16f);
            KeyValuePairDrawerHelper("Key Type: ", ref _key, ref _keyIndex);
            EditorGUILayout.Space(2f);
            KeyValuePairDrawerHelper("Value Type: ", ref _value, ref _valueIndex);
        }

        private void CreateButtonDrawer()
        {
            EditorGUILayout.Space(8f);
            EditorGUILayout.LabelField($"Output Path: {(string.IsNullOrEmpty(_selectedPath) ? "User Selected Path via Save File Dialog" : _selectedPath)}",
                EditorStyles.helpBox);
            if (GUILayout.Button("Create"))
            {
                if (_sDictionaryTab)
                {
                    CreateScriptHelper(CreateScripts.CreateFromTemplate(
                        Templates.SDictionary,
                        _sDictionaryName,
                        "",
                        _key,
                        _value,
                        _useNamespace ? _namespace : string.Empty
                    ), _sDictionaryName);
                }

                if (_monoBehaviourTab)
                {
                    CreateScriptHelper(CreateScripts.CreateFromTemplate(
                        Templates.MonoDictionary,
                        _className,
                        _sDictionaryName,
                        _key,
                        _value,
                        _useNamespace ? _namespace : string.Empty
                    ), _className);
                }

                if (_scriptableObjectTab)
                {
                    CreateScriptHelper(CreateScripts.CreateFromTemplate(
                        Templates.SoDictionary,
                        _className,
                        _sDictionaryName,
                        _key,
                        _value,
                        _useNamespace ? _namespace : string.Empty
                    ), _className);
                }

                AssetDatabase.Refresh();
            }
        }

        private void NotificationDrawer()
        {
            EditorGUILayout.Space(8f);
            EditorGUILayout.LabelField("Notifications", _centerLabelStyle);
            EditorGUILayout.LabelField(_notification, _centerErrorTextStyle, GUILayout.Height(48f));
        }

        #endregion

        #region HELPER METHODS

        private void KeyValuePairDrawerHelper(string itemLabel, ref string item, ref int itemIndex)
        {
            itemIndex = EditorGUILayout.Popup(itemLabel, itemIndex, _types);
            item = _types[itemIndex] == "Custom" ? EditorGUILayout.TextField($"Custom {itemLabel}", item) : _types[itemIndex];
        }

        private void TextFieldDrawerHelper(string label, ref string text, string tooltip = "", bool showNotification = true)
        {
            EditorGUILayout.Space(2f);
            EditorGUI.BeginChangeCheck();
            text = EditorGUILayout.TextField(new GUIContent($"{label}: ", tooltip), text, GUILayout.ExpandWidth(true));
            if (EditorGUI.EndChangeCheck())
            {
                if (string.IsNullOrEmpty(text) && showNotification)
                {
                    _notification = $"Cannot have an Empty {label}.";
                }
            }
        }

        private void CreateScriptHelper(string script, string scriptName)
        {
            if ((string.IsNullOrEmpty(_className) && !_sDictionaryTab) || string.IsNullOrEmpty(_sDictionaryName))
            {
                _notification = "Invalid Class Name or S-Dictionary Class Name!";
                return;
            }

            if (string.IsNullOrEmpty(script))
            {
                _notification = "Script Template is Empty!";
                return;
            }

            string path = string.IsNullOrEmpty(_selectedPath)
                ? EditorUtility.SaveFilePanelInProject($"Save {scriptName}", Path.GetFileNameWithoutExtension(scriptName), "cs", "")
                : Path.Combine(_selectedPath, $"{scriptName}.cs");

            if (string.IsNullOrEmpty(path))
            {
                _notification = "Path to Save Script cannot be Empty String!";
                return;
            }

            _notification = $"File {scriptName} has been Created!";
            File.WriteAllText(path, script);
        }

        #endregion

        #region HELPER METHODS

        private void InitializeStyles()
        {
            if (_centerLabelStyle == null)
            {
                _centerLabelStyle = new GUIStyle
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontStyle = FontStyle.Normal
                };
                _centerLabelStyle.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            }

            if (_centerErrorTextStyle == null)
            {
                _centerErrorTextStyle = new GUIStyle(EditorStyles.helpBox)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 12,
                    fontStyle = FontStyle.Normal,
                };
                _centerErrorTextStyle.normal.textColor = EditorGUIUtility.isProSkin ? new Color(0.93f, 0.87f, 1f) : Color.black;
            }
        }

        #endregion
    }
#endif
}