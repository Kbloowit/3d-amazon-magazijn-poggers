using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotDeliver : IRobotTask
    {
        public void startTask(Robot robot)
        {
            robot.removeShelf();
        }

        public bool taskCompleted(Robot robot)
        {
            return true;
        }
    }
}
