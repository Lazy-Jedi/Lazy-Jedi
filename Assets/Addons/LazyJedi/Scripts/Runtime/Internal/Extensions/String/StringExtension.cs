using System;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace LazyJedi.Extensions
{
    public static class StringExtension
    {
        #region ENCODING

        public static byte[] ToBytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        public static string FromBytes(this byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public static string ToBase64(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        public static string FromBase64(this string text64)
        {
            return string.IsNullOrEmpty(text64) ? string.Empty : Encoding.UTF8.GetString(Convert.FromBase64String(text64));
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

        public static float ToFloat(this string value)
        {
            return float.Parse(value);
        }

        public static double ToDouble(this string value)
        {
            return double.Parse(value);
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
        /// <param name="json"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        /// <summary>
        /// Overwrite an existing object from a Json String.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="objectToOverwrite"></param>
        /// <returns></returns>
        public static object FromJsonOverwrite(this string json, object objectToOverwrite)
        {
            JsonUtility.FromJsonOverwrite(json, objectToOverwrite);
            return objectToOverwrite;
        }

        #endregion

        #region PATTERN MATCHING

        public static bool MatchesPattern(this string input, string pattern)
        {
            return !string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(pattern) && Regex.IsMatch(input, pattern);
        }

        public static bool HasSpecialChars(this string value)
        {
            return Regex.IsMatch(value, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
        }

        public static string RemoveAllSpecialChars(this string value, string replacement = "")
        {
            return Regex.Replace(value, @"[^a-zA-Z0-9]", replacement);
        }

        public static string RemoveSpecialChars_ExclSpaces(this string value, string replacement = "")
        {
            return Regex.Replace(value, @"[^a-zA-Z0-9\s]+|(\s){2,}", replacement);
        }

        /// <summary>
        /// Removes all special Characters from a string excluding - [!@&()-\',.?":]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string RemoveSpecialChars_ExclPunctuation(this string value, string replacement = "")
        {
            return Regex.Replace(value, @"[^\w\d\s\n!@&()-\',.:\\""\\]", replacement).InnerTrim();
        }
        
        /// <summary>
        /// Trims 2 or more white spaces between words and/or letters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string InnerTrim(this string value)
        {
            return Regex.Replace(value, @"|(\s){2,}", string.Empty);
        }

        public static bool IsAlphanumeric(this string value)
        {
            return !string.IsNullOrEmpty(value) && Regex.IsMatch(value, "^[a-zA-Z0-9]*$");
        }

        public static bool IsValidUrl_Uri(this string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public static bool IsValidUrl_Regex(this string url)
        {
            return Regex.IsMatch(url, @"^(http|https):\/\/([\w-]+\.)+[\w-]+(\/[\w-./?%&=]*)?$");
        }

        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\+?[1-9]\d{0,3}-?\d{1,4}-?\d{1,4}$");
        }

        public static bool IsValidEmail_Regex(this string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        public static bool IsValidEmail_StrictRegex(this string email)
        {
            return Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
        }

        public static bool IsValidEmail_MailAddress(this string email)
        {
            MailAddress mailAddress = new MailAddress(email);
            return mailAddress.Address == email;
        }

        public static bool IsValidEmail_MailAddress(this string email, out MailAddress mailAddress)
        {
            mailAddress = new MailAddress(email);
            return mailAddress.Address == email;
        }

        #endregion
    }
}