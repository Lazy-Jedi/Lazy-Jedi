using LazyJedi.Guid;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LazyJedi.Editors.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(SerializableGuid))]
    public class SerializableGuidDrawer : PropertyDrawer
    {
        #region FIELDS

        private Texture2D _refreshIcon;

        #endregion

        #region UNITY METHODS

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Load the refresh icon
            _refreshIcon = Resources.Load("Icons/Material/refresh") as Texture2D;

            // Reference to the hidden GUID field
            SerializedProperty guidProperty = property.FindPropertyRelative("_guid");

            // Container for the property elements
            VisualElement container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            container.style.alignItems = Align.Center;

            // Readonly TextField to display the GUID
            TextField guidTextField = new TextField($"{property.displayName}:")
            {
                value = guidProperty.stringValue,
                isReadOnly = true, // Prevent user editing
                style =
                {
                    flexGrow = 1,
                    marginRight = 5 // Add some spacing
                }
            };
            container.Add(guidTextField);

            // Button to regenerate the GUID
            Button regenerateButton = new Button(() =>
            {
                guidProperty.stringValue = System.Guid.NewGuid().ToString();
                guidTextField.value = guidProperty.stringValue;
                guidProperty.serializedObject.ApplyModifiedProperties();
            });

            // Add refresh icon to the button
            Image refreshIcon = new Image
            {
                image = _refreshIcon,
                style =
                {
                    width = 16,
                    height = 16
                }
            };
            regenerateButton.Add(refreshIcon);

            container.Add(regenerateButton);

            // Ensure the GUID is initialized if empty
            if (!string.IsNullOrEmpty(guidProperty.stringValue)) return container;

            guidProperty.stringValue = System.Guid.NewGuid().ToString();
            guidProperty.serializedObject.ApplyModifiedProperties();

            return container;
        }

        #endregion
    }
}