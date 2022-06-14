using System.Collections.Generic;

public enum PrimitiveType
{
    CharType,
    StringType,
    Int32Type,
    DoubleType,
    FloatType,
    BoolType,
    AudioClipType,
    ColorType,
    GameObjectType,
    TransformType,
    Texture2D,
    MaterialType,
    CustomType,
}

public static class TypeString
{
    public static readonly Dictionary<PrimitiveType, string> TypesDictionary = new Dictionary<PrimitiveType, string>
    {
        { PrimitiveType.StringType, "string" },
        { PrimitiveType.CharType, "char" },
        { PrimitiveType.Int32Type, "int" },
        { PrimitiveType.DoubleType, "double" },
        { PrimitiveType.FloatType, "float" },
        { PrimitiveType.BoolType, "bool" },
        { PrimitiveType.AudioClipType, "AudioClip" },
        { PrimitiveType.ColorType, "Color" },
        { PrimitiveType.GameObjectType, "GameObject" },
        { PrimitiveType.MaterialType, "Material" },
        { PrimitiveType.TransformType, "Transform" },
        { PrimitiveType.Texture2D, "Texture2D" },
    };
}