﻿using System;
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
        /// list of all shelfs in the world
        /// </summary>
        private List<Shelf> shelfs = new List<Shelf>();
        /// <summary>
        /// list of all forklifts in the world
        /// </summary>
        private List<ShelfTransporters> forklifts = new List<ShelfTransporters>();
        /// <summary>
        /// List of all shelves that are in place and ready to be picked up
        /// </summary>
        private List<Node> shelvesInPlace = new List<Node>();
        /// <summary>
        /// List of all shelves that are ready to be restocked(set back to its place)
        /// </summary>
        private List<Node> ShelfReplace = new List<Node>();
        /// <summary>
        /// Truck in the world
        /// </summary>
        private Truck truck;
        /// <summary>
        /// Train in the world
        /// </summary>
        private Train train;
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
            g.AddNodes();
        }

        /// <summary>
        /// Manages the complete simulation, move trucks, robots, shelfs and trains
        /// </summary>
        public void Update()
        {
            ShelvesInPlace();
            Robot robot = robots.Find(x => x.Status() == false);
            switch (Math.Round(truck.x, 1))
            {
                case -45:
                    bool trainBusy = false;
                    switch (Math.Round(train.x, 1))
                    {
                        case 58:
                            if (shelvesInPlace.Count() == 0 && robots.Exists(x => x.Status() == true) == false)
                            {
                                train.AddDestination(g.TransportVehicles("TrainMid"));
                                trainBusy = true;
                            }
                            break;
                        case 16:
                            if (train.GetItems() == 0 && train.Status() == false)
                            {
                                int counter = 0;
                                foreach (Shelf s in shelfs)
                                    if (s.Status() == false)
                                        counter++;
                                train.setItems(counter);
                                train.updateArrived();
                            }
                            else if (train.GetItems() != 0)
                            {
                                ShelfRestock(train);
                            }
                            else if (ShelfReplace.Count != 0)
                            {
                                if (robot != null)
                                    RobotRestockShelf(robot);
                            }
                            else if (train.GetItems() == 0 && robots.Exists(x => x.Status() == true) == false)
                            {
                                train.updateArrived();
                                train.AddDestination(g.TransportVehicles("TrainEnd"));
                            }
                            break;
                        case -45:
                            train.Move(58, 2, 35.8);
                            break;
                    }
                    if (Math.Round(train.x, 1) == 58 && ShelfReplace.Count() == 0 && train.Status() == false && trainBusy == false)
                        truck.AddDestination(g.TransportVehicles("TruckMid"));
                    break;
                case 16:
                    if (truck.GetItems() == 0 && truck.Status() == false)
                    {
                        truck.updateArrived();
                        truck.setItems(r.Next(1, shelvesInPlace.Count()));
                    }
                    else if (truck.GetItems() != 0 && robot != null)
                    {
                        if (shelvesInPlace.Count() != 0)
                        {
                            RobotGetShelf(robot);
                            truck.itemRemove();
                        }
                    }
                    else if (truck.GetItems() == 0 && robots.Exists(x => x.Status() == true) == false)
                    {
                        truck.updateArrived();
                        truck.AddDestination(g.TransportVehicles("TruckEnd"));
                    }
                    break;
                case 58:
                    truck.Move(-45, 1.5, -6);
                    break;
            }
            foreach (Robot r in robots)
                if (r.Status() == true && r.GetTaskCount() == 0)
                    r.UpdateStatus();

            foreach (ShelfTransporters f in forklifts)
                if (f.Status() == true && f.GetTaskCount() == 0)
                    f.UpdateStatus();
        }

        /// <summary>
        /// Give a robot tasks to get a shelf and bring it to the truck
        /// </summary>
        /// <param name="robot">Robot</param>
        private void RobotGetShelf(Robot robot)
        {
            Node shelfNode = shelvesInPlace[r.Next(0, shelvesInPlace.Count())];
            robot.AddTask(new RobotMove(g.Shortest_Path(robot.GetRobotStation().name, shelfNode.name)));
            robot.AddTask(new RobotPickUp(shelfNode, shelfNode.shelf));
            robot.AddTask(new RobotMove(g.Shortest_Path(shelfNode.name, "T")));
            robot.AddTask(new RobotDeliver(shelfNode.shelf));
            string test = robot.GetRobotStation().name + "2";
            robot.AddTask(new RobotMove(g.Shortest_Path("T", test)));
            robot.UpdateStatus();
            shelfNode.shelf.updateStatus();
        }

        /// <summary>
        /// Restock the shelves
        /// </summary>
        /// <param name="train">Train that brings the shelves</param>
        private void ShelfRestock(Train train)
        {
            ShelfTransporters forklift = forklifts.Find(x => x.Status() == false);
            Shelf shelf = shelfs.First(x => x.Status() == false && Math.Round(x.x, 1) == 16 && Math.Round(x.z, 1) == -2.4 && Math.Round(x.y, 1) == 1000);
            Node node = g.GetNodes().First(x => x.name.Contains("Res") && x.shelf == null);
            if (forklift != null && shelf != null && node != null)
            {
                forklift.Move(18, 0, 31);
                shelf.Move(18, 0, 31);
                forklift.AddShelf(shelf);
                forklift.AddTask(new ForkliftPickUp(shelf));
                forklift.AddTask(new ForkliftMove(g.Shortest_Path("Forklift", node.name)));
                forklift.AddTask(new ForkliftDeliver(node, shelf));
                forklift.AddTask(new ForkliftMove(g.Shortest_Path(node.name, "Forklift")));
                forklift.AddTask(new ForkliftReset());
                forklift.UpdateStatus();
                train.itemRemove();
                node.shelf = shelf;
                ShelfReplace.Add(node);
            }
        }

        /// <summary>
        /// Give a robot tasks to get a shelf from the resupply shelves and set it back to its place
        /// </summary>
        /// <param name="robot">Robot</param>
        private void RobotRestockShelf(Robot robot)
        {
            Node pickupNode = ShelfReplace.First();
            Node deliverNode = g.GetNodes().First(x => x.shelf == null && x.name.Contains("Shelf"));
            robot.AddTask(new RobotMove(g.Shortest_Path(robot.GetRobotStation().name, pickupNode.name)));
            robot.AddTask(new RobotPickUp(pickupNode, pickupNode.shelf));
            robot.AddTask(new RobotMove(g.Shortest_Path(pickupNode.name, deliverNode.name)));
            robot.AddTask(new RobotDeliver(pickupNode.shelf));
            deliverNode.shelf = pickupNode.shelf;
            deliverNode.shelf.updateStatus();
            ShelfReplace.RemoveAt(0);
            robot.AddTask(new RobotMove(g.Shortest_Path(deliverNode.name, ShelfReplace.First().name)));
            pickupNode = ShelfReplace.First();
            deliverNode = g.GetNodes().First(x => x.shelf == null && x.name.Contains("Shelf"));
            robot.AddTask(new RobotPickUp(pickupNode, pickupNode.shelf));
            robot.AddTask(new RobotMove(g.Shortest_Path(pickupNode.name, deliverNode.name)));
            robot.AddTask(new RobotDeliver(pickupNode.shelf));
            robot.AddTask(new RobotMove(g.Shortest_Path(deliverNode.name, robot.GetRobotStation().name + "2")));
            deliverNode.shelf = pickupNode.shelf;
            deliverNode.shelf.updateStatus();
            ShelfReplace.RemoveAt(0);
            robot.UpdateStatus();
        }

        /// <summary>
        /// Updates the shelves that are still in his place
        /// </summary>
        private void ShelvesInPlace()
        {
            shelvesInPlace.Clear();
            foreach (Node s in g.GetNodes())
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
        /// sets the truck
        /// </summary>
        /// <param name="truck">Truck</param>
        public void SetTruck(Truck truck)
        {
            this.truck = truck;
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
        /// Sets the train
        /// </summary>
        /// <param name="train">Train</param>
        public void SetTrain(Train train)
        {
            this.train = train;
        }

        /// <summary>
        /// Add forklift to list
        /// </summary>
        /// <param name="train">Train</param>
        public void AddForkliftToList(ShelfTransporters forklift)
        {
            forklifts.Add(forklift);
        }

        /// <summary>
        /// Get list of nodes
        /// </summary>
        /// <returns>all nodes</returns>
        public List<Node> GetGraphNodes()
        {
            return g.GetNodes();
        }
    }
}
