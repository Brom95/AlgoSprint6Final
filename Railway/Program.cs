// https://contest.yandex.ru/contest/25070/run-report/70197228/
/*
 -- ПРИНЦИП РАБОТЫ -- 
 Я реализовал проверку схемы на оптимальность за счет инвертирования направления одного из типов дорог и поиска циклов по алгоритму обхода в глубину в получившемся графе.

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ -- 
Карта железных дорог называется оптимальной, если не существует пары городов A и B такой, что от A до B можно добраться как по дорогам типа R, так и по дорогам типа B. Иными словами, для любой пары городов верно, что от города с меньшим номером до города с бОльшим номером можно добраться по дорогам только какого-то одного типа или же что маршрут построить вообще нельзя.
Отсюда следует, что в случае, если карта дорог оптимально, то инвертирование направления одного из типов дорог не должно создавать циклы в итоговом графе, так как в противном случае это бы значило, что существует альтернативный моршрут достижения города, что противоречит условию оптимальности дорожной сети.

-- ВРЕМЕННАЯ СЛОЖНОСТЬ -- 
Заполнение графа осуществляется за О(N(N-1)/2) (формула суммы членов арифметической прогрессии) ~ O(N*N+N) ~ O(N^2), где N - количество городов. Так как число ребер квадратично зависит от числа вершин, то временная сложность обхода в глубину согласно https://practicum.yandex.ru/learn/algorithms/courses/7f101a83-9539-4599-b6e8-8645c3f31fad/sprints/49973/topics/45179065-a73b-473d-94d1-24774573f266/lessons/02ea981b-a081-4993-9df3-b4055c0749e0/ составит O(N^2).
Таким образом суммарная временная сложность алгоритма в худшем случае составит O(N^2 + N^2) = O(2N^2) ~ O(N^2). 
 

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ -- 
Для хранения графа требуется O(N^2) памяти, хранение цветов требует O(N) памяти. => O(
 
*/

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