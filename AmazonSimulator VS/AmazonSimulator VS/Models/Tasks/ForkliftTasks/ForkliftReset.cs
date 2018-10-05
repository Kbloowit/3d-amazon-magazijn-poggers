using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ForkliftReset : IForkliftTask
    {
        /// <summary>
        /// Resets the forklift
        /// </summary>
        /// <param name="forklift">Forklift</param>
        public void startTask(ShelfTransporters forklift)
        {
            forklift.Move(32, 1000, 32);
        }

        /// <summary>
        /// Bool check if robot has reset
        /// </summary>
        /// <param name="forklift">Forklift</param>
        /// <returns>Forklift reset</returns>
        public bool taskCompleted(ShelfTransporters forklift)
        {
            return Math.Round(forklift.x, 1) == 32 && Math.Round(forklift.y, 1) == 1000 && Math.Round(forklift.z, 1) == 32;
        }
    }
}
