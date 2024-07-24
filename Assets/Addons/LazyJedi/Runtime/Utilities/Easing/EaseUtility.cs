using UnityEngine;

namespace LazyJedi.Utilities
{
    /// <summary>
    /// Original Author - Kryzarel
    /// Link - https://gist.github.com/Kryzarel/bba64622057f21a1d6d44879f9cd7bd4
    /// </summary>
    public static class EaseUtility
    {
        #region FIELDS

        private const float PI = Mathf.PI;
        private const float HALF_PI = PI * 0.5f;
        private const float C1 = 1.70158f;
        private const float C2 = C1 + 1;

        #endregion

        #region EVALUATE METHODS

        /// <summary>
        /// Ease a value using the given ease type.<br/>
        /// </summary>
        /// <param name="ease">EaseType</param>
        /// <param name="time">Time is clamped between 0 and 1, for e.g. Time / Duration</param>
        /// <param name="period">Period limits the number of bounces</param>
        /// <param name="amplitude"> Amplitude limits the height of the bounces.</param>
        public static float Evaluate(EaseType ease, float time, float period = 0.3f, float amplitude = 1f)
        {
            return ease switch
            {
                EaseType.Linear       => Linear(time),
                EaseType.InSine       => InSine(time),
                EaseType.OutSine      => OutSine(time),
                EaseType.InOutSine    => InOutSine(time),
                EaseType.InQuad       => InQuad(time),
                EaseType.OutQuad      => OutQuad(time),
                EaseType.InOutQuad    => InOutQuad(time),
                EaseType.InCubic      => InCubic(time),
                EaseType.OutCubic     => OutCubic(time),
                EaseType.InOutCubic   => InOutCubic(time),
                EaseType.InQuart      => InQuart(time),
                EaseType.OutQuart     => OutQuart(time),
                EaseType.InOutQuart   => InOutQuart(time),
                EaseType.InQuint      => InQuint(time),
                EaseType.OutQuint     => OutQuint(time),
                EaseType.InOutQuint   => InOutQuint(time),
                EaseType.InExpo       => InExpo(time),
                EaseType.OutExpo      => OutExpo(time),
                EaseType.InOutExpo    => InOutExpo(time),
                EaseType.InCirc       => InCirc(time),
                EaseType.OutCirc      => OutCirc(time),
                EaseType.InOutCirc    => InOutCirc(time),
                EaseType.InBack       => InBack(time),
                EaseType.OutBack      => OutBack(time),
                EaseType.InOutBack    => InOutBack(time),
                EaseType.InElastic    => InElastic(time, amplitude, period),
                EaseType.OutElastic   => OutElastic(time, amplitude, period),
                EaseType.InOutElastic => InOutElastic(time, amplitude, period),
                EaseType.InBounce     => InBounce(time),
                EaseType.OutBounce    => OutBounce(time),
                EaseType.InOutBounce  => InOutBounce(time),
                _                     => Linear(time)
            };
        }

        /// <summary>
        /// Ease a value using the given animation curve.<br/>
        /// </summary>
        /// <param name="time">Time is clamped between 0 and 1, for e.g. Time / Duration</param>
        /// <param name="curve">Given animation curve</param>
        public static float Evaluate(float time, AnimationCurve curve)
        {
            return curve.Evaluate(time);
        }

        #endregion

        #region FLOAT EASING

        /// <summary>
        /// Ease a float value from a start value to an end value.<br/>
        /// </summary>
        /// <param name="ease">Ease Type - Linear, InQuad, etc</param>
        /// <param name="from">Starting Value</param>
        /// <param name="to">Ending Value</param>
        /// <param name="time">Time is clamped between 0 and 1, for e.g. Time / Duration</param>
        /// <param name="period">Period limits the number of bounces</param>
        /// <param name="amplitude"> Amplitude limits the height of the bounces.</param>
        public static float Float(EaseType ease, float from, float to, float time, float period = 0.3f, float amplitude = 1f)
        {
            return from + (to - from) * Evaluate(ease, time, period, amplitude);
        }

        /// <summary>
        /// Ease a float value from a start value to an end value.<br/>
        /// This method uses an AnimationCurve instead of an EaseType.
        /// </summary>
        /// <param name="from">Starting Value</param>
        /// <param name="to">Ending Value</param>
        /// <param name="time">Time is clamped between 0 and 1, for e.g. Time / Duration</param>
        /// <param name="curve">Given animation curve</param>
        public static float Float(float from, float to, float time, AnimationCurve curve)
        {
            return from + (to - from) * Evaluate(time, curve);
        }

        #endregion

        #region VECTOR EASING

        /// <summary>
        /// Ease a Vector2 value from a start value to an end value.<br/>
        /// </summary>
        /// <param name="ease">Ease Type - Linear, InQuad, etc</param>
        /// <param name="from">Starting Value</param>
        /// <param name="to">Ending Value</param>
        /// <param name="time">Time is clamped between 0 and 1, for e.g. Time / Duration</param>
        /// <param name="period">Period limits the number of bounces</param>
        /// <param name="amplitude"> Amplitude limits the height of the bounces.</param>
        public static Vector2 Vector2(EaseType ease, Vector2 from, Vector2 to, float time, float period = 0.3f, float amplitude = 1f)
        {
            return from + (to - from) * Evaluate(ease, time, period, amplitude);
        }

        /// <summary>
        /// Ease a Vector2 value from a start value to an end value.<br/>
        /// This method uses an AnimationCurve instead of an EaseType.
        /// </summary>
        /// <param name="from">Starting Value</param>
        /// <param name="to">Ending Value</param>
        /// <param name="time">Time is clamped between 0 and 1, for e.g. Time / Duration</param>
        /// <param name="curve">Given animation curve</param>
        public static Vector2 Vector2(Vector2 from, Vector2 to, float time, AnimationCurve curve)
        {
            return from + (to - from) * Evaluate(time, curve);
        }

        /// <summary>
        /// Ease a Vector3 value from a start value to an end value.<br/>
        /// </summary>
        /// <param name="ease">Ease Type - Linear, InQuad, etc</param>
        /// <param name="from">Starting Value</param>
        /// <param name="to">Ending Value</param>
        /// <param name="time">Time is clamped between 0 and 1, for e.g. Time / Duration</param>
        /// <param name="period">Period limits the number of bounces</param>
        /// <param name="amplitude"> Amplitude limits the height of the bounces.</param>
        public static Vector3 Vector3(EaseType ease, Vector3 from, Vector3 to, float time, float period = 0.3f, float amplitude = 1f)
        {
            return from + (to - from) * Evaluate(ease, time, period, amplitude);
        }

        /// <summary>
        /// Ease a Vector3 value from a start value to an end value.<br/>
        /// This method uses an AnimationCurve instead of an EaseType.
        /// </summary>
        /// <param name="from">Starting Value</param>
        /// <param name="to">Ending Value</param>
        /// <param name="time">Time is clamped between 0 and 1, for e.g. Time / Duration</param>
        /// <param name="curve">Given animation curve</param>
        public static Vector3 Vector3(Vector3 from, Vector3 to, float time, AnimationCurve curve)
        {
            return from + (to - from) * Evaluate(time, curve);
        }

        #endregion

        #region COLOR EASING

        /// <summary>
        /// Ease a Color value from a start value to an end value.<br/>
        /// </summary>
        /// <param name="ease">Ease Type - Linear, InQuad, etc</param>
        /// <param name="from">Starting Value</param>
        /// <param name="to">Ending Value</param>
        /// <param name="time">Time is clamped between 0 and 1, for e.g. Time / Duration</param>
        /// <param name="period">Period limits the number of bounces</param>
        /// <param name="amplitude"> Amplitude limits the height of the bounces.</param>
        public static Color Color(EaseType ease, Color from, Color to, float time, float period = 0.3f, float amplitude = 1f)
        {
            return from + (to - from) * Evaluate(ease, time, period, amplitude);
        }

        /// <summary>
        /// Ease a Color value from a start value to an end value.<br/>
        /// This method uses an AnimationCurve instead of an EaseType.
        /// </summary>
        /// <param name="from">Starting Value</param>
        /// <param name="to">Ending Value</param>
        /// <param name="time">Time is clamped between 0 and 1, for e.g. Time / Duration</param>
        /// <param name="curve">Given animation curve</param>
        public static Color Color(Color from, Color to, float time, AnimationCurve curve)
        {
            return from + (to - from) * Evaluate(time, curve);
        }

        #endregion

        #region LINEAR EASING METHODS

        public static float Linear(float time)
        {
            return time;
        }

        #endregion

        #region SINE EASING METHODS

        public static float InSine(float time)
        {
            return 1 - Mathf.Cos(time * HALF_PI);
        }

        public static float OutSine(float time)
        {
            return Mathf.Sin(time * HALF_PI);
        }

        public static float InOutSine(float time)
        {
            return -(Mathf.Cos(PI * time) - 1) * 0.5f;
        }

        #endregion

        #region QUAD EASING METHODS

        public static float InQuad(float time) => time * time;
        public static float OutQuad(float time) => 1 - InQuad(1 - time);

        public static float InOutQuad(float time)
        {
            if (time < 0.5)
            {
                return InQuad(time * 2) * 0.5f;
            }

            return 1 - InQuad((1 - time) * 2) * 0.5f;
        }

        #endregion

        #region CUBIC EASING METHODS

        public static float InCubic(float time)
        {
            return time * time * time;
        }

        public static float OutCubic(float time)
        {
            return 1 - InCubic(1 - time);
        }

        public static float InOutCubic(float time)
        {
            if (time < 0.5)
            {
                return InCubic(time * 2) * 0.5f;
            }

            return 1 - InCubic((1 - time) * 2) * 0.5f;
        }

        #endregion

        #region QUART EASING METHODS

        public static float InQuart(float time)
        {
            return time * time * time * time;
        }

        public static float OutQuart(float time)
        {
            return 1 - InQuart(1 - time);
        }

        public static float InOutQuart(float time)
        {
            if (time < 0.5)
            {
                return InQuart(time * 2) * 0.5f;
            }

            return 1 - InQuart((1 - time) * 2) * 0.5f;
        }

        #endregion

        #region QUINT EASING METHODS

        public static float InQuint(float time)
        {
            return time * time * time * time * time;
        }

        public static float OutQuint(float time)
        {
            return 1 - InQuint(1 - time);
        }

        public static float InOutQuint(float time)
        {
            if (time < 0.5)
            {
                return InQuint(time * 2) * 0.5f;
            }

            return 1 - InQuint((1 - time) * 2) * 0.5f;
        }

        #endregion

        #region EXPO EASING METHODS

        public static float InExpo(float time)
        {
            return Mathf.Pow(2, 10 * (time - 1));
        }

        public static float OutExpo(float time)
        {
            return 1 - InExpo(1 - time);
        }

        public static float InOutExpo(float time)
        {
            if (time < 0.5)
            {
                return InExpo(time * 2) * 0.5f;
            }

            return 1 - InExpo((1 - time) * 2) * 0.5f;
        }

        #endregion

        #region CIRC EASING METHODS

        public static float InCirc(float time)
        {
            return -(Mathf.Sqrt(1 - time * time) - 1);
        }

        public static float OutCirc(float t)
        {
            return 1 - InCirc(1 - t);
        }

        public static float InOutCirc(float t)
        {
            if (t < 0.5)
            {
                return InCirc(t * 2) * 0.5f;
            }

            return 1 - InCirc((1 - t) * 2) * 0.5f;
        }

        #endregion

        #region BACK EASING METHODS

        private static float BackEase(float time)
        {
            return time * time * (C2 * time - C1);
        }

        public static float InBack(float time)
        {
            return BackEase(time);
        }

        public static float OutBack(float time)
        {
            return 1 - BackEase(1 - (time));
        }

        public static float InOutBack(float time)
        {
            if (time < 0.5)
            {
                return InBack(time * 2) * 0.5f;
            }

            return 1 - InBack((1 - time) * 2) * 0.5f;
        }

        #endregion

        #region ELASTIC EASING METHODS

        public static float InElastic(float time, float amplitude = 1f, float period = 0.3f)
        {
            return 1 - OutElastic(1f - time, amplitude, period);
        }

        public static float OutElastic(float time, float amplitude = 1f, float period = 0.3f)
        {
            return amplitude * Mathf.Pow(2, -10 * time) * Mathf.Sin((time - period / 4) * (2 * Mathf.PI) / period) + 1;
        }

        public static float InOutElastic(float time, float amplitude = 1f, float period = 0.3f)
        {
            if (time < 0.5) return InElastic(time * 2, amplitude, period) / 2;
            return 1 - InElastic((1 - time) * 2, amplitude, period) / 2;
        }

        #endregion

        #region BOUNCE EASING METHODS

        public static float InBounce(float time)
        {
            return 1 - OutBounce(1 - time);
        }

        public static float OutBounce(float time)
        {
            float divider = 2.75f;
            float multiplier = 7.5625f;

            if (time < 1f / divider)
            {
                return multiplier * time * time;
            }

            if (time < 2f / divider)
            {
                return multiplier * (time -= 1.5f / divider) * time + 0.75f;
            }

            if (time < 2.5 / divider)
            {
                return multiplier * (time -= 2.25f / divider) * time + 0.9375f;
            }

            return multiplier * (time -= 2.625f / divider) * time + 0.984375f;
        }

        public static float InOutBounce(float time)
        {
            if (time < 0.5f)
            {
                return InBounce(time * 2) * 0.5f;
            }

            return 1 - InBounce((1 - time) * 2) * 0.5f;
        }

        #endregion
    }
}