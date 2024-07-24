#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif
using System;
using UnityEngine.UIElements;

namespace LazyJedi.Utilities
{
    public static class UIToolkitUtility
    {
        #region METHODS

        public static VisualElement CreateContainer(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<VisualElement>(parent, styleSheet, classNames);
        }

        public static Button CreateButton(string label, Action onClick = null,
            VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            Button button = CreateElement<Button>(parent, styleSheet, classNames);
            button.text = label;
            button.clickable.clicked += onClick;
            return button;
        }

        public static Label CreateLabel(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            Label labelElement = CreateElement<Label>(parent, styleSheet, classNames);
            labelElement.text = label;
            return labelElement;
        }

        public static TextField CreateTextField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            TextField textField = CreateElement<TextField>(parent, styleSheet, classNames);
            textField.label = label;
            return textField;
        }

        public static IntegerField CreateIntegerField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            IntegerField integerField = CreateElement<IntegerField>(parent, styleSheet, classNames);
            integerField.label = label;
            return integerField;
        }

        public static FloatField CreateFloatField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            FloatField floatField = CreateElement<FloatField>(parent, styleSheet, classNames);
            floatField.label = label;
            return floatField;
        }

        public static DoubleField CreateDoubleField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            DoubleField doubleField = CreateElement<DoubleField>(parent, styleSheet, classNames);
            doubleField.label = label;
            return doubleField;
        }

        public static LongField CreateLongField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            LongField longField = CreateElement<LongField>(parent, styleSheet, classNames);
            longField.label = label;
            return longField;
        }

        public static Hash128Field CreateHash128Field(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            Hash128Field hash128Field = CreateElement<Hash128Field>(parent, styleSheet, classNames);
            hash128Field.label = label;
            return hash128Field;
        }

        public static Vector2Field CreateVector2Field(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            Vector2Field vector2Field = CreateElement<Vector2Field>(parent, styleSheet, classNames);
            vector2Field.label = label;
            return vector2Field;
        }

        public static Vector3Field CreateVector3Field(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            Vector3Field vector3Field = CreateElement<Vector3Field>(parent, styleSheet, classNames);
            vector3Field.label = label;
            return vector3Field;
        }

        public static Vector4Field CreateVector4Field(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            Vector4Field vector4Field = CreateElement<Vector4Field>(parent, styleSheet, classNames);
            vector4Field.label = label;
            return vector4Field;
        }

        public static RectField CreateRectField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            RectField rectField = CreateElement<RectField>(parent, styleSheet, classNames);
            rectField.label = label;
            return rectField;
        }

        public static BoundsField CreateBoundsField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            BoundsField boundsField = CreateElement<BoundsField>(parent, styleSheet, classNames);
            boundsField.label = label;
            return boundsField;
        }

        public static UnsignedIntegerField CreateUnsignedIntegerField(string label, VisualElement parent = null, StyleSheet styleSheet = null,
            params string[] classNames)
        {
            UnsignedIntegerField unsignedIntegerField = CreateElement<UnsignedIntegerField>(parent, styleSheet, classNames);
            unsignedIntegerField.label = label;
            return unsignedIntegerField;
        }

        public static UnsignedLongField CreateUnsignedLongField(string label, VisualElement parent = null, StyleSheet styleSheet = null,
            params string[] classNames)
        {
            UnsignedLongField unsignedLongField = CreateElement<UnsignedLongField>(parent, styleSheet, classNames);
            unsignedLongField.label = label;
            return unsignedLongField;
        }

        public static Vector2IntField CreateVector2IntField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            Vector2IntField vector2IntField = CreateElement<Vector2IntField>(parent, styleSheet, classNames);
            vector2IntField.label = label;
            return vector2IntField;
        }

        public static Vector3IntField CreateVector3IntField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            Vector3IntField vector3IntField = CreateElement<Vector3IntField>(parent, styleSheet, classNames);
            vector3IntField.label = label;
            return vector3IntField;
        }

        public static RectIntField CreateRectIntField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            RectIntField rectIntField = CreateElement<RectIntField>(parent, styleSheet, classNames);
            rectIntField.label = label;
            return rectIntField;
        }

        public static BoundsIntField CreateBoundsIntField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            BoundsIntField boundsIntField = CreateElement<BoundsIntField>(parent, styleSheet, classNames);
            boundsIntField.label = label;
            return boundsIntField;
        }

        public static Slider CreateSlider(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            Slider slider = CreateElement<Slider>(parent, styleSheet, classNames);
            slider.label = label;
            return slider;
        }

        public static SliderInt CreateSliderInt(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            SliderInt slider = CreateElement<SliderInt>(parent, styleSheet, classNames);
            slider.label = label;
            return slider;
        }

        public static Toggle CreateToggle(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            Toggle toggle = CreateElement<Toggle>(parent, styleSheet, classNames);
            toggle.text = label;
            return toggle;
        }

        public static RadioButton CreateRadioButton(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            RadioButton radioButton = CreateElement<RadioButton>(parent, styleSheet, classNames);
            radioButton.text = label;
            return radioButton;
        }

        public static MinMaxSlider CreateMinMaxSlider(string label, float minValue, float maxValue,
            VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            MinMaxSlider minMaxSlider = CreateElement<MinMaxSlider>(parent, styleSheet, classNames);
            minMaxSlider.minValue = minValue;
            minMaxSlider.maxValue = maxValue;
            minMaxSlider.label = label;
            return minMaxSlider;
        }

        public static Foldout CreateFoldout(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            Foldout foldout = CreateElement<Foldout>(parent, styleSheet, classNames);
            foldout.text = label;
            return foldout;
        }

        public static ProgressBar CreateProgressBar(string title, float lowValue, float highValue,
            VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            ProgressBar progressBar = CreateElement<ProgressBar>(parent, styleSheet, classNames);
            progressBar.title = title;
            progressBar.lowValue = lowValue;
            progressBar.highValue = highValue;
            return progressBar;
        }

        public static EnumField CreateEnumField(string label, Enum enumValue, VisualElement parent = null, StyleSheet styleSheet = null,
            params string[] classNames)
        {
            EnumField enumField = CreateElement<EnumField>(parent, styleSheet, classNames);
            enumField.label = label;
            enumField.Init(enumValue);
            return enumField;
        }

        #endregion

        #region VIEW METHODS

        public static ScrollView CreateScrollView(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<ScrollView>(parent, styleSheet, classNames);
        }

        public static ListView CreateListView(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<ListView>(parent, styleSheet, classNames);
        }

        public static TreeView CreateTreeView(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<TreeView>(parent, styleSheet, classNames);
        }

        public static MultiColumnListView CreateMultiColumnListView(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<MultiColumnListView>(parent, styleSheet, classNames);
        }

        public static MultiColumnTreeView CreateMultiColumnTreeView(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<MultiColumnTreeView>(parent, styleSheet, classNames);
        }

        public static GroupBox CreateGroupBox(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            GroupBox groupBox = CreateElement<GroupBox>(parent, styleSheet, classNames);
            groupBox.text = label;
            return groupBox;
        }

        #endregion

#if UNITY_EDITOR

        #region CHOICE FIELD METHODS

        public static TagField CreateTagField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            TagField tagField = CreateElement<TagField>(parent, styleSheet, classNames);
            tagField.label = label;
            return tagField;
        }

        public static MaskField CreateMaskField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            MaskField maskField = CreateElement<MaskField>(parent, styleSheet, classNames);
            maskField.label = label;
            return maskField;
        }

        public static LayerField CreateLayerField(string label, VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            LayerField layerField = CreateElement<LayerField>(parent, styleSheet, classNames);
            layerField.label = label;
            return layerField;
        }

        public static EnumFlagsField CreateEnumFlagsField(string label, Enum enumValue,
            VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            EnumFlagsField enumFlagsField = CreateElement<EnumFlagsField>(parent, styleSheet, classNames);
            enumFlagsField.Init(enumValue);
            enumFlagsField.label = label;
            return enumFlagsField;
        }

        #endregion

        #region OBJECT FIELD METHODS

        public static ObjectField CreateObjectField(string label, bool allowSceneObjects, Type objectType,
            VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            ObjectField objectField = CreateElement<ObjectField>(parent, styleSheet, classNames);
            objectField.label = label;
            objectField.objectType = objectType;
            objectField.allowSceneObjects = allowSceneObjects;
            return objectField;
        }

        public static PropertyField CreatePropertyField(string label, SerializedProperty property,
            VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            PropertyField propertyField = CreateElement<PropertyField>(parent, styleSheet, classNames);
            propertyField.label = label;
            propertyField.BindProperty(property);
            return propertyField;
        }

        #endregion

        #region TOOLBAR METHODS

        public static Toolbar CreateToolbar(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<Toolbar>(parent, styleSheet, classNames);
        }

        public static ToolbarMenu CreateToolbarMenu(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<ToolbarMenu>(parent, styleSheet, classNames);
        }

        public static ToolbarButton CreateToolbarButton(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<ToolbarButton>(parent, styleSheet, classNames);
        }

        public static ToolbarSearchField CreateToolbarSearchField(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<ToolbarSearchField>(parent, styleSheet, classNames);
        }

        public static ToolbarSpacer CreateToolbarSpacer(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<ToolbarSpacer>(parent, styleSheet, classNames);
        }

        public static ToolbarToggle CreateToolbarToggle(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<ToolbarToggle>(parent, styleSheet, classNames);
        }

        public static ToolbarPopupSearchField CreateToolbarPopup(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames)
        {
            return CreateElement<ToolbarPopupSearchField>(parent, styleSheet, classNames);
        }

        #endregion

#endif

        #region HELPER METHODS

        /// <summary>
        /// Create a UI Toolkit Visual Element such as Button, Label, TextField, etc.
        /// </summary>
        /// <param name="parent"> Parent element to add the new element to </param>
        /// <param name="styleSheet"> Optional StyleSheet to apply to the new element </param>
        /// <param name="classNames"> Optional class names to apply to the new element </param>
        /// <typeparam name="T"> Type of Visual Element to create </typeparam>
        /// <returns> The new Visual Element </returns>
        public static T CreateElement<T>(VisualElement parent = null, StyleSheet styleSheet = null, params string[] classNames) where T : VisualElement, new()
        {
            T element = new T();

            if (styleSheet)
            {
                element.styleSheets.Add(styleSheet);
            }

            foreach (string className in classNames)
            {
                element.AddToClassList(className);
            }

            parent?.Add(element);
            return element;
        }

        #endregion
    }
}