using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Suurballe_s_Algorithm
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Graph g = new Graph();
            string start, finish;

            //Example from https://en.wikipedia.org/wiki/Suurballe's_algorithm

            //g.AddVertexAndOutgoingEdges("A", new Dictionary<string, int>() { { "B", 1 }, { "C", 2 } });
            //g.AddVertexAndOutgoingEdges("B", new Dictionary<string, int>() { { "A", 1 }, { "D", 1 }, { "E", 2 } });
            //g.AddVertexAndOutgoingEdges("C", new Dictionary<string, int>() { { "A", 2 }, { "D", 2 } });
            //g.AddVertexAndOutgoingEdges("D", new Dictionary<string, int>() { { "C", 2 }, { "F", 1 }, { "B", 1 } });
            //g.AddVertexAndOutgoingEdges("E", new Dictionary<string, int>() { { "B", 2 }, { "F", 2 } });
            //g.AddVertexAndOutgoingEdges("F", new Dictionary<string, int>() { { "D", 1 }, { "E", 2 } });
            //g.AddVertex("Z");


            //g.AddVertexAndOutgoingEdges("A", new Dictionary<string, int>() { { "B", 1 }, { "C", 3 } });
            //g.AddVertexAndOutgoingEdges("B", new Dictionary<string, int>() { { "D", 1 } });
            //g.AddVertexAndOutgoingEdges("C", new Dictionary<string, int>() { { "E", 3 } });
            //g.AddVertexAndOutgoingEdges("D", new Dictionary<string, int>() { { "E", 1 }, { "F", 4 } });
            //g.AddVertexAndOutgoingEdges("E", new Dictionary<string, int>() { { "D", 1 }, { "G", 1 }, { "F", 1 } });
            //g.AddVertexAndOutgoingEdges("F", new Dictionary<string, int>() { { "G", 1 }, { "H", 1 } });
            //g.AddVertexAndOutgoingEdges("G", new Dictionary<string, int>() { { "F", 1 }, { "I", 4 } });
            //g.AddVertexAndOutgoingEdges("H", new Dictionary<string, int>() { { "I", 1 }, { "J", 4 } });
            //g.AddVertexAndOutgoingEdges("I", new Dictionary<string, int>() { { "H", 1 }, { "J", 1 } });
            //g.AddVertex("J");

            for (int i = 65; i < 73; i++)
            {
                for (int j = 70; j < 73; j++)
                {
                    if (j == i) continue;
                    Graph g = new Graph();
                    //g.AddVertexAndOutgoingEdges("A", new Dictionary<string, int>() { { "D", 1 }, { "B", 3 } });
                    //g.AddVertexAndOutgoingEdges("B", new Dictionary<string, int>() { { "C", 1 } });
                    //g.AddVertexAndOutgoingEdges("C", new Dictionary<string, int>() { { "D", 1 } });
                    //g.AddVertexAndOutgoingEdges("D", new Dictionary<string, int>() { { "A", 1 }, { "C", 1 } });


                    //g.AddVertexAndOutgoingEdges("A", new Dictionary<string, int>() { { "E", 1 }, { "B", 3 } });
                    //g.AddVertexAndOutgoingEdges("B", new Dictionary<string, int>() { { "C", 1 } });
                    //g.AddVertexAndOutgoingEdges("C", new Dictionary<string, int>() { { "H", 1 }, { "E", 1 }, { "D", 1 } });
                    //g.AddVertexAndOutgoingEdges("D", new Dictionary<string, int>() { { "E", 3 }, { "C", 1 }, { "G", 1 } });
                    //g.AddVertexAndOutgoingEdges("E", new Dictionary<string, int>() { { "A", 1 }, { "F", 2 } });
                    //g.AddVertexAndOutgoingEdges("F", new Dictionary<string, int>() { { "G", 3 } });
                    //g.AddVertexAndOutgoingEdges("G", new Dictionary<string, int>() { { "F", 3 }, { "H", 2 } });
                    //g.AddVertexAndOutgoingEdges("H", new Dictionary<string, int>() { { "C", 1 }, { "G", 2 } });
                    g.AddVertexAndOutgoingEdges("A", new Dictionary<string, int>() { { "B", 1 }, { "C", 2 } });
                    g.AddVertexAndOutgoingEdges("B", new Dictionary<string, int>() { { "A", 1 }, { "D", 1 }, { "E", 2 } });
                    g.AddVertexAndOutgoingEdges("C", new Dictionary<string, int>() { { "A", 2 }, { "D", 2 } });
                    g.AddVertexAndOutgoingEdges("D", new Dictionary<string, int>() { { "C", 2 }, { "F", 1 }, { "B", 1 } });
                    g.AddVertexAndOutgoingEdges("E", new Dictionary<string, int>() { { "B", 2 }, { "F", 2 } });
                    g.AddVertexAndOutgoingEdges("F", new Dictionary<string, int>() { { "D", 1 }, { "E", 2 } });


                    //Console.Write("Give start vertex:\t");
                    //start = Console.ReadLine();
                    //Console.Write("Give finish vertex:\t");
                    //finish = Console.ReadLine();

                    var startD = (char)i;
                    start = startD.ToString();
                    var finishD = (char)j;
                    finish = finishD.ToString();


                    Console.WriteLine(start);
                    Console.WriteLine(finish);
                    Console.WriteLine("Vertex count: {0}", g.GetVertexCount());
                    Console.WriteLine("Edge count: {0}", g.GetEdgeCount());

                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    //g.PrintPath(g.ShortestPath(start, finish).Path);
                    //g.Suurballe(start, finish);
                    g.SuurballeDisjointVertices(start, finish);

                    sw.Stop();

                    Console.WriteLine("Elapsed={0}", sw.Elapsed);


                    Console.WriteLine("Finished");


                    Console.ReadKey();
                }
            }
        }
    }
}
