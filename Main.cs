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

            g.AddVertexAndOutgoingEdges('A', new Dictionary<char, int>() { { 'B', 1 }, { 'C', 2 } });
            g.AddVertexAndOutgoingEdges('B', new Dictionary<char, int>() { { 'A', 1 }, { 'D', 1 }, { 'E', 2 } });
            g.AddVertexAndOutgoingEdges('C', new Dictionary<char, int>() { { 'A', 2 }, { 'D', 2 } });
            g.AddVertexAndOutgoingEdges('D', new Dictionary<char, int>() { { 'C', 2 }, { 'F', 1 }, { 'B', 1 } });
            g.AddVertexAndOutgoingEdges('E', new Dictionary<char, int>() { { 'B', 2 }, { 'F', 2 } });
            g.AddVertexAndOutgoingEdges('F', new Dictionary<char, int>() { { 'D', 1 }, { 'E', 2 } });
            //g.AddVertex('Z');
            


            //g.PrintPath(g.ShortestPath('A', 'F').Path);

            g.Suurballe('A', 'F');
            Console.ReadLine();
        }
    }
}
