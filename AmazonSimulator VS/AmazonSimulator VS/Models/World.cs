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
            Robot r = CreateRobot(2, 0, 2);
            Truck t = CreateTruck(0, 0, 0);
            //Truck t2 = CreateTruck(2, 0, 2);
            Shelf s = CreateShelf(0, 0, 0);

            addNodes();
            AddVertexes();
            moveRobot(nodes, 'A', 'I');
            moveTruck(nodes);

        }
        public void AddVertexes()
        {
            g.add_vertex('A', new Dictionary<char, int>() { { 'B', 28 }, { 'C', 14 } });
            g.add_vertex('B', new Dictionary<char, int>() { { 'A', 28 }, { 'D', 14 } });
            g.add_vertex('C', new Dictionary<char, int>() { { 'A', 14 }, { 'E', 14 }, { 'H', 2 } });
            g.add_vertex('H', new Dictionary<char, int>() { { 'C', 2 }, { 'G', 12 } });
            g.add_vertex('D', new Dictionary<char, int>() { { 'B', 14 }, { 'F', 14 }, { 'I', 1 } });
            g.add_vertex('E', new Dictionary<char, int>() { { 'C', 14 }, { 'F', 28 } });
            g.add_vertex('F', new Dictionary<char, int>() { { 'D', 14 }, { 'E', 28 } });
            g.add_vertex('G', new Dictionary<char, int>() { { 'C', 14 }, { 'I', 13 } });
            g.add_vertex('I', new Dictionary<char, int>() { { 'G', 13 }, { 'D', 1 } });
            g.add_vertex('t', new Dictionary<char, int>() { { 'u', 16 }, });
            g.add_vertex('u', new Dictionary<char, int>() { { 't', 16 }, { 'v', 16 } });
            g.add_vertex('v', new Dictionary<char, int>() { { 'u', 16 }, });
        }

        public void addNodes()
        {
            nodes.Add(new Node('A', 2, 0, 2));
            nodes.Add(new Node('B', 30, 0, 2));
            nodes.Add(new Node('C', 2, 0, 16));
            nodes.Add(new Node('D', 30, 0, 16));
            nodes.Add(new Node('E', 2, 0, 30));
            nodes.Add(new Node('F', 30, 0, 30));
            nodes.Add(new Node('G', 16, 0, 16));
            nodes.Add(new Node('H', 4, 0, 16));
            nodes.Add(new Node('I', 29, 0, 10));
            nodes.Add(new Node('t', 0, 0, 0));
            nodes.Add(new Node('u', 16, 0, 0));
            nodes.Add(new Node('v', 32, 0, 0));
        }

        public void moveRobot(List<Node> nodes, Char from, Char to)
        {
            List<Node> nodePath = g.shortest_path(from, to, nodes);

            for (int i = 0; i < nodePath.Count(); i++)
            {
                foreach (Robot r in worldObjects)
                {
                    r.AddDestination(nodePath[i]);
                }
            }
        }

        public void moveTruck(List<Node> nodes)
        {
            //foreach (Truck t in worldObjects)
            //{
            //    foreach(Node i in nodes)
            //    {
            //        foreach(char n in i.GetName())
            //        {

            //        }
            //    }
            //    t.AddDestination(nodes);
            //}
        }

        private Robot CreateRobot(double x, double y, double z) {
            Robot r = new Robot("robot",x,y,z,0,0,0);
            worldObjects.Add(r);
            return r;
        }

        private Truck CreateTruck(double x, double y, double z)
        {
            Truck t = new Truck("truck",x, y, z, 0, 0, 0);
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