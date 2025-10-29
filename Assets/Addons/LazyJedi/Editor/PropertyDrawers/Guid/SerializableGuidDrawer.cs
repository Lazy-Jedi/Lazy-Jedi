using LazyJedi.Guid;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LazyJedi.Editors.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(SerializableGuid))]
    public class SerializableGuidDrawer : PropertyDrawer
    {
        private const string GuidPropertyName = "_guidString";
        private const string FallbackPropertyName = "GuidString";

        private static readonly Texture2D RefreshIcon = EditorGUIUtility.IconContent("Refresh").image as Texture2D;
        private static readonly Texture2D DeleteIcon = EditorGUIUtility.IconContent("TreeEditor.Trash").image as Texture2D;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
        }
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Always work with local variables (not class fields!)
            SerializedProperty guidProperty = property.FindPropertyRelative(GuidPropertyName)
                                              ?? property.FindPropertyRelative(FallbackPropertyName);

            if (guidProperty == null)
            {
                Debug.LogError($"❌ Could not find GuidString field for property {property.displayName}");
                return new Label("Missing GUID field");
            }

            // Ensure serialized object is updated with the latest values
            property.serializedObject.UpdateIfRequiredOrScript();

            // Auto-initialize empty GUIDs
            if (string.IsNullOrEmpty(guidProperty.stringValue))
            {
                guidProperty.stringValue = SerializableGuid.EmptyGuidString();
                property.serializedObject.ApplyModifiedProperties();
            }

            // --- Root container ---
            VisualElement root = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    alignItems = Align.Center,
                    marginBottom = 2
                }
            };

            // Label
            Label label = new Label(property.displayName)
            {
                style =
                {
                    minWidth = 120,
                    marginRight = 6,
                    unityFontStyleAndWeight = FontStyle.Bold
                }
            };
            root.Add(label);

            // GUID field (auto-bound)
            PropertyField guidField = new PropertyField(guidProperty)
            {
                label = string.Empty,
                enabledSelf = false,
                style =
                {
                    flexGrow = 1,
                    marginRight = 6,
                }
            };
            // guidField.SetEnabled(false);
            root.Add(guidField);

            // Buttons
            root.Add(CreateIconButton(RefreshIcon, "Regenerate GUID", () =>
            {
                if (guidProperty.stringValue != SerializableGuid.EmptyGuidString() &&
                    !EditorUtility.DisplayDialog("Regenerate GUID", "Are you sure you want to regenerate the GUID?", "Yes", "No"))
                {
                    return;
                }

                guidProperty.serializedObject.Update();
                guidProperty.stringValue = SerializableGuid.NewGuidString();
                guidProperty.serializedObject.ApplyModifiedProperties();
            }));

            root.Add(CreateIconButton(DeleteIcon, "Empty GUID", () =>
            {
                guidProperty.serializedObject.Update();
                guidProperty.stringValue = SerializableGuid.EmptyGuidString();
                guidProperty.serializedObject.ApplyModifiedProperties();
            }));

            // Auto-refresh field when value changes (after loading, etc.)
            guidField.BindProperty(guidProperty);

            return root;
        }

        /// <summary>
        /// Creates a small icon button for the inspector toolbar row.
        /// </summary>
        private static Button CreateIconButton(Texture2D icon, string tooltip, System.Action onClick)
        {
            Button button = new Button(onClick)
            {
                tooltip = tooltip,
            };

            button.Add(new Image
            {
                image = icon,
                style = { width = 16, height = 16 }
            });

            return button;
        }
    }
}