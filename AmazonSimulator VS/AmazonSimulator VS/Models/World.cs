using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models
{
    public class World : IObservable<Command>, IUpdatable
    {
        /// <summary>
        /// List containing all objects currently loaded in the world
        /// </summary>
        List<ThreeDModels> worldObjects = new List<ThreeDModels>();
        /// <summary>
        /// list with all observers
        /// </summary>
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        /// <summary>
        /// List containing all of the nodes on the main plane
        /// </summary>
        public List<Node> nodes = new List<Node>();
        /// <summary>
        /// Instance of the Graph class
        /// </summary>
        Graph g = new Graph();
        /// <summary>
        /// Instance of the worldmanager class
        /// </summary>
        WorldManager manager = new WorldManager();

        /// <summary>
        /// Constructor
        /// </summary>
        public World()
        {
            Robot robot1 = CreateRobot(13, 0, 2);
            Robot robot2 = CreateRobot(14, 0, 2);
            Robot robot3 = CreateRobot(15, 0, 2);
            Truck t = CreateTruck(0, 1, -5);
            Shelf s = CreateShelf(4, 0, 18);
            addNodes();
        }

        public void moveRobot(Char from, Char to, int robotIndex)
        {
            
            List<Node> nodePath = g.shortest_path(from, to, nodes);
            foreach (Node item in nodePath)
                Console.WriteLine(item.name);

            for (int i = 0; i < nodePath.Count(); i++)
            {
                worldObjects[robotIndex].AddDestination(nodePath[i]);
            }
        }
        /// <summary>
        /// Allows the truck to move to a node
        /// </summary>
        /// <param name="truckIndex">The truck that needs to move</param>
        /// <param name="to">Destination node</param>
        public void moveTruck(int truckIndex, char to)
        {
            var node = from s in nodes
                       where s.name == to
                       select s;
            foreach (Node i in node)
                worldObjects[truckIndex].AddDestination(i);
        }

        /// <summary>
        /// Creates a new instance of a robot object
        /// </summary>
        /// <param name="x">starting x cordinate in the world</param>
        /// <param name="y">starting y cordinate in the world</param>
        /// <param name="z">starting z cordinate in the world</param>
        /// <returns>Robot object</returns>
        private Robot CreateRobot(double x, double y, double z)
        {
            Robot r = new Robot(x, y, z, 0, 0, 0);
            worldObjects.Add(r);
            manager.AddRobotToList(r);
            return r;
        }
        /// <summary>
        /// Creates a new instance of a truck object
        /// </summary>
        /// <param name="x">starting x cordinate in the world</param>
        /// <param name="y">starting y cordinate in the world</param>
        /// <param name="z">starting z cordinate in the world</param>
        /// <returns>Truck object</returns>
        private Truck CreateTruck(double x, double y, double z)
        {
            Truck t = new Truck(x, y, z, 0, Math.PI, 0);
            worldObjects.Add(t);
            return t;
        }
        /// <summary>
        /// Creates a new instance of a truck object
        /// </summary>
        /// <param name="x">starting x cordinate in the world</param>
        /// <param name="y">starting y cordinate in the world</param>
        /// <param name="z">starting z cordinate in the world</param>
        /// <returns>Shelf object</returns>
        private Shelf CreateShelf(double x, double y, double z)
        {
            Shelf s = new Shelf(x, y, z, 0, 0, 0);
            worldObjects.Add(s);
            return s;
        }

        /// <summary>
        /// Adds observers to a list
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);

                SendCreationCommandsToObserver(observer);
            }
            return new Unsubscriber<Command>(observers, observer);
        }

        private void SendCommandToObservers(Command c)
        {
            for (int i = 0; i < this.observers.Count; i++)
            {
                this.observers[i].OnNext(c);
            }
        }

        private void SendCreationCommandsToObserver(IObserver<Command> obs)
        {
            foreach (ThreeDModels m3d in worldObjects)
            {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }
        }
        /// <summary>
        /// Updates the world (excuted 20 times per second)
        /// </summary>
        /// <param name="tick">server tick</param>
        /// <returns>boolean value indicating sucessfull update</returns>
        public bool Update(int tick)
        {
            for (int i = 0; i < worldObjects.Count; i++)
            {
                ThreeDModels u = worldObjects[i];

                var trucks = from world in worldObjects
                             where world.type == "truck"
                             select world;

                var shelfs = from world in worldObjects
                             where world.type == "shelf"
                             select world;

                var robots = from world in worldObjects
                             where world.type == "robot"
                             select world;

                foreach(Robot x in robots)
                {
                    if (x.getStatus() == true && x.getDestinations().Count() == 0)
                    {
                        x.updateStatus();
                    }
                }
                List<Truck> truck = new List<Truck>();
                foreach (Truck t in trucks)
                    truck.Add(t);

                if(truck[0].x == 0 && truck[0].getStatus() == false)
                {
                    int indexTruck = worldObjects.FindIndex(a => a.guid == truck[0].guid);
                    moveTruck(indexTruck, 'u');
                }
                if(Math.Round(truck[0].x) == 16 && truck[0].getStatus() == false)
                    {
                    if(truck[0].GetPacklist().Count() == 0 && Math.Round(truck[0].x) == 16 && truck[0].getStatus() == false)
                    {
                        Random r = new Random();
                        int random = r.Next(2, 6);
                        for (int j = 0; j < random; j++)
                            truck[0].addPackage("1");
                    }

                    if (truck[0].GetPacklist().Count() != 0)
                        {
                            var r = from world in (from world in worldObjects where world.type == "robot" select world)
                                    where world.getStatus() == false
                                    select world;
                            List<Robot> notBusyRobots = new List<Robot>();
                            foreach (Robot x in r)
                                notBusyRobots.Add(x);

                        if(notBusyRobots.Count() != 0 && truck[0].GetPacklist().Count() != 0)//een robot selecteren die niet busy is
                        {
                            int indicator = truck[0].GetPacklist().Count();
                            int indexRobot = worldObjects.FindIndex(a => a.guid == notBusyRobots[0].guid);
                            //zoek waar het paket is op de shelves
                            notBusyRobots[0].updateStatus();
                            moveRobot('P', 'I', indexRobot);
                            moveRobot('I', 'S', indexRobot);
                            truck[0].packlistRemove();
                        }
                    }
                        
                    }
                    if (truck[0].GetPacklist().Count() == 0 && Math.Round(worldObjects[0].x) == 30 && Math.Round(worldObjects[0].z) == 2)
                    {
                    truck[0].updateStatus();
                    //resetTruck(worldObjects.FindIndex(a => a.guid == truck[0].guid));
                    }
                if (truck[0].getStatus() == true)
                {
                    int indexTruck = worldObjects.FindIndex(a => a.guid == truck[0].guid);
                    moveTruck(indexTruck, 'v');
                }
                    if (u is IUpdatable)
                {
                    bool needsCommand = ((IUpdatable)u).Update(tick);

                    if (needsCommand){
                        SendCommandToObservers(new UpdateModel3DCommand(u));
                    }
                }
        }

            return true;
        }

        public void AddConnections()
        {
            nodes[nodes.FindIndex(a => a.name == 'A')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'P')], nodes[nodes.FindIndex(a => a.name == 'C')] });
            nodes[nodes.FindIndex(a => a.name == 'B')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'S')], nodes[nodes.FindIndex(a => a.name == 'D')] });
            nodes[nodes.FindIndex(a => a.name == 'C')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'A')], nodes[nodes.FindIndex(a => a.name == 'E')], nodes[nodes.FindIndex(a => a.name == 'G')] });
            nodes[nodes.FindIndex(a => a.name == 'D')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'B')], nodes[nodes.FindIndex(a => a.name == 'F')], nodes[nodes.FindIndex(a => a.name == 'I')] });
            nodes[nodes.FindIndex(a => a.name == 'E')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'C')], nodes[nodes.FindIndex(a => a.name == 'F')] });
            nodes[nodes.FindIndex(a => a.name == 'F')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'D')], nodes[nodes.FindIndex(a => a.name == 'E')] });
            nodes[nodes.FindIndex(a => a.name == 'G')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'C')], nodes[nodes.FindIndex(a => a.name == 'I')] });
            nodes[nodes.FindIndex(a => a.name == 'H')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'G')] });
            nodes[nodes.FindIndex(a => a.name == 'I')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'G')] });
            nodes[nodes.FindIndex(a => a.name == 'J')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'G')], nodes[nodes.FindIndex(a => a.name == 'M')] });
            nodes[nodes.FindIndex(a => a.name == 'K')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'J')] });
            nodes[nodes.FindIndex(a => a.name == 'L')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'J')] });
            nodes[nodes.FindIndex(a => a.name == 'M')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'J')], nodes[nodes.FindIndex(a => a.name == 'D')] });
            nodes[nodes.FindIndex(a => a.name == 'N')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'M')] });
            nodes[nodes.FindIndex(a => a.name == 'O')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'M')] });
            nodes[nodes.FindIndex(a => a.name == 'P')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'A')], nodes[nodes.FindIndex(a => a.name == 'Q')] });
            nodes[nodes.FindIndex(a => a.name == 'Q')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'P')], nodes[nodes.FindIndex(a => a.name == 'R')] });
            nodes[nodes.FindIndex(a => a.name == 'R')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'Q')] });
            nodes[nodes.FindIndex(a => a.name == 'S')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'B')] });
            nodes[nodes.FindIndex(a => a.name == 't')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'u')] });
            nodes[nodes.FindIndex(a => a.name == 'u')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 't')], nodes[nodes.FindIndex(a => a.name == 'v')] });
            nodes[nodes.FindIndex(a => a.name == 'v')].connections.AddRange(new List<Node> { nodes[nodes.FindIndex(a => a.name == 'u')] });

            foreach (Node item in nodes)
            {
                Dictionary<char, int> een = new Dictionary<char, int>();
                foreach (Node connection in item.connections)
                {
                    int deltaX = Math.Abs((int)item.x - (int)connection.x);
                    int deltaZ = Math.Abs((int)item.z - (int)connection.z);
                    int sum = deltaX + deltaZ;
                    een.Add(connection.name, sum);
                }
                g.add_vertex(item.name, een);
            }
        }

        public void addNodes()
        {
            nodes.Add(new Node('A', 2, 0, 2));//hoekpunt
            nodes.Add(new Node('B', 30, 0, 2));//hoekpunt
            nodes.Add(new Node('C', 2, 0, 16));//midden links
            nodes.Add(new Node('D', 30, 0, 16));//midden rechts
            nodes.Add(new Node('E', 2, 0, 30));//hoekpunt
            nodes.Add(new Node('F', 30, 0, 30));//hoekpunt
            nodes.Add(new Node('G', 4, 0, 16));//connectie node
            nodes.Add(new Node('H', 4, 0, 14));//shelf node
            nodes.Add(new Node('I', 4, 0, 18));//shelf node
            nodes.Add(new Node('J', 16, 0, 14));//connectie node
            nodes.Add(new Node('K', 16, 0, 16));//shelf node
            nodes.Add(new Node('L', 16, 0, 18));//shelf node
            nodes.Add(new Node('M', 28, 0, 14));//connectie node
            nodes.Add(new Node('N', 28, 0, 16));//shelf node
            nodes.Add(new Node('O', 28, 0, 18));//shelf node
            nodes.Add(new Node('P', 13, 0, 2));//robot node
            nodes.Add(new Node('Q', 14, 0, 2));//robot node
            nodes.Add(new Node('R', 15, 0, 2));//robot node
            nodes.Add(new Node('S', 16, 0, 2));//robot node
            nodes.Add(new Node('t', 0, 0, 0));//truck start
            nodes.Add(new Node('u', 16, 0, 0));//truck midden
            nodes.Add(new Node('v', 32, 0, 0));//truck eind
            AddConnections();
        }
    }

internal class Unsubscriber<Command> : IDisposable
{
    private List<IObserver<Command>> _observers;
    private IObserver<Command> _observer;

    internal Unsubscriber(List<IObserver<Command>> observers, IObserver<Command> observer)
    {
        this._observers = observers;
        this._observer = observer;
    }

    public void Dispose()
    {
        if (_observers.Contains(_observer))
            _observers.Remove(_observer);
    }
}
}