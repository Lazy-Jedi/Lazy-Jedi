#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.Hierarchy
{
    /// <summary>
    /// Allows you to group selected objects in the hierarchy to a single parent
    /// </summary>
    public static class GroupHierarchyObjects
    {
        #region METHODS

        // ToDo - Fix Shortcut
        [MenuItem("GameObject/Group Selected Objects", true)]
        private static bool GroupSelectedObjects()
        {
            return Selection.gameObjects?.Length > 1;
        }

        [MenuItem("GameObject/Group Selected Objects", false, 1)]
        private static void GroupSelectedObjects(MenuCommand menuCommand)
        {
            GroupSelectedObjectsHelper(menuCommand, true);
        }

        private static void GroupSelectedObjectsHelper(MenuCommand menuCommand, bool preventMultiSelect)
        {
            if (preventMultiSelect && menuCommand.context != Selection.objects[0])
            {
                return;
            }

            // Get the Selected Objects from the Scene
            GameObject[] selectedObjects = Selection.gameObjects;
            // Get the Length of the Selected Objects Array
            int length = selectedObjects.Length;

            // Get a Reference of the First Selected Object
            Transform firstObjectParent = selectedObjects[0].transform.parent;
            int index = selectedObjects[0].transform.GetSiblingIndex();

            // Create new Transform that the Selected Objects will be parented to
            Transform parent = new GameObject().transform;

            // Set the Parent of the Selected Objects to the New Parent you Created
            for (int i = 0; i < length; i++)
            {
                selectedObjects[i].transform.parent = parent;
            }

            // If the Selected Objects are Children of another, then set the Newly Created Parent as a Child of that Object
            if (firstObjectParent != null)
            {
                parent.SetParent(firstObjectParent);
                parent.SetSiblingIndex(index);
            }

            // Prepare the Parent Object to be Renamed 
            Selection.objects = new Object[] { parent.gameObject };

            // Execute the Renaming
            EditorApplication.update += EngageRenameMode;
        }

        private static void EngageRenameMode()
        {
            EditorApplication.update -= EngageRenameMode;
            EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
            EditorApplication.ExecuteMenuItem("Edit/Rename");
        }

        #endregion
    }
}
#endif