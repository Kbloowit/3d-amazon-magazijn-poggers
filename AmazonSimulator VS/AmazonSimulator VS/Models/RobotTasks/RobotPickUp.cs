using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotPickUp : IRobotTask
    {
        /// <summary>
        /// Shelf to pickup
        /// </summary>
        private Shelf shelf;
        /// <summary>
        /// Node the shelf is picked-up from
        /// </summary>
        private Node node;

        /// <summary>
        /// Constructor of RobotPickUp
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="s">Shelf</param>
        public RobotPickUp(Node node, Shelf s)
        {
            this.node = node;
            this.shelf = s;
        }

        /// <summary>
        /// Pick up the shelf
        /// </summary>
        /// <param name="robot">Robot</param>
        public void startTask(Robot robot)
        {
            node.shelf = null;
            shelf.Move(shelf.x, shelf.y + 0.3, shelf.z);
            robot.addShelf(shelf);
        }

        /// <summary>
        /// Bool check if the shelf is picked-up
        /// </summary>
        /// <param name="robot">robot</param>
        /// <returns>robotShelfStatus</returns>
        public bool taskCompleted(Robot robot)
        {
            return robot.robotShelfStatus() == true;
        }
    }
}
