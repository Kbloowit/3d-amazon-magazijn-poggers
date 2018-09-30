using System;
using System.Collections.Generic;

namespace Models
{
    class Graph
    {
        Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();
        /// <summary>
        /// List containing all of the nodes on the main plane
        /// </summary>
        public List<Node> nodes = new List<Node>();

        public void add_vertex(char name, Dictionary<char, int> edges)
        {
            vertices[name] = edges;
        }

        public List<Node> shortest_path(char from, char to)
        {
            var previous = new Dictionary<char, char>();
            var distances = new Dictionary<char, int>();
            var nodeChar = new List<char>();

            List<char> path = new List<char>();
            List<Node> nodePath = new List<Node>();

            foreach (var vertex in vertices)
            {
                if (vertex.Key == from)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = int.MaxValue;
                }

                nodeChar.Add(vertex.Key);
            }

            while (nodeChar.Count != 0)
            {
                nodeChar.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodeChar[0];
                nodeChar.Remove(smallest);

                if (smallest == to)
                {
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
            path.Reverse();
            for (int i = 0; i < path.Count; i++)
            {
                foreach (Node l in nodes)
                {
                    if (l.name == path[i])
                    {
                        nodePath.Add(l);
                    }
                }
            }
            return nodePath;
        }

        public Node truckPath(char to)
        {
            Node node = nodes.Find(x => x.name == to);
            return node;
        }

        public void AddConnections()
        {
            nodes[nodes.FindIndex(a => a.name == 'A')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'P')], nodes[nodes.FindIndex(a => a.name == 'C')] });
            nodes[nodes.FindIndex(a => a.name == 'B')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'S')], nodes[nodes.FindIndex(a => a.name == 'D')] });
            nodes[nodes.FindIndex(a => a.name == 'C')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'A')], nodes[nodes.FindIndex(a => a.name == 'E')], nodes[nodes.FindIndex(a => a.name == 'G')] });
            nodes[nodes.FindIndex(a => a.name == 'D')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'B')], nodes[nodes.FindIndex(a => a.name == 'F')], nodes[nodes.FindIndex(a => a.name == 'I')] });
            nodes[nodes.FindIndex(a => a.name == 'E')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'C')], nodes[nodes.FindIndex(a => a.name == 'F')] });
            nodes[nodes.FindIndex(a => a.name == 'F')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'D')], nodes[nodes.FindIndex(a => a.name == 'E')] });
            nodes[nodes.FindIndex(a => a.name == 'G')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'C')], nodes[nodes.FindIndex(a => a.name == 'I')] });
            nodes[nodes.FindIndex(a => a.name == 'H')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'G')] });
            nodes[nodes.FindIndex(a => a.name == 'I')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'G')] });
            nodes[nodes.FindIndex(a => a.name == 'J')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'G')], nodes[nodes.FindIndex(a => a.name == 'M')] });
            nodes[nodes.FindIndex(a => a.name == 'K')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'J')] });
            nodes[nodes.FindIndex(a => a.name == 'L')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'J')] });
            nodes[nodes.FindIndex(a => a.name == 'M')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'J')], nodes[nodes.FindIndex(a => a.name == 'D')] });
            nodes[nodes.FindIndex(a => a.name == 'N')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'M')] });
            nodes[nodes.FindIndex(a => a.name == 'O')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'M')] });
            nodes[nodes.FindIndex(a => a.name == 'P')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'A')], nodes[nodes.FindIndex(a => a.name == 'Q')] });
            nodes[nodes.FindIndex(a => a.name == 'Q')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'P')], nodes[nodes.FindIndex(a => a.name == 'R')] });
            nodes[nodes.FindIndex(a => a.name == 'R')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'Q')] });
            nodes[nodes.FindIndex(a => a.name == 'S')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'B')] });
            nodes[nodes.FindIndex(a => a.name == 't')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'u')] });
            nodes[nodes.FindIndex(a => a.name == 'u')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 't')], nodes[nodes.FindIndex(a => a.name == 'v')] });
            nodes[nodes.FindIndex(a => a.name == 'v')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'u')] });

            foreach (Node item in nodes)
            {
                Dictionary<char, int> een = new Dictionary<char, int>();
                foreach (Node connection in item.connections)
                {
                    int deltaX = Math.Abs((int)item.x - (int)connection.x);
                    int deltaZ = Math.Abs((int)item.z - (int)connection.z);
                    int sum = deltaX + deltaZ;
                    een.Add(connection.name, sum);
                }
                add_vertex(item.name, een);
            }
        }

        public void addNodes()
        {
            nodes.Add(new Node('A', 2, 0, 2));//hoekpunt
            nodes.Add(new Node('B', 30, 0, 2));//hoekpunt
            nodes.Add(new Node('C', 2, 0, 16));//midden links
            nodes.Add(new Node('D', 30, 0, 16));//midden rechts
            nodes.Add(new Node('E', 2, 0, 30));//hoekpunt
            nodes.Add(new Node('F', 30, 0, 30));//hoekpunt
            nodes.Add(new Node('G', 4, 0, 16));//connectie node
            nodes.Add(new Node('H', 4, 0, 14));//shelf node
            nodes.Add(new Node('I', 4, 0, 18));//shelf node
            nodes.Add(new Node('J', 16, 0, 14));//connectie node
            nodes.Add(new Node('K', 16, 0, 16));//shelf node
            nodes.Add(new Node('L', 16, 0, 18));//shelf node
            nodes.Add(new Node('M', 28, 0, 14));//connectie node
            nodes.Add(new Node('N', 28, 0, 16));//shelf node
            nodes.Add(new Node('O', 28, 0, 18));//shelf node
            nodes.Add(new Node('P', 13, 0, 2));//robot node
            nodes.Add(new Node('Q', 14, 0, 2));//robot node
            nodes.Add(new Node('R', 15, 0, 2));//robot node
            nodes.Add(new Node('S', 16, 0, 2));//robot node
            nodes.Add(new Node('t', 0, 0, 0));//truck start
            nodes.Add(new Node('u', 16, 0, 0));//truck midden
            nodes.Add(new Node('v', 32, 0, 0));//truck eind
            AddConnections();
        }
    }
}