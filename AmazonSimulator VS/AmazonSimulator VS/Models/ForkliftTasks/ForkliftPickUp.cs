using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ForkliftPickUp : IForkliftTask
    {
        private Shelf shelf;

        public ForkliftPickUp(Shelf s)
        {
            this.shelf = s;
        }

        public void startTask(Forklift forklift)
        {
            shelf.Move(shelf.x, shelf.y + 0.3, shelf.z);
            forklift.addShelf(shelf);
        }

        public bool taskCompleted(Forklift forklift)
        {
            return Math.Round(forklift.x, 1) == shelf.x && Math.Round(forklift.z, 1) == shelf.z;
        }
    }
}
