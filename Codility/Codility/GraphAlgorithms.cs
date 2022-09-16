using System;
using System.Collections.Generic;

namespace Codility
{
    public class GraphAlgorithms
    {
        public HashSet<T> BFS<T>(Graph<T> graph, T start)
        {
            var visited = new HashSet<T>();

            if (!graph.AdjacencyList.ContainsKey(start))
                return visited;

            var queue = new Queue<T>();
            queue.Enqueue(start);
            while(queue.Count > 0)
            {
                var vertex = queue.Dequeue();

                if(visited.Contains(vertex))
                    continue;

                visited.Add(vertex);

                // get neighbourghs
                foreach (T neighbourgh in graph.AdjacencyList[vertex])
                {
                    if (!visited.Contains(neighbourgh))
                    {
                        queue.Enqueue(neighbourgh);
                    }
                }
                Console.WriteLine("Queue: " + string.Join(", ", queue));
            }

            return visited;
        }

        // uses BFS search behind the scenes
        public Func<T, IEnumerable<T>> ShortestPathFunction<T>(Graph<T> graph, T start)
        {
            var previous = new Dictionary<T, T>();

            var queue = new Queue<T>();
            queue.Enqueue(start);

            while(queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (T neighbourgh in graph.AdjacencyList[vertex])
                {
                    if (previous.ContainsKey(neighbourgh))
                        continue;

                    previous[neighbourgh] = vertex;
                    Console.WriteLine("Previous K: " + String.Join(",", previous.Keys));
                    Console.WriteLine("Previous V: " + String.Join(",", previous.Values));
                    queue.Enqueue(neighbourgh);
                }
            }

            Func<T, IEnumerable<T>> shortestPath = v => 
            { 
                var path = new List<T>();

                var current = v;
                while (!current.Equals(start))
                {
                    path.Add(current);
                    current = previous[current];
                }

                path.Add(start);
                path.Reverse();

                return path;
            };

            return shortestPath;
        }
    }
}
