using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotManager : IRobotTask
{
        //private bool ready = false;
        private bool done = false;

        private List<Node> path = new List<Node>();

        public bool robotDone(Robot robot)
        {
            //robot check of hij klaar is
            return true;
        }

        public void startTask(Robot robot)
        {
            //opdracht geven aan de robot. toevoegen aan tasks?
            throw new NotImplementedException();
        }
    }
}
