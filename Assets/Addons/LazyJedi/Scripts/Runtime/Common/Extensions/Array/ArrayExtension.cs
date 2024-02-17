using Random = System.Random;

namespace LazyJedi.Common.Extensions
{
    /// <summary>
    /// Extend the Array Class.
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        ///     Shuffling Algorithm Based on Knuth Fisher Yates Shuffle.
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this T[] array)
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
        public static void Swap<T>(this T[] array, int i, int j)
        {
            (array[i], array[j]) = (array[j], array[i]);
        }

        /// <summary>
        /// Get a random Item from the array
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRandomItem<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }
    }
}