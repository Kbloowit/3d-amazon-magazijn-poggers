using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ShelfTransporters : ThreeDModels
    {
        /// <summary>
        /// List of tasks for the ShelfTransporter
        /// </summary>
        List<ITask> tasks = new List<ITask>();
        /// <summary>
        /// Shelf the ShelfTransporter is carrying
        /// </summary>
        private Shelf shelf;
        /// <summary>
        /// Bool if the ShelfTransporter is busy
        /// </summary>
        private bool busy;


        /// <summary>
        /// Constructor of the ShelfTransporters class
        /// </summary>
        /// <param name="type">type of object</param>
        /// <param name="x">X coördinate</param>
        /// <param name="y">Y coördinate</param>
        /// <param name="z">Z coördinate</param>
        /// <param name="rotationX">X Rotation</param>
        /// <param name="rotationY">Y Rotation</param>
        /// <param name="rotationZ">Z Rotation</param>
        public ShelfTransporters(string type, double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(type, x, y, z, rotationX, rotationY, rotationZ)
        {

        }

        /// <summary>
        /// Updates the shelftransporter
        /// </summary>
        /// <param name="tick">Tick time (50 = 20 times per second)</param>
        /// <returns>Update(Tick)</returns>
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
                if (tasks.Count != 0)
                    tasks.First().startTask(this);
            }
            return base.Update(tick);
        }

        /// <summary>
        /// Move the ShelfTransporter over his path
        /// </summary>
        /// <param name="path">Path that the ShelfTransporter should take</param>
        public void MoveOverPath(List<Node> path)
        {
            double deltaX;
            double deltaZ;
            if (path.Count() != 0)
            {
                if (path.First().x == Math.Round(this.x, 1) && path.First().z == Math.Round(this.z, 1))
                    path.RemoveAt(0);

                deltaX = path.First().x - this.x; //waar hij naar toe moet - waar hij is
                deltaZ = path.First().z - this.z; //waar hij naar toe moet - waar hij is

                if (path.First().x > Math.Round(this.x))
                {
                    this.Rotate(this.rotationX, this.rotationY - this.rotationY + (Math.PI / 2), this.rotationZ);
                    if (shelf != null)
                        shelf.Rotate(rotationX, rotationY - rotationY + (Math.PI / 2), rotationZ);
                }
                else if (path.First().x < Math.Round(this.x))
                {
                    this.Rotate(this.rotationX, this.rotationY - this.rotationY - (Math.PI / 2), this.rotationZ);
                    if (shelf != null)
                        shelf.Rotate(rotationX, rotationY - rotationY - (Math.PI / 2), rotationZ);
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
        /// Adds a task to the tasklist of the ShelfTransporter
        /// </summary>
        /// <param name="Task"></param>
        public void addTask(ITask Task)
        {
            tasks.Add(Task);
        }

        /// <summary>
        /// Get the status of the ShelfTransporter
        /// </summary>
        /// <returns>busy</returns>
        public override bool Status()
        {
            return busy;
        }

        /// <summary>
        /// Updates the status of the ShelfTransporter
        /// </summary>
        public void updateStatus()
        {
            if (busy == false)
                busy = true;
            else
                busy = false;
        }

        /// <summary>
        /// Bool if ShelfTransporter has a shelf
        /// </summary>
        /// <returns>ShelfTransporterShelfStatus</returns>
        public bool ShelfTransporterShelfStatus()
        {
            if (shelf == null)
                return false;
            else
                return true;
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
        /// Adds a shelf to the ShelfTransporter
        /// </summary>
        /// <param name="shelf">Shelf to add</param>
        public void addShelf(Shelf shelf)
        {
            this.shelf = shelf;
        }

        /// <summary>
        /// removes the shelf from the ShelfTransporter
        /// </summary>
        public void removeShelf()
        {
            shelf = null;
        }
    }
}



