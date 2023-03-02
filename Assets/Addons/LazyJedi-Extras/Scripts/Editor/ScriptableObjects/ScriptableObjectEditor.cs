#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LazyJedi.Editors.Internal;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace LazyJedi.Editors.ScriptableObjects
{
    [CustomEditor(typeof(ScriptableObject), true)]
    public class ScriptableObjectEditor : Editor
    {
        #region VARIABLES

        [SerializeField]
        private VisualTreeAsset _design;

        private string outPath = string.Empty;
        private string inPath = string.Empty;

        private ScriptableObject _target;
        private bool _showOverwriteWarning = false;

        #endregion

        #region PROPERTIES

        private string TemporaryPath
        {
            get
            {
                string path = new ProjectSetup().LoadSettings().TemporaryFolder;
                path = Path.Combine(string.IsNullOrEmpty(path) ? LazyStrings.DEFAULT_TEMPORARY_PATH : path, "Json");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        #endregion

        #region UNITY METHODS

        private VisualElement _root;

        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement();
            _design.CloneTree(_root);

            VisualElement defaultInspector = _root.Q("Default_Inspector");
            InspectorElement.FillDefaultInspector(defaultInspector, serializedObject, this);
            _target = (ScriptableObject)serializedObject.targetObject;

            Button btnAssign = _root.Q<Button>("btnAssign");
            btnAssign.RegisterCallback<MouseUpEvent>(OnAssignSelfButton_Clicked);

            Button btnOpen = _root.Q<Button>("btnOpen");
            btnOpen.RegisterCallback<MouseUpEvent>(OnOpenButton_Clicked);

            Button btnSave = _root.Q<Button>("btnSave");
            btnSave.RegisterCallback<MouseUpEvent>(OnSaveButton_Clicked);

            Button btnSaveTo = _root.Q<Button>("btnSaveTo");
            btnSaveTo.RegisterCallback<MouseUpEvent>(OnSaveToButton_Clicked);

            Button btnLoad = _root.Q<Button>("btnLoad");
            btnLoad.RegisterCallback<MouseUpEvent>(OnLoadButton_Clicked);

            Button btnLoadFrom = _root.Q<Button>("btnLoadFrom");
            btnLoadFrom.RegisterCallback<MouseUpEvent>(OnLoadFromButton_Clicked);

            Toggle tglOverwrite = _root.Q<Toggle>("tglOverwrite");
            tglOverwrite.RegisterValueChangedCallback(OnOverwriteToggle_Click);
            _showOverwriteWarning = tglOverwrite.value;

            return _root;
        }

        #endregion

        #region UI EVENT METHODS

        private void OnOverwriteToggle_Click(ChangeEvent<bool> evt)
        {
            _showOverwriteWarning = evt.newValue;
        }

        private void OnAssignSelfButton_Clicked(MouseUpEvent evt)
        {
            AssignSelf();
        }

        private void OnOpenButton_Clicked(MouseUpEvent evt)
        {
            EditorUtility.RevealInFinder(TemporaryPath);
        }

        private void OnSaveButton_Clicked(MouseUpEvent evt)
        {
            Serialize();
        }

        private void OnSaveToButton_Clicked(MouseUpEvent evt)
        {
            SerializeToFile();
        }

        private void OnLoadButton_Clicked(MouseUpEvent evt)
        {
            Deserialize();
        }

        private void OnLoadFromButton_Clicked(MouseUpEvent evt)
        {
            DeserializeFromFile();
        }

        #endregion

        #region METHODS

        private void AssignSelf()
        {
            List<GameObject> rootObjects = new List<GameObject>();
            int sceneCount = EditorSceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                EditorSceneManager.GetSceneAt(i).GetRootGameObjects(rootObjects);
                foreach (GameObject rootObject in rootObjects)
                {
                    foreach (MonoBehaviour behaviour in rootObject.GetComponentsInChildren<MonoBehaviour>(true))
                    {
                        foreach (FieldInfo field in behaviour.GetType().GetFields())
                        {
                            if (field.FieldType.IsInstanceOfType(_target))
                            {
                                field.SetValue(behaviour, _target);
                            }
                        }
                    }
                }

                EditorSceneManager.MarkAllScenesDirty();
                EditorUtility.SetDirty(_target);
            }
        }

        private void Serialize()
        {
            if (string.IsNullOrEmpty(outPath)) outPath = TemporaryPath;
            if (!outPath.Contains(".json"))
            {
                if (!Directory.Exists(outPath)) Directory.CreateDirectory(outPath);
                outPath = Path.Combine(outPath, $"{target.name}.json");
            }

            if (File.Exists(outPath) && _showOverwriteWarning)
            {
                if (!EditorUtility.DisplayDialog($"Overwrite {_target.name}.json?"
                                               , $"Are you sure you want to overwrite the save file {_target.name}.json?"
                                               , "Yes"
                                               , "No"))
                    return;
            }

            string json = JsonUtility.ToJson(_target, true);
            File.WriteAllText(outPath, json);
            Debug.unityLogger.Log($"Serialized - {target.name} to {outPath}");
            AssetDatabase.Refresh();
        }

        private void SerializeToFile()
        {
            outPath = EditorUtility.SaveFilePanel($"Save {_target.name}", TemporaryPath, _target.name, "json");
            if (string.IsNullOrEmpty(outPath)) return;
            Serialize();
        }

        private void Deserialize()
        {
            if (string.IsNullOrEmpty(inPath)) inPath = Path.Combine(TemporaryPath, $"{_target.name}.json");
            if (!File.Exists(inPath))
            {
                Debug.unityLogger.LogWarning("", $"The file @ {inPath} does not exist.");
                return;
            }

            string json = File.ReadAllText(inPath);
            JsonUtility.FromJsonOverwrite(json, _target);
            EditorUtility.SetDirty(_target);
            Debug.unityLogger.Log($"Deserialized - {target.name} from {inPath}");
        }

        private void DeserializeFromFile()
        {
            inPath = EditorUtility.OpenFilePanel($"Load {_target.name}", TemporaryPath, "json");
            Deserialize();
        }

        #endregion
    }
#endif
}