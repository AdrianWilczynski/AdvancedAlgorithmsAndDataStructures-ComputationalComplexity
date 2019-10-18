namespace GraphProperties.Tests
{
    public class TestData
    {
        public TestData(Graph graph, bool isBipartite, bool isConnected, int subgraphCount, bool isCyclic, bool isTree)
        {
            Graph = graph;
            IsBipartite = isBipartite;
            IsConnected = isConnected;
            SubgraphCount = subgraphCount;
            IsCyclic = isCyclic;
            IsTree = isTree;
        }

        public Graph Graph { get; set; }

        public bool IsBipartite { get; set; }

        public bool IsConnected { get; set; }
        public int SubgraphCount { get; set; }

        public bool IsCyclic { get; set; }

        public bool IsTree { get; set; }
    }
}