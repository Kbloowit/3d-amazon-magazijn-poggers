using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : ThreeDModels
    {

        private List<Node> destinations = new List<Node>(); //later lijst van task, kunnen checken of ze al klaar zijn

        double deltaX;
        double deltaZ;

        //private double speed;

        public Robot(string type, double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("robot", x, y, z, rotationX, rotationY, rotationZ)
        {

        }

        public override void Move(double x, double y, double z)
        {
            base.Move(x, y, z);
        }

        public override bool Update(int tick)
        {
            if(destinations.Count() != 0)
            {
                if (Math.Round(deltaZ) == 0 && Math.Round(deltaX) == 0)
                {
                    destinations.RemoveAt(0);
                    if(destinations.Count() != 0)
                    {
                    deltaX = destinations[0].x - this.x; //waar hij naar toe moet - waar hij is
                    deltaZ = destinations[0].z - this.z; //waar hij naar toe moet - waar hij is
                        if (destinations[0].x > this.x)
                            this.Rotate(this._rX, this._rY = -(Math.PI / 2), this._rZ);
                        else if (destinations[0].x < this.x)
                            this.Rotate(this._rX, this._rY = (Math.PI / 2), this._rZ);
                        else if (destinations[0].z > 0)
                            this.Rotate(this._rX, this._rY = 0, this._rZ);
                        else if (destinations[0].z < 0)
                            this.Rotate(this._rX, this._rY = Math.PI, this._rZ);
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

                if (Math.Round(deltaZ) > 0) // als deltaY positief is dan gaat hij vooruit
                {
                    this.Move(this._x, this._y, this._z += 0.20);
                    deltaZ -= 0.20;
                }
                else if (Math.Round(deltaZ) < 0) // als deltaY negatief is dan gaat hij achteruit
                {
                    this.Move(this._x, this._y, this._z -= 0.20);
                    deltaZ += 0.20;
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
