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
        private List<Node> shelvesInPlace = new List<Node>();
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
            Train train = trains.First();
            Robot robot = robots.Find(x => x.Status() == false);
            ShelvesInPlace();
            switch(Math.Round(truck.x, 1))
            {
                case 0:
                    truck.AddDestination(g.transportVehicle("TruckMid"));
                    break;
                case 16:
                    if (truck.GetPacklist().Count() == 0 && truck.Arrived() == false)
                    {
                        truck.updateArrived();
                        if (shelvesInPlace.Count() != 0)
                        {
                            Random r = new Random();
                            int random = r.Next(1, shelvesInPlace.Count());
                            for (int j = 0; j < 6; j++)
                                truck.addPackage("1");
                        }
                    }
                    else if (truck.GetPacklist().Count() != 0 && robot != null)
                    {
                        if (shelvesInPlace.Count() != 0)
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
                    else if (truck.GetPacklist().Count() == 0 && robots.Exists(x => x.Status() == true) == false)
                    {
                        truck.updateArrived();
                        truck.AddDestination(g.transportVehicle("TruckEnd"));
                    }
                    break;
                case 32:
                    truck.Move(0, 0, -5);
                    break;
            }
            switch(Math.Round(train.x, 1)){
                case 32:
                    if(shelvesInPlace.Count() == 0 && robots.Exists(x => x.Status() == true) == false)
                    train.AddDestination(g.transportVehicle("TrainMid"));
                    break;
                case 16:
                    if (train.GetCargoList().Count() == 0 && train.Arrived() == false)
                    {
                        foreach (Shelf s in shelfs)
                            if (s.Status() == false)
                                train.addCargo("1");
                        train.updateArrived();
                    }
                    else if (train.GetCargoList().Count() != 0)
                        shelfRestock(train);
                    break;
                case -8:
                    train.Move(32, 0, 32);
                    break;
            }

            foreach (Robot r in robots)
                if (r.Status() == true && r.getTasksCount() == 0)
                    r.updateStatus();
        }

        /// <summary>
        /// Restock the shelves
        /// </summary>
        /// <param name="train">Train that brings the shelves</param>
        private void shelfRestock(Train train)
        {
            foreach (Shelf s in shelfs)
                if (s.Status() == false)
                    if (Math.Round(s.x, 1) == 16 && Math.Round(s.z, 1) == 2)
                    {
                        //robot tasks geven
                        Node node = g.getNodes().First(x => x.name.Contains("Shelf") && x.shelf == null);
                        s.Move(node.x, node.y, node.z);
                        s.Rotate(0, 0, 0);
                        s.updateStatus();
                        node.shelf = s;
                    }
            train.updateArrived();
            for (int i = 0; i < train.GetCargoList().Count(); i++)
                train.cargolistRemove();
            train.AddDestination(g.transportVehicle("TrainEnd"));
        }

        /// <summary>
        /// Updates the shelves that are still in his place
        /// </summary>
        public void ShelvesInPlace()
        {
            shelvesInPlace.Clear();
            foreach (Node s in g.getNodes())
                if (s.shelf != null)
                    if (s.shelf.Status() == true)
                        shelvesInPlace.Add(s);
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

        /// <summary>
        /// Get list of nodes
        /// </summary>
        /// <returns>all nodes</returns>
        public List<Node> getGraphNodes()
        {
            return g.getNodes();
        }
    }
}
