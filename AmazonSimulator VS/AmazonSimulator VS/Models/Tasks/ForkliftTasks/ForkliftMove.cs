using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ForkliftMove : IForkliftTask
    {
        /// <summary>
        /// List of nodes the forklift should take
        /// </summary>
        private List<Node> path;

        /// <summary>
        /// Constructor of ForkliftMove
        /// </summary>
        /// <param name="path">Path the forklift should take</param>
        public ForkliftMove(List<Node> path)
        {
            this.path = path;
        }

        /// <summary>
        /// Move robot of his path
        /// </summary>
        /// <param name="robot">robot</param>
        public void StartTask(ShelfTransporters forklift)
        {
            forklift.MoveOverPath(this.path);
        }

        /// <summary>
        /// Bool if forklift has arrived at his last destination
        /// </summary>
        /// <param name="forklift">Forklift</param>
        /// <returns>Forklift arrived</returns>
        public bool TaskCompleted(ShelfTransporters forklift)
        {
            return Math.Round(forklift.x, 1) == path.Last().x && Math.Round(forklift.z, 1) == path.Last().z;
        }
    }
}
