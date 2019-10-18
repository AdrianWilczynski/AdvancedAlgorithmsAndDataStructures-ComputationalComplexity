using System;
using System.Linq;

namespace GraphProperties
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            var graphs = Input.Read();

            foreach (var (graph, i) in graphs.Select((g, i) => (g, i)))
            {
                var isBipartite = GraphProperties.IsBipartite(graph);
                var (isConnected, subgraphCount) = GraphProperties.IsConnected(graph);
                var isCyclic = GraphProperties.IsCyclic(graph);
                var isTree = GraphProperties.IsTree(isConnected, isCyclic);

                static string YesOrNo(bool value) => value ? "TAK" : "NIE";

                if (i != 0)
                {
                    Console.WriteLine();
                }

                Console.WriteLine($"Graf {i + 1}");
                Console.WriteLine($"Dwudzielny {YesOrNo(isBipartite)}");
                Console.WriteLine($"Spojny {YesOrNo(isConnected)} ({subgraphCount})");
                Console.WriteLine($"Cykle {YesOrNo(isCyclic)}");
                Console.Write($"Drzewo {YesOrNo(isTree)}");
            }
        }
    }
}
