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
        private Graph g = new Graph();

        public WorldManager()
        {
            g.addNodes();
        }

        public void Update()
        {
            Truck truck = trucks.First();
            Robot robot = robots.Find(x => x.Status() == false);
            Shelf shelf = shelfs.First();
            if (Math.Round(truck.x) == 0)
            {
                truck.AddDestination(g.truckPath('u'));
            }
            else if (Math.Round(truck.x, 1) == 16)
            {
                if (truck.GetPacklist().Count() == 0 && truck.Status() == false && truck.Arrived() == false)
                {
                    Random r = new Random();
                    int random = r.Next(2, 6);
                    for (int j = 0; j < 1; j++)
                        truck.addPackage("1");
                    truck.updateArrived();
                }
                else if (truck.GetPacklist().Count() != 0 && robot != null)
                {
                    robot.addTask(new RobotMove(g.shortest_path('P', 'I')));//eigenlijk eerst naar shelf zoeken niet gwn een nummer
                    robot.addTask(new RobotPickUp(shelf));
                    robot.addTask(new RobotMove(g.shortest_path('I', 'R')));
                    robot.updateStatus();
                    truck.packlistRemove();
                }
                if (truck.GetPacklist().Count() == 0 && robots.Exists(x => x.Status() == true) == false)
                {
                    truck.updateStatus();
                    truck.updateArrived();
                    truck.AddDestination(g.truckPath('v'));
                }
            }
            else if (Math.Round(truck.x, 1) == 32)
                truck.AddDestination(g.truckPath('t')); // or truck.resetTruck (moet nog anders)

            foreach (Robot r in robots)
                if (r.Status() == true && r.getTasksCount() == 0)
                    r.updateStatus();
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
