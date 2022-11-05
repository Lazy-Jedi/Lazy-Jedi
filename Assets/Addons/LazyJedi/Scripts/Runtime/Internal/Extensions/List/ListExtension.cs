using System.Collections.Generic;
using Random = System.Random;

namespace LazyJedi.Extensions
{
    /// <summary>
    ///     Extend the List Class.
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Shuffle a List of Elements using Knuth Fisher Yates Algorithm
        /// </summary>
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
        /// Swap 2 Elements in a List
        /// </summary>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        ///     The list with the newly Swapped Elements.
        /// </returns>
        public static void Swap<T>(this List<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }

        public static T GetRandomItem<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}