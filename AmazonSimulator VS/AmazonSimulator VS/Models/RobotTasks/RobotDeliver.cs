using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotDeliver : IRobotTask
    {
        /// <summary>
        /// Shelf to Deliver
        /// </summary>
        Shelf shelf;

        /// <summary>
        /// Constructor of RobotDeliver
        /// </summary>
        /// <param name="shelf">Shelf</param>
        public RobotDeliver(Shelf shelf)
        {
            this.shelf = shelf;
        }

        /// <summary>
        /// Deliver the shelf
        /// </summary>
        /// <param name="robot">Robot</param>
        public void startTask(Robot robot)
        {
            shelf.Move(shelf.x, 1000, shelf.z);
            robot.removeShelf();
        }

        /// <summary>
        /// Check if shelf is delivered
        /// </summary>
        /// <param name="robot">robot</param>
        /// <returns>robotShelfStatus</returns>
        public bool taskCompleted(Robot robot)
        {
            return robot.robotShelfStatus() == false;
        }
    }
}
