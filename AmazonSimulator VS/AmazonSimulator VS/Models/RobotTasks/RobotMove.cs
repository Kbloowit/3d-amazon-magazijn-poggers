using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotMove : IRobotTask
    {
        /// <summary>
        /// List of nodes the robot should take
        /// </summary>
        private List<Node> path;

        /// <summary>
        /// Constructor of RobotMove
        /// </summary>
        /// <param name="path">Path</param>
        public RobotMove(List<Node> path)
        {
            this.path = path;
        }

        /// <summary>
        /// Move robot of his path
        /// </summary>
        /// <param name="robot">robot</param>
        public void startTask(Robot robot)
        {
            robot.MoveOverPath(this.path);
        }

        /// <summary>
        /// Bool if task is completed
        /// </summary>
        /// <param name="robot">robot</param>
        /// <returns>Robot arrived at last destination</returns>
        public bool taskCompleted(Robot robot)
        {
            return Math.Round(robot.x, 1) == path.Last().x && Math.Round(robot.z, 1) == path.Last().z;
        }
    }
}
