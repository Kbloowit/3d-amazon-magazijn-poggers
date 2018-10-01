using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : ThreeDModels
    {
        private Node robotStation;
        private List<IRobotTask> tasks = new List<IRobotTask>();
        private Shelf shelf;
        private double deltaX;
        private double deltaZ;
        private bool busy;

        //private double speed;

        public Robot(Node robotStation, double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("robot", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.robotStation = robotStation;
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

        /// <summary>
        /// Move the robot over his path
        /// </summary>
        /// <param name="path">Path that the robot should take</param>
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

        /// <summary>
        /// Reset the robot to its robot station
        /// </summary>
        public void robotReset()
        {
            Move(this.x - this.x + robotStation.x, this.y - this.y + robotStation.y, this.z - this.z + robotStation.z);
        }

        /// <summary>
        /// Adds a task to the tasklist of the robot
        /// </summary>
        /// <param name="robotMove"></param>
        public void addTask(IRobotTask robotMove)
        {
            tasks.Add(robotMove);
        }

        /// <summary>
        /// Get the status of the robot
        /// </summary>
        /// <returns>busy</returns>
        public override bool Status()
        {
            return busy;
        }

        /// <summary>
        /// Updates the status of the robot
        /// </summary>
        public override void updateStatus()
        {
            if(busy == false)
                busy = true;
            else
                busy = false;
        }

        /// <summary>
        /// Task count
        /// </summary>
        /// <returns>Ammount of tasks</returns>
        public int getTasksCount()
        {
            return tasks.Count;
        }

        /// <summary>
        /// Adds a shelf to the robot
        /// </summary>
        /// <param name="s">Shelf to add</param>
        public void addShelf(Shelf s)
        {
            shelf = s;
        }

        /// <summary>
        /// removes the shelf from the robot();
        /// </summary>
        public void removeShelf()
        {
            shelf = null;
        }

        /// <summary>
        /// get the station that belongs to this robot
        /// </summary>
        /// <returns>robotStation</returns>
        public Node getRobotStation()
        {
            return robotStation;
        }
    }
}
