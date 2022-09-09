using System;
using System.Collections.Generic;
using System.Linq;

public record Egde(int from, int to, int weight) : IComparable<Egde>
{
    public int CompareTo(Egde other)
    {
        var weightComparison = weight - other.weight;
        if (weightComparison != 0) return weightComparison;
        var fromComparison = from - other.from;
        if (fromComparison != 0) return fromComparison;
        return to - other.to;
    }
}

class Program
{
    public static HashSet<int> added = new();
    public static int resultWeihgt = 0;
    public static HashSet<int> notAdded = new();
    public static SortedSet<Egde> edges = new();
    public static HashSet<Egde> graphEdges = new();


    public static void AddVertex(int v)
    {
        added.Add(v);
        notAdded.Remove(v);
        foreach (var edge in graphEdges)
        {
            if (
                (edge.from == v || edge.to == v)
                &&
                (notAdded.Contains(edge.to) || notAdded.Contains(edge.from))
            )
            {
                if (
                    (added.Contains(edge.to) && added.Contains(edge.from))
                    ||
                    (added.Contains(edge.from) && added.Contains(edge.to))
                )
                {
                    graphEdges.Remove(edge);
                    continue;
                }

                edges.Add(edge);
            }
        }
    }


    static void Main(string[] args)
    {
        var vertexEdge = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        if (vertexEdge[0] - 1 > vertexEdge[1])
        {
            Console.WriteLine("Oops! I did it again");
            return;
        }

        for (int i = 0; i < vertexEdge[1]; i++)
        {
            var edge = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            notAdded.Add(edge[0]);
            notAdded.Add(edge[1]);
            graphEdges.Add(new Egde(edge[0], edge[1], edge[2]));
            // graphEdges.Add(new Egde(edge[1], edge[0], edge[2]));
        }

        // notAdded = graphVertices;
        if (notAdded.Count > 0)
        {
            var v = notAdded.First();
            AddVertex(v);
            while (notAdded.Count > 0 && edges.Count > 0)
            {
                var e = edges.Max;
                edges.Remove(e);

                if (notAdded.Contains(e.to) || notAdded.Contains(e.from))
                {
                    resultWeihgt += e.weight;
                    AddVertex((notAdded.Contains(e.to)) ? e.to : e.from);
                }
            }
        }

        if (notAdded.Count > 0)
        {
            Console.WriteLine("Oops! I did it again");
        }
        else
        {
            Console.WriteLine(resultWeihgt);
        }
    }
}