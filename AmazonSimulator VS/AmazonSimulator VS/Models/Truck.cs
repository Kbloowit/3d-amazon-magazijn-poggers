using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Truck : ThreeDModels
    {

        private List<Node> destinations = new List<Node>(); //later lijst van task, kunnen checken of ze al klaar zijn
        //private List<int> items = new List<int>();
        private bool arrived = false;
        double deltaX;


        public Truck(string type, double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("truck", x, y, z, rotationX, rotationY, rotationZ)
        {

        }

        public override void Move(double x, double y, double z)
        {
            base.Move(x, y, z);
        }

        public override bool Update(int tick)
        {
            this.Rotate(this._rX, this._rY, this._rZ);
            if (Math.Round(deltaX) == 0)
            {
                if (destinations.Count() != 0)
                {
                    deltaX = destinations[0].x - this.x; //waar hij naar toe moet - waar hij is
                    destinations.RemoveAt(0);
                }
                else if( destinations.Count() == 0 && Math.Round(this.x) == 16)
                {
                    arrived = true;
                }
                }
                if (Math.Round(deltaX) > 0) // als deltaX positief is gaat hij vooruit
                {
                    this.Move(this._x += 0.20, this._y, this._z);
                    deltaX -= 0.20;
                }
                else if (Math.Round(deltaX) < 0) // als deltaX negatief is gaat hij actheruit
                {
                    this.Move(this._x -= 0.20, this._y, this._z);
                    deltaX += 0.20;
            }
           return base.Update(tick);
        }

        public override void AddDestination(Node d)
        {
            destinations.Add(d);
        }

        public bool Status()
        {
            return arrived;
        }
    }
}