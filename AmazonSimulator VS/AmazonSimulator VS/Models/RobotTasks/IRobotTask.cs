using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public interface IRobotTask
{
        void startTask(Robot robot);

        bool taskCompleted(Robot robot);
}
}
