﻿using System;
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
        public void StartTask(ShelfTransporters robot)
        {
            if (Math.Round(robot.x, 1) == 16 && Math.Round(robot.z, 1) == -2.4)
                shelf.Move(shelf.x, 1000, shelf.z);
            else
                shelf.Move(shelf.x, 0, shelf.z);
            robot.RemoveShelf();
        }

        /// <summary>
        /// Check if shelf is delivered
        /// </summary>
        /// <param name="robot">robot</param>
        /// <returns>robotShelfStatus</returns>
        public bool TaskCompleted(ShelfTransporters robot)
        {
            return robot.ShelfTransporterShelfStatus() == false;
        }
    }
}
