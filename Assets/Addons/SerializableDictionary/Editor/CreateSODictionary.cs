#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SerializableDictionary
{
    public class CreateSODictionary : EditorWindow
    {
        #region VARIABLES

        private string _namespace = String.Empty;
        private string _soClassName = String.Empty;
        private string _sClassName = String.Empty;

        private PrimitiveType _keyType = PrimitiveType.StringType;
        private string _key = String.Empty;
        private PrimitiveType _valueType = PrimitiveType.StringType;
        private string _value = String.Empty;

        private TextAsset _soDatabaseTemplate;
        private TextAsset _sDictionaryTemplate;

        private string _data = String.Empty;
        private static string _path = String.Empty;

        private readonly GUIContent _sDictionaryClassNameContent =
            new GUIContent("S Dictionary Class Name: ", "Serializable Dictionary Name.");

        #endregion

        #region WINDOW METHODS

        public static CreateSODictionary _window;

        [MenuItem("Dictionary/Create/ScriptableObject-Dictionary", priority = 20)]
        [MenuItem("Assets/Create/Dictionary/ScriptableObject Dictionary", priority = 89)]
        public static void OpenWindow()
        {
            _window = GetWindow<CreateSODictionary>("Create SO Database");
            _window.Show();
            _window.minSize = new Vector2(450, 200);

            Object selectedObject = Selection.activeObject;
            _path = AssetDatabase.GetAssetPath(selectedObject);
            if (!Directory.Exists(_path)) _path = Path.GetDirectoryName(_path);
        }

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            _soDatabaseTemplate = Resources.Load<TextAsset>("template_so_dictionary");
            _sDictionaryTemplate = Resources.Load<TextAsset>("template_s_dictionary");

            if (EditorPrefs.HasKey("$NAMESPACE")) _namespace = EditorPrefs.GetString("$NAMESPACE");
        }

        public void OnGUI()
        {
            EditorGUILayout.ObjectField("Template:", _soDatabaseTemplate, typeof(TextAsset), false);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            _namespace = EditorGUILayout.TextField("Namespace:", _namespace);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetString("$NAMESPACE", _namespace);
                Debug.Log(_namespace);
            }

            EditorGUILayout.Space();
            _soClassName = EditorGUILayout.TextField("SO-Dictionary Class Name:", _soClassName);
            _sClassName = EditorGUILayout.TextField(_sDictionaryClassNameContent, _sClassName);

            EditorGUILayout.Space();

            _keyType = (PrimitiveType)EditorGUILayout.EnumPopup("Select Key Type", _keyType);
            _valueType = (PrimitiveType)EditorGUILayout.EnumPopup("Select Value Type", _valueType);

            if (_keyType == PrimitiveType.CustomType)
            {
                _key = EditorGUILayout.TextField("Custom Key Type", _key);
            }

            if (_valueType == PrimitiveType.CustomType)
            {
                _value = EditorGUILayout.TextField("Custom Value Type", _value);
            }

            EditorGUILayout.Space();

            if (!GUILayout.Button("Create")) return;
            if (string.IsNullOrEmpty(_soClassName) || string.IsNullOrEmpty(_sClassName))
            {
                Debug.LogError("Cannot have Empty Class Name!");
                return;
            }

            CreateSoDatabase();
            CreateSDictionary();

            AssetDatabase.Refresh();
        }

        #endregion

        #region METHODS

        private void CreateSoDatabase()
        {
            _data = _soDatabaseTemplate.text.Replace("$SO_DICTIONARY", _soClassName);

            _data = _data.Replace("$NAMESPACE", _namespace);

            _data = _data.Replace("$S_DICTIONARY", _sClassName);

            _data = _data.Replace("$KEY", _keyType != PrimitiveType.CustomType
                ? TypeString.TypesDictionary[_keyType]
                : _key);

            _data = _data.Replace("$VALUE", _valueType != PrimitiveType.CustomType
                ? TypeString.TypesDictionary[_valueType]
                : _value);

            string path = _path;
            if (string.IsNullOrEmpty(_path))
            {
                path = EditorUtility.SaveFilePanel("Save", Application.dataPath, _soClassName, "cs");
            }
            else
            {
                path = Path.Combine(path, $"{_soClassName}.cs");
            }

            File.WriteAllText(path, _data);
            _data = string.Empty;
        }

        private void CreateSDictionary()
        {
            _data = _sDictionaryTemplate.text.Replace("$S_DICTIONARY", _sClassName);

            _data = _data.Replace("$NAMESPACE", _namespace);

            _data = _data.Replace("$KEY", _keyType != PrimitiveType.CustomType
                ? TypeString.TypesDictionary[_keyType]
                : _key);

            _data = _data.Replace("$VALUE", _valueType != PrimitiveType.CustomType
                ? TypeString.TypesDictionary[_valueType]
                : _value);

            string path = _path;
            if (string.IsNullOrEmpty(_path))
            {
                path = EditorUtility.SaveFilePanel("Save", Application.dataPath, _sClassName, "cs");
            }
            else
            {
                path = Path.Combine(path, $"{_sClassName}.cs");
            }

            File.WriteAllText(path, _data);
            _data = string.Empty;
        }

        #endregion
    }
}
#endif