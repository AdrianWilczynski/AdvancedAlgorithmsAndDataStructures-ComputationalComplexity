using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimumSpanningTree
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            var graphs = ReadInput();

            foreach (var (graph, i) in graphs.Select((g, i) => (g, i)))
            {
                if (i != 0)
                {
                    Console.WriteLine();
                }
                Console.Write(MinimumSpanningTreeWeight(graph)?.ToString() ?? "brak");
            }
        }

        public static IEnumerable<int[,]> ReadInput()
        {
            var graphCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < graphCount; i++)
            {
                var row = Console.ReadLine().Split(' ');

                var vertexCount = int.Parse(row[0]);
                var edgeCount = int.Parse(row[1]);

                var adjacencyMatrix = new int[vertexCount, vertexCount];

                for (int j = 0; j < edgeCount; j++)
                {
                    row = Console.ReadLine().Split(' ');

                    var firstVertex = int.Parse(row[0]) - 1;
                    var secondVertex = int.Parse(row[1]) - 1;

                    var weight = int.Parse(row[2]);

                    adjacencyMatrix[firstVertex, secondVertex] = adjacencyMatrix[secondVertex, firstVertex] = weight;
                }

                yield return adjacencyMatrix;
            }
        }

        public static int? MinimumSpanningTreeWeight(int[,] adjacencyMatrix)
        {
            var vertexCount = adjacencyMatrix.GetLength(0);

            var included = new bool[vertexCount];
            var costsOfIncluding = (Enumerable.Repeat((isReachable: false, value: int.MaxValue), vertexCount)).ToArray();

            var totalWeight = 0;

            int? Cheapest()
            {
                int? cheapestVertex = null;
                var minCost = int.MaxValue;

                for (int vertex = 0; vertex < vertexCount; vertex++)
                {
                    var (isReachable, cost) = costsOfIncluding[vertex];

                    if (!included[vertex] && isReachable && cost <= minCost)
                    {
                        cheapestVertex = vertex;
                        minCost = cost;
                    }
                }

                return cheapestVertex;
            }

            void UpdateCosts(int fromVertex)
            {
                for (int toVertex = 0; toVertex < vertexCount; toVertex++)
                {
                    var updatedCost = adjacencyMatrix[fromVertex, toVertex];
                    var isReachable = updatedCost != 0;

                    if (!included[toVertex] && isReachable && updatedCost <= costsOfIncluding[toVertex].value)
                    {
                        costsOfIncluding[toVertex] = (isReachable: true, updatedCost);
                    }
                }
            }

            costsOfIncluding[0] = (isReachable: true, value: 0);

            for (int i = 0; i < vertexCount; i++)
            {
                if (Cheapest() is int vertex)
                {
                    included[vertex] = true;
                    totalWeight += costsOfIncluding[vertex].value;

                    UpdateCosts(vertex);
                }
                else
                {
                    return null;
                }
            }

            return totalWeight;
        }
    }
}