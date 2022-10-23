using UnityEngine;

#if UNITY_EDITOR
namespace LazyJedi.Editors.Internal
{
    public static class LazyEditorArt
    {
        #region ARTWORK

        public static Texture2D LazyJediLiteLogo
        {
            get => Resources.Load<Texture2D>("Icons/light-saber-light");
        }

        public static Texture2D LazyJediDarkLogo
        {
            get => Resources.Load<Texture2D>("Icons/light-saber-dark");
        }

        #endregion

        #region FONT

        /// <summary>
        /// Kenney Mini Square Font
        /// </summary>
        public static Font KenneyMiniSquareFont
        {
            get => Resources.Load<Font>("Fonts/kenney-fonts/MiniSquare");
        }

        #endregion
    }
}
#endif