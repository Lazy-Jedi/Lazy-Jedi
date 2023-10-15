using System.Collections;
using System.Collections.Generic;
using LazyJedi.Utility;
using UnityEngine;

namespace LazyJedi.Components.UI
{
    public class UIFader : MonoBehaviour
    {
        #region FIELDS

        [Header("Fader Properties")]
        public bool UseCanvasGroup = true;

        [Header("Fade Components")]
        public CanvasGroup CanvasGroup;
        public List<CanvasRenderer> UIElements = new List<CanvasRenderer>();

        [Header("Fade Properties")]
        public bool UseEaseType = true;
        public EaseType EaseType = EaseType.Linear;
        public AnimationCurve AnimationCurve;
        public float FadeDuration = 1f;

        private float _currentAlpha = 1f;
        private Coroutine _fadeRoutine;

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            FadeOut();
        }

        #endregion

        #region METHODS

        public void FadeIn()
        {
            if (_fadeRoutine != null)
            {
                StopCoroutine(_fadeRoutine);
            }

            print("FadeIn");
            _fadeRoutine = StartCoroutine(UseCanvasGroup ? FadeCanvasGroup(CanvasGroup.alpha, 1f) : FadeUIElements(_currentAlpha, 1f));
        }

        public void FadeOut()
        {
            if (_fadeRoutine != null)
            {
                StopCoroutine(_fadeRoutine);
            }

            print("FadeOut");
            _fadeRoutine = StartCoroutine(UseCanvasGroup ? FadeCanvasGroup(CanvasGroup.alpha, 0f) : FadeUIElements(_currentAlpha, 0f));
        }

        #endregion

        #region FADE METHODS

        private IEnumerator FadeCanvasGroup(float from, float to)
        {
            float elapsedTime = 0f;
            while (elapsedTime < FadeDuration)
            {
                CanvasGroup.alpha = ApplyEasingHelper(from, to, elapsedTime);
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            CanvasGroup.alpha = to;
        }

        private IEnumerator FadeUIElements(float from, float to)
        {
            float elapsedTime = 0f;
            while (elapsedTime < FadeDuration)
            {
                foreach (CanvasRenderer element in UIElements)
                {
                    _currentAlpha = ApplyEasingHelper(from, to, elapsedTime / FadeDuration);
                    element.SetAlpha(_currentAlpha);
                }

                yield return null;
                elapsedTime += Time.deltaTime;
            }

            foreach (CanvasRenderer uiElement in UIElements)
            {
                uiElement.SetAlpha(to);
            }
        }

        #endregion

        #region HELPER METHODS

        private float ApplyEasingHelper(float from, float to, float elapsedTime)
        {
            return UseEaseType
                ? EaseUtility.Float(EaseType, from, to, elapsedTime / FadeDuration)
                : EaseUtility.Float(from, to, elapsedTime / FadeDuration, AnimationCurve);
        }

        #endregion
    }
}