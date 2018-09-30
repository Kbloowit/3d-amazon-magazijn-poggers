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
            return done;
        }

        public void startTask(Robot robot)
        {
            //opdracht geven aan de robot, hij begint nu dus done == false
            done = false;
            throw new NotImplementedException();
            //wanneer de robot klaar is done op true zetten
            //done = true;
        }
    }
}
