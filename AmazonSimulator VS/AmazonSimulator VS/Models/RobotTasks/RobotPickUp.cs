using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotPickUp : IRobotTask
    {
        private Shelf shelf;
        private Node node;

        public RobotPickUp(Node node, Shelf s)
        {
            this.node = node;
            this.shelf = s;
        }

        public void startTask(Robot robot)
        {
            node.shelf = null;
            shelf.Move(shelf.x, shelf.y + 0.3, shelf.z);
            robot.addShelf(shelf);
        }

        public bool taskCompleted(Robot robot)
        {
            return true;
        }
    }
}
