using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : ThreeDModels
    {
        /// <summary>
        /// Node where the robot starts and resets
        /// </summary>
        private Node robotStation;
        /// <summary>
        /// List of tasks for the robot
        /// </summary>
        private List<IRobotTask> tasks = new List<IRobotTask>();
        /// <summary>
        /// Shelf the robot is carrying
        /// </summary>
        private Shelf shelf;
        private double deltaX;
        private double deltaZ;
        /// <summary>
        /// Bool if robot is busy (if he has tasks)
        /// </summary>
        private bool busy;

        /// <summary>
        /// Constructor of the robot
        /// </summary>
        /// <param name="robotStation">Node the robot charges/starts and resets</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="z">Z postiion</param>
        /// <param name="rotationX">X Rotation</param>
        /// <param name="rotationY">Y Rotation</param>
        /// <param name="rotationZ">Z Rotatoin</param>
        public Robot(Node robotStation, double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("robot", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.robotStation = robotStation;
        }

        /// <summary>
        /// Updates the robot
        /// </summary>
        /// <param name="tick">Tick time (50 = 20 times per second)</param>
        /// <returns>Update(Tick)</returns>
        public override bool Update(int tick)
        {
            if (tasks.Count != 0)
            {
                if (tasks.First().taskCompleted(this) == true)
                    tasks.RemoveAt(0);

                if (tasks.Count != 0)
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
                        else if (path.First().z < Math.Round(this.z))
                        {
                            this.Rotate(this.rotationX, this.rotationY - this.rotationY + Math.PI, this.rotationZ);
                            if (shelf != null)
                                shelf.Rotate(rotationX, rotationY - rotationY + Math.PI, rotationZ);
                        }
                        if (path.Count != 1)
                            path.RemoveAt(0);
                    }
                }
                if (Math.Round(deltaX, 1) > 0) // als deltaX positief is gaat hij vooruit
                {
                    this.Move(this.x + 0.20, this.y, this.z);
                    if (shelf != null)
                        shelf.Move(this.x, shelf.y, this.z);
                    deltaX -= 0.20; //deltaX -= speed * tick/1000
                }
                else if (Math.Round(deltaX, 1) < 0) // als deltaX negatief is gaat hij actheruit
                {
                    this.Move(this.x - 0.20, this.y, this.z);
                    if (shelf != null)
                        shelf.Move(this.x, shelf.y, this.z);
                    deltaX += 0.20;
                }

                if (Math.Round(deltaZ, 1) > 0) // als deltaY positief is dan gaat hij vooruit
                {
                    this.Move(this.x, this.y, this.z + 0.20);
                    if (shelf != null)
                        shelf.Move(this.x, shelf.y, this.z);
                    deltaZ -= 0.20;
                }
                else if (Math.Round(deltaZ, 1) < 0) // als deltaY negatief is dan gaat hij achteruit
                {
                    this.Move(this.x, this.y, this.z - 0.20);
                    if (shelf != null)
                        shelf.Move(this.x, shelf.y, this.z);
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
        /// <param name="robottask"></param>
        public void addTask(IRobotTask robottask)
        {
            tasks.Add(robottask);
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
        public void updateStatus()
        {
            if (busy == false)
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
        /// <param name="shelf">Shelf to add</param>
        public void addShelf(Shelf shelf)
        {
            this.shelf = shelf;
        }

        /// <summary>
        /// removes the shelf from the robot
        /// </summary>
        public void removeShelf()
        {
            shelf = null;
        }

        /// <summary>
        /// Bool if robot has a shelf
        /// </summary>
        /// <returns>robotShelfStatus</returns>
        public bool robotShelfStatus()
        {
            if (shelf == null)
                return false;
            else
                return true;
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
