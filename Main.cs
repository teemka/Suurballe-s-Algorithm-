using System;
using System.Collections.Generic;

namespace Suurballe_s_Algorithm
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Graph g = new Graph();

            //Example from https://en.wikipedia.org/wiki/Suurballe's_algorithm
            /*
            g.AddVertexAndOutgoingEdges('A', new Dictionary<char, int>() { { 'B', 1 }, { 'C', 2 } });
            g.AddVertexAndOutgoingEdges('B', new Dictionary<char, int>() { { 'A', 1 }, { 'D', 1 }, { 'E', 2 } });
            g.AddVertexAndOutgoingEdges('C', new Dictionary<char, int>() { { 'A', 2 }, { 'D', 2 } });
            g.AddVertexAndOutgoingEdges('D', new Dictionary<char, int>() { { 'C', 2 }, { 'F', 1 }, { 'B', 1 } });
            g.AddVertexAndOutgoingEdges('E', new Dictionary<char, int>() { { 'B', 2 }, { 'F', 2 } });
            g.AddVertexAndOutgoingEdges('F', new Dictionary<char, int>() { { 'D', 1 }, { 'E', 2 } });
            //g.AddVertex('Z');
            */
            /*
            g.AddVertexAndOutgoingEdges('A', new Dictionary<char, int>() { { 'B', 1 }, { 'C', 3 } });
            g.AddVertexAndOutgoingEdges('B', new Dictionary<char, int>() { { 'A', 1 }, { 'D', 1 } });
            g.AddVertexAndOutgoingEdges('C', new Dictionary<char, int>() { { 'A', 3 }, { 'E', 3 } });
            g.AddVertexAndOutgoingEdges('D', new Dictionary<char, int>() { { 'B', 1 }, { 'E', 1 }, { 'F', 4 } });
            g.AddVertexAndOutgoingEdges('E', new Dictionary<char, int>() { { 'C', 3 }, { 'D', 1 }, { 'G', 1 } });
            g.AddVertexAndOutgoingEdges('F', new Dictionary<char, int>() { { 'D', 4 }, { 'G', 1 }, { 'H', 1 } });
            g.AddVertexAndOutgoingEdges('G', new Dictionary<char, int>() { { 'F', 1 }, { 'E', 1 }, { 'I', 4 } });
            g.AddVertexAndOutgoingEdges('H', new Dictionary<char, int>() { { 'F', 1 }, { 'I', 1 }, { 'J', 4 } });
            g.AddVertexAndOutgoingEdges('I', new Dictionary<char, int>() { { 'G', 4 }, { 'H', 1 }, { 'J', 1 } });
            g.AddVertexAndOutgoingEdges('J', new Dictionary<char, int>() { { 'H', 4 }, { 'I', 1 } });
            */
            g.AddVertexAndOutgoingEdges('A', new Dictionary<char, int>() { { 'B', 7 }, { 'C', 8 } });
            g.AddVertexAndOutgoingEdges('B', new Dictionary<char, int>() { { 'A', 7 }, { 'F', 2 } });
            g.AddVertexAndOutgoingEdges('C', new Dictionary<char, int>() { { 'A', 8 }, { 'F', 6 }, { 'G', 4 } });
            g.AddVertexAndOutgoingEdges('D', new Dictionary<char, int>() { { 'F', 8 } });
            g.AddVertexAndOutgoingEdges('E', new Dictionary<char, int>() { { 'H', 1 } });
            g.AddVertexAndOutgoingEdges('F', new Dictionary<char, int>() { { 'B', 2 }, { 'C', 6 }, { 'D', 8 }, { 'G', 9 }, { 'H', 3 } });
            g.AddVertexAndOutgoingEdges('G', new Dictionary<char, int>() { { 'C', 4 }, { 'F', 9 } });
            g.AddVertexAndOutgoingEdges('H', new Dictionary<char, int>() { { 'E', 1 }, { 'F', 3 } });


            //g.PrintPath(g.ShortestPath('A', 'F').Path);

            //g.Suurballe('A', 'F');
            g.Suurballe('A', 'H');
            
            Console.ReadLine();
        }
    }
}
