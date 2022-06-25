using System;
using System.Text;

public static class StringExtension
{
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
}