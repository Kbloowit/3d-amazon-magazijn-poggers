using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ForkliftDeliver : IForkliftTask
    {
        private Shelf shelf;
        private Node node;

        public ForkliftDeliver(Node node, Shelf shelf)
        {
            this.node = node;
            this.shelf = shelf;
        }
        public void startTask(Forklift forklift)
        {
            node.shelf = null;
            shelf.Move(shelf.x, 0, shelf.z);
            forklift.removeShelf();
        }

        public bool taskCompleted(Forklift forklift)
        {
            return true;
        }
    }
}
