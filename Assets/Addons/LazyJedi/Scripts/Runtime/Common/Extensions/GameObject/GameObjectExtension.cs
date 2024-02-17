using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LazyJedi.Common.Extensions
{
    public static class GameObjectExtension
    {
        #region GAMEOBJECT EXTENSIONS METHODS

        /// <summary>
        /// Get the Parents Transform of this GameObject, otherwise return this GameObjects Transform if there is no Parent.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static Transform GetParent(this GameObject gameObject)
        {
            return !gameObject.transform.parent ? gameObject.transform : gameObject.transform.parent;
        }

        /// <summary>
        /// Get the Parents GameObject, of this GameObject otherwise return this GameObject if there is no Parent.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static GameObject GetParentGo(this GameObject gameObject)
        {
            return gameObject.GetParent().gameObject;
        }

        /// <summary>
        /// Set the Parent of this GameObject.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="parent"></param>
        public static void SetParent(this GameObject gameObject, Transform parent)
        {
            gameObject.transform.SetParent(parent);
        }

        /// <summary>
        /// Set the Parent of this GameObject.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="parent"></param>
        public static void SetParent(this GameObject gameObject, GameObject parent)
        {
            gameObject.SetParent(parent.transform);
        }

        /// <summary>
        /// Enable this Instance
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Activate(this GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Activate a GameObject after X seconds has elapsed.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static IEnumerator Activate(this GameObject gameObject, float seconds)
        {
            float elapsedTime = 0f;
            while (elapsedTime <= seconds)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            gameObject.Activate();
        }

        /// <summary>
        /// Disable this Instance.
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Deactivate(this GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Deactivate a GameObject after X seconds has Elapsed.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static IEnumerator Deactivate(this GameObject gameObject, float seconds)
        {
            float elapsedTime = 0f;
            while (elapsedTime <= seconds)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            gameObject.Deactivate();
        }

        /// <summary>
        /// Destroy this Instance
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Destroy(this GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }

        public static void Destroy(this GameObject gameObject, float seconds)
        {
            Object.Destroy(gameObject, seconds);
        }

        /// <summary>
        /// Clone this Object
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="position"></param>
        /// <param name="identity"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject Clone(this GameObject gameObject, Vector3 position = new Vector3(), Quaternion identity = new Quaternion(),
            Transform parent = null)
        {
            return Object.Instantiate(gameObject, position, identity, parent);
        }

        #endregion
    }
}