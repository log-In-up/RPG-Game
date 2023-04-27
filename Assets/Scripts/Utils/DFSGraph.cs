using System;
using System.Collections.Generic;

namespace Utills
{
    public sealed class DFSGraph
    {
        #region Fields
        private LinkedList<int>[] _adjacencyList;
        #endregion

        public DFSGraph(ushort vertices)
        {
            _adjacencyList = new LinkedList<int>[vertices];

            for (ushort index = 0; index < vertices; index++)
                _adjacencyList[index] = new LinkedList<int>();
        }

        #region Public methods
        internal void AddEdges(HashSet<Tuple<int, int>> edges)
        {
            foreach (Tuple<int, int> item in edges)
            {
                _adjacencyList[item.Item1].AddLast(item.Item2);
            }
        }

        internal bool IsReachable(int source, int destination)
        {
            if (source == destination) return true;

            foreach (var x in _adjacencyList[source])
            {
                if (IsReachable(x, destination) == true)
                    return true;
            }
            return false;
        }
        #endregion
    }
}