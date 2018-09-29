using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class WorldManager
    {
        private List<Robot> robots = new List<Robot>();
        private List<Truck> trucks = new List<Truck>();
        private List<Shelf> shelfs = new List<Shelf>();

        public WorldManager()
        {

        }

        public void robotsUpdate()
        {
            Robot robot = robots.Find(x => x.getStatus() == false);
            
        }

        public void AddRobotToList(Robot robot)
        {
            robots.Add(robot);
        }

        public void AddTruckToList(Truck truck)
        {
            trucks.Add(truck);
        }

        public void AddShelfToList(Shelf shelf)
        {
            shelfs.Add(shelf);
        }

    }
}
