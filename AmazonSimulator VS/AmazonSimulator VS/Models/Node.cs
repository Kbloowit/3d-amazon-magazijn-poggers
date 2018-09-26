using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// A "node" is a point in the 3d world where the objects can move to.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// The name of the node (get, set)
        /// </summary>
        public char name { get; set; }
        /// <summary>
        /// The X cordinate for the node
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// The Y cordinate for the node
        /// </summary>
        public double y { get; set; }
        /// <summary>
        /// The Z cordinate for the node
        /// </summary>
        public double z { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">given name</param>
        /// <param name="x">given x cordinate</param>
        /// <param name="y">given y cordinate</param>
        /// <param name="z">given z cordinate</param>
        public Node(char name, double x, double y, double z)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
        }
        /// <summary>
        /// Returns the name of the node
        /// </summary>
        /// <returns>Char name</returns>
        public char GetName()
        {
            return name;
        }
        /// <summary>
        /// Returns the x coridnate of the node
        /// </summary>
        /// <returns>Double x</returns>
        public double GetX()
        {
            return x;
        }
    }
}
