using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public interface IRobotTask :ITask
    {
        /// <summary>
        /// Start a task
        /// </summary>
        /// <param name="robot">Robot</param>
        void StartTask(Robot robot);

        /// <summary>
        /// Checks if task is completed
        /// </summary>
        /// <param name="robot">Robot</param>
        /// <returns>Completed</returns>
        bool TaskCompleted(Robot robot);
    }
}
