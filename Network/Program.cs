// https://contest.yandex.ru/contest/25070/run-report/70046313/
/*
-- ПРИНЦИП РАБОТЫ -- 
 Я реализовал вычисление веса максимального остовного дерева по алгоритму Прима (https://practicum.yandex.ru/learn/algorithms/courses/7f101a83-9539-4599-b6e8-8645c3f31fad/sprints/49973/topics/45179065-a73b-473d-94d1-24774573f266/lessons/adb9a06e-f8a5-4d9b-b88a-2085cc8458f9/) на красно-черном дереве  (SortedSet)

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ -- 
Алгоритм Прима позволяет находить минимальное/максимальное остоновное дерево, таким образом, сумма весов ребер полученная в результате работы алгоритма будет весом минимального/максимального остовного дерева.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ -- 
Заполнение графа осуществляется за O(|E|).
Cложность алгоритма Прима O(∣E∣⋅log∣V∣), где |E| — количество рёбер в графе, а |V| — количество вершин.
Таким образом итоговая временная сложность составит O(2∣E∣⋅log∣V∣) ~ O(∣E∣⋅log∣V∣)  
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ -- 
O(|V|) для хранения добавленных вершин и O(|V|) для не добавленных вершин, O(|E|) для хранения ребер графа и O(|E|) для хранения отсортированных по весу ребер исходящих из остова => O(2|V| + 2|E|) ~ O(|V| + |E|)
 * */

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

    private static void PrimaWeightCalc()
    {
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
    }

    private static void AddVertex(int v)
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
        }

        PrimaWeightCalc();

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