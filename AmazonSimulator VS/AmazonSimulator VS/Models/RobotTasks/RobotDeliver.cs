using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotDeliver : IRobotTask
    {
        Shelf shelf;

        public RobotDeliver(Shelf shelf)
        {
            this.shelf = shelf;
        }

        public void startTask(Robot robot)
        {
            shelf.Move(shelf.x, 0, shelf.z);
            robot.removeShelf();
        }

        public bool taskCompleted(Robot robot)
        {
            return true;
        }
    }
}
