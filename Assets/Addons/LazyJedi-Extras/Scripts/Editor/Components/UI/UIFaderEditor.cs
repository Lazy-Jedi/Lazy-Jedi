#if UNITY_EDITOR
using LazyJedi.Components.UI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LazyJedi.Editors.Common.Components
{
    [CustomEditor(typeof(UIFader))]
    public class UIFaderEditor : Editor
    {
        #region FIELDS

        public VisualTreeAsset _design;
        private VisualElement _root;

        private Toggle _useCanvasGroup;
        private ObjectField _canvasGroup;
        private PropertyField _uiElements;
        private Toggle _useEaseType;
        private EnumField _easeType;
        private PropertyField _animationCurve;

        #endregion

        #region UNITY METHODS

        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement();
            _design.CloneTree(_root);

            _useCanvasGroup = _root.Q<Toggle>("tglUseCanvasGroup");
            _canvasGroup = _root.Q<ObjectField>("objCanvasGroup");
            _uiElements = _root.Q<PropertyField>("pfUIElements");
            _useEaseType = _root.Q<Toggle>("tglUseEaseType");
            _easeType = _root.Q<EnumField>("efEaseType");
            _animationCurve = _root.Q<PropertyField>("pfAnimationCurve");

            _useCanvasGroup.RegisterValueChangedCallback(evt =>
            {
                _canvasGroup.SetEnabled(evt.newValue);
                _canvasGroup.style.display = evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
                _uiElements.SetEnabled(!evt.newValue);
                _uiElements.style.display = !evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
            });

            _useEaseType.RegisterValueChangedCallback(evt =>
            {
                _easeType.SetEnabled(evt.newValue);
                _easeType.style.display = evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
                _animationCurve.SetEnabled(!evt.newValue);
                _animationCurve.style.display = !evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
            });


            EditorUtility.SetDirty(this);
            return _root;
        }

        #endregion
    }
}
#endif