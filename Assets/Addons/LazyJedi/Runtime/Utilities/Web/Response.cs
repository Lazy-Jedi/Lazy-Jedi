using UnityEngine;

namespace LazyJedi.Common
{
    public class Response<T>
    {
        public long StatusCode { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
    }

    public class AudioResponse : Response<byte[]>
    {
        public AudioClip AudioClip;
    }

    public class Texture2DResponse : Response<byte[]>
    {
        public Texture2D Texture2D;

        public Sprite GetSpriteFromTexture(Vector2 pivot, float pixelsPerUnit)
        {
            return Sprite.Create(Texture2D, new Rect(0, 0, Texture2D.width, Texture2D.height), pivot, pixelsPerUnit);
        }

        public byte[] EncodeTexture2D(TextureType type)
        {
            switch (type)
            {
                case TextureType.PNG:
                    return Texture2D.EncodeToPNG();
                case TextureType.JPG:
                    return Texture2D.EncodeToJPG();
                case TextureType.EXR:
                    return Texture2D.EncodeToEXR();
                case TextureType.TGA:
                    return Texture2D.EncodeToTGA();
            }

            return Texture2D.GetRawTextureData();
        }
    }
}