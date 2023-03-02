using System;
using System.Text;
using UnityEngine;

namespace LazyJedi.Extensions
{
    public static class StringExtension
    {
        public static byte[] ToBytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        public static string FromBytes(this byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public static string ToBase64(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        public static string FromBase64(this string text64)
        {
            return string.IsNullOrEmpty(text64) ? string.Empty : Encoding.UTF8.GetString(Convert.FromBase64String(text64));
        }

        #region LAZY PARSING

        public static short ToShort(this string value)
        {
            return short.Parse(value);
        }

        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        public static float ToFloat(this string value)
        {
            return float.Parse(value);
        }

        public static double ToDouble(this string value)
        {
            return double.Parse(value);
        }

        public static long ToLong(this string value)
        {
            return long.Parse(value);
        }

        public static bool ToBool(this string value)
        {
            return bool.Parse(value);
        }

        #endregion

        #region JSON

        /// <summary>
        /// Create an object from a Json String
        /// </summary>
        /// <param name="json"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        /// <summary>
        /// Overwrite an existing object from a Json String.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="objectToOverwrite"></param>
        /// <returns></returns>
        public static object FromJsonOverwrite(this string json, object objectToOverwrite)
        {
            JsonUtility.FromJsonOverwrite(json, objectToOverwrite);
            return objectToOverwrite;
        }

        #endregion
    }
}