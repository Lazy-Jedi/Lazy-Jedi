/*
 * Created By: BluMalice
 */

namespace LazyJedi.Utility
{
    public static class MathUtility
    {
        #region MATH UTILITY METHODS

        /// <summary>
        /// Return the Value between Min and Max for the Given Percentage.
        /// </summary>
        /// <param name="percentage"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float GetValueFromPercentage(float percentage, float min, float max)
        {
            return max - percentage * (min - max);
        }

        /// <summary>
        /// Returns the Percentage between Min and Max for the Given Value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float GetPercentageFromValue(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }

        #endregion
    }
}