using System.Collections;
using LazyJedi.Utility;
using UnityEngine;

namespace LazyJedi.Examples.Easing
{
    public class EaseExample : MonoBehaviour
    {
        #region FIELDS

        [Header("Object and Target")]
        public Transform Square;
        public Transform Target;

        [Header("Ease Settings")]
        public EaseType EaseType = EaseType.Linear;
        public AnimationCurve AnimationCurve;
        public float Duration = 5f;

        [Header("Period and Amplitude")]
        [Range(0.3f, 100f)]
        public float Period = .3f;
        [Range(1f, 100f)]
        public float Amplitude = 1f;

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            StartCoroutine(TestEase());
        }

        #endregion

        #region METHODS

        private IEnumerator TestEase()
        {
            float time = 0f;
            Vector3 position = Square.position;
            float startY = position.y;
            float targetY = Target.position.y;

            while (time < Duration)
            {
                position.y = startY + (targetY - startY) * EaseUtility.Evaluate(EaseType, time / Duration, Period, Amplitude);
                // position.y = EaseUtility.Float(EaseType, startY, targetY, time / Duration);
                // position.y = EaseUtility.Float(startY, targetY, time / Duration, AnimationCurve);
                Square.position = position;
                yield return null;
                time += Time.deltaTime;
            }

            Square.position = Target.position;

            yield return null;
        }

        #endregion
    }
}