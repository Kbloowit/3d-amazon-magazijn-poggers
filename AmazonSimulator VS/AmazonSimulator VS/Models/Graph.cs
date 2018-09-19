﻿using System;
using System.Collections.Generic;

namespace Models
{
    class Graph
    {
        Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();

        public void add_vertex(char name, Dictionary<char, int> edges)
        {
            vertices[name] = edges;
        }

        public List<Node> shortest_path(char start, char finish, List<Node> allNodes)
        {
            var previous = new Dictionary<char, char>();
            var distances = new Dictionary<char, int>();
            var nodes = new List<char>();

            List<char> path = new List<char>();
            List<Node> nodePath = new List<Node>();

            foreach (var vertex in vertices)
            {
                if (vertex.Key == start)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = int.MaxValue;
                }

                nodes.Add(vertex.Key);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == finish)
                {
                    //path = new List<char>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == int.MaxValue)
                {
                    break;
                }

                foreach (var neighbor in vertices[smallest])
                {
                    var alt = distances[smallest] + neighbor.Value;
                    if (alt < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alt;
                        previous[neighbor.Key] = smallest;
                    }
                }
            }
            path.Add(start);
            path.Reverse();
            for (int i = 0; i < path.Count; i++)
            {
                foreach (Node l in allNodes)
                {
                    if (l.name == path[i])
                    {
                        nodePath.Add(l);
                    }
                }
            }
            return nodePath;
        }
    }
}