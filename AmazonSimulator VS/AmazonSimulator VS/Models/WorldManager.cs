using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class WorldManager
    {
        /// <summary>
        /// List of all robots in the world
        /// </summary>
        private List<Robot> robots = new List<Robot>();
        /// <summary>
        /// List of all trucks in the world
        /// </summary>
        private List<Truck> trucks = new List<Truck>();
        /// <summary>
        /// list of all shelfs in the world
        /// </summary>
        private List<Shelf> shelfs = new List<Shelf>();
        /// <summary>
        /// list of all train in the world
        /// </summary>
        private List<Train> trains = new List<Train>();
        /// <summary>
        /// list of all forklifts in the world
        /// </summary>
        private List<Forklift> forklifts = new List<Forklift>();
        /// <summary>
        /// List of all shelves that are in place and ready to be picked up
        /// </summary>
        private List<Node> shelvesInPlace = new List<Node>();
        /// <summary>
        /// List of all shelves that are ready to be restocked(set back to its place)
        /// </summary>
        private List<Node> ShelfReplace = new List<Node>();
        /// <summary>
        /// Instance of the Graph class
        /// </summary>
        private Graph g = new Graph();
        /// <summary>
        /// Instance of Random
        /// </summary>
        private Random r = new Random();

        /// <summary>
        /// Constructor of the worldmanager class
        /// </summary>
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
                                    robotRestockShelf(robot);
                            }
                            else if (train.GetItemlist().Count() == 0 && robots.Exists(x => x.Status() == true) == false)
                            {
                                train.updateArrived();
                                train.AddDestination(g.transportVehicle("TrainEnd"));
                            }
                            break;
                        case -8:
                            train.Move(32, 0, 32);
                            break;
                    }
                    if (Math.Round(train.x, 1) == 32 && ShelfReplace.Count() == 0 && train.Status() == false && trainBusy == false)
                        truck.AddDestination(g.transportVehicle("TruckMid"));
                    break;
                case 16:
                    if (truck.GetItemlist().Count() == 0 && truck.Status() == false)
                    {
                        truck.updateArrived();
                        for (int j = 0; j < r.Next(1, shelvesInPlace.Count()); j++)
                            truck.addItem("1");
                    }
                    else if (truck.GetItemlist().Count() != 0 && robot != null)
                    {
                        if (shelvesInPlace.Count() != 0)
                        {
                            robotGetShelf(robot);
                            truck.itemListRemove();
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
        /// Give a robot tasks to get a shelf and bring it to the truck
        /// </summary>
        /// <param name="robot">Robot</param>
        private void robotGetShelf(Robot robot)
        {
            Node shelfNode = shelvesInPlace[r.Next(0, shelvesInPlace.Count())];
            robot.addTask(new RobotMove(g.shortest_path(robot.getRobotStation().name, shelfNode.name)));
            robot.addTask(new RobotPickUp(shelfNode, shelfNode.shelf));
            robot.addTask(new RobotMove(g.shortest_path(shelfNode.name, "S")));
            robot.addTask(new RobotDeliver(shelfNode.shelf));
            robot.addTask(new RobotReset());
            robot.updateStatus();
            shelfNode.shelf.updateStatus();
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
        /// Give a robot tasks to get a shelf from the resupply shelves and set it back to its place
        /// </summary>
        /// <param name="robot">Robot</param>
        private void robotRestockShelf(Robot robot)
        {
            Node FromNode = ShelfReplace.First();
            Node ToNode = g.getNodes().First(x => x.shelf == null && x.name.Contains("Shelf"));
            robot.addTask(new RobotMove(g.shortest_path(robot.getRobotStation().name, FromNode.name)));
            robot.addTask(new RobotPickUp(FromNode, FromNode.shelf));
            robot.addTask(new RobotMove(g.shortest_path(FromNode.name, ToNode.name)));
            robot.addTask(new RobotDeliver(FromNode.shelf));
            ToNode.shelf = FromNode.shelf;
            ToNode.shelf.updateStatus();
            ShelfReplace.RemoveAt(0);
            robot.addTask(new RobotMove(g.shortest_path(ToNode.name, ShelfReplace.First().name)));
            FromNode = ShelfReplace.First();
            ToNode = g.getNodes().First(x => x.shelf == null && x.name.Contains("Shelf"));
            robot.addTask(new RobotPickUp(FromNode, FromNode.shelf));
            robot.addTask(new RobotMove(g.shortest_path(FromNode.name, ToNode.name)));
            robot.addTask(new RobotDeliver(FromNode.shelf));
            robot.addTask(new RobotMove(g.shortest_path(ToNode.name, robot.getRobotStation().name)));
            ToNode.shelf = FromNode.shelf;
            ToNode.shelf.updateStatus();
            ShelfReplace.RemoveAt(0);
            robot.updateStatus();
        }

        /// <summary>
        /// Updates the shelves that are still in his place
        /// </summary>
        private void ShelvesInPlace()
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
