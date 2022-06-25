/*
 * Created By: Uee
 * GitHub - https://github.com/Lazy-Jedi/Lazy-Jedi
 */

using UnityEngine;

public static class LayerMaskExtension
{
    /// <summary>
    /// Check if Layer is in Layer Mask.
    /// </summary>
    /// <param name="layerMask"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool InLayerMask(this LayerMask layerMask, int value)
    {
        return (layerMask | (1 << value)) == layerMask;
    }
}