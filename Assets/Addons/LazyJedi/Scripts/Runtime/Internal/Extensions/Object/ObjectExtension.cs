using UnityEngine;

public static class ObjectExtension
{
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
        return @object;
    }

    /// <summary>
    /// Check if a Unity Object is Not Null
    /// </summary>
    /// <param name="object"></param>
    /// <returns></returns>
    public static bool IsNotNull(this Object @object)
    {
        return !@object.IsNull();
    }
}