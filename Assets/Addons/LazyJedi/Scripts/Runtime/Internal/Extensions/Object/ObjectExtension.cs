using LazyJedi.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LazyJedi.Extensions
{
    public static class ObjectExtension
    {
        #region SERIALIZATION AND DESERIALIZATION

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
        /// Serialize an object to Json and Convert the Json string to Bytes.
        /// </summary>
        /// <param name="object"></param>
        /// <param name="prettyPrint"></param>
        /// <returns></returns>
        public static byte[] ToJsonBytes(this object @object, bool prettyPrint = false)
        {
            return JsonUtility.ToJson(@object, prettyPrint).ToBytes();
        }

        /// <summary>
        /// Serialize a Unity Object. Works best with a ScriptableObject.
        /// </summary>
        /// <param name="data">Serializable Object</param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <param name="prettyPrint">Pretty Print the JSON data</param>
        /// <typeparam name="T"></typeparam>
        public static void Save<T>(this T data, string filename = "", PathType pathType = PathType.DefaultFolder, bool prettyPrint = false) where T : Object
        {
            DataIO.Save(data, filename, pathType, prettyPrint);
        }

        /// <summary>
        /// Serialize a Unity Object to a Slot. Works best with a ScriptableObject.
        /// </summary>
        /// <param name="data">Serializable Object</param>
        /// <param name="slotIndex"> This is the index of the Save Slot, this value needs to be greater than 0. </param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <param name="prettyPrint">Pretty Print the JSON data</param>
        /// <typeparam name="T"></typeparam>
        public static void Save<T>(this T data, int slotIndex, string filename = "", PathType pathType = PathType.DefaultFolder, bool prettyPrint = false)
            where T : Object
        {
            DataIO.SaveToSlot(data, slotIndex, filename, pathType, prettyPrint);
        }

        /// <summary>
        /// Deserialize a Json Data to a Unity Object. Works best with a ScriptableObject.
        /// </summary>
        /// <param name="data">Serializable Object</param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <typeparam name="T"></typeparam>
        public static void Overwrite<T>(this T data, string filename = "", PathType pathType = PathType.DefaultFolder) where T : Object
        {
            DataIO.LoadAndOverwrite(data, filename, pathType);
        }

        /// <summary>
        /// Deserialize a Json Data from a Slot to a Unity Object. Works best with a ScriptableObject.
        /// </summary>
        /// <param name="data">Serializable Object</param>
        /// <param name="slotIndex"> This is the index of the Save Slot, this value needs to be greater than 0. </param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <typeparam name="T"></typeparam>
        public static void Overwrite<T>(this T data, int slotIndex, string filename = "", PathType pathType = PathType.DefaultFolder) where T : Object
        {
            DataIO.LoadFromSlotAndOverwrite(data, slotIndex, filename, pathType);
        }

        #endregion

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