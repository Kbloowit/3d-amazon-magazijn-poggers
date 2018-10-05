using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotDeliver : ITask
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
        public void startTask(ShelfTransporters robot)
        {
            shelf.Move(shelf.x, 0, shelf.z);
            robot.removeShelf();
        }

        /// <summary>
        /// Check if shelf is delivered
        /// </summary>
        /// <param name="robot">robot</param>
        /// <returns>robotShelfStatus</returns>
        public bool taskCompleted(ShelfTransporters robot)
        {
            return robot.ShelfTransporterShelfStatus() == false;
        }
    }
}
