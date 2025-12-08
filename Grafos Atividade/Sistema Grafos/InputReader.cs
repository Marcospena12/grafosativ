using System;
using System.IO;
using System.Linq;

public static class InputReader
{
    // -----------------------------------------------------
    // DETECÇÃO AUTOMÁTICA DO TIPO DE ARQUIVO
    // -----------------------------------------------------
    public static void DetectAndRead(string path, Graph g)
    {
        var lines = File.ReadAllLines(path)
                        .Where(l => !string.IsNullOrWhiteSpace(l))
                        .ToArray();

        var first = lines[0].Trim();
        var firstParts = first.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (first.Contains(":"))
        {
            ReadFromAdjList(path, g);
        }
        else if (firstParts.Length == 3)
        {
            ReadFromEdgeList(path, g);
        }
        else
        {
            ReadFromAdjMatrix(path, g);
        }
    }

    // -----------------------------------------------------
    // LEITURA: LISTA DE ARESTAS (u v w)
    // -----------------------------------------------------
    public static void ReadFromEdgeList(string path, Graph g)
    {
        g.Clear();

        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var p = line.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (p.Length != 3) continue;

            int u = int.Parse(p[0]);
            int v = int.Parse(p[1]);
            int w = int.Parse(p[2]);

            g.AddEdge(u, v, w);
        }
    }

    // -----------------------------------------------------
    // LEITURA: LISTA DE ADJACÊNCIA
    // EX: 1: 2(w=4), 3(w=2)
    // -----------------------------------------------------
    public static void ReadFromAdjList(string path, Graph g)
    {
        g.Clear();

        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(":");
            int u = int.Parse(parts[0].Trim());

            var neighbors = parts[1].Split(",", StringSplitOptions.RemoveEmptyEntries);

            foreach (var n in neighbors)
            {
                var temp = n.Trim();
                int idx = temp.IndexOf("(w=");
                int v = int.Parse(temp.Substring(0, idx));
                int w = int.Parse(temp.Substring(idx + 3).Replace(")", ""));

                g.AddEdge(u, v, w);
            }
        }
    }

    // -----------------------------------------------------
    // LEITURA: MATRIZ DE ADJACÊNCIA NxN
    // -----------------------------------------------------
    public static void ReadFromAdjMatrix(string path, Graph g)
    {
        g.Clear();

        var lines = File.ReadAllLines(path)
                        .Where(l => !string.IsNullOrWhiteSpace(l))
                        .ToList();

        int n = lines.Count;

        for (int i = 0; i < n; i++)
        {
            var row = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < n; j++)
            {
                int w = int.Parse(row[j]);

                if (w > 0)
                {
                    g.AddEdge(i + 1, j + 1, w);
                }
            }
        }
    }
}
