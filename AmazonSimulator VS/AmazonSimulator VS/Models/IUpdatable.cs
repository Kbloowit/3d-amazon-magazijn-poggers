using System;
using System.Collections.Generic;
using System.Linq;

namespace Models {
    interface IUpdatable
    {
        /// <summary>
        /// Updates the world (excuted 20 times per second)
        /// </summary>
        /// <param name="tick">server tick</param>
        /// <returns>boolean value indicating sucessfull update</returns>
        bool Update(int tick);
    }
}
