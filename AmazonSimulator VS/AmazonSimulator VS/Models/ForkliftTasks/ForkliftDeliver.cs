﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Class that gives the forklift the task to put a shelf on a delivery node
    /// </summary>
    public class ForkliftDeliver : IForkliftTask
    {
        /// <summary>
        /// Shelf to be delivered
        /// </summary>
        private Shelf shelf;
        /// <summary>
        /// Node to place the shelf on
        /// </summary>
        private Node node;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="node">Node to place shelf on</param>
        /// <param name="shelf">Shelf to deliver</param>
        public ForkliftDeliver(Node node, Shelf shelf)
        {
            this.node = node;
            this.shelf = shelf;
        }

        /// <summary>
        /// Starts the delivery task
        /// </summary>
        /// <param name="forklift">The forklift that needs to deliver the shelf</param>
        public void startTask(Forklift forklift)
        {
            node.shelf = shelf;
            shelf.Move(shelf.x, 0, shelf.z);
            forklift.removeShelf();
        }

        /// <summary>
        /// Bool Check if shelf is delivered
        /// </summary>
        /// <param name="forklift"></param>
        /// <returns>Shelf delivered</returns>
        public bool taskCompleted(Forklift forklift)
        {
            return forklift.forkliftShelfStatus() == false && node.shelf != null;
        }
    }
}
