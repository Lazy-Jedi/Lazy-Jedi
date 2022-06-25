using System.Collections.Generic;
using Random = System.Random;

/// <summary>
///     Extend the List Class.
/// </summary>
public static class ListExtension
{
    /// <summary>
    ///     Shuffling Algorithm Based on Knuth Fisher Yates Shuffle.
    /// </summary>
    /// <example>
    ///     <code>
    /// List<T>
    ///             list = new List
    ///             <T>
    ///                 (){ 1, 2, 3};
    ///                 list.Shuffle();
    /// </code>
    /// </example>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    public static void Shuffle<T>(this List<T> list)
    {
        Random random = new Random();
        for (int i = 0; i < list.Count; i++)
        {
            list.Swap(i, i + random.Next(list.Count - i));
        }
    }

    /// <summary>
    ///     Swap 2 Elements at Index i and Index j.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>
    ///     The list with the newly Swapped Elements.
    /// </returns>
    private static void Swap<T>(this List<T> list, int i, int j)
    {
        (list[i], list[j]) = (list[j], list[i]);
    }
}