using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphColoring
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            var graphs = ReadInput();

            foreach (var ((adjacencyMatrix, colorCount), i) in graphs.Select((g, i) => (g, i)))
            {
                var vertexColors = ColorGraph(adjacencyMatrix, colorCount);

                if (i != 0)
                {
                    Console.WriteLine();
                }
                Console.Write(vertexColors.Any() ? string.Join(' ', vertexColors) : "NIE");
            }
        }

        public static IEnumerable<(bool[,] adjacencyMatrix, int colorCount)> ReadInput()
        {
            var graphCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < graphCount; i++)
            {
                var row = Console.ReadLine().Split(' ');

                var vertexCount = int.Parse(row[0]);
                var colorCount = int.Parse(row[1]);

                var adjacencyMatrix = new bool[vertexCount, vertexCount];

                for (int from = 0; from < vertexCount; from++)
                {
                    row = Console.ReadLine().Split(' ');

                    for (int to = 0; to < vertexCount; to++)
                    {
                        adjacencyMatrix[from, to] = Convert.ToBoolean(int.Parse(row[to]));
                    }
                }

                yield return (adjacencyMatrix, colorCount);
            }
        }

        public static IEnumerable<int> ColorGraph(bool[,] adjacencyMatrix, int colorCount)
        {
            var vertexCount = adjacencyMatrix.GetLength(0);

            var colors = new int[vertexCount];

            IEnumerable<int> NeighbouringColors(int vertex)
            {
                for (int potentialNeighbour = 0; potentialNeighbour < vertexCount; potentialNeighbour++)
                {
                    if (adjacencyMatrix[vertex, potentialNeighbour])
                    {
                        var color = colors[potentialNeighbour];

                        if (color != 0)
                        {
                            yield return color;
                        }
                    }
                }
            }

            bool ColorVertices(int vertex = 0)
            {
                for (int color = 1; color <= colorCount; color++)
                {
                    if (vertex == 0 && color == 2)
                    {
                        break;
                    }

                    if (!NeighbouringColors(vertex).Contains(color))
                    {
                        colors[vertex] = color;

                        if (vertex == vertexCount - 1 || ColorVertices(vertex + 1))
                        {
                            return true;
                        }

                        colors[vertex] = 0;
                    }
                }

                return false;
            }

            return ColorVertices() ? colors : Enumerable.Empty<int>();
        }
    }
}
