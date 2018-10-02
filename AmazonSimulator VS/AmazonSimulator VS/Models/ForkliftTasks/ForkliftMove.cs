using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ForkliftMove : IForkliftTask
    {
        private bool startupComplete = false;
        private bool complete = false;
        private List<Node> path = new List<Node>();

        public ForkliftMove(List<Node> path)
        {
            this.path = path;
        }

        public void startTask(Forklift forklift)
        {
            forklift.MoveOverPath(this.path);
        }

        public bool taskCompleted(Forklift forklift)
        {
            return Math.Round(forklift.x, 1) == path.Last().x && Math.Round(forklift.z, 1) == path.Last().z;
        }
    }
}
