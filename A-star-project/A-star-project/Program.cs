﻿using System;
using System.Collections.Generic;
using System.Linq;
class Problem
{
    public string InitialState { get; }

    public Problem(string initialState)
    {
        InitialState = initialState;
    }

    public bool IsGoal(string state)
    {
        return state == "Bucharest";
    }

    public IEnumerable<string> Actions(string state)
    {
        var graph = new Dictionary<string, List<string>>()
        {
            { "Arad", new List<string> { "Zerind", "Timisoara", "Sibiu" } },
            { "Zerind", new List<string> { "Arad", "Oradea" } },
            { "Oradea", new List<string> { "Zerind", "Sibiu" } },
            { "Sibiu", new List<string> { "Arad", "Oradea", "Fagaras", "Rimnicu Vilcea" } },
            { "Fagaras", new List<string> { "Sibiu", "Bucharest" } },
            { "Rimnicu Vilcea", new List<string> { "Sibiu", "Pitesti", "Craiova" } },
            { "Pitesti", new List<string> { "Rimnicu Vilcea", "Craiova", "Bucharest" } },
            { "Craiova", new List<string> { "Drobeta", "Pitesti", "Rimnicu Vilcea" } },
            { "Drobeta", new List<string> { "Mehadia", "Craiova" } },
            { "Mehadia", new List<string> { "Drobeta", "Lugoj" } },
            { "Lugoj", new List<string> { "Mehadia", "Timisoara" } },
            { "Timisoara", new List<string> { "Arad", "Lugoj" } },
            { "Bucharest", new List<string> { "Fagaras", "Pitesti", "Giurgiu", "Urziceni" } },
            { "Giurgiu", new List<string> { "Bucharest" } },
            { "Urziceni", new List<string> { "Bucharest", "Hirsova", "Vaslui" } },
            { "Hirsova", new List<string> { "Urziceni", "Eforie" } },
            { "Eforie", new List<string> { "Hirsova" } },
            { "Vaslui", new List<string> { "Urziceni", "Iasi" } },
            { "Iasi", new List<string> { "Vaslui", "Neamt" } },
            { "Neamt", new List<string> { "Iasi" } }
        };

        if (graph.ContainsKey(state))
        {
            return graph[state];
        }
        else
        {
            return Enumerable.Empty<string>();
        }
    }

    public string Result(string state, string action)
    {
        // The action is the destination city in this case
        return action;
    }

    public double ActionCost(string state, string action, string nextState)
    {
        var cityPairs = new Dictionary<(string, string), double>
        {
            {("Arad", "Zerind"), 75},
            {("Zerind", "Arad"), 75},
            {("Arad", "Sibiu"), 140},
            {("Sibiu", "Arad"), 140},
            {("Arad", "Timisoara"), 118},
            {("Timisoara", "Arad"), 118},
            {("Zerind", "Oradea"), 71},
            {("Oradea", "Zerind"), 71},
            {("Oradea", "Sibiu"), 151},
            {("Sibiu", "Oradea"), 151},
            {("Timisoara", "Lugoj"), 111},
            {("Lugoj", "Timisoara"), 111},
            {("Lugoj", "Mehadia"), 70},
            {("Mehadia", "Lugoj"), 70},
            {("Mehadia", "Drobeta"), 75},
            {("Drobeta", "Mehadia"), 75},
            {("Drobeta", "Craiova"), 120},
            {("Craiova", "Drobeta"), 120},
            {("Sibiu", "Rimnicu Vilcea"), 80},
            {("Rimnicu Vilcea", "Sibiu"), 80},
            {("Sibiu", "Fagaras"), 99},
            {("Fagaras", "Sibiu"), 99},
            {("Craiova", "Pitesti"), 138},
            {("Pitesti", "Craiova"), 138},
            {("Rimnicu Vilcea", "Craiova"), 146},
            {("Craiova", "Rimnicu Vilcea"), 146},
            {("Rimnicu Vilcea", "Pitesti"), 97},
            {("Pitesti", "Rimnicu Vilcea"), 97},
            {("Bucharest", "Fagaras"), 211},
            {("Fagaras", "Bucharest"), 211},
            {("Pitesti", "Bucharest"), 101},
            {("Bucharest", "Pitesti"), 101},
            {("Giurgiu", "Bucharest"), 90},
            {("Bucharest", "Giurgiu"), 90},
            {("Neamt", "Iasi"), 87},
            {("Iasi", "Neamt"), 87},
            {("Vaslui", "Iasi"), 92},
            {("Iasi", "Vaslui"), 92},
            {("Urziceni", "Vaslui"), 142},
            {("Vaslui", "Urziceni"), 142},
            {("Bucharest", "Urziceni"), 85},
            {("Urziceni", "Bucharest"), 85},
            {("Hirsova", "Eforie"), 86},
            {("Eforie", "Hirsova"), 86},
            {("Urziceni", "Hirsova"), 98},
            {("Hirsova", "Urziceni"), 98}
        };

        return cityPairs[(state, nextState)];
    }

    public double Heuristic(string state)
    {
        var distances = new Dictionary<string, double>
        {
            {"Arad", 366},
            {"Bucharest", 0},
            {"Craiova", 160},
            {"Drobeta", 242},
            {"Eforie", 161},
            {"Fagaras", 176},
            {"Giurgiu", 77},
            {"Hirsova", 151},
            {"Iasi", 226},
            {"Lugoj", 244},
            {"Mehadia", 241},
            {"Neamt", 234},
            {"Oradea", 380},
            {"Pitesti", 100},
            {"Rimnicu Vilcea", 193},
            {"Sibiu", 253},
            {"Timisoara", 329},
            {"Urziceni", 80},
            {"Vaslui", 199},
            {"Zerind", 374}
        };

        return distances[state];
    }
}

class Node
{
    public string State { get; }
    public Node Parent { get; }
    public string Action { get; }
    public double PathCost { get; }

    public Node(string state, Node parent = null, string action = null, double pathCost = 0)
    {
        State = state;
        Parent = parent;
        Action = action;
        PathCost = pathCost;
    }
}

class AStarSearch
{
    public static Node BestFirstSearch(Problem problem, Func<Node, double> f)
    {
        var initialNode = new Node(state: problem.InitialState);
        var frontier = new List<Node> { initialNode };
        var reached = new Dictionary<string, Node> { { problem.InitialState, initialNode } };

        while (frontier.Any())
        {
            var node = frontier.First();
            frontier.RemoveAt(0);

            if (problem.IsGoal(node.State))
            {
                return node;
            }

            foreach (var child in Expand(problem, node))
            {
                var s = child.State;
                if (!reached.ContainsKey(s) || child.PathCost < reached[s].PathCost)
                {
                    reached[s] = child;
                    frontier.Add(child);
                }
            }

            frontier.Sort((a, b) => f(a).CompareTo(f(b)));
        }

        return null;
    }

    public static IEnumerable<Node> Expand(Problem problem, Node node)
    {
        foreach (var action in problem.Actions(node.State))
        {
            var sPrime = problem.Result(node.State, action);
            var cost = node.PathCost + problem.ActionCost(node.State, action, sPrime);
            yield return new Node(state: sPrime, parent: node, action: action, pathCost: cost);
        }
    }

    public static List<Node> GetSolutionPath(Node node)
    {
        var path = new List<Node>();
        while (node != null)
        {
            path.Add(node);
            node = node.Parent;
        }
        path.Reverse();
        return path;
    }

    static void Main()
    {
        var problemInstance = new Problem(initialState: "Arad");
        var solutionNode = BestFirstSearch(problemInstance, f: node => node.PathCost + problemInstance.Heuristic(node.State));

        if (solutionNode != null)
        {
            var solutionPath = GetSolutionPath(solutionNode);
            Console.WriteLine("Solution path:");
            foreach (var node in solutionPath)
            {
                Console.WriteLine(node.State);
            }
        }
        else
        {
            Console.WriteLine("No solution found.");
        }
    }
}