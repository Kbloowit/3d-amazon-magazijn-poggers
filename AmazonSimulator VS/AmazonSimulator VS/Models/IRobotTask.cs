using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    interface IRobotTask
{
        void startTask(Robot robot);

        bool robotDone(Robot robot);
}
}
