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
        private EnumField _fadeInEaseType;
        private EnumField _fadeOutEaseType;
        private PropertyField _FadeInAnimationCurve;
        private PropertyField _FadeOutAnimationCurve;

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
            _fadeInEaseType = _root.Q<EnumField>("efFadeInEaseType");
            _fadeOutEaseType = _root.Q<EnumField>("efFadeOutEaseType");
            _FadeInAnimationCurve = _root.Q<PropertyField>("pfFadeInAnimCurve");
            _FadeOutAnimationCurve = _root.Q<PropertyField>("pfFadeOutAnimCurve");

            _useCanvasGroup.RegisterValueChangedCallback(evt =>
            {
                _canvasGroup.SetEnabled(evt.newValue);
                _canvasGroup.style.display = evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
                _uiElements.SetEnabled(!evt.newValue);
                _uiElements.style.display = !evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
            });

            _useEaseType.RegisterValueChangedCallback(evt =>
            {
                _fadeInEaseType.SetEnabled(evt.newValue);
                _fadeInEaseType.style.display = evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
                
                _fadeOutEaseType.SetEnabled(evt.newValue);
                _fadeOutEaseType.style.display = evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
                
                _FadeInAnimationCurve.SetEnabled(!evt.newValue);
                _FadeInAnimationCurve.style.display = !evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
                
                _FadeOutAnimationCurve.SetEnabled(!evt.newValue);
                _FadeOutAnimationCurve.style.display = !evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
            });


            EditorUtility.SetDirty(this);
            return _root;
        }

        #endregion
    }
}
#endif