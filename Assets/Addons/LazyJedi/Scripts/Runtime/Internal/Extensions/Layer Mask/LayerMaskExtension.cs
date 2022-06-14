/*
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System.Linq;
using UnityEngine;

public static class LayerMaskExtension
{
	/// <summary>
	/// Check if Layer is in Layer Mask.
	/// </summary>
	/// <param name="layerMask"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public static bool LayerInLayerMask(this LayerMask layerMask, int value)
	{
		return (layerMask | (1 << value)) == layerMask;
	}

	/// <summary>
	/// Bit Mask, to Get collisions only with this Layer Mask.
	/// </summary>
	/// <param name="layerMask"></param>
	/// <returns></returns>
	public static LayerMask BitMask(this LayerMask layerMask)
	{
		return 1 << layerMask;
	}

	/// <summary>
	/// Bit Masks, to Get All Collisions with only the Specified Layer Masks.
	/// </summary>
	/// <param name="layerMask"></param>
	/// <param name="layerMasks"></param>
	/// <returns></returns>
	public static LayerMask BitMasks(this LayerMask layerMask, params LayerMask[] layerMasks)
	{
		return layerMasks.Aggregate(layerMask.BitMask(), (current, value) => current | (1 << value));
	}

	/// <summary>
	/// Invert Bit Mask, to Ignore all Collisions with this Layer Mask.
	/// </summary>
	/// <param name="layerMask"></param>
	public static LayerMask InvertBitMask(this LayerMask layerMask)
	{
		return ~layerMask.BitMask();
	}

	/// <summary>
	/// Invert Bit Masks, to Ignore all Collisions with the Specified Layer Masks.
	/// </summary>
	/// <param name="layerMask"></param>
	/// <param name="layerMasks"></param>
	/// <returns></returns>
	public static LayerMask InvertBitMasks(this LayerMask layerMask, params LayerMask[] layerMasks)
	{
		return ~layerMasks.Aggregate(layerMask.BitMask(), (current, value) => current | (1 << value));
	}
}