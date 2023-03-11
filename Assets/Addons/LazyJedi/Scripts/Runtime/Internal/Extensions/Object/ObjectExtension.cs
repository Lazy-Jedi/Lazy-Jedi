using UnityEngine;
using Object = UnityEngine.Object;

namespace LazyJedi.Extensions
{
    public static class ObjectExtension
    {
        #region NULL CHECKS

        /// <summary>
        /// Check if a System Object is Null
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static bool IsNull(this object @object)
        {
            return @object == null;
        }

        /// <summary>
        /// Check if a System Object is Not Null
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object @object)
        {
            return @object != null;
        }

        /// <summary>
        /// Serialize an object to a Json String.
        /// </summary>
        /// <param name="object"></param>
        /// <param name="prettyPrint"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToJson(this object @object, bool prettyPrint = false)
        {
            return JsonUtility.ToJson(@object, prettyPrint);
        }

        /// <summary>
        /// Check if a Unity Object is Null
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static bool IsNull(this Object @object)
        {
            return @object is null;
        }

        /// <summary>
        /// Check if a Unity Object is Not Null
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static bool IsNotNull(this Object @object)
        {
            return @object is not null;
        }

        #endregion
    }
}