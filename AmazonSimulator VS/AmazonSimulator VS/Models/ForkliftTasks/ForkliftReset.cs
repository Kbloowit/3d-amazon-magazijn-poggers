using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ForkliftReset : IForkliftTask
    {
        public void startTask(Forklift forklift)
        {
            forklift.Move(32, 1000, 32);
        }

        public bool taskCompleted(Forklift forklift)
        {
            return true;
        }
    }
}
