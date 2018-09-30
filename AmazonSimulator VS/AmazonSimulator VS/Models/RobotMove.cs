using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotMove : IRobotTask
{
        private bool startupComplete = false;
        private bool complete = false;
        private List<Node> path = new List<Node>();

        public RobotMove(List<Node> path)
        {
            this.path = path;
        }

        public void startTask(Robot robot)
        {
            robot.MoveOverPath(this.path);
        }

        public bool taskCompleted(Robot robot)
        {
            return Math.Round(robot.x, 1) == path.Last().x && Math.Round(robot.z, 1) == path.Last().z;
        }
    }
}
