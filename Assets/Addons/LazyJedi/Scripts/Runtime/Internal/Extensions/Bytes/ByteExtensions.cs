using System;
using System.Text;

namespace LazyJedi.Extensions
{
    public static class ByteExtensions
    {
        #region METHODS

        /// <summary>
        /// Convert a byte array to a base 64 string.
        /// </summary>
        /// <param name="data">byte[] data</param>
        /// <returns>Base 64 string </returns>
        public static string ToBase64(this byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// Convert a byte array to a string.
        /// </summary>
        /// <param name="data">Byte[] to convert to a string</param>
        /// <param name="encodingType">Encoding to use when converting the byte[] to a string, default is set to UTF8</param>
        /// <returns>Plain text string</returns>
        public static string FromBytes(this byte[] data, EncodingType encodingType = EncodingType.UTF8)
        {
            return encodingType switch
            {
                EncodingType.UTF8 => Encoding.UTF8.GetString(data),
                EncodingType.UTF32 => Encoding.UTF32.GetString(data),
                EncodingType.ASCII => Encoding.ASCII.GetString(data),
                EncodingType.Unicode => Encoding.Unicode.GetString(data),
                EncodingType.BigEndianUnicode => Encoding.BigEndianUnicode.GetString(data),
                _ => Encoding.UTF8.GetString(data)
            };
        }

        #endregion
    }
}