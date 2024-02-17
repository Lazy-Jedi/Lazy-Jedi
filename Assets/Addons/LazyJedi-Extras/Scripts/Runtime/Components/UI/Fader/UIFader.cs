using System;
using System.Collections;
using System.Collections.Generic;
using LazyJedi.Utilities;
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

        [Header("Fade On Start")]
        public bool FadeOnStart = false;
        public FadeType FadeOnStartType = FadeType.FadeIn;

        [Header("Fade Properties")]
        public bool UseEaseType = true;
        public EaseType FadeInEaseType = EaseType.Linear;
        public EaseType FadeOutEaseType = EaseType.Linear;
        public AnimationCurve FadeInAnimationCurve;
        public AnimationCurve FadeOutAnimationCurve;
        public float FadeInDuration = 1f;
        public float FadeOutDuration = 1f;

        private float _currentAlpha = 1f;
        private Coroutine _fadeRoutine;

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            DoOnStart();
        }

        #endregion

        #region METHODS

        public void FadeInNow()
        {
            FadeNow(1f);
        }

        public void FadeOutNow()
        {
            FadeNow(0f);
        }

        public void FadeIn()
        {
            if (_fadeRoutine != null)
            {
                StopCoroutine(_fadeRoutine);
            }

            print("FadeIn");
            _fadeRoutine = StartCoroutine(UseCanvasGroup
                ? FadeCanvasGroup(FadeInEaseType, CanvasGroup.alpha, 1f, FadeInDuration, FadeInAnimationCurve)
                : FadeUIElements(FadeInEaseType, _currentAlpha, 1f, FadeInDuration, FadeInAnimationCurve));
        }

        public void FadeOut()
        {
            if (_fadeRoutine != null)
            {
                StopCoroutine(_fadeRoutine);
            }

            print("FadeOut");
            _fadeRoutine = StartCoroutine(UseCanvasGroup
                ? FadeCanvasGroup(FadeOutEaseType, CanvasGroup.alpha, 0f, FadeOutDuration, FadeOutAnimationCurve)
                : FadeUIElements(FadeOutEaseType, _currentAlpha, 0f, FadeOutDuration, FadeOutAnimationCurve));
        }

        #endregion

        #region FADE METHODS

        private IEnumerator FadeCanvasGroup(EaseType easeType, float from, float to, float duration, AnimationCurve animationCurve)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                CanvasGroup.alpha = ApplyEasingHelper(easeType, from, to, elapsedTime / duration, animationCurve);
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            CanvasGroup.alpha = to;
        }

        private IEnumerator FadeUIElements(EaseType easeType, float from, float to, float duration, AnimationCurve animationCurve)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                foreach (CanvasRenderer element in UIElements)
                {
                    _currentAlpha = ApplyEasingHelper(easeType, from, to, elapsedTime / duration, animationCurve);
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

        private void FadeNow(float fadeValue)
        {
            if (UseCanvasGroup)
            {
                CanvasGroup.alpha = fadeValue;
                return;
            }

            foreach (CanvasRenderer element in UIElements)
            {
                element.SetAlpha(fadeValue);
            }
        }

        #endregion

        #region HELPER METHODS

        private float ApplyEasingHelper(EaseType easeType, float from, float to, float elapsedTime, AnimationCurve animationCurve)
        {
            return UseEaseType
                ? EaseUtility.Float(easeType, from, to, elapsedTime)
                : EaseUtility.Float(from, to, elapsedTime, animationCurve);
        }

        private void DoOnStart()
        {
            if (!FadeOnStart)
            {
                return;
            }

            switch (FadeOnStartType)
            {
                case FadeType.FadeIn:
                    FadeIn();
                    break;
                case FadeType.FadeOut:
                    FadeOut();
                    break;
                case FadeType.FadeInNow:
                    FadeInNow();
                    break;
                case FadeType.FadeOutNow:
                    FadeOutNow();
                    break;
            }
        }

        #endregion
    }
}