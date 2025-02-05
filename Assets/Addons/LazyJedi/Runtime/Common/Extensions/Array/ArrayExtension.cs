using Random = System.Random;

namespace LazyJedi.Common.Extensions
{
    /// <summary>
    /// Extend the Array Class.
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        ///  Shuffling Algorithm Based on Knuth Fisher Yates Shuffle.
        /// </summary>
        /// <param name="array"> The Array to Shuffle.</param>
        public static void Shuffle<T>(this T[] array)
        {
            Random random = new Random();
            int n = array.Length;
            for (int i = n - 1; i > 0; i--)
            {
                array.Swap(i, random.Next(i + 1));
            }
        }

        /// <summary>
        /// Swap 2 Elements at Index i and Index j.
        /// </summary>
        /// <param name="array"> The Array to Swap Elements.</param>
        /// <param name="i"> The Index of the First Element.</param>
        /// <param name="j"> The Index of the Second Element.</param>
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