using System;
using System.Collections.Generic;



class Program
{
    public static List<int>[] Graph;
    public static int[] GraphColor;

    private static bool DfsIsCycle(int startFrom)
    {
        var stack = new Stack<int>();
        stack.Push(startFrom);
        while (stack.Count > 0)
        {
            var v = stack.Pop();
            if (GraphColor[v] == 0)
            {
                GraphColor[v] = 1;
                stack.Push(v);
                foreach (var vertex in Graph[v])
                {
                    if (GraphColor[vertex] == 0)
                    {
                        stack.Push(vertex);
                    }

                    if (GraphColor[vertex] == 1)
                    {
                        return true;
                    }
                }
                continue;
            }
            if (GraphColor[v] == 1)
            {
                GraphColor[v] = 2;
            }
        }
        return false;
    }

    static void Main(string[] args)
    {
        var citiesCount = int.Parse(Console.ReadLine());
        Graph = new List<int>[citiesCount];
        GraphColor = new int[citiesCount];
        for (var i = 0; i < citiesCount; i++)
        {
            Graph[i] = new List<int>();
        }

        for (int i = 0; i < citiesCount - 1; i++)
        {
            var input = Console.ReadLine();
            for (var j = 0; j < input!.Length; j++)
            {
                if (input[j] == 'R')
                {
                    Graph[i].Add(i + j + 1);
                }
                else
                {
                    Graph[j + i + 1].Add(i);
                }
            }
        }

        for (var i = 0; i < citiesCount; i++)
        {
            if (GraphColor[i] == 0 && DfsIsCycle(i))
            {
                Console.WriteLine("NO");
                return;
            }
        }
        Console.WriteLine("YES");
    }
}