using System.Collections.Generic;

namespace GraphProperties
{
    public static partial class GraphProperties
    {
        public static bool IsCyclic(Graph graph)
        {
            var visited = new bool[graph.VertexCount];

            foreach (var vertex in graph.Vertices)
            {
                if (visited[vertex])
                {
                    continue;
                }

                var stack = new Stack<(int current, int? preceding)>(new[] { (vertex, (int?)null) });

                visited[vertex] = true;

                while (stack.Count > 0)
                {
                    var (current, preceding) = stack.Pop();

                    foreach (var neighbour in graph.Neighbours(current))
                    {
                        if (!visited[neighbour])
                        {
                            stack.Push((current: neighbour, preceding: current));

                            visited[neighbour] = true;
                        }
                        else if (neighbour != preceding)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}