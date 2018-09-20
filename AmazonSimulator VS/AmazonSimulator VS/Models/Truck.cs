using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Truck : ThreeDModels
    {

        private List<Node> destinations = new List<Node>(); //later lijst van task, kunnen checken of ze al klaar zijn
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
            if (destinations.Count() != 0)
            {
                if (Math.Round(deltaX) == 0)
                {
                    destinations.RemoveAt(0);
                    if (destinations.Count() != 0)
                    {
                        deltaX = destinations[0].x - this.x;
                    }
                }
                if (Math.Round(deltaX) > 0)
                {
                    this.Move(this._x += 0.20, this._y, this._z);
                    deltaX -= 0.20;
                }
            }



                return base.Update(tick);
        }

        public override void AddDestination(Node d)
        {
            destinations.Add(d);
        }

    }
}