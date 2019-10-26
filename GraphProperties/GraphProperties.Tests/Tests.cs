using System.Collections.Generic;
using Xunit;

namespace GraphProperties.Tests
{
    public class Tests
    {
        public static IEnumerable<object[]> TestData = new[]
        {
            new object[]
            {
                new TestData(
                    graph: new Graph(Converter.ToBooleanArray(new int[10, 10]
                        {
                            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 1, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 }
                        })),
                    isBipartite: true,
                    isConnected: true,
                    subgraphCount: 1,
                    isCyclic: false,
                    isTree: true)
            },
            new object[]
            {
                new TestData(
                    graph: new Graph(Converter.ToBooleanArray(new int[8, 8]
                        {
                            { 0, 1, 1, 0, 0, 0, 0, 0 },
                            { 1, 0, 1, 0, 0, 0, 0, 0 },
                            { 1, 1, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 1, 1, 0, 0 },
                            { 0, 0, 0, 1, 0, 1, 0, 0 },
                            { 0, 0, 0, 1, 1, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 1 },
                            { 0, 0, 0, 0, 0, 0, 1, 0 }
                        })),
                    isBipartite: false,
                    isConnected: false,
                    subgraphCount: 3,
                    isCyclic: true,
                    isTree: false)
            },
            new object[]
            {
                new TestData(
                    graph: new Graph(Converter.ToBooleanArray(new int[3, 3]
                        {
                            { 0, 0, 1 },
                            { 0, 0, 0 },
                            { 1, 0, 0 }
                        })),
                    isBipartite: true,
                    isConnected: false,
                    subgraphCount: 2,
                    isCyclic: false,
                    isTree: false)
            },
            new object[]
            {
                new TestData(
                    graph: new Graph(Converter.ToBooleanArray(new int[7, 7]
                        {
                            { 0, 1, 0, 0, 0, 0, 0 },
                            { 1, 0, 1, 0, 0, 0, 0 },
                            { 0, 1, 0, 1, 0, 0, 0 },
                            { 0, 0, 1, 0, 1, 0, 1 },
                            { 0, 0, 0, 1, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 1 },
                            { 0, 0, 0, 1, 0, 1, 0 }
                        })),
                    isBipartite: true,
                    isConnected: true,
                    subgraphCount: 1,
                    isCyclic: false,
                    isTree: true)
            },
            new object[]
            {
                new TestData(
                    graph: new Graph(Converter.ToBooleanArray(new int[5, 5]
                        {
                            { 0, 1, 1, 0, 0 },
                            { 1, 0, 1, 0, 0 },
                            { 1, 1, 0, 0, 0 },
                            { 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0 }
                        })),
                    isBipartite: false,
                    isConnected: false,
                    subgraphCount: 3,
                    isCyclic: true,
                    isTree: false)
            },
            new object[]
            {
                new TestData(
                    graph: new Graph(Converter.ToBooleanArray(new int[1, 1]
                        {
                            { 0 }
                        })),
                    isBipartite: true,
                    isConnected: true,
                    subgraphCount: 1,
                    isCyclic: false,
                    isTree: true)
            },
            new object[]
            {
                new TestData(
                    graph: new Graph(Converter.ToBooleanArray(new int[2, 2]
                        {
                            { 0, 1 },
                            { 1, 0 }
                        })),
                    isBipartite: true,
                    isConnected: true,
                    subgraphCount: 1,
                    isCyclic: false,
                    isTree: true)
            },
            new object[]
            {
                new TestData(
                    graph: new Graph(Converter.ToBooleanArray(new int[4, 4]
                        {
                            { 0, 0, 0, 0 },
                            { 0, 0, 0, 0 },
                            { 0, 0, 0, 0 },
                            { 0, 0, 0, 0 }
                        })),
                    isBipartite: true,
                    isConnected: false,
                    subgraphCount: 4,
                    isCyclic: false,
                    isTree: false)
            },
            new object[]
            {
                new TestData(
                    graph: new Graph(Converter.ToBooleanArray(new int[4, 4]
                        {
                            { 0, 1, 1, 1 },
                            { 1, 0, 1, 1 },
                            { 1, 1, 0, 1 },
                            { 1, 1, 1, 0 }
                        })),
                    isBipartite: false,
                    isConnected: true,
                    subgraphCount: 1,
                    isCyclic: true,
                    isTree: false)
            },
            new object[]
            {
                new TestData(
                    graph: new Graph(Converter.ToBooleanArray(new int[5, 5]
                        {
                            { 0, 1, 0, 0, 0 },
                            { 1, 0, 0, 0, 0 },
                            { 0, 0, 0, 1, 1 },
                            { 0, 0, 1, 0, 1 },
                            { 0, 0, 1, 1, 0 }
                        })),
                    isBipartite: false,
                    isConnected: false,
                    subgraphCount: 2,
                    isCyclic: true,
                    isTree: false)
            },
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void IsBipartite(TestData testGraph)
            => Assert.Equal(testGraph.IsBipartite, GraphProperties.IsBipartite(testGraph.Graph));

        [Theory]
        [MemberData(nameof(TestData))]
        public void IsConnected(TestData testGraph)
        {
            var (isConnected, subgraphCount) = GraphProperties.IsConnected(testGraph.Graph);

            Assert.Equal(testGraph.IsConnected, isConnected);
            Assert.Equal(testGraph.SubgraphCount, subgraphCount);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void IsCyclic(TestData testGraph)
            => Assert.Equal(testGraph.IsCyclic, GraphProperties.IsCyclic(testGraph.Graph));

        [Theory]
        [MemberData(nameof(TestData))]
        public void IsTree(TestData testGraph)
            => Assert.Equal(
                    testGraph.IsTree,
                    GraphProperties.IsTree(
                        GraphProperties.IsConnected(testGraph.Graph).isConnected,
                        GraphProperties.IsCyclic(testGraph.Graph)));
    }
}
