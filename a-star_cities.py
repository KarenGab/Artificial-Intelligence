hSLD = {
    'Arad': 366,
    'Bucharest': 0,
    'Craiova': 160,
    'Drobeta': 242,
    'Eforie': 161,
    'Fagaras': 176,
    'Giurgiu': 77,
    'Hirsova': 151,
    'Iasi': 226,
    'Lugoj': 244,
    'Mehadia': 241,
    'Neamt': 234,
    'Oradea': 380,
    'Pitesti': 100,
    'Rimnicu Vilcea': 193,
    'Sibiu': 253,
    'Timisoara': 329,
    'Urziceni': 80,
    'Vaslui': 199,
    'Zerind': 374
}

# Example of A* algorithm implementation
def a_star_search(graph, start, goal):
    open_list = set([start])
    closed_list = set([])
    g = {}  # Store distance from start node
    parents = {}  # Store the optimal parent of each node

    g[start] = 0
    parents[start] = start

    while len(open_list) > 0:
        n = None

        # Find the node with the lowest f(n) = g(n) + h(n)
        for v in open_list:
            if n == None or g[v] + hSLD[v] < g[n] + hSLD[n]:
                n = v

        if n == goal or graph[n] == None:
            pass
        else:
            for (m, weight) in graph[n]:
                if m not in open_list and m not in closed_list:
                    open_list.add(m)
                    parents[m] = n
                    g[m] = g[n] + weight
                else:
                    if g[m] > g[n] + weight:
                        g[m] = g[n] + weight
                        parents[m] = n

                        if m in closed_list:
                            closed_list.remove(m)
                            open_list.add(m)

        if n == None:
            print('Path does not exist!')
            return None

        if n == goal:
            path = []

            while parents[n] != n:
                path.append(n)
                n = parents[n]

            path.append(start)
            path.reverse()

            print('Path found: {}'.format(path))
            return path

        open_list.remove(n)
        closed_list.add(n)

    print('Path does not exist!')
    return None

# Example graph
graph = {
    'Arad': [('Zerind', 75), ('Timisoara', 118), ('Sibiu', 140)],
    'Zerind': [('Arad', 75), ('Oradea', 71)],
    'Oradea': [('Zerind', 71), ('Sibiu', 151)],
    'Sibiu': [('Arad', 140), ('Oradea', 151), ('Fagaras', 99), ('Rimnicu Vilcea', 80)],
    'Fagaras': [('Sibiu', 99), ('Bucharest', 211)],
    'Rimnicu Vilcea': [('Sibiu', 80), ('Pitesti', 97), ('Craiova', 146)],
    'Pitesti': [('Rimnicu Vilcea', 97), ('Craiova', 138), ('Bucharest', 101)],
    'Craiova': [('Drobeta', 120), ('Pitesti', 138), ('Rimnicu Vilcea', 146)],
    'Drobeta': [('Mehadia', 75), ('Craiova', 120)],
    'Mehadia': [('Drobeta', 75), ('Lugoj', 70)],
    'Lugoj': [('Mehadia', 70), ('Timisoara', 111)],
    'Timisoara': [('Arad', 118), ('Lugoj', 111)],
    'Bucharest': [('Fagaras', 211), ('Pitesti', 101), ('Giurgiu', 90), ('Urziceni', 85)],
    'Giurgiu': [('Bucharest', 90)],
    'Urziceni': [('Bucharest', 85), ('Hirsova', 98), ('Vaslui', 142)],
    'Hirsova': [('Urziceni', 98), ('Eforie', 86)],
    'Eforie': [('Hirsova', 86)],
    'Vaslui': [('Urziceni', 142), ('Iasi', 92)],
    'Iasi': [('Vaslui', 92), ('Neamt', 87)],
    'Neamt': [('Iasi', 87)]
}

# Run the A* algorithm
a_star_search(graph, 'Arad', 'Bucharest')