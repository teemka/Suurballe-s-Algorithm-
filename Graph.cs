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
        public void ReverseEdge(char from, char to)//only works if there is no reversed edge there already
        {
            var value = this.GetEdgeValue(from, to);
            this.RemoveEdge(from, to);
            this.AddEdge(to, from, value);
        }
        public DijkstraOut ShortestPath(char Start, char Finish)
        {
            var Parents = new Dictionary<char, char>();
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

                var Smallest = Nodes[0];
                Nodes.Remove(Smallest);

                if (Smallest == Finish)
                {
                    PathDistance = Distances[Finish];
                    Path = new List<char>();
                    while (Parents.ContainsKey(Smallest))
                    {
                        Path.Add(Smallest);
                        Smallest = Parents[Smallest];
                    }
                    Path.Add(Start);
                    Path.Reverse();
                    break;
                }

                if (Distances[Smallest] == int.MaxValue)
                {
                    break;
                }

                foreach (var Neighbour in Vertices[Smallest])
                {
                    var alt = Distances[Smallest] + Neighbour.Value;
                    if (alt < Distances[Neighbour.Key])
                    {
                        Distances[Neighbour.Key] = alt;
                        Parents[Neighbour.Key] = Smallest;
                    }
                }
            }
            return new DijkstraOut(Path, Parents, Distances);
        }
        public void Suurballe(char Start, char Finish)
        {
            var Dijkstra1 = ShortestPath(Start, Finish);
            var ResidualGraph = this;
            foreach (var Vertex in Vertices)
            {                
                foreach (var Edge in Vertex.Value.ToList())
                {
                    ResidualGraph.SetEdgeValue(Vertex.Key, Edge.Key, Edge.Value - Dijkstra1.Distances[Edge.Key] + Dijkstra1.Distances[Vertex.Key]);
                    //replace the cost w(u,v) of every edge (u,v) by w′(u,v) = w(u,v) − d(v) + d(u).
                }
                
            }
            foreach(var Vertex in Dijkstra1.Path)
            {
                if (Dijkstra1.Parents.TryGetValue(Vertex, out var value))
                    ResidualGraph.RemoveEdge(Vertex, Dijkstra1.Parents[Vertex]);

                //Create a residual graph Gt formed from G by removing the edges of G on path P1 that are directed into start
                if (Dijkstra1.Parents.TryGetValue(Vertex, out var value1))
                    ResidualGraph.ReverseEdge(Dijkstra1.Parents[Vertex], Vertex);
                //ResidualGraph.AddEdge(Vertex, Dijkstra1.Parents[Vertex], 0);
                //reverse the direction of the zero length edges along path P1
            }
            var Dijkstra2 = ResidualGraph.ShortestPath(Start, Finish);
            //Find the shortest path P2 in the residual graph Gt by running Dijkstra's algorithm.


            PrintPath(Dijkstra1.Path);
            PrintPath(Dijkstra2.Path);

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
