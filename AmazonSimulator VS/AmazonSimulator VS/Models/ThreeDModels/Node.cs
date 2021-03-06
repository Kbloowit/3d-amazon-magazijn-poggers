﻿using System;
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
        public string name { get; set; }
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
        /// Shelf that is on the node
        /// </summary>
        public Shelf shelf { get; set; }
        /// <summary>
        /// List of node the node is connected to
        /// </summary>
        public List<Node> connections = new List<Node>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">given name</param>
        /// <param name="x">given x cordinate</param>
        /// <param name="y">given y cordinate</param>
        /// <param name="z">given z cordinate</param>
        public Node(string name, double x, double y, double z)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}