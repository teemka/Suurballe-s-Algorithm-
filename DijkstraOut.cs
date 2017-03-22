﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suurballe_s_Algorithm
{
    class DijkstraOut //Used to return all usefull information from Dijkstra Algorithm Implementation.
    {
        public List<char> Path { get; set; }
        public Dictionary<char, char> Parents { get; set; }
        public Dictionary<char, int> Distances { get; set; }
        public Dictionary<char,char> DictionaryPath { get; set; }
        public DijkstraOut(List<char> _Path, Dictionary<char, char> _Parents, Dictionary<char, int> _Distances)
        {
            Path = _Path;
            Parents = _Parents;
            Distances = _Distances;
            Dictionary<char, char> DictionaryPathTemp = new Dictionary<char, char>();
            var Previous = Path[0];
            foreach (var Node in Path)
            {
                
#pragma warning disable CS0642 // Possible mistaken empty statement
                if (Node == Path[0]) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
                else
                DictionaryPathTemp.Add(Previous,Node);               
                Previous = Node;
            }
            DictionaryPath = DictionaryPathTemp;
        }        
    }

}
