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
        private List<Forklift> forklifts = new List<Forklift>();
        private List<Node> shelvesInPlace = new List<Node>();
        private List<Node> ShelfReplace = new List<Node>();
        private Graph g = new Graph();

        public WorldManager()
        {
            g.addNodes();
        }

        /// <summary>
        /// Manages the complete simulation, move trucks, robots, shelfs and trains
        /// </summary>
        public void Update()
        {
            ShelvesInPlace();
            Truck truck = trucks.First();
            Train train = trains.First();
            Robot robot = robots.Find(x => x.Status() == false);
                switch (Math.Round(truck.x, 1))
                {
                    case 0:
                    bool trainBusy = false;
                    switch (Math.Round(train.x, 1))
                    {
                        case 32:
                            if (shelvesInPlace.Count() == 0 && robots.Exists(x => x.Status() == true) == false)
                            {
                                train.AddDestination(g.transportVehicle("TrainMid"));
                                trainBusy = true;
                            }
                            break;
                        case 16:
                            bool test = g.getNodes().Exists(x => x.name.Contains("Res") && x.shelf != null);
                            if (train.GetItemlist().Count() == 0 && train.Status() == false)
                            {
                                foreach (Shelf s in shelfs)
                                    if (s.Status() == false)
                                        train.addItem("1");
                                train.updateArrived();
                            }
                            else if (train.GetItemlist().Count() != 0)
                            {
                                shelfRestock(train);
                            }
                            else if (ShelfReplace.Count != 0)
                            {
                                if (robot != null)
                                {
                                    Node FromNode = ShelfReplace.First();
                                    Node ToNode = g.getNodes().First(x => x.shelf == null && x.name.Contains("Shelf"));
                                    robot.addTask(new RobotMove(g.shortest_path(robot.getRobotStation().name, FromNode.name)));
                                    robot.addTask(new RobotPickUp(FromNode, FromNode.shelf));
                                    robot.addTask(new RobotMove(g.shortest_path(FromNode.name, ToNode.name)));
                                    robot.addTask(new RobotDeliver(FromNode.shelf));
                                    robot.addTask(new RobotReset());
                                    ToNode.shelf = FromNode.shelf;
                                    ToNode.shelf.updateStatus();
                                    robot.updateStatus();
                                    ShelfReplace.RemoveAt(0);
                                }
                            }
                            else if (train.GetItemlist().Count() == 0 && robots.Exists(x => x.Status() == true) == false && g.getNodes().Exists(x => x.name.Contains("Res") && x.shelf != null) == false)
                            {
                                train.updateArrived();
                                train.AddDestination(g.transportVehicle("TrainEnd"));
                            }
                            break;
                        case -8:
                            train.Move(32, 0, 32);
                            break;
                    }
                    if(Math.Round(train.x, 1) == 32 && ShelfReplace.Count() == 0 && train.Status() == false && trainBusy == false)
                        truck.AddDestination(g.transportVehicle("TruckMid"));
                    break;
                    case 16:
                        if (truck.GetItemlist().Count() == 0 && truck.Status() == false)
                        {
                            truck.updateArrived();
                            if (shelvesInPlace.Count() != 0)
                            {
                                Random r = new Random();
                                int random = r.Next(1, shelvesInPlace.Count());
                                for (int j = 0; j < 6; j++)
                                    truck.addItem("1");
                            }
                        }
                        else if (truck.GetItemlist().Count() != 0 && robot != null)
                        {
                            if (shelvesInPlace.Count() != 0)
                            {
                                Random r = new Random();
                                int random = r.Next(0, shelvesInPlace.Count());
                                Node shelfNode = shelvesInPlace[random]; //random node met een shelf erop
                                robot.addTask(new RobotMove(g.shortest_path(robot.getRobotStation().name, shelfNode.name)));
                                robot.addTask(new RobotPickUp(shelfNode, shelfNode.shelf));
                                robot.addTask(new RobotMove(g.shortest_path(shelfNode.name, "S")));
                                robot.addTask(new RobotDeliver(shelfNode.shelf));
                                robot.addTask(new RobotReset());
                                robot.updateStatus();
                                truck.itemListRemove();
                                shelfNode.shelf.updateStatus();
                            }
                        }
                        else if (truck.GetItemlist().Count() == 0 && robots.Exists(x => x.Status() == true) == false)
                        {
                            truck.updateArrived();
                            truck.AddDestination(g.transportVehicle("TruckEnd"));
                        }
                        break;
                    case 32:
                        truck.Move(0, 0, -5);
                        break;
                }
            
            

            foreach (Robot r in robots)
                if (r.Status() == true && r.getTasksCount() == 0)
                    r.updateStatus();

            foreach (Forklift f in forklifts)
                if (f.Status() == true && f.getTasksCount() == 0)
                    f.updateStatus();
        }

        /// <summary>
        /// Restock the shelves
        /// </summary>
        /// <param name="train">Train that brings the shelves</param>
        private void shelfRestock(Train train)
        {
            Forklift forklift = forklifts.Find(x => x.Status() == false);
            Shelf shelf = shelfs.First(x => x.Status() == false && Math.Round(x.x, 1) == 16 && Math.Round(x.z, 1) == 2);
            Node node = g.getNodes().First(x => x.name.Contains("Res") && x.shelf == null);
            if (forklift != null && shelf != null && node != null)
            {
                forklift.Move(18, 0, 31);
                shelf.Move(18, 0, 31);
                forklift.addShelf(shelf);
                forklift.addTask(new ForkliftPickUp(shelf));
                forklift.addTask(new ForkliftMove(g.shortest_path("Forklift", node.name)));
                forklift.addTask(new ForkliftDeliver(node, shelf));
                forklift.addTask(new ForkliftMove(g.shortest_path(node.name, "Forklift")));
                forklift.addTask(new ForkliftReset());
                forklift.updateStatus();
                train.itemListRemove();
                node.shelf = shelf;
                ShelfReplace.Add(node);
            }
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
        /// Add forklift to list
        /// </summary>
        /// <param name="train">Train</param>
        public void AddForkliftToList(Forklift forklift)
        {
            forklifts.Add(forklift);
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
