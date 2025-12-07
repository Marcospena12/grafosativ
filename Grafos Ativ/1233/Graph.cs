using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Graph
{
    public Dictionary<int, List<(int to, int w)>> AdjacencyList { get; private set; }

    public Graph()
    {
        AdjacencyList = new Dictionary<int, List<(int, int)>>();
    }

    public void Clear()
    {
        AdjacencyList.Clear();
    }

    public void AddVertex(int v)
    {
        if (!AdjacencyList.ContainsKey(v))
            AdjacencyList[v] = new List<(int, int)>();
    }

    // SEMPRE NÃO DIRECIONADO
    public void AddEdge(int u, int v, int w)
    {
        AddVertex(u);
        AddVertex(v);

        if (!AdjacencyList[u].Contains((v, w)))
            AdjacencyList[u].Add((v, w));

        if (!AdjacencyList[v].Contains((u, w)))
            AdjacencyList[v].Add((u, w));
    }

    // -----------------------------
    // LISTA DE ARESTAS (para mostrar)
    // -----------------------------
    public string ToEdgeListString()
    {
        var sb = new StringBuilder();
        var printed = new HashSet<(int, int)>();

        foreach (var (u, lista) in AdjacencyList)
        {
            foreach (var (v, w) in lista)
            {
                if (printed.Contains((v, u))) continue;
                printed.Add((u, v));

                sb.AppendLine($"{u} {v} {w}");
            }
        }

        return sb.ToString();
    }

    // -----------------------------
    // LISTA DE ADJACÊNCIA (texto)
    // -----------------------------
    public string ToAdjacencyString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Lista de Adjacência:");

        foreach (var kv in AdjacencyList.OrderBy(x => x.Key))
        {
            sb.Append(kv.Key + ": ");
            sb.AppendLine(string.Join(", ", kv.Value.Select(p => $"{p.to}(w={p.w})")));
        }

        return sb.ToString();
    }

    // -----------------------------
    // MATRIZ DE ADJACÊNCIA
    // -----------------------------
    public string ToMatrixString()
    {
        var sb = new StringBuilder();
        var vertices = AdjacencyList.Keys.OrderBy(x => x).ToList();
        int n = vertices.Count;

        sb.AppendLine("Matriz de Adjacência:");
        sb.Append("    ");

        foreach (var v in vertices)
            sb.Append($"{v,4}");

        sb.AppendLine();
        sb.AppendLine(new string('-', 4 + 4 * n));

        foreach (var u in vertices)
        {
            sb.Append($"{u,4}|");

            foreach (var v in vertices)
            {
                var edge = AdjacencyList[u].FirstOrDefault(x => x.to == v);
                int w = edge.to == v ? edge.w : 0;
                sb.Append($"{w,4}");
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}
