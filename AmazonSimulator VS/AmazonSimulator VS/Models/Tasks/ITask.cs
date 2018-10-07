using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public interface ITask
    {
        /// <summary>
        /// Tasks for the shelf Transporters
        /// </summary>
        /// <param name="shelfTransporters">Robots/Forklifts</param>
        void StartTask(ShelfTransporters shelfTransporter);

        /// <summary>
        /// Bool check if task is completed
        /// </summary>
        /// <param name="shelfTransporters">Robots/Forklifts</param>
        /// <returns>Bool Completed</returns>
        bool TaskCompleted(ShelfTransporters shelfTransporter);
    }
}