#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace LazyJedi.Editors.ScriptableObjects
{
    [CustomEditor(typeof(ScriptableObject), true)]
    public class ScriptableObjectEditor : Editor
    {
        #region VARIABLES

        private ScriptableObject _target;

        private bool _showOverwriteWarning = false;

        private GUIContent _saveBtnContent;
        private GUIContent _saveToBtnContent;
        private GUIContent _loadBtnContent;
        private GUIContent _loadFromBtnContent;

        #endregion

        #region PROPERTIES

        private string StandardPath => Path.Combine(Application.dataPath, "Temporary", "json");

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            _target = target as ScriptableObject;

            _saveBtnContent     = new GUIContent("Save", $"Serialize {_target?.name ?? ""} to Temporary/json folder.");
            _saveToBtnContent   = new GUIContent("Save To...", $"Serialize {_target?.name ?? ""} to a folder of your choice.");
            _loadBtnContent     = new GUIContent("Load", $"Deserialize {_target?.name ?? ""} from Temporary/json folder.");
            _loadFromBtnContent = new GUIContent("Load From...", $"Deserialize {_target?.name ?? ""} from a file of your choice.");
        }

        public override void OnInspectorGUI()
        {
            using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Assign Self"))
                {
                    AssignSelf();
                }
            }

            _showOverwriteWarning = EditorGUILayout.ToggleLeft("Enable Overwrite Warning", _showOverwriteWarning);

            using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button(_saveBtnContent))
                {
                    Serialize();
                }

                if (GUILayout.Button(_saveToBtnContent))
                {
                    SerializeToFile();
                }

                if (GUILayout.Button(_loadBtnContent))
                {
                    Deserialize();
                }

                if (GUILayout.Button(_loadFromBtnContent))
                {
                    DeserializeFromFile();
                }
            }

            DrawDefaultInspector();
        }

        #endregion

        #region METHODS

        private void AssignSelf()
        {
            List<GameObject> rootObjects = new List<GameObject>();
            int              sceneCount  = EditorSceneManager.sceneCount;

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

        private void Serialize(string outPath = "")
        {
            if (string.IsNullOrEmpty(outPath)) outPath = StandardPath;
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
            string outPath = EditorUtility.SaveFilePanel($"Save {_target.name}", Application.dataPath, _target.name, "json");
            Serialize(outPath);
        }


        private void Deserialize(string inPath = "")
        {
            if (string.IsNullOrEmpty(inPath)) inPath = Path.Combine(StandardPath, $"{_target.name}.json");
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
            string inPath = EditorUtility.OpenFilePanel($"Load {_target.name}", Application.dataPath, "json");
            Deserialize(inPath);
        }

        #endregion
    }
#endif
}