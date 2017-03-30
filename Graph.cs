using System;
using System.Collections.Generic;
using System.Linq;

namespace Suurballe_s_Algorithm
{
    class Graph
    {

        Dictionary<string, Dictionary<string, int>> Vertices = new Dictionary<string, Dictionary<string, int>>();        
        public void AddVertexAndOutgoingEdges(string name, Dictionary<string, int> edges)
        {
            if (Vertices.ContainsKey(name))
                throw new Exception("This vertex is already defined.");
            else
                Vertices[name] = edges;
        }

        public void AddVertex(string name)
        {
            if (Vertices.ContainsKey(name))
                throw new Exception("This vertex is already defined.");
            else
                Vertices[name] = new Dictionary<string, int>();
        }

        public void RemoveVertex(string name)
        {
            if (Vertices.ContainsKey(name))
                Vertices.Remove(name);
            else
                throw new Exception("Vertex does not exist.");
        }

        public void AddEdge(string from, string to, int value)
        {
            Vertices[from].Add(to, value);
        }

        public void RemoveEdge(string from, string to)
        {
            if (Vertices[from].ContainsKey(to))
                Vertices[from].Remove(to);
            else
                throw new Exception("Edge does not exist.");
        }

        public int GetEdgeValue(string from, string to)
        {
            if (Vertices[from].ContainsKey(to))
                return Vertices[from][to];
            else
                throw new Exception("Edge does not exist.");                      
        }

        public void SetEdgeValue(string from, string to, int value)
        {
            if (Vertices[from].ContainsKey(to))
                Vertices[from][to] = value;
            else
                throw new Exception("Edge does not exist.");            
        }

        public void ReverseEdge(string from, string to)
        {           
            try
            {
                var value = this.GetEdgeValue(from, to);
                this.RemoveEdge(from, to);
                this.AddEdge(to, from, value);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);                
            }
        }

        public DijkstraOut ShortestPath(string Start, string Finish)
        {
            if (!Vertices.ContainsKey(Start))
            {
                Console.WriteLine("Graph does not contain defined starting Vertex");
                return null;
            }
            if (!Vertices.ContainsKey(Finish))
            {
                Console.WriteLine("Graph does not contain defined finish Vertex");
                return null;
            }
            var Parents = new Dictionary<string, string>();
            var Distances = new Dictionary<string, int>();
            var Nodes = new List<string>();

            List<string> Path = null;

            foreach (var Vertex in Vertices)
            {
                if (Vertex.Key == Start)
                {
                    Distances[Vertex.Key] = 0;
                }
                else
                {
                    Distances[Vertex.Key] = int.MaxValue-1000; // Watch out for stack overflow.
                }

                Nodes.Add(Vertex.Key);
            }

            while (Nodes.Count != 0)
            {
                Nodes.Sort((x, y) => Distances[x] - Distances[y]); //Priority Queue

                var Smallest = Nodes[0];
                Nodes.Remove(Smallest);

                if (Smallest == Finish)
                {                    
                    Path = new List<string>();
                    while (Parents.ContainsKey(Smallest))
                    {
                        Path.Add(Smallest);
                        Smallest = Parents[Smallest];
                    }
                    Path.Add(Start);
                    Path.Reverse();
                    //break; 
                    /*  ^ This break and if statement below stop the while loop earlier.
                    Without them the algorithm calculates shortest route tree.*/
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

        public void Suurballe(string Start, string Finish)
        {
            if (!Vertices.ContainsKey(Start))
            {
                Console.WriteLine("Graph does not contain defined starting Vertex");
                return;
            }
            if (!Vertices.ContainsKey(Finish))
            {
                Console.WriteLine("Graph does not contain defined finish Vertex");
                return;
            }
            var Dijkstra1 = ShortestPath(Start, Finish);
            var ResidualGraph = this; //It does not create a copy

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
                        ResidualGraph.RemoveEdge(Vertex, value); // Create a residual graph Gt formed from G by removing the edges of G on path P1 that are directed into start.
               
                if (Dijkstra1.Parents.TryGetValue(Vertex, out var value1))
                    ResidualGraph.ReverseEdge(value1, Vertex); // Reverse the direction of the zero length edges along path P1.                
                
            } // Create a residual graph.

            var Dijkstra2 = ResidualGraph.ShortestPath(Start, Finish);
            //Find the shortest path P2 in the residual graph Gt by running Dijkstra's algorithm.

            List<KeyValuePair<string, string>> FinalPath1 = new List<KeyValuePair<string, string>>();
            List<KeyValuePair<string, string>> FinalPath2 = new List<KeyValuePair<string, string>>();

            foreach (var Node1 in Dijkstra1.EdgePath.ToList())
            {                
                foreach(var Node2 in Dijkstra2.EdgePath.ToList())
                {
                    if(Node1.Key==Node2.Value&&Node1.Value==Node2.Key)
                    {
                        Dijkstra1.EdgePath.Remove(Node1.Key);
                        Dijkstra2.EdgePath.Remove(Node2.Key);
                    }            
                }
            }// Discard the common reversed edges between both paths.
            try
            {
                FinalPath1.Add(new KeyValuePair<string, string>(Start, Dijkstra1.EdgePath[Start]));// Add first edge to the path.
                Dijkstra1.EdgePath.Remove(Start);// Shorten the Dictionary

                FinalPath2.Add(new KeyValuePair<string, string>(Start, Dijkstra2.EdgePath[Start]));// Add first edge to the path.
                Dijkstra2.EdgePath.Remove(Start);// Shorten the Dictionary
            }
            catch
            {
                Console.WriteLine("Impossible to find two paths");
                return;
            }
            Dictionary<string,string> SharedPoolofEdges = Dijkstra1.EdgePath
                .Concat(Dijkstra2.EdgePath)
                .ToDictionary(x => x.Key, x => x.Value); //Creates Shared Pool of Edges for paths building           

            while(SharedPoolofEdges.ContainsKey(FinalPath1.Last().Value))
            {
                FinalPath1.Add(new KeyValuePair<string, string>(FinalPath1.Last().Value, SharedPoolofEdges[FinalPath1.Last().Value]));
                SharedPoolofEdges.Remove(FinalPath1[FinalPath1.Count - 2].Value);
            }// Build Disjoint Path 1 by searching edges outgoing from the vertex at the end of path, while removing edges already added to the Path.

            while (SharedPoolofEdges.ContainsKey(FinalPath2.Last().Value))
            {
                FinalPath2.Add(new KeyValuePair<string, string>(FinalPath2.Last().Value, SharedPoolofEdges[FinalPath2.Last().Value]));
                SharedPoolofEdges.Remove(FinalPath2[FinalPath2.Count - 2].Value);
            }// Build Disjoint Path 2 by searching edges outgoing from the vertex at the end of path, while removing edges already added to the Path.           
            
            PrintPathListofKeyValuePair(FinalPath1);
            PrintPathListofKeyValuePair(FinalPath2);
            
        }

        public void PrintPath(List<string> Path)
        {
            var Last = Path[Path.Count - 1];
            foreach (var Node in Path)
            {
                if (Node == Last)
                    Console.WriteLine(Node);
                else
                    Console.Write(Node + " -> ");
            }
        }

        public void PrintDictionaryPath(Dictionary<string, string> DictionaryPath)
        {
            var PathListofKeyValuePair = DictionaryPath.ToList();
            Console.Write(PathListofKeyValuePair[0].Key);
            foreach(var Node in PathListofKeyValuePair)
            {
                Console.Write(" -> " + Node.Value);               
            }
            Console.WriteLine();
        }

        public void PrintPathListofKeyValuePair(List<KeyValuePair<string, string>> PathListofKeyValuePair)
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
