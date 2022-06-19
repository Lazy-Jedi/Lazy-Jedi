#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
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

        private ScriptableObject _scriptableObject;

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            _scriptableObject = target as ScriptableObject;
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

            DrawDefaultInspector();
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
                            if (field.FieldType.IsInstanceOfType(_scriptableObject))
                            {
                                field.SetValue(behaviour, _scriptableObject);
                            }
                        }
                    }
                }

                EditorSceneManager.MarkAllScenesDirty();
                EditorUtility.SetDirty(_scriptableObject);
            }
        }

        #endregion
    }
#endif
}