using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class WorldManager : IUpdatable
    {
        private List<Robot> robots = new List<Robot>();

        public WorldManager()
        {

        }

        public bool Update(int tick)
        {
            foreach (Robot x in robots)
            {
                if (x.getStatus() == true && x.getDestinations().Count() == 0)
                {
                    x.updateStatus();
                }
            }
            return true;
        }

        public void AddRobotToList(Robot robot)
        {
            robots.Add(robot);
        }

        
    }
}
