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
        /// Instance of the worldmanager class
        /// </summary>
        WorldManager worldManager = new WorldManager();

        /// <summary>
        /// Constructor
        /// </summary>
        public World()
        {
            Robot robot1 = CreateRobot("P", 13, 0, 2);
            Robot robot2 = CreateRobot("Q", 14, 0, 2);
            Robot robot3 = CreateRobot("R", 15, 0, 2);
            Truck truck1 = CreateTruck(0, 1, -5);
            Train train = CreateTrain(32, 1.4, 32);
            foreach(Node n in worldManager.getGraphNodes())
                if (n.name.Contains("Shelf") && n.shelf == null)
                {
                    Shelf shelf  = CreateShelf(n.x, n.y, n.z);
                    n.shelf = shelf;
                }
            for (int i = 0; i < 6; i++)
                CreateForklift(32, 1000, 32);
        }

        /// <summary>
        /// Creates a new instance of a robot object
        /// </summary>
        /// <param name="x">starting x cordinate in the world</param>
        /// <param name="y">starting y cordinate in the world</param>
        /// <param name="z">starting z cordinate in the world</param>
        /// <returns>Robot object</returns>
        private Robot CreateRobot(string robotStation, double x, double y, double z)
        {
            Node node = worldManager.getGraphNodes()[worldManager.getGraphNodes().FindIndex(a => a.GetName() == robotStation)];
            Robot r = new Robot(node, x, y, z, 0, 0, 0);
            worldObjects.Add(r);
            worldManager.AddRobotToList(r);
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
            worldManager.AddTruckToList(t);
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
            worldManager.AddShelfToList(s);
            return s;
        }

        /// <summary>
        /// Creates a new instance of a train object
        /// </summary>
        /// <param name="x">starting x cordinate in the world</param>
        /// <param name="y">starting y cordinate in the world</param>
        /// <param name="z">starting z cordinate in the world</param>
        /// <returns>Train object</returns>
        private Train CreateTrain(double x, double y, double z)
        {
            Train train = new Train(x, y, z, 0, Math.PI, 0);
            worldObjects.Add(train);
            worldManager.AddTrainToList(train);
            return train;
        }

        /// <summary>
        /// Creates a new instance of a forklift object
        /// </summary>
        /// <param name="x">starting x cordinate in the world</param>
        /// <param name="y">starting y cordinate in the world</param>
        /// <param name="z">starting z cordinate in the world</param>
        /// <returns>Forklift object</returns>
        private void CreateForklift(double x, double y, double z)
        {
            Forklift forklift = new Forklift(x, y, z, 0, Math.PI, 0);
            worldObjects.Add(forklift);
            worldManager.AddForkliftToList(forklift);
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
            worldManager.Update();
            for (int i = 0; i < worldObjects.Count; i++)
            {
                ThreeDModels u = worldObjects[i];
                if (u is IUpdatable)
                {
                    bool needsCommand = ((IUpdatable)u).Update(tick);
                    if (needsCommand)
                        SendCommandToObservers(new UpdateModel3DCommand(u));
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