/*
 * Created By: BluMalice
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LazyJedi.Editors.Hierarchy
{
    [InitializeOnLoad]
    public static class DragScriptToHierarchy
    {
        #region VARIABLES

        private static int _delayBetweenRegisters = 5000;
        private static Type _hierarchyType = null;

        #endregion

        #region CONSTRUCTOR

        static DragScriptToHierarchy()
        {
            RegisterHierarchyWindows();
        }

        #endregion

        #region METHODS

        private static async void RegisterHierarchyWindows()
        {
            await Task.Delay(_delayBetweenRegisters);

            List<EditorWindow> hierarchies = new List<EditorWindow>();

            foreach (EditorWindow window in Resources.FindObjectsOfTypeAll<EditorWindow>())
            {
                if (window.GetType().Name == "SceneHierarchyWindow")
                {
                    hierarchies.Add(window);

                    if (_hierarchyType.IsNull())
                    {
                        _hierarchyType = window.GetType();
                    }
                }
            }

            foreach (EditorWindow hierarchy in hierarchies)
            {
                FieldInfo sceneHierarchyField = _hierarchyType.GetField("m_SceneHierarchy", BindingFlags.Instance | BindingFlags.NonPublic);
                object sceneHierarchy = sceneHierarchyField.GetValue(hierarchy);
                Type sceneHierarchyType = sceneHierarchy.GetType();

                PropertyInfo treeViewProperty = sceneHierarchyType.GetProperty("treeView", BindingFlags.Instance | BindingFlags.NonPublic);
                object treeView = treeViewProperty.GetValue(sceneHierarchy);
                Type treeViewType = treeView.GetType();

                PropertyInfo keyboardInputCallbackProperty = treeViewType.GetProperty("keyboardInputCallback", BindingFlags.Instance | BindingFlags.Public);
                Action keyboardInputCallback = (Action)keyboardInputCallbackProperty.GetValue(treeView);

                if (keyboardInputCallback != null)
                {
                    foreach (Delegate callback in keyboardInputCallback.GetInvocationList())
                    {
                        if (callback.Method.Name.Equals(nameof(RegisteredIndicator)))
                        {
                            goto ContinueRegistration;
                        }
                    }
                }

                keyboardInputCallback += RegisteredIndicator;
                keyboardInputCallback += () => HandleDragAndDropScript(hierarchy);
                keyboardInputCallbackProperty.SetValue(treeView, keyboardInputCallback);

                ContinueRegistration: ;
            }

            RegisterHierarchyWindows();
        }

        private static void RegisteredIndicator()
        {
        }

        private static void HandleDragAndDropScript(EditorWindow hierarchy)
        {
            switch (Event.current.type)
            {
                case EventType.DragExited:
                    if (!hierarchy.position.Contains(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)))
                    {
                        break;
                    }

                    PropertyInfo hoveredObjectProperty = _hierarchyType.GetProperty("hoveredObject", BindingFlags.Instance | BindingFlags.Public);
                    object hoveredObject = hoveredObjectProperty.GetValue(hierarchy);

                    if (hoveredObject != null)
                    {
                        break;
                    }

                    GameObject selection = null;

                    foreach (Object reference in DragAndDrop.objectReferences)
                    {
                        if (reference is not MonoScript script) continue;
                        Type scriptClass = script.GetClass();
                        if (scriptClass == null || !scriptClass.IsSubclassOf(typeof(MonoBehaviour)) || scriptClass.IsAbstract) continue;
                        Component newObject = new GameObject($"New {ObjectNames.NicifyVariableName(script.name)}").AddComponent(scriptClass);
                        Debug.Log($"Dragged Script - {script.name}");

                        if (selection.IsNull())
                        {
                            selection = newObject.gameObject;
                        }
                    }

                    SelectOnDelay(selection);
                    break;
            }
        }

        private static async void SelectOnDelay(GameObject gameObject)
        {
            await Task.Delay(10);
            Selection.activeGameObject = gameObject;
        }

        #endregion
    }
}