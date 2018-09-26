using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Node
    {
        public char name { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public List<Node> connections = new List<Node>();

        public Node(char name, double x, double y, double z)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public char GetName()
        {
            return name;
        }

        public double GetX()
        {
            return x;
        }
    }
}
