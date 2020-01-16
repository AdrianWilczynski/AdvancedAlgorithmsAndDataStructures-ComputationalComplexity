using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace ThreeSATToHamiltonianPath
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            var clauses = new[]
            {
                new [] { new Variable("a1", negation: true), new Variable("b1"), new Variable("c1") },
                new [] { new Variable("a2"), new Variable("b2"), new Variable("c2", negation: true) },
                new [] { new Variable("a3"), new Variable("b3"), new Variable("c3") }
            };

            var variables = clauses
                .SelectMany(c => c)
                .Select(v => v.Name)
                .Distinct()
                .ToArray();

            var edges = new List<Edge>();

            var verticalVertex = Vertex.S;

            foreach (var (l, variable) in variables.Index())
            {
                edges.Add(verticalVertex, Vertex.XLeft(l));
                edges.Add(verticalVertex, Vertex.XRight(l));

                var separatorVertex = Vertex.Separator(l, 0);

                edges.AddBothWays(Vertex.XLeft(l), separatorVertex);

                foreach (var (k, clause) in clauses.Index())
                {
                    edges.AddBothWays(separatorVertex, Vertex.CHorizontalLeft(l, k));
                    edges.AddBothWays(Vertex.CHorizontalLeft(l, k), Vertex.CHorizontalRight(l, k));

                    if (clause.HasVariable(variable, out var found))
                    {
                        if (!found.Negation)
                        {
                            edges.Add(Vertex.CHorizontalLeft(l, k), Vertex.C(k));
                            edges.Add(Vertex.C(k), Vertex.CHorizontalRight(l, k));
                        }
                        else
                        {
                            edges.Add(Vertex.C(k), Vertex.CHorizontalLeft(l, k));
                            edges.Add(Vertex.CHorizontalRight(l, k), Vertex.C(k));
                        }
                    }

                    separatorVertex = Vertex.Separator(l, k + 1);

                    edges.AddBothWays(Vertex.CHorizontalRight(l, k), separatorVertex);
                }

                edges.AddBothWays(separatorVertex, Vertex.XRight(l));

                verticalVertex = l == variables.Length - 1
                    ? Vertex.T
                    : Vertex.Vertical(l);

                edges.Add(Vertex.XLeft(l), verticalVertex);
                edges.Add(Vertex.XRight(l), verticalVertex);
            }

            foreach (var edge in edges)
            {
                Console.WriteLine(edge);
            }
        }
    }

    public static class Vertex
    {
        public static string S => "S";
        public static string T => "T";

        public static string XLeft(int index) => $"X_{index}_Left";
        public static string XRight(int index) => $"X_{index}_Right";

        public static string Separator(int rowIndex, int index) => $"Separator_{rowIndex}_{index}";

        public static string CHorizontalLeft(int rowIndex, int index) => $"C_{rowIndex}_{index}_Horizontal_Left";
        public static string CHorizontalRight(int rowIndex, int index) => $"C_{rowIndex}_{index}_Horizontal_Right";

        public static string C(int index) => $"C_{index}";

        public static string Vertical(int index) => $"V_{index}";
    }

    #region Models
    public class Variable
    {
        public Variable(string name, bool negation = false)
        {
            Name = name;
            Negation = negation;
        }

        public string Name { get; set; }
        public bool Negation { get; set; }
    }

    public class Edge
    {
        public Edge(string from, string to)
        {
            From = from;
            To = to;
        }

        public string From { get; set; }
        public string To { get; set; }

        public override string ToString()
            => $"{From} {To}";
    }
    #endregion

    #region Extensions
    public static class EdgesExtensions
    {
        public static void Add(this List<Edge> edges, string from, string to)
            => edges.Add(new Edge(from, to));

        public static void AddBothWays(this List<Edge> edges, string first, string second)
        {
            edges.Add(new Edge(first, second));
            edges.Add(new Edge(second, first));
        }
    }

    public static class ClauseExtensions
    {
        public static bool HasVariable(this Variable[] clause, string name, out Variable variable)
        {
            variable = clause[0].Name == name ? clause[0]
                : clause[1].Name == name ? clause[1]
                : clause[2].Name == name ? clause[2]
                : null;

            return variable != null;
        }
    }
    #endregion
}