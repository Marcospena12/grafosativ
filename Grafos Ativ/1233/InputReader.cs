using System;
using System.IO;
using System.Linq;

public static class InputReader
{
    // Detecta qual tipo de arquivo
    public static void DetectAndRead(string path, Graph g)
    {
        var lines = File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

        // Lista de adjacência
        if (lines[0].Contains(":"))
        {
            ReadFromAdjList(path, g);
            return;
        }

        // Matriz (primeira linha com vários números)
        if (lines[0].Trim().Contains(" "))
        {
            ReadFromAdjMatrix(path, g);
            return;
        }

        // Lista de arestas (u v w)
        ReadFromEdgeList(path, g);
    }

    // -----------------------------
    // LEITURA: LISTA DE ARESTAS
    // -----------------------------
    public static void ReadFromEdgeList(string path, Graph g)
    {
        g.Clear();

        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var p = line.Trim().Split(" ");
            int u = int.Parse(p[0]);
            int v = int.Parse(p[1]);
            int w = int.Parse(p[2]);

            g.AddEdge(u, v, w);
        }
    }

    // -----------------------------
    // LEITURA: LISTA DE ADJACÊNCIA
    // -----------------------------
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
                var n2 = n.Trim();
                var idx = n2.IndexOf("(w=");
                int v = int.Parse(n2.Substring(0, idx));
                int w = int.Parse(n2.Substring(idx + 3).Replace(")", ""));

                g.AddEdge(u, v, w);
            }
        }
    }

    // -----------------------------
    // LEITURA: MATRIZ DE ADJACÊNCIA
    // -----------------------------
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
