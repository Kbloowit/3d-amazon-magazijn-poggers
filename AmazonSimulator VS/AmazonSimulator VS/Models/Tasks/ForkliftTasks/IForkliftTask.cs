using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public interface IForkliftTask : ITask
    {
        /// <summary>
        /// Start a task
        /// </summary>
        /// <param name="forklift">Forklift</param>
        void startTask(ShelfTransporters forklift);

        /// <summary>
        /// Bool check if task is completed
        /// </summary>
        /// <param name="forklift">forklift</param>
        /// <returns>TaskCompleted</returns>
        bool taskCompleted(ShelfTransporters forklift);
    }
}
