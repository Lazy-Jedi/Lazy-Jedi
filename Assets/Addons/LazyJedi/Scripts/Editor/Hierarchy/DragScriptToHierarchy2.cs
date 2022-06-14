using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LazyJedi.Editors.Hierarchy
{
    // [InitializeOnLoad]
    public static class DragScriptToHierarchy2
    {
        #region CONSTRUCTOR

        static DragScriptToHierarchy2()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyDrag;
        }

        #endregion

        #region METHODS

        private static void HierarchyDrag(int instanceID, Rect selectionRect)
        {
            if (Event.current.type != EventType.DragExited && Event.current.type != EventType.DragUpdated) return;
            
            if (Event.current.type == EventType.DragUpdated)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            }
            else
            {
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
            }

            Event.current.Use();
        }

        private static async void SelectOnDelay(GameObject gameObject)
        {
            await Task.Delay(10);
            Selection.activeGameObject = gameObject;
        }

        #endregion
    }
}