using System;

namespace LazyJedi.Common.Extensions
{
    public static class FloatExtensions
    {
        #region TIME METHODS

        /// <summary>
        /// Convert a float to a Time String representing mm:ss (minutes and seconds)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTimeMS(this float value)
        {
            return $"{TimeSpan.FromSeconds(value):mm\\:ss}";
        }

        /// <summary>
        /// Convert a float to a Time String representing hh:mm:ss (hours, minutes and seconds)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTimeHMS(this float value)
        {
            return $"{TimeSpan.FromSeconds(value):hh\\:mm\\:ss}";
        }

        #endregion
    }
}