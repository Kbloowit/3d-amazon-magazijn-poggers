using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<ThreeDModels> worldObjects = new List<ThreeDModels>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        public List<Node> nodes = new List<Node>();
        Graph g = new Graph();


        public World() {
            Robot robot1 = CreateRobot(2, 0, 2);
            int indexRobot1 = worldObjects.FindIndex(a => a.guid == robot1.guid);
            //Robot robot2 = CreateRobot(2, 0, 2);
            //int indexRobot2 = worldObjects.FindIndex(a => a.guid == robot2.guid);
            //Robot robot3 = CreateRobot(2, 0, 2);
            //int indexRobot3 = worldObjects.FindIndex(a => a.guid == robot3.guid);
            Truck t = CreateTruck(0, 1, -5);
            int indexTruck = worldObjects.FindIndex(a => a.guid == t.guid);
            Shelf s = CreateShelf(4, 0, 18);
            int indexShelf = worldObjects.FindIndex(a => a.guid == s.guid);

            addNodes();
            AddVertexes();
            moveRobot(nodes, 'A', 'I', 0);
            moveTruck(nodes, indexTruck, 'u');

        }
        public void AddVertexes()
        {
            g.add_vertex('A', new Dictionary<char, int>() { { 'B', 28 }, { 'C', 14 } });
            g.add_vertex('B', new Dictionary<char, int>() { { 'A', 28 }, { 'D', 14 } });
            g.add_vertex('C', new Dictionary<char, int>() { { 'A', 14 }, { 'E', 14 }, { 'G', 2 } });
            g.add_vertex('D', new Dictionary<char, int>() { { 'B', 14 }, { 'F', 14 }, { 'I', 1 } });
            g.add_vertex('E', new Dictionary<char, int>() { { 'C', 14 }, { 'F', 28 } });
            g.add_vertex('F', new Dictionary<char, int>() { { 'D', 14 }, { 'E', 28 } });
            g.add_vertex('G', new Dictionary<char, int>() { { 'C', 14 }, { 'I', 13 } });
            g.add_vertex('H', new Dictionary<char, int>() { { 'G', 2 } });
            g.add_vertex('I', new Dictionary<char, int>() { { 'G', 2 } });
            g.add_vertex('J', new Dictionary<char, int>() { { 'G', 12 }, { 'M', 12 } });
            g.add_vertex('K', new Dictionary<char, int>() { { 'J', 2 } });
            g.add_vertex('L', new Dictionary<char, int>() { { 'J', 2 } });
            g.add_vertex('M', new Dictionary<char, int>() { { 'J', 12 }, { 'D', 2 } });
            g.add_vertex('L', new Dictionary<char, int>() { { 'M', 2 } });
            g.add_vertex('L', new Dictionary<char, int>() { { 'M', 2 } });
            g.add_vertex('t', new Dictionary<char, int>() { { 'u', 16 }, });
            g.add_vertex('u', new Dictionary<char, int>() { { 't', 16 }, { 'v', 16 } });
            g.add_vertex('v', new Dictionary<char, int>() { { 'u', 16 }, });
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
            nodes.Add(new Node('t', 0, 0, 0));//truck start
            nodes.Add(new Node('u', 16, 0, 0));//truck midden
            nodes.Add(new Node('v', 32, 0, 0));//truck eind
        }

        public void moveRobot(List<Node> nodes, Char from, Char to, int robotIndex)
        {
            List<Node> nodePath = g.shortest_path(from, to, nodes);

            for (int i = 0; i < nodePath.Count(); i++)
            {
                    worldObjects[robotIndex].AddDestination(nodePath[i]);
            }
        }

        public void moveTruck(List<Node> nodes, int truckIndex, char to)
        {
            var node = from s in nodes
                       where s.name == to
                       select s;
            foreach(Node i in node)
            worldObjects[truckIndex].AddDestination(i);
        }

        private Robot CreateRobot(double x, double y, double z) {
            Robot r = new Robot("robot",x,y,z,0,0,0);
            worldObjects.Add(r);
            return r;
        }

        private Truck CreateTruck(double x, double y, double z)
        {
            Truck t = new Truck("truck",x, y, z, 0, Math.PI, 0);
            worldObjects.Add(t);
            return t;
        }

        private Shelf CreateShelf(double x, double y, double z)
        {
            Shelf s = new Shelf("shelf",x, y, z, 0, 0, 0);
            worldObjects.Add(s);
            return s;
        }


        public IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer)) {
                observers.Add(observer);

                SendCreationCommandsToObserver(observer);
            }
            return new Unsubscriber<Command>(observers, observer);
        }

        private void SendCommandToObservers(Command c) {
            for(int i = 0; i < this.observers.Count; i++) {
                this.observers[i].OnNext(c);
            }
        }

        private void SendCreationCommandsToObserver(IObserver<Command> obs) {
            foreach(ThreeDModels m3d in worldObjects) {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }
        }

        public bool Update(int tick)
        {
            for(int i = 0; i < worldObjects.Count; i++) {
                ThreeDModels u = worldObjects[i];

                if(u is IUpdatable) {
                    bool needsCommand = ((IUpdatable)u).Update(tick);

                    if(needsCommand) {
                        SendCommandToObservers(new UpdateModel3DCommand(u));
                    }
                }
            }

            return true;
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