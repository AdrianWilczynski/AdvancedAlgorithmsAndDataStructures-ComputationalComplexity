using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphProperties
{
    public class Graph
    {
        private bool[,] _adjacencyMatrix;

        public Graph(bool[,] adjacencyMatrix)
        {
            AdjacencyMatrix = adjacencyMatrix;
        }

        public bool[,] AdjacencyMatrix
        {
            get => _adjacencyMatrix;
            private set => _adjacencyMatrix =
                value.Rank == 2 && value.GetLength(0) == value.GetLength(1)
                ? value
                : throw new ArgumentException();
        }

        public int VertexCount
            => AdjacencyMatrix.GetLength(0);

        public IEnumerable<int> Vertices
            => Enumerable.Range(0, VertexCount);

        public IEnumerable<int> Neighbours(int vertex)
            => Vertices.Where(potentialNeighbour => AdjacencyMatrix[vertex, potentialNeighbour]);
    }
}