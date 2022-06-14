#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SerializableDictionary
{
    public class CreateSDictionary : EditorWindow
    {
        #region VARIABLES

        private string _namespace = String.Empty;
        private string _className = String.Empty;

        private PrimitiveType _keyType = PrimitiveType.StringType;
        private string _key = String.Empty;
        private PrimitiveType _valueType = PrimitiveType.StringType;
        private string _value = String.Empty;

        private TextAsset _sDictionaryTemplate;
        private string _data = String.Empty;
        private static string _path = String.Empty;

        private readonly GUIContent _sDictionaryClassNameContent =
            new GUIContent("Class Name: ", "Serializable Dictionary Name.");

        #endregion

        #region WINDOW METHODS

        public static CreateSDictionary _window;

        [MenuItem("Dictionary/Create/Serializable-Dictionary", priority = 20)]
        [MenuItem("Assets/Create/Dictionary/Serializable Dictionary", priority = 89)]
        public static void OpenWindow()
        {
            _window = GetWindow<CreateSDictionary>("Create S Dictionary");
            _window.Show();
            _window.maxSize = new Vector2(450, 200);

            Object selectedObject = Selection.activeObject;
            _path = AssetDatabase.GetAssetPath(selectedObject);
            if (!Directory.Exists(_path)) _path = Path.GetDirectoryName(_path);
        }

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            _sDictionaryTemplate = Resources.Load<TextAsset>("template_s_dictionary");
            if (EditorPrefs.HasKey("$NAMESPACE")) _namespace = EditorPrefs.GetString("$NAMESPACE");
        }

        public void OnGUI()
        {
            EditorGUILayout.ObjectField("Template:", _sDictionaryTemplate, typeof(TextAsset), false);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            _namespace = EditorGUILayout.TextField("Namespace:", _namespace);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetString("$NAMESPACE", _namespace);
            }

            EditorGUILayout.Space();

            _className = EditorGUILayout.TextField(_sDictionaryClassNameContent, _className);
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
            if (string.IsNullOrEmpty(_className))
            {
                Debug.LogError("Cannot have Empty Class Name!");
                return;
            }

            CreateSDictionaryScript();
            AssetDatabase.Refresh();
        }

        #endregion

        #region METHODS

        private void CreateSDictionaryScript()
        {
            _data = _sDictionaryTemplate.text.Replace("$S_DICTIONARY", _className);

            _data = _data.Replace("$NAMESPACE", _namespace);

            _data = _data.Replace("$KEY", _keyType != PrimitiveType.CustomType
                ? TypeString.TypesDictionary[_keyType]
                : _key);

            _data = _data.Replace("$VALUE", _valueType != PrimitiveType.CustomType
                ? TypeString.TypesDictionary[_valueType]
                : _value);

            if (string.IsNullOrEmpty(_path))
            {
                _path = EditorUtility.SaveFilePanel("Save", Application.dataPath, _className, "cs");
            }
            else
            {
                _path = Path.Combine(_path, $"{_className}.cs");
            }

            File.WriteAllText(_path, _data);
        }

        #endregion
    }
}
#endif