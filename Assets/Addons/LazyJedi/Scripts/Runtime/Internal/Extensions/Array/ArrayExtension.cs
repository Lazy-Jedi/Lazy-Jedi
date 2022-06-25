using System;
using Random = System.Random;

/// <summary>
/// Extend the Array Class.
/// </summary>
public static class ArrayExtension
{
    /// <summary>
    ///     Shuffling Algorithm Based on Knuth Fisher Yates Shuffle.
    /// </summary>
    /// <example>
    ///     <code>
    /// T[] list = { 1, 2, 3};
    /// list.Shuffle();
    /// </code>
    /// </example>
    /// <param name="array"></param>
    /// <typeparam name="T"></typeparam>
    public static void Shuffle(this Array array)
    {
        Random random = new Random();
        for (int i = 0; i < array.Length; i++)
        {
            array.Swap(i, i + random.Next(array.Length - i));
        }
    }

    /// <summary>
    ///     Swap 2 Elements at Index i and Index j.
    /// </summary>
    /// <param name="array"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <returns>
    ///     The list with the newly Swapped Elements.
    /// </returns>
    private static void Swap(this Array array, int i, int j)
    {
        object element = array.GetValue(i);
        array.SetValue(array.GetValue(j), i);
        array.SetValue(element, j);
    }
}