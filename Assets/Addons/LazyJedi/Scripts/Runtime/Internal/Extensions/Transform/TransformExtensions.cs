// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    ///     Returns a Vector3Int representation of the world position for this transform.
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Vector3Int PositionToInt(this Transform transform)
    {
        Vector3 position = transform.position;

        return new Vector3Int(Mathf.RoundToInt(position.x),
            Mathf.RoundToInt(position.y),
            Mathf.RoundToInt(position.z));
    }

    /// <summary>
    ///     Reparents and aligns this transform to the specified parent transform.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="parent"></param>
    public static void ReparentAndAlign(this Transform transform, Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    /// <summary>
    ///     Sets the interaction layer for all colliders within the given transform.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="layer"></param>
    public static void SetColliderInteractionLayers(this Transform transform, string layer)
    {
        foreach (Collider child in transform.GetComponentsInChildren<Collider>())
            child.gameObject.layer = LayerMask.NameToLayer(layer);
    }

    /// <summary>
    ///     Destroys all child objects within this transform.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="destroyInactive"></param>
    public static void DestroyAllChildren(this Transform transform, bool destroyInactive = false)
    {
        foreach (Transform child in transform)
            if (child != transform)
                if (destroyInactive || child.gameObject.activeSelf)
                    Object.Destroy(child.gameObject);
    }

    public static void SetActive(this Transform transform, bool value)
    {
        transform.gameObject.SetActive(value);
    }
}