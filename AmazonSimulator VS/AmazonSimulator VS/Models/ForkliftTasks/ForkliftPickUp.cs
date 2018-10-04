using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ForkliftPickUp : IForkliftTask
    {
        /// <summary>
        /// Shelf the forklift will pick up
        /// </summary>
        private Shelf shelf;

        /// <summary>
        /// Constructor of ForkliftPickUp
        /// </summary>
        /// <param name="s">Shelf to pick up</param>
        public ForkliftPickUp(Shelf s)
        {
            this.shelf = s;
        }

        /// <summary>
        /// Forklift pick up the shelf
        /// </summary>
        /// <param name="forklift">Forklift</param>
        public void startTask(Forklift forklift)
        {
            shelf.Move(shelf.x, shelf.y + 0.3, shelf.z);
            forklift.addShelf(shelf);
        }

        /// <summary>
        /// Bool if forklift has picked-up the shelf
        /// </summary>
        /// <param name="forklift">Forklift</param>
        /// <returns>Shelf picked-upS</returns>
        public bool taskCompleted(Forklift forklift)
        {
            return Math.Round(forklift.x, 1) == shelf.x && Math.Round(forklift.z, 1) == shelf.z;
        }
    }
}
