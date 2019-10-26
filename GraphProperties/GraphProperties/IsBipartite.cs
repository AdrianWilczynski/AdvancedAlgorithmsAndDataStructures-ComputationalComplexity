using System.Collections.Generic;

namespace GraphProperties
{
    public static partial class GraphProperties
    {
        public static bool IsBipartite(Graph graph)
        {
            var queue = new Queue<int>();
            var colors = new Color[graph.VertexCount];

            foreach (var vertex in graph.Vertices)
            {
                if (colors[vertex] == Color.Grey)
                {
                    colors[vertex] = Color.Red;
                    queue.Enqueue(vertex);
                }

                while (queue.Count > 0)
                {
                    var dequeuedVertex = queue.Dequeue();

                    foreach (var neighbour in graph.Neighbours(dequeuedVertex))
                    {
                        if (colors[dequeuedVertex] == colors[neighbour])
                        {
                            return false;
                        }

                        if (colors[neighbour] == Color.Grey)
                        {
                            colors[neighbour] = colors[dequeuedVertex].Opposite();
                            queue.Enqueue(neighbour);
                        }
                    }
                }
            }

            return true;
        }
    }
}