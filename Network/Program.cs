using System;
using System.Collections.Generic;
using System.Linq;

public record Egde(int from, int to, int weight);


class Program
{
    public static HashSet<int> added = new();

    // public static HashSet<int> maximumTreeEdges = new();
    public static int resultWeihgt = 0;
    public static HashSet<int> notAdded = new();
    public static List<Egde> edges = new();
    public static List<Egde> graphEdges = new();
    public static HashSet<int> graphVertices = new();

    public static void AddVertex(int v)
    {
        added.Add(v);
        notAdded.Remove(v);
        edges.AddRange(graphEdges.Where(e =>
            (e.from == v && notAdded.Contains(e.to)) || (e.to == v && notAdded.Contains(e.from))));
    }


    static void Main(string[] args)
    {
        var vertexEdge = Console.ReadLine().Split().Select(int.Parse).ToArray();
        for (int i = 0; i < vertexEdge[1]; i++)
        {
            var edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
            graphVertices.Add(edge[0]);
            graphVertices.Add(edge[1]);
            graphEdges.Add(new Egde(edge[0], edge[1], edge[2]));
        }

        notAdded = graphVertices;
        if (notAdded.Any())
        {
            var v = graphVertices.First();
            AddVertex(v);
            while (notAdded.Any() && edges.Any())
            {
                // var e = edges.MaxBy(edge => edge.weight);
                Egde e = new Egde(0, 0, 0);
                foreach (var edge in edges)
                {
                    if (edge.weight >= e.weight)
                        e = edge;
                }

                edges.Remove(e);
                if (notAdded.Contains(e.from) || notAdded.Contains(e.to))
                {
                    resultWeihgt += e.weight;
                    AddVertex((notAdded.Contains(e.from)) ? e.from : e.to);
                }
            }
        }

        if (notAdded.Any() || resultWeihgt == 0)
        {
            Console.WriteLine("Oops! I did it again");
        }
        else
        {
            Console.WriteLine(resultWeihgt);
        }
    }
}