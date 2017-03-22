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
                    Distances[Vertex.Key] = int.MaxValue-100; // Watch out for stack overflow.
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
                    //break; 
                    /* This break and if statement below stop the while loop from finding shortest path three for every node.
                    Without them the algorithm calculates shortest route for every Vertex.*/
                }
                /* 
                if (Distances[Smallest] == int.MaxValue)
                {
                    throw new Exception("Finish Unreachable");                    
                    //break;
                }
                */
                foreach (var Neighbour in Vertices[Smallest])
                {
                    var Alternative = Distances[Smallest] + Neighbour.Value;
                    if (Alternative < Distances[Neighbour.Key])
                    {
                        Distances[Neighbour.Key] = Alternative;
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
                }

            } // Replace the cost w(u,v) of every edge (u,v) by w′(u,v) = w(u,v) − d(v) + d(u).
            foreach (var Vertex in Dijkstra1.Path)
            {
                if (Dijkstra1.Parents.TryGetValue(Vertex, out var value))
                    if(ResidualGraph.Vertices[Vertex].ContainsKey(value))
                        ResidualGraph.RemoveEdge(Vertex, value);

                // Create a residual graph Gt formed from G by removing the edges of G on path P1 that are directed into start
                if (Dijkstra1.Parents.TryGetValue(Vertex, out var value1))
                    ResidualGraph.ReverseEdge(value1, Vertex);

                // ResidualGraph.AddEdge(Vertex, Dijkstra1.Parents[Vertex], 0);
                // Reverse the direction of the zero length edges along path P1.
            }// Create a residual graph
            var Dijkstra2 = ResidualGraph.ShortestPath(Start, Finish);
            //Find the shortest path P2 in the residual graph Gt by running Dijkstra's algorithm.
            List<KeyValuePair<char, char>> FinalPath1 = new List<KeyValuePair<char, char>>();
            List<KeyValuePair<char, char>> FinalPath2 = new List<KeyValuePair<char, char>>();
            foreach (var Node1 in Dijkstra1.DictionaryPath.ToList())
            {
                
                foreach(var Node2 in Dijkstra2.DictionaryPath.ToList())
                {
                    if(Node1.Key==Node2.Value&&Node1.Value==Node2.Key)
                    {
                        Dijkstra1.DictionaryPath.Remove(Node1.Key);
                        Dijkstra2.DictionaryPath.Remove(Node2.Key);
                    }                
                    
                }
            }// Discard the common reversed edges between both paths.

            FinalPath1.Add(new KeyValuePair<char, char>(Start, Dijkstra1.DictionaryPath[Start]));// Initiate Disjoint Path 1
            Dijkstra1.DictionaryPath.Remove(Start);// Add first edge to the path.
            FinalPath2.Add(new KeyValuePair<char, char>(Start, Dijkstra2.DictionaryPath[Start]));// Initiate Disjoint Path 2
            Dijkstra2.DictionaryPath.Remove(Start);// Add first edge to the path.
            while (Dijkstra1.DictionaryPath.ContainsKey(FinalPath1[FinalPath1.Count - 1].Value) 
                || Dijkstra2.DictionaryPath.ContainsKey(FinalPath1[FinalPath1.Count - 1].Value))
            {
                if (Dijkstra1.DictionaryPath.ContainsKey(FinalPath1[FinalPath1.Count - 1].Value))
                {
                    FinalPath1.Add(new KeyValuePair<char, char>(FinalPath1[FinalPath1.Count - 1].Value, Dijkstra1.DictionaryPath[FinalPath1[FinalPath1.Count - 1].Value]));
                    Dijkstra1.DictionaryPath.Remove(FinalPath1[FinalPath1.Count - 2].Value);
                }
                if (Dijkstra2.DictionaryPath.ContainsKey(FinalPath1[FinalPath1.Count - 1].Value))
                {
                    FinalPath1.Add(new KeyValuePair<char, char>(FinalPath1[FinalPath1.Count - 1].Value, Dijkstra2.DictionaryPath[FinalPath1[FinalPath1.Count - 1].Value]));
                    Dijkstra2.DictionaryPath.Remove(FinalPath1[FinalPath1.Count - 2].Value);
                }
            }// Build Disjoint Path 1 by searching edges outgoing from the vertex at the end of path, while removing edges already added to the Path.
            while (Dijkstra1.DictionaryPath.ContainsKey(FinalPath2[FinalPath2.Count - 1].Value)
                || Dijkstra2.DictionaryPath.ContainsKey(FinalPath2[FinalPath2.Count - 1].Value))
            {
                if (Dijkstra1.DictionaryPath.ContainsKey(FinalPath2[FinalPath2.Count - 1].Value))
                {
                    FinalPath2.Add(new KeyValuePair<char, char>(FinalPath2[FinalPath2.Count - 1].Value, Dijkstra1.DictionaryPath[FinalPath2[FinalPath2.Count - 1].Value]));
                    Dijkstra1.DictionaryPath.Remove(FinalPath2[FinalPath2.Count - 2].Value);
                }
                if (Dijkstra2.DictionaryPath.ContainsKey(FinalPath2[FinalPath2.Count - 1].Value))
                {
                    FinalPath2.Add(new KeyValuePair<char, char>(FinalPath2[FinalPath2.Count - 1].Value, Dijkstra2.DictionaryPath[FinalPath2[FinalPath2.Count - 1].Value]));
                    Dijkstra2.DictionaryPath.Remove(FinalPath2[FinalPath2.Count - 2].Value);
                }
            }// Build Disjoint Path 2 by searching edges outgoing from the vertex at the end of path, while removing edges already added to the Path.



            PrintPathListofKeyValuePair(FinalPath1);
            PrintPathListofKeyValuePair(FinalPath2);


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
        public void PrintDictionaryPath(Dictionary<char, char> DictionaryPath)
        {
            var PathListofKeyValuePair = DictionaryPath.ToList();
            Console.Write(PathListofKeyValuePair[0].Key);
            foreach(var Node in PathListofKeyValuePair)
            {
                Console.Write(" -> " + Node.Value);               
            }
            Console.WriteLine();
        }
        public void PrintPathListofKeyValuePair(List<KeyValuePair<char, char>> PathListofKeyValuePair)
        {
            
            Console.Write(PathListofKeyValuePair[0].Key);
            foreach (var Node in PathListofKeyValuePair)
            {
                Console.Write(" -> " + Node.Value);
            }
            Console.WriteLine();
        }
    }
}
