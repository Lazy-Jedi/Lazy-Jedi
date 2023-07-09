using System.Text;

public enum EncodingType
{
    UTF8,
    ASCII,
    Unicode,
    UTF32,
    BigEndianUnicode
}

public enum PathType
{
    SaveFolder,
    OptionsFolder,
    DefaultFolder,
}

public static class Enums
{
    public static Encoding GetEncoding(EncodingType encodingType)
    {
        return encodingType switch
        {
            EncodingType.UTF8 => Encoding.UTF8,
            EncodingType.ASCII => Encoding.ASCII,
            EncodingType.Unicode => Encoding.Unicode,
            EncodingType.UTF32 => Encoding.UTF32,
            EncodingType.BigEndianUnicode => Encoding.BigEndianUnicode,
            _ => Encoding.UTF8
        };
    }
}