using UnityEngine;

namespace LazyJedi.Common.Extensions
{
    public static class ObjectExtension
    {
        #region SERIALIZATION AND DESERIALIZATION

        /// <summary>
        /// Use Unity JsonUtility to Serialize an object to a Json String.
        /// </summary>
        /// <param name="instance">Object to Serialize</param>
        /// <param name="prettyPrint">Pretty Print Json</param>
        /// <returns>Return a Json String</returns>
        public static string ToJson(this object instance, bool prettyPrint = false)
        {
            return JsonUtility.ToJson(instance, prettyPrint);
        }

        #endregion

        #region NULL CHECKS

        /// <summary>
        /// Check if a System Object is Null
        /// </summary>
        /// <returns></returns>
        public static bool IsNull(this object @object)
        {
            return @object == null;
        }

        /// <summary>
        /// Check if a System Object is Not Null
        /// </summary>
        /// <returns></returns>
        public static bool IsNotNull(this object @object)
        {
            return @object != null;
        }

        /// <summary>
        /// Check if a Unity Object is Null
        /// </summary>
        /// <returns></returns>
        public static bool IsNull(this Object @object)
        {
            return @object is null;
        }

        /// <summary>
        /// Check if a Unity Object is Not Null
        /// </summary>
        /// <returns></returns>
        public static bool IsNotNull(this Object @object)
        {
            return @object is not null;
        }

        #endregion
    }
}