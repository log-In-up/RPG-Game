using System;
using System.Collections.Generic;
using System.Linq;

namespace Utills
{
    public static class Sorting
    {
        public static List<T> TopologicalSort<T>(HashSet<T> nodes, HashSet<Tuple<T, T>> edges) where T : IEquatable<T>
        {
            List<T> list = new();

            HashSet<T> hashSet = new(nodes.Where(node => edges.All(edge => edge.Item2.Equals(node) == false)));

            while (hashSet.Any())
            {
                T node = hashSet.First();
                hashSet.Remove(node);

                list.Add(node);

                List<Tuple<T, T>> tupleEdges = edges.Where(edge => edge.Item1.Equals(node)).ToList();

                foreach (Tuple<T, T> edge in tupleEdges)
                {
                    T item = edge.Item2;

                    edges.Remove(edge);

                    if (edges.All(itemEdge => itemEdge.Item2.Equals(item) == false))
                    {
                        hashSet.Add(item);
                    }
                }
            }

            return edges.Any() ? null : list;
        }
    }
}