using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : ThreeDModels
    {
        private List<IRobotTask> tasks = new List<IRobotTask>();
        private Shelf shelf;
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
                if(tasks.Count != 0)
                tasks.First().startTask(this);
            }
            return base.Update(tick);
        }

        public void MoveOverPath(List<Node> path)
        {
            if (path.Count() != 0)
            {
                if (Math.Round(deltaZ) == 0 && Math.Round(deltaX) == 0)
                {
                    if (path.Count() != 0)
                    {
                        deltaX = path.First().x - this.x; //waar hij naar toe moet - waar hij is
                        deltaZ = path.First().z - this.z; //waar hij naar toe moet - waar hij is

                        if (path.First().x > Math.Round(this.x))
                        {
                            this.Rotate(this.rotationX, this.rotationY - this.rotationY - (Math.PI / 2), this.rotationZ);
                            if (shelf != null)
                                shelf.Rotate(rotationX, rotationY - rotationY - (Math.PI / 2), rotationZ);
                        }
                        else if (path.First().x < Math.Round(this.x))
                        {
                            this.Rotate(this.rotationX, this.rotationY - this.rotationY + (Math.PI / 2), this.rotationZ);
                            if (shelf != null)
                                shelf.Rotate(rotationX, rotationY - rotationY + (Math.PI / 2), rotationZ);
                        }
                        else if (path.First().z > Math.Round(this.z))
                        {
                            this.Rotate(this.rotationX, this.rotationY - this.rotationY, this.rotationZ);
                            if (shelf != null)
                                shelf.Rotate(rotationX, rotationY - rotationY, rotationZ);
                        }
                        else if (path.First().z < Math.Round(this.z)){
                            this.Rotate(this.rotationX, this.rotationY - this.rotationY + Math.PI, this.rotationZ);
                            if (shelf != null)
                                shelf.Rotate(rotationX, rotationY - rotationY + Math.PI, rotationZ);
                        }
                        if(path.Count != 1)
                        path.RemoveAt(0);
                    }
                }

                if (Math.Round(deltaX, 1) > 0) // als deltaX positief is gaat hij vooruit
                {
                    this.Move(this.x + 0.20, this.y, this.z);
                    if (shelf != null)
                        shelf.Move(x + 0.20, y, z);
                    deltaX -= 0.20;
                }
                else if (Math.Round(deltaX, 1) < 0) // als deltaX negatief is gaat hij actheruit
                {
                    this.Move(this.x - 0.20, this.y, this.z);
                    if (shelf != null)
                        shelf.Move(x - 0.20, y, z);
                    deltaX += 0.20;
                }

                if (Math.Round(deltaZ, 1) > 0) // als deltaY positief is dan gaat hij vooruit
                {
                    this.Move(this.x, this.y, this.z + 0.20);
                    if (shelf != null)
                        shelf.Move(x, y, z + 0.20);
                    deltaZ -= 0.20;
                }
                else if (Math.Round(deltaZ, 1) < 0) // als deltaY negatief is dan gaat hij achteruit
                {
                    this.Move(this.x, this.y, this.z - 0.20);
                    if (shelf != null)
                        shelf.Move(x, y, z - 0.20);
                    deltaZ += 0.20;
                }
            }
        }

        public void addTask(IRobotTask robotMove)
        {
            tasks.Add(robotMove);
        }

        public override bool Status()
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

        public int getTasksCount()
        {
            return tasks.Count;
        }

        public void addShelf(Shelf s)
        {
            shelf = s;
        }

        public void removeShelf()
        {
            shelf = null;
        }
    }
}
