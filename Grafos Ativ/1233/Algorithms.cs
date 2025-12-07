using System;
using System.Collections.Generic;
using System.Linq;

public static class Algorithms
{
    // -----------------------------------------------------
    // BFS
    // -----------------------------------------------------
    public static List<int> BFS(Graph g, int start)
    {
        var visited = new HashSet<int>();
        var queue = new Queue<int>();
        var order = new List<int>();

        if (!g.AdjacencyList.ContainsKey(start))
            return order;

        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();
            order.Add(u);

            foreach (var (v, w) in g.AdjacencyList[u])
            {
                if (!visited.Contains(v))
                {
                    visited.Add(v);
                    queue.Enqueue(v);
                }
            }
        }
        return order;
    }

    // -----------------------------------------------------
    // DFS (recursivo)
    // -----------------------------------------------------
    public static List<int> DFS(Graph g, int start)
    {
        var visited = new HashSet<int>();
        var order = new List<int>();

        void dfs(int u)
        {
            visited.Add(u);
            order.Add(u);

            foreach (var (v, w) in g.AdjacencyList[u])
                if (!visited.Contains(v))
                    dfs(v);
        }

        if (g.AdjacencyList.ContainsKey(start))
            dfs(start);

        return order;
    }

    // -----------------------------------------------------
    // Dijkstra
    // -----------------------------------------------------
    public static (Dictionary<int, long> dist, Dictionary<int, int?> prev)
        Dijkstra(Graph g, int start)
    {
        var dist = new Dictionary<int, long>();
        var prev = new Dictionary<int, int?>();

        foreach (var v in g.AdjacencyList.Keys)
        {
            dist[v] = long.MaxValue;
            prev[v] = null;
        }

        if (!g.AdjacencyList.ContainsKey(start))
            return (dist, prev);

        dist[start] = 0;

        var pq = new PriorityQueue<int, long>();
        pq.Enqueue(start, 0);

        while (pq.Count > 0)
        {
            pq.TryDequeue(out int u, out long du);

            if (du > dist[u]) continue;

            foreach (var (v, w) in g.AdjacencyList[u])
            {
                long nd = dist[u] + w;
                if (nd < dist[v])
                {
                    dist[v] = nd;
                    prev[v] = u;
                    pq.Enqueue(v, nd);
                }
            }
        }

        return (dist, prev);
    }

    public static List<int> ReconstructPath(Dictionary<int, int?> prev, int start, int end)
    {
        var path = new List<int>();
        int? curr = end;

        while (curr != null)
        {
            path.Add(curr.Value);
            curr = prev[curr.Value];
        }

        path.Reverse();

        if (path.Count == 0 || path[0] != start)
            return new List<int>(); // caminho inexistente

        return path;
    }

    // -----------------------------------------------------
    // PRIM (MST)
    // -----------------------------------------------------
    public static (List<(int u, int v, long w)> edges, long total)
        Prim(Graph g)
    {
        var vertices = g.AdjacencyList.Keys.ToList();
        if (vertices.Count == 0)
            return (new List<(int, int, long)>(), 0);

        var start = vertices[0];

        var mstEdges = new List<(int u, int v, long w)>();
        var visited = new HashSet<int>();
        var pq = new PriorityQueue<(int u, int v, long w), long>();

        void AddEdges(int u)
        {
            visited.Add(u);
            foreach (var (v, w) in g.AdjacencyList[u])
                if (!visited.Contains(v))
                    pq.Enqueue((u, v, w), w);
        }

        AddEdges(start);

        long total = 0;

        while (pq.Count > 0)
        {
            pq.TryDequeue(out var edge, out long _);

            if (visited.Contains(edge.v)) continue;

            mstEdges.Add(edge);
            total += edge.w;

            AddEdges(edge.v);
        }

        return (mstEdges, total);
    }

    // -----------------------------------------------------
    // KRUSKAL (MST)
    // -----------------------------------------------------
    public static (List<(int u, int v, long w)> edges, long total)
        Kruskal(Graph g)
    {
        var allEdges = new List<(int u, int v, long w)>();

        foreach (var u in g.AdjacencyList.Keys)
            foreach (var (v, w) in g.AdjacencyList[u])
                if (u < v) // evita duplicar arestas
                    allEdges.Add((u, v, w));

        allEdges.Sort((a, b) => a.w.CompareTo(b.w));

        var parent = new Dictionary<int, int>();
        var rank = new Dictionary<int, int>();

        foreach (var v in g.AdjacencyList.Keys)
        {
            parent[v] = v;
            rank[v] = 0;
        }

        int Find(int x)
        {
            if (parent[x] != x)
                parent[x] = Find(parent[x]);
            return parent[x];
        }

        void Union(int a, int b)
        {
            a = Find(a); b = Find(b);
            if (a == b) return;

            if (rank[a] < rank[b]) parent[a] = b;
            else if (rank[a] > rank[b]) parent[b] = a;
            else { parent[b] = a; rank[a]++; }
        }

        long total = 0;
        var mst = new List<(int u, int v, long w)>();

        foreach (var (u, v, w) in allEdges)
        {
            if (Find(u) != Find(v))
            {
                mst.Add((u, v, w));
                total += w;
                Union(u, v);
            }
        }

        return (mst, total);
    }
}
