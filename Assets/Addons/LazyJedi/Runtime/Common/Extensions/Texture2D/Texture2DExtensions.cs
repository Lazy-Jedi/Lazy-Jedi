#region CREDIT

// Cristian Alexandru Geambasu
// Creator of the following Methods: ToBase64(), ToTexture2D(), GetImageSize(), ReadInt()
// Link - https://gist.github.com/daemon3000/609e44a907a8b37def0d
// License - MIT

#endregion

using System;
using UnityEngine;

namespace LazyJedi.Common.Extensions
{
    public static class Texture2DExtensions
    {
        #region METHODS

        /// <summary>
        /// Convert Texture2D to a Base64 string.
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public static string ToBase64(this Texture2D texture)
        {
            return texture ? Convert.ToBase64String(texture.EncodeToPNG()) : string.Empty;
        }

        /// <summary>
        /// Convert a Base64 string to Texture2D
        /// </summary>
        /// <param name="encodedData"></param>
        /// <returns></returns>
        public static Texture2D ToTexture2D(this string encodedData)
        {
            byte[] imageData = Convert.FromBase64String(encodedData);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageData);
            return texture;
        }

        #endregion
    }
}