using System;

namespace GraphProperties.Tests
{
    public static class Converter
    {
        public static bool[,] ToBooleanArray(int[,] array)
        {
            var converted = new bool[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    converted[i, j] = Convert.ToBoolean(array[i, j]);
                }
            }

            return converted;
        }
    }
}