using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public interface IForkliftTask
    {
        void startTask(Forklift forklift);

        bool taskCompleted(Forklift forklift);
    }
}
