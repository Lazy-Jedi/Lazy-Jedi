/*
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UnityEngine;

public static class GameObjectExtension
{
    #region GAMEOBJECT EXTENSIONS METHODS

    /// <summary>
    /// Get Transform Parent of this GameObject otherwise return This GameObject Transform if there is no Parent.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static Transform Parent(this GameObject gameObject)
    {
        return gameObject.transform.parent ? gameObject.transform.parent : gameObject.transform;
    }

    /// <summary>
    /// Get the GameObject Parent of this GameObject otherwise return This GameObject if there is no Parent GameObject.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static GameObject ParentGameObject(this GameObject gameObject)
    {
        return gameObject.Parent() ? gameObject.Parent().gameObject : gameObject;
    }

    /// <summary>
    /// Set The Parent of this GameObject.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="parent"></param>
    public static void SetParent(this GameObject gameObject, Transform parent)
    {
        gameObject.transform.SetParent(parent);
    }

    /// <summary>
    /// Set The Parent of this GameObject.
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

    #endregion
}