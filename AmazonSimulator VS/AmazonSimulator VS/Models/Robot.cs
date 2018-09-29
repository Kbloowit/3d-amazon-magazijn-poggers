using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : ThreeDModels
    {

        private List<Node> destinations = new List<Node>(); //later lijst van task, kunnen checken of ze al klaar zijn
        private List<IRobotTask> tasks = new List<IRobotTask>();

        private double deltaX;
        private double deltaZ;
        private bool busy;

        //private double speed;

        public Robot(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("robot", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.Move(this.x, this.y, this.z);
        }

        public override bool Update(int tick)
        {
            if (tasks.Count != 0)
            {
                if (tasks.First().taskCompleted(this) == true)
                    tasks.RemoveAt(0);

                if (tasks.Count == 0)
                {
                    tasks.Clear();
                }
                tasks.First().startTask(this);
            }

            return base.Update(tick);
        }

        public void MoveOverPath(List<Node> path)
        {
            foreach (Node x in path)
                destinations.Add(x);

            if (destinations.Count() != 0)
            {
                if (Math.Round(deltaZ) == 0 && Math.Round(deltaX) == 0)
                {

                    destinations.RemoveAt(0);
                    if (destinations.Count() != 0)
                    {
                        deltaX = this.destinations[0].x - this.x; //waar hij naar toe moet - waar hij is
                        deltaZ = this.destinations[0].z - this.z; //waar hij naar toe moet - waar hij is

                        if (this.destinations[0].x > Math.Round(this.x))
                            this.Rotate(this.rotationX, this.rotationY - this.rotationY - (Math.PI / 2), this.rotationZ);
                        else if (this.destinations[0].x < Math.Round(this.x))
                            this.Rotate(this.rotationX, this.rotationY - this.rotationY + (Math.PI / 2), this.rotationZ);
                        else if (this.destinations[0].z > Math.Round(this.z))
                            this.Rotate(this.rotationX, this.rotationY - this.rotationY, this.rotationZ);
                        else if (this.destinations[0].z < Math.Round(this.z))
                            this.Rotate(this.rotationX, this.rotationY - this.rotationY + Math.PI, this.rotationZ);
                    }
                }

                if (Math.Round(deltaX, 1) > 0) // als deltaX positief is gaat hij vooruit
                {
                    this.Move(this.x + 0.20, this.y, this.z);
                    deltaX -= 0.20;
                }
                else if (Math.Round(deltaX, 1) < 0) // als deltaX negatief is gaat hij actheruit
                {
                    this.Move(this.x - 0.20, this.y, this.z);
                    deltaX += 0.20;
                }

                if (Math.Round(deltaZ, 1) > 0) // als deltaY positief is dan gaat hij vooruit
                {
                    this.Move(this.x, this.y, this.z + 0.20);
                    deltaZ -= 0.20;
                }
                else if (Math.Round(deltaZ, 1) < 0) // als deltaY negatief is dan gaat hij achteruit
                {
                    this.Move(this.x, this.y, this.z - 0.20);
                    deltaZ += 0.20;
                }
            }
        }

        public List<Node> getDestinations()
        {
            return destinations;
        }

        public override void AddDestination(Node d)
        {
            destinations.Add(d);
        }

        public void addTask(RobotMove robotMove)
        {
            tasks.Add(robotMove);
        }

        public override bool getStatus()
        {
            return busy;
        }

        public override void updateStatus()
        {
            if(busy == false)
                busy = true;
            else
                busy = false;
        }

        public List<IRobotTask> getTasks()
        {
            return tasks;
        }
    }
}
