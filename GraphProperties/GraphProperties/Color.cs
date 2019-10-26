using System;

namespace GraphProperties
{
    public enum Color
    {
        Grey = 0,
        Red,
        Blue
    }

    public static class ColorExtensions
    {
        public static Color Opposite(this Color color)
            => color switch
            {
                Color.Red => Color.Blue,
                Color.Blue => Color.Red,
                _ => throw new ArgumentException()
            };
    }
}