using System.Collections.Generic;

namespace GraphProperties
{
    public static partial class GraphProperties
    {
        public static (bool isConnected, int subgraphCount) IsConnected(Graph graph)
        {
            var subgraphs = new int[graph.VertexCount];
            var subgraphCount = 0;

            foreach (var vertex in graph.Vertices)
            {
                if (subgraphs[vertex] != 0)
                {
                    continue;
                }

                subgraphCount++;

                var stack = new Stack<int>(new[] { vertex });

                while (stack.Count > 0)
                {
                    var poppedVertex = stack.Pop();

                    foreach (var neighbour in graph.Neighbours(poppedVertex))
                    {
                        if (subgraphs[neighbour] == 0)
                        {
                            stack.Push(neighbour);
                            subgraphs[neighbour] = subgraphCount;
                        }
                    }
                }
            }

            return (subgraphCount == 1, subgraphCount);
        }
    }
}