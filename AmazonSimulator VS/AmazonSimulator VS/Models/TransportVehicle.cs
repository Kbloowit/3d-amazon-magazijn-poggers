using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class TransportVehicle : ThreeDModels
    {
        private List<Node> destinations = new List<Node>();
        private List<string> itemList = new List<string>();
        private bool arrived = false;
        private double deltaX;

        /// <summary>
        /// Constructor of the TransportVehicle
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="z">Z postiion</param>
        /// <param name="rotationX">X Rotation</param>
        /// <param name="rotationY">Y Rotation</param>
        /// <param name="rotationZ">Z Rotatoin</param>
        public TransportVehicle(string type, double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base(type, x, y, z, rotationX, rotationY, rotationZ)
        {

        }

        /// <summary>
        /// Updates the TransportVehicle
        /// </summary>
        /// <param name="tick">Tick time (50 = 20 times per second)</param>
        /// <returns>Update(Tick)</returns>
        public override bool Update(int tick)
        {
            this.Rotate(this.rotationX, this.rotationY, this.rotationZ);
            if (Math.Round(deltaX, 1) == 0)
            {
                if (destinations.Count() != 0)
                {
                    deltaX = destinations[0].x - this.x; //waar hij naar toe moet - waar hij is
                    destinations.RemoveAt(0);
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
            return base.Update(tick);
        }

        /// <summary>
        /// Add a new destination to destination
        /// </summary>
        /// <param name="d">Node</param>
        public void AddDestination(Node d)
        {
            destinations.Add(d);
        }

        /// <summary>
        /// Get the status of the TransportVehicle
        /// </summary>
        /// <returns>arrived</returns>
        public override bool Status()
        {
            return arrived;
        }

        /// <summary>
        /// Item list of the TransportVehicle
        /// </summary>
        /// <returns>itemlist</returns>
        public List<string> GetItemlist()
        {
            return itemList;
        }

        /// <summary>
        /// Remove pack from itemlist
        /// </summary>
        public void itemListRemove()
        {
            itemList.RemoveAt(0);
        }

        /// <summary>
        /// Add package to itemlist
        /// </summary>
        /// <param name="item">item</param>
        public void addItem(string item)
        {
            itemList.Add(item);
        }

        /// <summary>
        /// Update arrived
        /// </summary>
        public virtual void updateArrived()
        {
            if (arrived == true)
                arrived = false;
            else if (arrived == false)
                arrived = true;
        }
    }
}