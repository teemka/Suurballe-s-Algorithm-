﻿using System;
using System.Threading.Tasks;
using System.IO;

namespace Suurballe_s_Algorithm
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Graph g = new Graph();
            string start, finish, line;
            int lineNumber = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\tomas\Downloads\network_3hE8S8ohRErocpkY7uJW4a.gdf");
            while ((line = file.ReadLine()) != null)
            {
                string[] fields = line.Split(',');
                if (lineNumber > 0 && lineNumber < 234)
                {
                    g.AddVertexName(fields[0], fields[1]);
                }
                if (lineNumber > 235)
                {
                    g.AddEdge(fields[0], fields[1], 1);
                    g.AddEdge(fields[1], fields[0], 1);
                }
                lineNumber++;
            }            
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
            /*
            g.AddVertexAndOutgoingEdges("A", new Dictionary<string, int>() { { "B", 1 }, { "C", 3 } });
            g.AddVertexAndOutgoingEdges("B", new Dictionary<string, int>() { { "A", 1 }, { "D", 1 } });
            g.AddVertexAndOutgoingEdges("C", new Dictionary<string, int>() { { "A", 3 }, { "E", 3 } });
            g.AddVertexAndOutgoingEdges("D", new Dictionary<string, int>() { { "B", 1 }, { "E", 1 }, { "F", 4 } });
            g.AddVertexAndOutgoingEdges("E", new Dictionary<string, int>() { { "C", 3 }, { "D", 1 }, { "G", 1 }, { "F", 1 } });
            g.AddVertexAndOutgoingEdges("F", new Dictionary<string, int>() { { "D", 4 }, { "G", 1 }, { "H", 1 } });
            g.AddVertexAndOutgoingEdges("G", new Dictionary<string, int>() { { "F", 1 }, { "E", 1 }, { "I", 4 } });
            g.AddVertexAndOutgoingEdges("H", new Dictionary<string, int>() { { "F", 1 }, { "I", 1 }, { "J", 4 } });
            g.AddVertexAndOutgoingEdges("I", new Dictionary<string, int>() { { "G", 4 }, { "H", 1 }, { "J", 1 } });
            g.AddVertexAndOutgoingEdges("J", new Dictionary<string, int>() { { "H", 4 }, { "I", 1 } });
            */
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

            Console.Write("Give start vertex:\t");
            start = g.VerticesNamesToID[Console.ReadLine()];
            Console.Write("Give finish vertex:\t");
            finish = g.VerticesNamesToID[Console.ReadLine()];

            
            g.PrintPath(g.ShortestPath(start, finish).Path);
            g.SuurballeDisjointVertices(start, finish);                
            
            Console.ReadLine();
        }
    }
}
