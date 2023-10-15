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

        [Header("Colour")]
        public SpriteRenderer SpriteRenderer;
        public Color StartColor = Color.white;
        public Color EndColor = Color.black;

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
            Vector3 target = Target.position;
            float startY = position.y;
            float targetY = target.y;

            while (time < Duration)
            {
                // Change the position of the square by updating the y value
                position.y = EaseUtility.Float(EaseType, startY, targetY, time / Duration, Period, Amplitude);
                // position.y = EaseUtility.Float(startY, targetY, time / Duration, AnimationCurve);
                // position.y = startY + (targetY - startY) * EaseUtility.Evaluate(EaseType, time / Duration, Period, Amplitude);
                Square.position = position;

                // Change the colour of the square by updating the colour value
                // SpriteRenderer.color = EaseUtility.Color(EaseType, StartColor, EndColor, time / Duration, Period, Amplitude);
                SpriteRenderer.color = EaseUtility.Color(StartColor, EndColor, time / Duration, AnimationCurve);

                // Change the Vector3 position of the square by updating the Vector3 value
                // Square.position = EaseUtility.Vector3(EaseType, position, target, time / Duration, Period, Amplitude);
                // Square.position = EaseUtility.Vector3(position, target, time / Duration, AnimationCurve);

                yield return null;
                time += Time.deltaTime;
            }

            Square.position = Target.position;
            yield return null;
        }

        #endregion
    }
}