using System;
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
            { "Arad", new List<string> { "ToZerind", "ToTimisoara", "ToSibiu" } },
            { "Zerind", new List<string> { "ToArad", "ToOradea" } },
            { "Oradea", new List<string> { "ToZerind", "ToSibiu" } },
            { "Sibiu", new List<string> { "ToArad", "ToOradea", "ToFagaras", "ToRimnicu Vilcea" } },
            { "Fagaras", new List<string> { "ToSibiu", "ToBucharest" } },
            { "Rimnicu Vilcea", new List<string> { "ToSibiu", "ToPitesti", "ToCraiova" } },
            { "Pitesti", new List<string> { "ToRimnicu Vilcea", "ToCraiova", "ToBucharest" } },
            { "Craiova", new List<string> { "ToDrobeta", "ToPitesti", "ToRimnicu Vilcea" } },
            { "Drobeta", new List<string> { "ToMehadia", "ToCraiova" } },
            { "Mehadia", new List<string> { "ToDrobeta", "ToLugoj" } },
            { "Lugoj", new List<string> { "ToMehadia", "ToTimisoara" } },
            { "Timisoara", new List<string> { "ToArad", "ToLugoj" } },
            { "Bucharest", new List<string> { "ToFagaras", "ToPitesti", "ToGiurgiu", "ToUrziceni" } },
            { "Giurgiu", new List<string> { "ToBucharest" } },
            { "Urziceni", new List<string> { "ToBucharest", "ToHirsova", "ToVaslui" } },
            { "Hirsova", new List<string> { "ToUrziceni", "ToEforie" } },
            { "Eforie", new List<string> { "ToHirsova" } },
            { "Vaslui", new List<string> { "ToUrziceni", "ToIasi" } },
            { "Iasi", new List<string> { "ToVaslui", "ToNeamt" } },
            { "Neamt", new List<string> { "ToIasi" } }
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
        // Regresa el estado que resulta de aplicar la acción sobre el estado
        return action.Substring(2);
    }

    public double ActionCost(string state, string action, string sPrime)
    {
        var cityActions = new Dictionary<(string, string), double>
        {
            {("Arad", "ToZerind"), 75},
            {("Zerind", "ToArad"), 75},
            {("Arad", "ToSibiu"), 140},
            {("Sibiu", "ToArad"), 140},
            {("Arad", "ToTimisoara"), 118},
            {("Timisoara", "ToArad"), 118},
            {("Zerind", "ToOradea"), 71},
            {("Oradea", "ToZerind"), 71},
            {("Oradea", "ToSibiu"), 151},
            {("Sibiu", "ToOradea"), 151},
            {("Timisoara", "ToLugoj"), 111},
            {("Lugoj", "ToTimisoara"), 111},
            {("Lugoj", "ToMehadia"), 70},
            {("Mehadia", "ToLugoj"), 70},
            {("Mehadia", "ToDrobeta"), 75},
            {("Drobeta", "ToMehadia"), 75},
            {("Drobeta", "ToCraiova"), 120},
            {("Craiova", "ToDrobeta"), 120},
            {("Sibiu", "ToRimnicu Vilcea"), 80},
            {("Rimnicu Vilcea", "ToSibiu"), 80},
            {("Sibiu", "ToFagaras"), 99},
            {("Fagaras", "ToSibiu"), 99},
            {("Craiova", "ToPitesti"), 138},
            {("Pitesti", "ToCraiova"), 138},
            {("Rimnicu Vilcea", "ToCraiova"), 146},
            {("Craiova", "ToRimnicu Vilcea"), 146},
            {("Rimnicu Vilcea", "ToPitesti"), 97},
            {("Pitesti", "ToRimnicu Vilcea"), 97},
            {("Bucharest", "ToFagaras"), 211},
            {("Fagaras", "ToBucharest"), 211},
            {("Pitesti", "ToBucharest"), 101},
            {("Bucharest", "ToPitesti"), 101},
            {("Giurgiu", "ToBucharest"), 90},
            {("Bucharest", "ToGiurgiu"), 90},
            {("Neamt", "ToIasi"), 87},
            {("Iasi", "ToNeamt"), 87},
            {("Vaslui", "ToIasi"), 92},
            {("Iasi", "ToVaslui"), 92},
            {("Urziceni", "ToVaslui"), 142},
            {("Vaslui", "ToUrziceni"), 142},
            {("Bucharest", "ToUrziceni"), 85},
            {("Urziceni", "ToBucharest"), 85},
            {("Hirsova", "ToEforie"), 86},
            {("Eforie", "ToHirsova"), 86},
            {("Urziceni", "ToHirsova"), 98},
            {("Hirsova", "ToUrziceni"), 98}
        };

        return cityActions[(state, action)];
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