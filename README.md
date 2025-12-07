# Analisador Grafos - Sistema de Análise de Grafos em C#

> **Trabalho Prático - Algoritmos em Grafos**  
> Pontifícia Universidade Católica de Minas Gerais  
> Sistemas de Informação - Turma 2489100

## Equipe

- **Caio Viera de Freitas**
- **Gustavo Costa Pinho Tavares**
- **Jean Lucas Pereira de Assis**
- **Marcos Vinícius Costa Pena**

---

## Sobre o Projeto

Sistema completo de análise e manipulação de grafos desenvolvido em C#, implementando os principais algoritmos clássicos da Teoria dos Grafos. O projeto oferece suporte a múltiplas representações de grafos e execução interativa de algoritmos através de uma interface de console intuitiva.

### Objetivos

- Representar grafos ponderados não-direcionados em diferentes formatos
- Implementar algoritmos fundamentais de busca e otimização
- Proporcionar interface interativa para análise de grafos
- Demonstrar aplicações práticas da Teoria dos Grafos

---

## Funcionalidades

### Entrada de Dados
-  Leitura automática de arquivos `.txt` e `.csv`
-  Detecção inteligente do formato de entrada:
  - Lista de Arestas
  - Lista de Adjacência
  - Matriz de Adjacência

### Representações do Grafo
- **Lista de Adjacência** - Estrutura principal otimizada
- **Matriz de Adjacência** - Visualização matricial completa
- **Lista de Arestas** - Formato simplificado de arestas

### Algoritmos Implementados

| Algoritmo | Tipo | Complexidade | Aplicação |
|-----------|------|--------------|-----------|
| **BFS** | Busca em Largura | O(V + E) | Menor caminho (grafos não ponderados) |
| **DFS** | Busca em Profundidade | O(V + E) | Exploração, detecção de ciclos |
| **Dijkstra** | Menor Caminho | O((V + E) log V) | Caminho mínimo em grafos ponderados |
| **Prim** | Árvore Geradora Mínima | O(E log V) | MST - Minimum Spanning Tree |
| **Kruskal** | Árvore Geradora Mínima | O(E log E) | MST - com Union-Find |

---

## Arquitetura do Projeto

```
GraphAnalyzer/
│
├── 📄 Graph.cs              # Estrutura de dados do grafo
├── 📄 Algorithms.cs         # Implementação dos algoritmos clássicos
├── 📄 InputReader.cs        # Leitor inteligente de arquivos
├── 📄 Program.cs            # Interface de menu e controle principal
├── 📄 sample_graph.txt      # Arquivo de exemplo para testes
└── 📄 README.md             # Este arquivo
```

### Componentes Principais

#### `Graph.cs` - Estrutura de Dados
- Dicionário de listas de adjacência para armazenamento otimizado
- Métodos para adicionar vértices e arestas
- Conversão automática entre representações
- Suporte a grafos ponderados e não-direcionados

#### `Algorithms.cs` - Motor de Algoritmos
- **BFS**: Busca em largura com fila
- **DFS**: Busca em profundidade recursiva
- **Dijkstra**: Caminho mínimo com Priority Queue
- **Prim**: MST com heap mínimo
- **Kruskal**: MST com Union-Find otimizado

#### `InputReader.cs` - Parser Inteligente
- Detecção automática do formato de entrada
- Suporte a múltiplos formatos (lista, matriz, arestas)
- Tratamento de erros e validação de dados

#### `Program.cs` - Interface Interativa
- Menu intuitivo de console
- Carregamento automático do grafo
- Execução controlada de algoritmos
- Exportação de resultados

---

## Como Usar

### Pré-requisitos

- .NET 6.0 ou superior
- Sistema operacional: Windows, Linux ou macOS

### Instalação

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/graph-analyzer.git

# Entre no diretório
cd graph-analyzer

# Compile o projeto
dotnet build

# Execute o programa
dotnet run
```

### Uso Básico

1. **Prepare seu arquivo de entrada** (`sample_graph.txt`)
2. **Execute o programa** - o grafo será carregado automaticamente
3. **Escolha uma opção do menu** para visualizar ou executar algoritmos

---

## Formatos de Entrada Suportados

### Lista de Adjacência
```
1: 2(w=4), 3(w=2)
2: 1(w=4), 4(w=5)
3: 1(w=2), 4(w=1)
4: 2(w=5), 3(w=1)
```

### Lista de Arestas
```
1 2 4
1 3 2
2 4 5
3 4 1
```

### Matriz de Adjacência
```
0 4 2 0
4 0 0 5
2 0 0 1
0 5 1 0
```

---

## Menu Interativo

```
Menu:
1   - Mostrar Lista de Adjacência
1.1 - Mostrar Matriz de Adjacência
1.2 - Mostrar Lista de Arestas
2   - BFS
3   - DFS
4   - Dijkstra (menor caminho)
5   - Prim (MST)
6   - Kruskal (MST)
7   - Ler grafo de arquivo (.txt ou .csv)
8   - Exportar representação textual (saida.txt)
0   - Sair
```

---

## Exemplos de Uso

### Busca em Largura (BFS)
```
Escolha: 2
Início (vértice): 1
Ordem BFS: 1 2 3 4
```

### Algoritmo de Dijkstra
```
Escolha: 4
Origem: 1
Destino: 4
Distância mínima: 3
Caminho: 1 -> 3 -> 4
```

### Árvore Geradora Mínima (Prim)
```
Escolha: 5
Arestas MST (Prim):
3 - 4 (w=1)
1 - 3 (w=2)
1 - 2 (w=4)
Peso total MST: 7
```

---

## Dados de Teste

O repositório inclui o arquivo `sample_graph.txt` com um grafo de exemplo. Você pode criar seus próprios arquivos de teste seguindo os formatos documentados acima.

### Grafo de Exemplo

```
    1 ----4---- 2
    |           |
    2           5
    |           |
    3 ----1---- 4
```

---

## Fundamentos Teóricos

### Representações de Grafos

- **Lista de Adjacência**: Eficiente para grafos esparsos (O(V + E) espaço)
- **Matriz de Adjacência**: Consulta rápida de arestas (O(V²) espaço)
- **Lista de Arestas**: Simples e compacta (O(E) espaço)

### Complexidade dos Algoritmos

| Algoritmo | Tempo | Espaço | Estrutura Auxiliar |
|-----------|-------|--------|-------------------|
| BFS | O(V + E) | O(V) | Queue |
| DFS | O(V + E) | O(V) | Stack (recursão) |
| Dijkstra | O((V+E) log V) | O(V) | Priority Queue |
| Prim | O(E log V) | O(V) | Priority Queue |
| Kruskal | O(E log E) | O(V) | Union-Find |

---

## Aprendizados

### Conhecimentos Técnicos Adquiridos
- Estruturas de dados avançadas (grafos, filas de prioridade, union-find)
- Algoritmos clássicos de otimização e busca
- Modularização e arquitetura de software
- Leitura e parsing de arquivos
- Tratamento de exceções e validação de dados

### Desafios Superados
- Detecção automática de formato de entrada
- Implementação eficiente de Union-Find para Kruskal
- Construção correta de lista de arestas para MST
- Depuração de inconsistências em dados de entrada

---

## Estruturas de Dados Utilizadas

```csharp
// Lista de Adjacência
Dictionary<int, List<(int to, long weight)>>

// Priority Queue (Dijkstra, Prim)
PriorityQueue<int, long>

// Union-Find (Kruskal)
class UnionFind {
    private int[] parent;
    private int[] rank;
}
```

---

## Exportação de Dados

O sistema permite exportar todas as representações do grafo em um único arquivo:

```
Escolha: 8
Arquivo 'saida.txt' gerado com sucesso!
```

O arquivo `saida.txt` conterá:
- Lista de Adjacência completa
- Matriz de Adjacência formatada
- Lista de Arestas ordenada

---

