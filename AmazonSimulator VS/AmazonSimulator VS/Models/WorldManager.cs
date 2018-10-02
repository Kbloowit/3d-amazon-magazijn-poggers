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
        private List<Train> trains = new List<Train>();
        private Graph g = new Graph();

        //shelfs verwijder list maken die de world checkt en verwijderd uit de worldobjectlist hij blijft dan nog wel in de wereld dus dan verplaats je hem naar 2000 ofzo
        
        public WorldManager()
        {
            g.addNodes();
        }

        /// <summary>
        /// Manages the complete simulation, move trucks, robots, shelfs and trains
        /// </summary>
        public void Update()
        {
            Truck truck = trucks.First();
            Robot robot = robots.Find(x => x.Status() == false);
            List<Node> shelvesInPlace = new List<Node>();
            foreach (Node s in g.getNodes())
                if (s.shelf != null)
                    if (s.shelf.Status() == true)
                        shelvesInPlace.Add(s); //vul de lijst van shelverInPlace met nodes die een shelferop hebben staan
            if (Math.Round(truck.x) == 0)
            {
                truck.AddDestination(g.transportVehicle("TruckMid"));
            }
            else if (Math.Round(truck.x, 1) == 16)
            {
                if (truck.GetPacklist().Count() == 0 && truck.Status() == false && truck.Arrived() == false)
                {
                    if(shelvesInPlace.Count() != 0)
                    {
                    Random r = new Random();
                    int random = r.Next(1, shelvesInPlace.Count());
                    for (int j = 0; j <= random; j++)
                        truck.addPackage("1");
                    }
                    truck.updateArrived();
                }
                else if (truck.GetPacklist().Count() != 0 && robot != null)
                {
                    if(shelvesInPlace.Count() != 0)
                    {
                    Random r = new Random();
                    int random = r.Next(0, shelvesInPlace.Count());
                    Node shelfNode = shelvesInPlace[random]; //random node met een shelf erop
                    robot.addTask(new RobotMove(g.shortest_path(robot.getRobotStation().name, shelfNode.name)));
                    robot.addTask(new RobotPickUp(shelfNode.shelf));
                    robot.addTask(new RobotMove(g.shortest_path(shelfNode.name, "S")));
                    robot.addTask(new RobotDeliver());
                    robot.addTask(new RobotReset());
                    robot.updateStatus();
                    truck.packlistRemove();
                    shelfNode.shelf.updateStatus();
                    shelfNode.shelf = null;
                    }
                }
                if (truck.GetPacklist().Count() == 0 && robots.Exists(x => x.Status() == true) == false)
                {
                    truck.updateArrived();
                    truck.updateArrived();
                    truck.AddDestination(g.transportVehicle("TruckEnd"));
                }
            }
            else if (Math.Round(truck.x, 1) == 32)
                truck.Move(truck.x - truck.x, truck.y, truck.z);

            foreach (Robot r in robots)
                if (r.Status() == true && r.getTasksCount() == 0)
                    r.updateStatus();

            //if(shelvesInPlace.Count() == 0 && robot.Status() == false && truck.Status())
            //{
            //    shelfRestock();
            //}
        }

        private void shelfRestock()
        {
            Train train = trains.First();
            train.Move(32, 0, 32);
            train.AddDestination(g.transportVehicle("TrainMid"));
            if(Math.Round(train.x, 1) == 16)
            {
                if (train.GetCargoList().Count() == 0 && train.Status() == false && train.Arrived() == false)
                {
                    foreach (Node node in g.getNodes())
                        if (node.shelf == null)
                            train.addCargo("1");
                    train.updateArrived();
                }
                else if(train.GetCargoList().Count() != 0 && train.Status() == false)
                {

                    foreach (Shelf s in shelfs)
                    {
                        if (s.Status() == false && s.x == 16 && s.z == 2)
                        {
                            s.Move(g.getNodes().Find(z => z.shelf == null).x, g.getNodes().Find(z => z.shelf == null).y, g.getNodes().Find(z => z.shelf == null).x);
                        }
                    }
                }

            }

        }

        /// <summary>
        /// Add robot to list
        /// </summary>
        /// <param name="robot">Robot</param>
        public void AddRobotToList(Robot robot)
        {
            robots.Add(robot);
        }

        /// <summary>
        /// Add truck to list
        /// </summary>
        /// <param name="truck">Truck</param>
        public void AddTruckToList(Truck truck)
        {
            trucks.Add(truck);
        }

        /// <summary>
        /// Add shelf to list
        /// </summary>
        /// <param name="shelf">Shelfs</param>
        public void AddShelfToList(Shelf shelf)
        {
            shelfs.Add(shelf);
        }

        /// <summary>
        /// Add train to list
        /// </summary>
        /// <param name="train">Train</param>
        public void AddTrainToList(Train train)
        {
            trains.Add(train);
        }

        public List<Node> getGraphNodes()
        {
            return g.getNodes();
        }
    }
}
