using System.Collections.Generic;

namespace Suurballe_s_Algorithm
{
    class DijkstraOut //Used to return all usefull information from Dijkstra Algorithm Implementation.
    {
        public List<string> Path { get; set; }
        public Dictionary<string, string> Parents { get; set; }
        public Dictionary<string, int> Distances { get; set; }
        public Dictionary<string,string> EdgePath { get; set; }
        public DijkstraOut(List<string> _Path, Dictionary<string, string> _Parents, Dictionary<string, int> _Distances)
        {
            Path = _Path;
            Parents = _Parents;
            Distances = _Distances;
            Dictionary<string, string> DictionaryPathTemp = new Dictionary<string, string>();
            var Previous = Path[0];
            foreach (var Node in Path)
            {             
                if (Node != Path[0])
                DictionaryPathTemp.Add(Previous,Node);               
                Previous = Node;
            }
            EdgePath = DictionaryPathTemp;
        }        
        public DijkstraOut()
        {
            
        }
    }

}
