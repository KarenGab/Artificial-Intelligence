using System;
using System.Collections.Generic;

public class Item
{
    public int Weight { get; set; }
    public int Value { get; set; }
}

public class Node
{
    public int Level { get; set; }
    public int Profit { get; set; }
    public int Weight { get; set; }
    public double Bound { get; set; }
    public List<Item> SelectedItems { get; set; } = new List<Item>();
}

public class Knapsack
{
    public static double Bound(Node u, int n, int W, List<Item> items)
    {
        if (u.Weight >= W)
            return 0;

        double profitBound = u.Profit;
        int j = u.Level + 1;
        int totWeight = u.Weight;

        while ((j < n) && (totWeight + items[j].Weight <= W))
        {
            totWeight += items[j].Weight;
            profitBound += items[j].Value;
            j++;
        }

        if (j < n)
            profitBound += (W - totWeight) * items[j].Value / items[j].Weight;

        return profitBound;
    }

    public static (int, List<Item>) KnapsackAStar(int W, List<Item> items, int n)
    {
        Queue<Node> Q = new Queue<Node>();
        Node u = new Node(), v = new Node();
        u.Level = -1;
        u.Profit = u.Weight = 0;
        Q.Enqueue(u);

        int maxProfit = 0;
        List<Item> bestItems = new List<Item>();

        while (Q.Count != 0)
        {
            u = Q.Dequeue();

            if (u.Level == -1)
                v.Level = 0;

            if (u.Level == n - 1)
                continue;

            v.Level = u.Level + 1;

            v.Weight = u.Weight + items[v.Level].Weight;
            v.Profit = u.Profit + items[v.Level].Value;
            v.SelectedItems = new List<Item>(u.SelectedItems) { items[v.Level] };

            if (v.Weight <= W && v.Profit > maxProfit)
            {
                maxProfit = v.Profit;
                bestItems = v.SelectedItems;
            }

            v.Bound = Bound(v, n, W, items);

            if (v.Bound > maxProfit)
                Q.Enqueue(new Node { Level = v.Level, Profit = v.Profit, Weight = v.Weight, Bound = v.Bound, SelectedItems = new List<Item>(v.SelectedItems) });

            v.Weight = u.Weight;
            v.Profit = u.Profit;
            v.SelectedItems = new List<Item>(u.SelectedItems);
            v.Bound = Bound(v, n, W, items);

            if (v.Bound > maxProfit)
                Q.Enqueue(new Node { Level = v.Level, Profit = v.Profit, Weight = v.Weight, Bound = v.Bound, SelectedItems = new List<Item>(v.SelectedItems) });
        }

        return (maxProfit, bestItems);
    }

    public static void Main()
    {
        int W = 50; // Capacidad de la mochila
        List<Item> items = new List<Item>
        {
            new Item { Weight = 10, Value = 60 },
            new Item { Weight = 5, Value = 12 },
            new Item { Weight = 1, Value = 30 },
            new Item { Weight = 20, Value = 50 },
            new Item { Weight = 23, Value = 80 },
            new Item { Weight = 40, Value = 100 },
            new Item { Weight = 14, Value = 60 },
            new Item { Weight = 4, Value = 70 },
            new Item { Weight = 10, Value = 40 },
            new Item { Weight = 8, Value = 72 },
            new Item { Weight = 6, Value = 40 },
            new Item { Weight = 12, Value = 90 },
            new Item { Weight = 13, Value = 55 },
            new Item { Weight = 15, Value = 101 },
            new Item { Weight = 22, Value = 300 },
            new Item { Weight = 30, Value = 45 },
            new Item { Weight = 2, Value = 16 },
            new Item { Weight = 8, Value = 150 },
            new Item { Weight = 26, Value = 400 }
        };

        int n = items.Count;
        var result = KnapsackAStar(W, items, n);
        Console.WriteLine("Valor máximo en la mochila = " + result.Item1);
        Console.WriteLine("Elementos seleccionados:");

        foreach (var item in result.Item2)
        {
            Console.WriteLine($"Peso: {item.Weight}, Valor: {item.Value}");
        }
    }
}