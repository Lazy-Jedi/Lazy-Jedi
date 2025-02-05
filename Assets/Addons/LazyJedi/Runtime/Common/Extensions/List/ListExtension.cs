using System.Collections.Generic;
using Random = System.Random;

namespace LazyJedi.Common.Extensions
{
    /// <summary>
    ///     Extend the List Class.
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Shuffle a List of Elements using Knuth Fisher Yates Algorithm
        /// </summary>
        /// <param name="list"> The List to Shuffle.</param>
        public static void Shuffle<T>(this List<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                list.Swap(i, random.Next(i + 1));
            }
        }

        /// <summary>
        /// Swap 2 Elements in a List
        /// </summary>
        /// <param name="list">The List to Swap Elements.</param>
        /// <param name="i"> The Index of the First Element.</param>
        /// <param name="j"> The Index of the Second Element.</param>
        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }

        public static T GetRandomItem<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}