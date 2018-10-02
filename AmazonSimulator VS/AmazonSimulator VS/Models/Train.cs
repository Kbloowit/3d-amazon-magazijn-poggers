using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Train : ThreeDModels
    {
        private List<Node> destinations = new List<Node>();
        private List<string> cargolist = new List<string>();
        private bool arrived = false;
        private double deltaX;

        /// <summary>
        /// Constructor of the train
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="z">Z postiion</param>
        /// <param name="rotationX">X Rotation</param>
        /// <param name="rotationY">Y Rotation</param>
        /// <param name="rotationZ">Z Rotatoin</param>
        public Train(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("train", x, y, z, rotationX, rotationY, rotationZ)
        {

        }

        /// <summary>
        /// Updates the Train
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
        /// Add a new destination to destinations
        /// </summary>
        /// <param name="d">Node</param>
        public void AddDestination(Node d)
        {
            destinations.Add(d);
        }

        /// <summary>
        /// Get if the train arrived or not
        /// </summary>
        /// <returns>arrived</returns>
        public bool Arrived()
        {
            return arrived;
        }

        /// <summary>
        /// Cargo list of the train
        /// </summary>
        /// <returns>cargolist</returns>
        public List<string> GetCargoList()
        {
            return cargolist;
        }

        /// <summary>
        /// Remove cargo from cargolist
        /// </summary>
        public void cargolistRemove()
        {
            cargolist.RemoveAt(0);
        }

        /// <summary>
        /// Add cargo to cargolist
        /// </summary>
        /// <param name="cargo">cargo</param>
        public void addCargo(string cargo)
        {
            cargolist.Add(cargo);
        }

        /// <summary>
        /// Update arrived
        /// </summary>
        public void updateArrived()
        {
            if (arrived == true)
                arrived = false;
            else if (arrived == false)
                arrived = true;
        }

        public override bool Status()
        {
            throw new NotImplementedException();
        }
    }
}
