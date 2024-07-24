using System;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LazyJedi.Common.Extensions
{
    public static class StringExtension
    {
        #region STRING METHODS

        /// <summary>
        /// Check if a string is a null or white space.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value, bool trim = false)
        {
            return string.IsNullOrWhiteSpace(trim ? value.Trim() : value);
        }

        /// <summary>
        /// Check if a string is a null or empty.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value, bool trim = false)
        {
            return string.IsNullOrEmpty(trim ? value.Trim() : value);
        }

        #endregion

        #region ENCODING

        /// <summary>
        /// Convert plain text to a byte array.
        /// </summary>
        /// <param name="plainString">The plain text string to convert to a byte[]</param>
        /// <param name="encodingType">The string encoding format, default is UTF8</param>
        /// <returns>The byte[] equivalent of the plain text string</returns>
        public static byte[] ToBytes(this string plainString, EncodingType encodingType = EncodingType.UTF8)
        {
            return encodingType switch
            {
                EncodingType.UTF8 => Encoding.UTF8.GetBytes(plainString),
                EncodingType.UTF32 => Encoding.UTF32.GetBytes(plainString),
                EncodingType.ASCII => Encoding.ASCII.GetBytes(plainString),
                EncodingType.Unicode => Encoding.Unicode.GetBytes(plainString),
                EncodingType.BigEndianUnicode => Encoding.BigEndianUnicode.GetBytes(plainString),
                _ => Encoding.UTF8.GetBytes(plainString)
            };
        }

        /// <summary>
        /// Convert plain text to a base 64 string.
        /// </summary>
        /// <param name="plainString">The plain text to convert to base 64 string</param>
        /// <param name="encodingType">The string encoding format, default is UTF8</param>
        /// <returns>Base 64 string</returns>
        public static string ToBase64(this string plainString, EncodingType encodingType = EncodingType.UTF8)
        {
            return plainString.IsNullOrEmpty() ? string.Empty : Convert.ToBase64String(plainString.ToBytes(encodingType));
        }

        /// <summary>
        /// Convert a base64 string to a plain text.
        /// </summary>
        /// <param name="base64String">The base 64 string to convert to plain text</param>
        /// <param name="decodingType">The string decoding format, default is UTF8</param>
        /// <returns>Plain text string</returns>
        public static string FromBase64(this string base64String, EncodingType decodingType = EncodingType.UTF8)
        {
            return base64String.IsNullOrEmpty() ? string.Empty : Convert.FromBase64String(base64String).FromBytes(decodingType);
        }

        /// <summary>
        /// Convert a Base 64 string to a byte array.
        /// </summary>
        /// <param name="base64String">The base 64 string</param>
        /// <returns>The plain text byte array</returns>
        public static byte[] FromB64ToBytes(this string base64String)
        {
            return string.IsNullOrEmpty(base64String) ? Array.Empty<byte>() : Convert.FromBase64String(base64String);
        }

        #endregion

        #region LAZY PARSING

        public static short ToShort(this string value)
        {
            return short.Parse(value);
        }

        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        public static float ToFloat(this string value, NumberStyles numberStyles = NumberStyles.Any, IFormatProvider formatProvider = null)
        {
            return float.Parse(value, numberStyles, formatProvider ?? CultureInfo.InvariantCulture);
        }

        public static double ToDouble(this string value, NumberStyles numberStyles = NumberStyles.Any, IFormatProvider formatProvider = null)
        {
            return double.Parse(value, numberStyles, formatProvider ?? CultureInfo.InvariantCulture);
        }

        public static long ToLong(this string value)
        {
            return long.Parse(value);
        }

        public static bool ToBool(this string value)
        {
            return bool.Parse(value);
        }

        #endregion

        #region JSON

        /// <summary>
        /// Create an object from a Json String
        /// </summary>
        /// <param name="json">Json string to deserialized</param>
        /// <typeparam name="T">Generic Serializable Type</typeparam>
        /// <returns>Object Created from Deserializing the Json String</returns>
        public static T FromJson<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        /// <summary>
        /// Overwrite an existing object from a Json String.
        /// </summary>
        /// <param name="json">Json string to deserialize</param>
        /// <param name="objectToOverwrite">Object that will be overwritten</param>
        /// <returns>The object that has been overwritten</returns>
        public static object FromJsonOverwrite(this string json, object objectToOverwrite)
        {
            JsonUtility.FromJsonOverwrite(json, objectToOverwrite);
            return objectToOverwrite;
        }

        /// <summary>
        /// Overwrite an existing object from a Json String.
        /// </summary>
        /// <param name="json">Json string to deserialize</param>
        /// <param name="objectToOverwrite">Object that will be overwritten</param>
        /// <returns>The Unity Object that has been overwritten</returns>
        public static Object FromJsonOverwrite(this string json, Object objectToOverwrite)
        {
            JsonUtility.FromJsonOverwrite(json, objectToOverwrite);
            return objectToOverwrite;
        }

        #endregion

        #region PATTERN MATCHING

        /// <summary>
        /// Uses Regex to check if a string matches a pattern
        /// </summary>
        /// <param name="input">The string to check for patterns</param>
        /// <param name="pattern">The pattern that will be used</param>
        /// <returns>True or False if the string has or does not match the given pattern</returns>
        public static bool MatchesPattern(this string input, string pattern)
        {
            return !input.IsNullOrEmpty() && !input.IsNullOrEmpty() && Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// Checks if a string contains any special characters
        /// </summary>
        /// <param name="value">The string to check for special characters</param>
        /// <returns>Returns True or False if the String has or does not have any special characters</returns>
        public static bool HasSpecialChars(this string value)
        {
            return Regex.IsMatch(value, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
        }

        /// <summary>
        /// Removes all special Characters from a string
        /// </summary>
        /// <param name="value">The string to adjust</param>
        /// <param name="replacement">The character used to replace the Special Characters with</param>
        /// <returns>The new string after removing the special characters.</returns>
        public static string RemoveAllSpecialChars(this string value, string replacement = "")
        {
            return Regex.Replace(value, @"[^a-zA-Z0-9]", replacement);
        }

        /// <summary>
        /// Removes all special Characters from a string excluding white spaces
        /// </summary>
        /// <param name="value">The string to adjust</param>
        /// <param name="replacement">The character used to replace the Special Characters with</param>
        /// <returns>The new string after removing the special characters.</returns>
        public static string RemoveSpecialChars_ExclSpaces(this string value, string replacement = "")
        {
            return Regex.Replace(value, @"[^a-zA-Z0-9\s]+|(\s){2,}", replacement);
        }

        /// <summary>
        /// Removes all special Characters from a string excluding - [!@&()-\',.?":]
        /// </summary>
        /// <param name="value">The string to adjust</param>
        /// <param name="replacement">The character used to replace the Special Characters with</param>
        /// <returns>The new string after removing the special characters.</returns>
        public static string RemoveSpecialChars_ExclPunctuation(this string value, string replacement = "")
        {
            return Regex.Replace(value, @"[^\w\d\s\n!@&()-\',.:\\""\\]", replacement).InnerTrim();
        }

        /// <summary>
        /// Trims 2 or more white spaces between words and/or letters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The st</returns>
        public static string InnerTrim(this string value)
        {
            return Regex.Replace(value, @"|(\s){2,}", string.Empty);
        }

        /// <summary>
        /// Checks if the string is a valid alphanumeric string.
        /// </summary>
        /// <param name="value">The alphanumeric string</param>
        /// <returns>Returns True or False if the Alphanumeric String is Valid</returns>
        public static bool IsAlphanumeric(this string value)
        {
            return !string.IsNullOrEmpty(value) && Regex.IsMatch(value, "^[a-zA-Z0-9]*$");
        }

        /// <summary>
        /// Checks if the string is a valid URL using a Uri.
        /// </summary>
        /// <param name="url">The URL string</param>
        /// <returns>Returns True or False if the URL is Valid</returns>
        public static bool IsValidUri(this string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Checks if the url is valid using a Regex.
        /// </summary>
        /// <param name="url">The URL string</param>
        /// <returns>Returns True or False if the URL is Valid</returns>
        public static bool IsValidUrl_Regex(this string url)
        {
            return Regex.IsMatch(url, @"^(http|https):\/\/([\w-]+\.)+[\w-]+(\/[\w-./?%&=]*)?$");
        }

        /// <summary>
        /// Checks if the phone number is valid using a Regex.
        /// </summary>
        /// <param name="phoneNumber">The phone string</param>
        /// <returns>Returns True or False if the Phone Number is Valid</returns>
        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\+?[1-9]\d{0,3}-?\d{1,4}-?\d{1,4}$");
        }

        /// <summary>
        /// Checks if the email is valid using a Regex.
        /// </summary>
        /// <param name="email">The email string</param>
        /// <returns>Returns True or False if the Email Address is Valid</returns>
        public static bool IsValidEmail_Regex(this string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        /// <summary>
        /// Checks if the email is valid using a strict Regex.
        /// </summary>
        /// <param name="email">The email string</param>
        /// <returns>Returns True or False if the Email Address is Valid</returns>
        public static bool IsValidEmail_StrictRegex(this string email)
        {
            return Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
        }

        /// <summary>
        /// Checks if the email is valid using the MailAddress class.
        /// </summary>
        /// <param name="email">The email string</param>
        /// <returns>Returns True or False if the Email Address is Valid</returns>
        public static bool IsValidEmail_MailAddress(this string email)
        {
            return new MailAddress(email).Address == email;
        }

        /// <summary>
        /// Checks if the email is valid using the MailAddress class. <br/>
        /// Out parameter returns the MailAddress object.
        /// </summary>
        /// <param name="email">The email string</param>
        /// <param name="mailAddress">The MailAddress Instance</param>
        /// <returns>Returns True or False if the Email Address is Valid</returns>
        public static bool IsValidEmail_MailAddress(this string email, MailAddress mailAddress)
        {
            if (mailAddress.IsNull())
            {
                mailAddress = new MailAddress(email);
            }
            return mailAddress.Address == email;
        }

        #endregion
    }
}