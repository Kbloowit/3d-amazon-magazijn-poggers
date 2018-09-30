using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotPickUp : IRobotTask
    {
        private Shelf shelf;

        public RobotPickUp(Shelf s)
        {
            this.shelf = s;
        }

        public void startTask(Robot robot)
        {
            robot.addShelf(shelf);
        }

        public bool taskCompleted(Robot robot)
        {
            return Math.Round(robot.x, 1) == shelf.x && Math.Round(robot.z, 1) == shelf.z;
        }
    }
}
