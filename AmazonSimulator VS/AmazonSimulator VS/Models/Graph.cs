using System;
using System.Collections.Generic;

namespace Models
{
    public class Graph
    {
        Dictionary<string, Dictionary<string, int>> vertices = new Dictionary<string, Dictionary<string, int>>();
        /// <summary>
        /// List containing all of the nodes on the main plane
        /// </summary>
        private List<Node> nodes = new List<Node>();

        public void add_vertex(string name, Dictionary<string, int> edges)
        {
            vertices[name] = edges;
        }

        public List<Node> shortest_path(string from, string to)
        {
            var previous = new Dictionary<string, string>();
            var distances = new Dictionary<string, int>();
            var nodeChar = new List<string>();

            List<string> path = new List<string>();
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

        public List<Node> getNodes()
        {
            return nodes;
        }

        public Node transportVehicle(string to)
        {
            Node node = nodes.Find(x => x.name == to);
            return node;
        }

        public void AddConnections()
        {
            nodes[nodes.FindIndex(a => a.name == "A")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "P")], nodes[nodes.FindIndex(a => a.name == "C")] });
            nodes[nodes.FindIndex(a => a.name == "B")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "S")], nodes[nodes.FindIndex(a => a.name == "D")] });
            nodes[nodes.FindIndex(a => a.name == "C")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "A")], nodes[nodes.FindIndex(a => a.name == "E")], nodes[nodes.FindIndex(a => a.name == "G")] });
            nodes[nodes.FindIndex(a => a.name == "D")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "B")], nodes[nodes.FindIndex(a => a.name == "F")], nodes[nodes.FindIndex(a => a.name == "ShelfI")] });
            nodes[nodes.FindIndex(a => a.name == "E")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "C")], nodes[nodes.FindIndex(a => a.name == "ConA")] });
            nodes[nodes.FindIndex(a => a.name == "ConA")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "E")], nodes[nodes.FindIndex(a => a.name == "ConB")], nodes[nodes.FindIndex(a => a.name == "ResA")] });
            nodes[nodes.FindIndex(a => a.name == "ResA")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConA")] });
            nodes[nodes.FindIndex(a => a.name == "ConB")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConA")], nodes[nodes.FindIndex(a => a.name == "ConC")], nodes[nodes.FindIndex(a => a.name == "ResB")] });
            nodes[nodes.FindIndex(a => a.name == "ResB")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConB")] });
            nodes[nodes.FindIndex(a => a.name == "ConC")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConB")], nodes[nodes.FindIndex(a => a.name == "ConD")], nodes[nodes.FindIndex(a => a.name == "ResC")] });
            nodes[nodes.FindIndex(a => a.name == "ResC")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConC")] });
            nodes[nodes.FindIndex(a => a.name == "ConD")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConC")], nodes[nodes.FindIndex(a => a.name == "ConE")], nodes[nodes.FindIndex(a => a.name == "ResD")] });
            nodes[nodes.FindIndex(a => a.name == "ResD")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConD")] });
            nodes[nodes.FindIndex(a => a.name == "ConE")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConD")], nodes[nodes.FindIndex(a => a.name == "ConF")], nodes[nodes.FindIndex(a => a.name == "ResE")] });
            nodes[nodes.FindIndex(a => a.name == "ResE")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConE")] });
            nodes[nodes.FindIndex(a => a.name == "ConF")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConE")], nodes[nodes.FindIndex(a => a.name == "E")], nodes[nodes.FindIndex(a => a.name == "Forklift")], nodes[nodes.FindIndex(a => a.name == "ResF")] });
            nodes[nodes.FindIndex(a => a.name == "ResF")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConF")] });
            nodes[nodes.FindIndex(a => a.name == "Forklift")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "ConF")] });
            nodes[nodes.FindIndex(a => a.name == "F")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "D")], nodes[nodes.FindIndex(a => a.name == "ConF")] });
            nodes[nodes.FindIndex(a => a.name == "G")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "C")], nodes[nodes.FindIndex(a => a.name == "J")], nodes[nodes.FindIndex(a => a.name == "ShelfI")], nodes[nodes.FindIndex(a => a.name == "ShelfH")] });
            nodes[nodes.FindIndex(a => a.name == "ShelfH")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "G")] });
            nodes[nodes.FindIndex(a => a.name == "ShelfI")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "G")] });
            nodes[nodes.FindIndex(a => a.name == "J")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "G")], nodes[nodes.FindIndex(a => a.name == "M")], nodes[nodes.FindIndex(a => a.name == "ShelfK")], nodes[nodes.FindIndex(a => a.name == "ShelfL")] });
            nodes[nodes.FindIndex(a => a.name == "ShelfK")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "J")] });
            nodes[nodes.FindIndex(a => a.name == "ShelfL")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "J")] });
            nodes[nodes.FindIndex(a => a.name == "M")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "J")], nodes[nodes.FindIndex(a => a.name == "D")], nodes[nodes.FindIndex(a => a.name == "ShelfN")], nodes[nodes.FindIndex(a => a.name == "ShelfO")] });
            nodes[nodes.FindIndex(a => a.name == "ShelfN")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "M")] });
            nodes[nodes.FindIndex(a => a.name == "ShelfO")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "M")] });
            nodes[nodes.FindIndex(a => a.name == "P")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "A")], nodes[nodes.FindIndex(a => a.name == "Q")] });
            nodes[nodes.FindIndex(a => a.name == "Q")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "P")], nodes[nodes.FindIndex(a => a.name == "R")] });
            nodes[nodes.FindIndex(a => a.name == "R")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "Q")] });
            nodes[nodes.FindIndex(a => a.name == "S")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "B")] });
            nodes[nodes.FindIndex(a => a.name == "TruckStart")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "TruckMid")] });
            nodes[nodes.FindIndex(a => a.name == "TruckMid")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "TruckStart")], nodes[nodes.FindIndex(a => a.name == "TrainEnd")] });
            nodes[nodes.FindIndex(a => a.name == "TruckEnd")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "TruckMid")] });
            nodes[nodes.FindIndex(a => a.name == "TrainStart")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "TrainMid")] });
            nodes[nodes.FindIndex(a => a.name == "TrainMid")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "TrainStart")], nodes[nodes.FindIndex(a => a.name == "TrainEnd")] });
            nodes[nodes.FindIndex(a => a.name == "TrainEnd")].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == "TrainMid")] });

            foreach (Node item in nodes)
            {
                Dictionary<string, int> een = new Dictionary<string, int>();
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
            nodes.Add(new Node("A", 2, 0, 2));//hoekpunt
            nodes.Add(new Node("B", 30, 0, 2));//hoekpunt
            nodes.Add(new Node("C", 2, 0, 16));//midden links
            nodes.Add(new Node("D", 30, 0, 16));//midden rechts
            nodes.Add(new Node("E", 2, 0, 30));//hoekpunt
            nodes.Add(new Node("ConA", 8, 0, 30));//connectie naar ResA
            nodes.Add(new Node("ResA", 8, 0, 28));//Shelf Restock1
            nodes.Add(new Node("ConB", 10, 0, 30));//connectie naar ResB
            nodes.Add(new Node("ResB", 10, 0, 28));//Shelf Restock2
            nodes.Add(new Node("ConC", 12, 0, 30));//connectie naar ResC
            nodes.Add(new Node("ResC", 12, 0, 28));//Shelf Restock3
            nodes.Add(new Node("ConD", 14, 0, 30));//connectie naar ResD
            nodes.Add(new Node("ResD", 14, 0, 28));//Shelf Restock4
            nodes.Add(new Node("ConE", 16, 0, 30));//connectie naar ResE
            nodes.Add(new Node("ResE", 16, 0, 28));//Shelf Restock5
            nodes.Add(new Node("ConF", 18, 0, 30));//connectie naar ResF
            nodes.Add(new Node("ResF", 18, 0, 28));//Shelf Restock6
            nodes.Add(new Node("Forklift", 18, 0, 31));//Forklift node
            nodes.Add(new Node("F", 30, 0, 30));//hoekpunt
            nodes.Add(new Node("G", 4, 0, 16));//connectie node
            nodes.Add(new Node("ShelfH", 4, 0, 14));//shelf node
            nodes.Add(new Node("ShelfI", 4, 0, 18));//shelf node
            nodes.Add(new Node("J", 16, 0, 16));//connectie node
            nodes.Add(new Node("ShelfK", 16, 0, 14));//shelf node
            nodes.Add(new Node("ShelfL", 16, 0, 18));//shelf node
            nodes.Add(new Node("M", 28, 0, 16));//connectie node
            nodes.Add(new Node("ShelfN", 28, 0, 14));//shelf node
            nodes.Add(new Node("ShelfO", 28, 0, 18));//shelf node
            nodes.Add(new Node("P", 13, 0, 2));//robot node
            nodes.Add(new Node("Q", 14, 0, 2));//robot node
            nodes.Add(new Node("R", 15, 0, 2));//robot node
            nodes.Add(new Node("S", 16, 0, 2));//robot node
            nodes.Add(new Node("TruckStart", 0, 0, 0));//truck start
            nodes.Add(new Node("TruckMid", 16, 0, 0));//truck midden
            nodes.Add(new Node("TruckEnd", 32, 0, 0));//truck eind
            nodes.Add(new Node("TrainStart", 40, 0, 32));//train start
            nodes.Add(new Node("TrainMid", 16, 0, 32));//train midden
            nodes.Add(new Node("TrainEnd", -8, 0, 32));//train eind
            AddConnections();
        }
    }
}