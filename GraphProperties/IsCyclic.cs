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

                var stack = new Stack<int>();
                stack.Push(vertex);
                stack.Push(-1);

                visited[vertex] = true;

                while (stack.Count > 0)
                {
                    var preceding = stack.Pop();
                    var current = stack.Pop();

                    foreach (var neighbour in graph.Neighbours(current))
                    {
                        if (!visited[neighbour])
                        {
                            stack.Push(neighbour);
                            stack.Push(current);

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