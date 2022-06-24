/*
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UnityEngine;

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
    /// Disable this Instance.
    /// </summary>
    /// <param name="gameObject"></param>
    public static void Deactivate(this GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Destroy this Instance
    /// </summary>
    /// <param name="gameObject"></param>
    public static void Destroy(this GameObject gameObject)
    {
        Object.Destroy(gameObject);
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