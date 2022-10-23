using System.Runtime.CompilerServices;
using UnityEngine;

namespace LazyJedi.Globals
{
    public static class LazyColors
    {
        #region PROPERTIES

        public static Color UnityFontColorDark
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new Color(0.1803922f, 0.1803922f, 0.1803922f);
        }

        public static Color UnityFontColorLite
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new Color(0.8431373f, 0.8431373f, 0.8431373f);
        }

        #endregion
    }
}