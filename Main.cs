using System;
using System.Diagnostics;

namespace Suurballe_s_Algorithm
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Graph g = new Graph();
            //string start, finish;

            //Example from https://en.wikipedia.org/wiki/Suurballe's_algorithm
            /*
            g.AddVertexAndOutgoingEdges("A", new Dictionary<string, int>() { { "B", 1 }, { "C", 2 } });
            g.AddVertexAndOutgoingEdges("B", new Dictionary<string, int>() { { "A", 1 }, { "D", 1 }, { "E", 2 } });
            g.AddVertexAndOutgoingEdges("C", new Dictionary<string, int>() { { "A", 2 }, { "D", 2 } });
            g.AddVertexAndOutgoingEdges("D", new Dictionary<string, int>() { { "C", 2 }, { "F", 1 }, { "B", 1 } });
            g.AddVertexAndOutgoingEdges("E", new Dictionary<string, int>() { { "B", 2 }, { "F", 2 } });
            g.AddVertexAndOutgoingEdges("F", new Dictionary<string, int>() { { "D", 1 }, { "E", 2 } });
            //g.AddVertex("Z");
            */

            //g.AddVertexAndOutgoingEdges("A", new Dictionary<string, int>() { { "B", 1 }, { "C", 3 } });
            //g.AddVertexAndOutgoingEdges("B", new Dictionary<string, int>() { { "A", 1 }, { "D", 1 } });
            //g.AddVertexAndOutgoingEdges("C", new Dictionary<string, int>() { { "A", 3 }, { "E", 3 } });
            //g.AddVertexAndOutgoingEdges("D", new Dictionary<string, int>() { { "B", 1 }, { "E", 1 }, { "F", 4 } });
            //g.AddVertexAndOutgoingEdges("E", new Dictionary<string, int>() { { "C", 3 }, { "D", 1 }, { "G", 1 }, { "F", 1 } });
            //g.AddVertexAndOutgoingEdges("F", new Dictionary<string, int>() { { "D", 4 }, { "G", 1 }, { "H", 1 } });
            //g.AddVertexAndOutgoingEdges("G", new Dictionary<string, int>() { { "F", 1 }, { "E", 1 }, { "I", 4 } });
            //g.AddVertexAndOutgoingEdges("H", new Dictionary<string, int>() { { "F", 1 }, { "I", 1 }, { "J", 4 } });
            //g.AddVertexAndOutgoingEdges("I", new Dictionary<string, int>() { { "G", 4 }, { "H", 1 }, { "J", 1 } });
            //g.AddVertexAndOutgoingEdges("J", new Dictionary<string, int>() { { "H", 4 }, { "I", 1 } });

            /*
            g.AddVertexAndOutgoingEdges("A", new Dictionary<string, int>() { { "E", 1 }, { "B", 3 } });
            g.AddVertexAndOutgoingEdges("B", new Dictionary<string, int>() { { "C", 1 } });
            g.AddVertexAndOutgoingEdges("C", new Dictionary<string, int>() { { "H", 1 }, { "E", 1 } });
            g.AddVertexAndOutgoingEdges("D", new Dictionary<string, int>() { { "E", 3 }, { "C", 1 } });
            g.AddVertexAndOutgoingEdges("E", new Dictionary<string, int>() { { "A", 1 }, { "F", 2 } });
            g.AddVertexAndOutgoingEdges("F", new Dictionary<string, int>() { { "G", 3 } });
            g.AddVertexAndOutgoingEdges("G", new Dictionary<string, int>() { { "F", 3 }, { "J", 1 } });
            g.AddVertexAndOutgoingEdges("H", new Dictionary<string, int>() { { "C", 1 }, { "G", 1 }, { "I", 2 } });
            g.AddVertexAndOutgoingEdges("I", new Dictionary<string, int>() { { "H", 2 }, { "L", 1 } });
            g.AddVertexAndOutgoingEdges("J", new Dictionary<string, int>() { { "G", 1 }, { "K", 2 } });
            g.AddVertexAndOutgoingEdges("K", new Dictionary<string, int>() { { "J", 2 }, { "M", 1 } });
            g.AddVertexAndOutgoingEdges("L", new Dictionary<string, int>() { { "I", 1 }, { "M", 3 } });
            g.AddVertexAndOutgoingEdges("M", new Dictionary<string, int>() { { "K", 1 }, { "L", 3 } });
            */

            //Console.Write("Give start vertex:\t");
            //start = Console.ReadLine();
            //Console.Write("Give finish vertex:\t");
            //finish = Console.ReadLine();
            

            //System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\tomas\Google Drive\school\EiTI\GIS\testing.csv");
            //file.WriteLine("Vertices,Edges,Milliseconds");

            for (int v = 100; v<=300;v=v+100)
            {
                for (int p = 1; p < 100; p += 5)
                {
                    Graph g = new Graph();
                    string start, finish;


                    for (int i = 0; i < v; i++)
                    {
                        g.AddVertex(i.ToString());
                    }
                    Random rnd = new Random();
                    Random prob = new Random();
                    for (int i = 0; i < v; i++)
                    {
                        for (int j = 0; j < v; j++)
                        {

                            if (prob.Next(0, 100) < p && j != i)
                                g.AddEdge(i.ToString(), j.ToString(), rnd.Next(1, 20));
                        }
                    }

                    start = "0";
                    finish = (v - 1).ToString();

                    Console.WriteLine("Vertex count: {0}", g.GetVertexCount());
                    Console.WriteLine("Edge count: {0}", g.GetEdgeCount());

                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    g.SuurballeDisjointVertices(start, finish);

                    sw.Stop();

                    Console.WriteLine("Elapsed={0}", sw.Elapsed);
                    //file.WriteLine(v.ToString() + "," + g.GetEdgeCount() + "," + sw.ElapsedMilliseconds);
                }
            }

            Console.WriteLine("Finished");
            //file.Close();

            Console.ReadLine();
        }
    }
}
