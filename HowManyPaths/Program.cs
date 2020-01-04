#pragma warning disable CS1591, CS8509

using System;
using System.Collections.Generic;
using System.Linq;

namespace HowManyPaths
{
    public static class Program
    {
        /// <summary>
        /// Count all possible paths from center to target.
        /// </summary>
        /// <param name="n">Board size</param>
        /// <param name="x">Target x possition</param>
        /// <param name="y">Target y possition</param>
        /// <param name="d">Number of moves</param>
        /// <param name="method">Method</param>
        public static void Main(int n, int x, int y, int d, Method method)
        {
            if (n <= d / 2)
            {
                Console.WriteLine($"Expected {nameof(n)} > {nameof(d)} / 2.");
                return;
            }

            var numberOfPaths = method switch
            {
                Method.Dynamic => Dynamic(n, (x, y), d),
                Method.Recursive => Recursive(n, (x, y), d)
            };

            Console.WriteLine($"Number of paths: {numberOfPaths}");
        }

        public static int Dynamic(int boardSize, (int x, int y) target, int numberOfMoves)
        {
            var pathsPerField = new int[boardSize, boardSize];

            var center = Center(boardSize);

            pathsPerField.SetInRange(center.Left(), 1);
            pathsPerField.SetInRange(center.Up(), 1);
            pathsPerField.SetInRange(center.Right(), 1);
            pathsPerField.SetInRange(center.Down(), 1);

            for (var _ = 1; _ < numberOfMoves; _++)
            {
                var updatedPathsPerField = new int[boardSize, boardSize];

                for (var x = 0; x < boardSize; x++)
                {
                    for (var y = 0; y < boardSize; y++)
                    {
                        var possition = (x, y);

                        var updatedValue = pathsPerField.GetInRange(possition.Left())
                            + pathsPerField.GetInRange(possition.Up())
                            + pathsPerField.GetInRange(possition.Right())
                            + pathsPerField.GetInRange(possition.Down());

                        updatedPathsPerField.SetInRange(possition, updatedValue);
                    }
                }

                pathsPerField = updatedPathsPerField;
            }

            return pathsPerField.GetInRange(target);
        }

        public static int Recursive(int boardSize, (int x, int y) target, int numberOfMoves)
        {
            var numberOfPaths = 0;

            void Visit((int x, int y) possition, int move = 0, IEnumerable<(int x, int y)> path = null)
            {
                path = (path ?? Enumerable.Empty<(int, int)>()).Append(possition);

                if (!possition.IsInRange(boardSize))
                    return;

                if (move == numberOfMoves)
                {
                    if (possition == target)
                    {
                        numberOfPaths++;

                        Console.WriteLine($"Path: {string.Join(" - ", path)}");
                    }

                    return;
                }

                move++;

                Visit(possition.Left(), move, path);
                Visit(possition.Up(), move, path);
                Visit(possition.Right(), move, path);
                Visit(possition.Down(), move, path);
            }

            Visit(Center(boardSize));

            return numberOfPaths;
        }

        private static (int x, int y) Center(int boardSize)
            => ((boardSize - 1) / 2, (boardSize - 1) / 2);
    }

    public enum Method { Dynamic, Recursive }

    internal static class BoardExtensions
    {
        public static void SetInRange(this int[,] array, (int x, int y) possition, int value)
        {
            if (possition.IsInRange(array.GetLength(0)))
                array[possition.x, possition.y] = value;
        }

        public static int GetInRange(this int[,] array, (int x, int y) possition)
            => possition.IsInRange(array.GetLength(0)) ? array[possition.x, possition.y] : 0;
    }

    internal static class PossitionExtensions
    {
        public static (int, int) Left(this (int x, int y) possition) => (possition.x - 1, possition.y);
        public static (int, int) Up(this (int x, int y) possition) => (possition.x, possition.y + 1);
        public static (int, int) Right(this (int x, int y) possition) => (possition.x + 1, possition.y);
        public static (int, int) Down(this (int x, int y) possition) => (possition.x, possition.y - 1);

        public static bool IsInRange(this (int x, int y) possition, int boardSize)
            => possition.x >= 0 && possition.x < boardSize
            && possition.y >= 0 && possition.y < boardSize;
    }
}