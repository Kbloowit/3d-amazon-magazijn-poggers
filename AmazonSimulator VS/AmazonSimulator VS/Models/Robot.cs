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

        public override double currentPositionX()
        {
            return _x;
        }

        public override double currentPositionZ()
        {
            return _z;
        }

        public override void Move(double x, double y, double z)
        {
            base.Move(x, y, z);
        }

        public override bool Update(int tick)
        {
            if(destinations.Count() != 0 && deltaZ == 0 && deltaX ==0)
            {
                deltaX = destinations[0].x - this.x;
                deltaZ = destinations[0].z - this.z;
            }

            if (deltaX > 0)
            {
                this.Move(this._x += 0.20, this._y, this._z);
                deltaX -= 0.20;
            }
            else if(deltaX < 0)
            {
                this.Move(this._x -= 0.20, this._y, this._z);
                deltaX += 0.20;
            }
            else if(deltaZ > 0)
            {
                this.Move(this._x, this._y, this._z += 0.20);
                deltaZ -= 0.20;
            }
            else if(deltaZ < 0)
            {
                this.Move(this._x, this._y, this._z -= 0.20);
                deltaZ += 0.20;
            }
            else
            {
                if(destinations.Count() != 0)
                {
                    destinations.RemoveAt(0);
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
