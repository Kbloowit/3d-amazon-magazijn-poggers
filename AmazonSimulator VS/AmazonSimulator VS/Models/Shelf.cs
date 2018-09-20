using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Shelf : ThreeDModels
    {

        public Shelf(string type, double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("shelf", x, y, z, rotationX, rotationY, rotationZ)
        {

        }

        public override void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            base.Rotate(rotationX, rotationY, rotationZ);
        }

        public override void Move(double x, double y, double z)
        {
            base.Move(x, y, z);
        }

        public override bool Update(int tick)
        {
            this.Move(this._x, this._y, this._z);
            this.Rotate(this._rX, this._rY, this._rZ);
            return base.Update(tick);
        }
    }
}