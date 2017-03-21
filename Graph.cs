using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suurballe_s_Algorithm
{
    class Graph
    {
        public int PathDistance { get; set; }

        Dictionary<char, Dictionary<char, int>> Vertices = new Dictionary<char, Dictionary<char, int>>();

        public void AddVertexAndOutgoingEdges(char name, Dictionary<char, int> edges)
        {
            if (Vertices.ContainsKey(name))
                throw new Exception("This vertex is already defined.");
            //Console.WriteLine("This vertex is already defined.");
            else
                Vertices[name] = edges;
        }
        public void AddVertex(char name)
        {
            if (Vertices.ContainsKey(name))
                throw new Exception("This vertex is already defined.");
            //Console.WriteLine("This vertex is already defined.");
            else
                Vertices[name] = null;
        }
        public void RemoveVertex(char name)
        {
            if (Vertices.ContainsKey(name))
                Vertices.Remove(name);
            else
                throw new Exception("Vertex does not exist.");
            //Console.WriteLine("Vertex {0} does not exist.", name);
        }
        public void AddEdge(char from, char to, int value)
        {
            if (Vertices[from].ContainsKey(to))
                throw new Exception("Edge already exists.");
            //Console.WriteLine("Edge {0} -> {1} already exists!", from, to);
            else
                Vertices[from].Add(to, value);
        }
        public void RemoveEdge(char from, char to)
        {
            if (Vertices[from].ContainsKey(to))
                Vertices[from].Remove(to);
            else
                throw new Exception("Edge does not exist.");
            //Console.WriteLine("Edge {0} -> {1} does not exist.", from, to);
        }
        public int GetEdgeValue(char from, char to)
        {
            if (Vertices[from].ContainsKey(to))
                return Vertices[from][to];
            else
                throw new Exception("Edge does not exist.");
            //Console.WriteLine("Edge {0} -> {1} does not exist.", from, to);            
        }
        public void SetEdgeValue(char from, char to, int value)
        {
            if (Vertices[from].ContainsKey(to))
                Vertices[from][to] = value;
            else
                throw new Exception("Edge does not exist.");
            //Console.WriteLine("Edge {0} -> {1} does not exist!", from, to);
        }
        public DijkstraOut ShortestPath(char Start, char Finish)
        {
            var Previous = new Dictionary<char, char>();
            var Distances = new Dictionary<char, int>();
            var Nodes = new List<char>();

            List<char> Path = null;

            foreach (var Vertex in Vertices)
            {
                if (Vertex.Key == Start)
                {
                    Distances[Vertex.Key] = 0;
                }
                else
                {
                    Distances[Vertex.Key] = int.MaxValue;
                }

                Nodes.Add(Vertex.Key);
            }

            while (Nodes.Count != 0)
            {
                Nodes.Sort((x, y) => Distances[x] - Distances[y]);

                var smallest = Nodes[0];
                Nodes.Remove(smallest);

                if (smallest == Finish)
                {
                    PathDistance = Distances[Finish];
                    Path = new List<char>();
                    while (Previous.ContainsKey(smallest))
                    {
                        Path.Add(smallest);
                        smallest = Previous[smallest];
                    }
                    Path.Add(Start);
                    Path.Reverse();
                    break;
                }

                if (Distances[smallest] == int.MaxValue)
                {
                    break;
                }

                foreach (var neighbor in Vertices[smallest])
                {
                    var alt = Distances[smallest] + neighbor.Value;
                    if (alt < Distances[neighbor.Key])
                    {
                        Distances[neighbor.Key] = alt;
                        Previous[neighbor.Key] = smallest;
                    }
                }
            }
            return new DijkstraOut(Path, Previous, Distances);
        }
        public void Suurballe(char Start, char Finish)
        {
            var Dijkstra1 = ShortestPath(Start, Finish);
            var ResidualGraph = Vertices;
            foreach (var Vertex in Vertices)
            {
                foreach (var Edge in Vertex.Value)
                {
                    SetEdgeValue(Vertex.Key, Edge.Key, Edge.Value - Dijkstra1.Distances[Edge.Key] + Dijkstra1.Distances[Vertex.Key]);
                }
            }
        }
        public void PrintPath(List<char> Path)
        {
            var Last = Path[Path.Count - 1];
            foreach (var Node in Path)
            {
                if (Node == Last)
                    Console.WriteLine(Node);
                else
                    Console.Write(Node + " -> ");
            }
            Console.WriteLine("Distance: {0}", PathDistance);
        }
    }
}
