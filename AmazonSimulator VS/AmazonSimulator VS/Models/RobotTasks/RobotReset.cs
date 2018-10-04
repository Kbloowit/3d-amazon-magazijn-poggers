using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotReset : IRobotTask
    {
        /// <summary>
        /// Resets the robot to its robotstation
        /// </summary>
        /// <param name="robot">Robot</param>
        public void startTask(Robot robot)
        {
            robot.robotReset();
        }

        /// <summary>
        /// Bool check if robot is reset
        /// </summary>
        /// <param name="robot">Robot</param>
        /// <returns>Robot reset</returns>
        public bool taskCompleted(Robot robot)
        {
            return Math.Round(robot.x, 1) == robot.getRobotStation().x && Math.Round(robot.z, 1) == robot.getRobotStation().z;
        }
    }
}
