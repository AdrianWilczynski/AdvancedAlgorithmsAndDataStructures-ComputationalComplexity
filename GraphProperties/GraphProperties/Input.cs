using System;
using System.Collections.Generic;

namespace GraphProperties
{
    public static class Input
    {
        public static IEnumerable<Graph> Read()
        {
            var graphCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < graphCount; i++)
            {
                var vertexCount = int.Parse(Console.ReadLine());

                var adjacencyMatrix = new bool[vertexCount, vertexCount];

                for (int r = 0; r < vertexCount; r++)
                {
                    var row = Console.ReadLine().Split(' ');

                    for (int c = 0; c < vertexCount; c++)
                    {
                        adjacencyMatrix[r, c] = Convert.ToBoolean(int.Parse(row[c]));
                    }
                }

                yield return new Graph(adjacencyMatrix);
            }
        }
    }
}