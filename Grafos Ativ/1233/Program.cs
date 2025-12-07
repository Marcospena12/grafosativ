using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Graph Analyzer - C#");
        Console.WriteLine("Autor: Gustavo Costa Pinho Tavares");

        Graph graph = new Graph();

        // Tenta ler o arquivo padrão
        string defaultFile = "sample_graph.txt";
        if (File.Exists(defaultFile))
        {
            Console.WriteLine($"Lendo grafo de '{defaultFile}'...");
            InputReader.DetectAndRead(defaultFile, graph);
        }
        else
        {
            Console.WriteLine("Arquivo padrão não encontrado.");
        }

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1   - Mostrar Lista de Adjacência");
            Console.WriteLine("1.1 - Mostrar Matriz de Adjacência");
            Console.WriteLine("1.2 - Mostrar Lista de Arestas");
            Console.WriteLine("2   - BFS");
            Console.WriteLine("3   - DFS");
            Console.WriteLine("4   - Dijkstra (menor caminho)");
            Console.WriteLine("5   - Prim (MST)");
            Console.WriteLine("6   - Kruskal (MST)");
            Console.WriteLine("7   - Ler grafo de arquivo (.txt ou .csv)");
            Console.WriteLine("8   - Exportar representação textual (saida.txt)");
            Console.WriteLine("0   - Sair");
            Console.Write("Escolha: ");

            string op = Console.ReadLine();

            if (op == "0") break;

            // ----------------------------- //
            // REPRESENTAÇÕES
            // ----------------------------- //
            if (op == "1")
            {
                Console.WriteLine(graph.ToAdjacencyString());
            }
            else if (op == "1.1")
            {
                Console.WriteLine(graph.ToMatrixString());
            }
            else if (op == "1.2")
            {
                Console.WriteLine("Lista de Arestas:");
                Console.WriteLine(graph.ToEdgeListString());
            }

            // ----------------------------- //
            // BFS
            // ----------------------------- //
            else if (op == "2")
            {
                Console.Write("Início (vértice): ");
                if (int.TryParse(Console.ReadLine(), out int s))
                {
                    var bfs = Algorithms.BFS(graph, s);
                    Console.WriteLine("Ordem BFS: " + string.Join(" ", bfs));
                }
                else Console.WriteLine("Vértice inválido.");
            }

            // ----------------------------- //
            // DFS
            // ----------------------------- //
            else if (op == "3")
            {
                Console.Write("Início (vértice): ");
                if (int.TryParse(Console.ReadLine(), out int s))
                {
                    var dfs = Algorithms.DFS(graph, s);
                    Console.WriteLine("Ordem DFS (recursiva): " + string.Join(" ", dfs));
                }
                else Console.WriteLine("Vértice inválido.");
            }

            // ----------------------------- //
            // DIJKSTRA
            // ----------------------------- //
            else if (op == "4")
            {
                Console.Write("Origem: ");
                if (!int.TryParse(Console.ReadLine(), out int origem))
                {
                    Console.WriteLine("Origem inválida.");
                    continue;
                }

                Console.Write("Destino: ");
                if (!int.TryParse(Console.ReadLine(), out int destino))
                {
                    Console.WriteLine("Destino inválido.");
                    continue;
                }

                var (dist, prev) = Algorithms.Dijkstra(graph, origem);

                if (!dist.ContainsKey(destino) || dist[destino] == long.MaxValue)
                {
                    Console.WriteLine("Destino inacessível.");
                }
                else
                {
                    Console.WriteLine($"Distância mínima: {dist[destino]}");
                    var path = Algorithms.ReconstructPath(prev, origem, destino);
                    Console.WriteLine("Caminho: " + string.Join(" -> ", path));
                }
            }

            // ----------------------------- //
            // PRIM (MST)
            // ----------------------------- //
            else if (op == "5")
            {
                var (mstEdges, total) = Algorithms.Prim(graph);
                Console.WriteLine("Arestas MST (Prim):");

                foreach (var e in mstEdges)
                    Console.WriteLine($"{e.u} - {e.v} (w={e.w})");

                Console.WriteLine($"Peso total MST: {total}");
            }

            // ----------------------------- //
            // KRUSKAL (MST)
            // ----------------------------- //
            else if (op == "6")
            {
                var (mstEdges, total) = Algorithms.Kruskal(graph);
                Console.WriteLine("Arestas MST (Kruskal):");

                foreach (var e in mstEdges)
                    Console.WriteLine($"{e.u} - {e.v} (w={e.w})");

                Console.WriteLine($"Peso total MST: {total}");
            }

            // ----------------------------- //
            // LER GRAFO DE ARQUIVO
            // ----------------------------- //
            else if (op == "7")
            {
                Console.Write("Caminho do arquivo: ");
                string file = Console.ReadLine();

                try
                {
                    graph = new Graph();
                    InputReader.DetectAndRead(file, graph);
                    Console.WriteLine("Grafo carregado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao ler arquivo: " + ex.Message);
                }
            }

            // ----------------------------- //
            // EXPORTAR
            // ----------------------------- //
            else if (op == "8")
            {
                var text =
                    "--- Lista de Adjacência ---\n" +
                    graph.ToAdjacencyString() + "\n\n" +
                    "--- Matriz de Adjacência ---\n" +
                    graph.ToMatrixString() + "\n\n" +
                    "--- Lista de Arestas ---\n" +
                    graph.ToEdgeListString();

                File.WriteAllText("saida.txt", text);
                Console.WriteLine("Arquivo 'saida.txt' gerado com sucesso!");
            }

            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }

        Console.WriteLine("Encerrando...");
    }
}
