using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotReset : IRobotTask
    {
        public void startTask(Robot robot)
        {
            robot.robotReset();
        }

        public bool taskCompleted(Robot robot)
        {
            return true;
        }
    }
}
