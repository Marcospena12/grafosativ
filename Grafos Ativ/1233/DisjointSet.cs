using System;
using System.Collections.Generic;

public class DisjointSet
{
    private Dictionary<int, int> parent = new Dictionary<int, int>();
    private Dictionary<int, int> rank = new Dictionary<int, int>();

    public void MakeSet(int x)
    {
        if (!parent.ContainsKey(x))
        {
            parent[x] = x;
            rank[x] = 0;
        }
    }

    public int Find(int x)
    {
        if (parent[x] != x) parent[x] = Find(parent[x]);
        return parent[x];
    }

    public void Union(int x, int y)
    {
        int rx = Find(x);
        int ry = Find(y);
        if (rx == ry) return;
        if (rank[rx] < rank[ry]) parent[rx] = ry;
        else if (rank[ry] < rank[rx]) parent[ry] = rx;
        else { parent[ry] = rx; rank[rx]++; }
    }
}