using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Shelf : ThreeDModels
    {
        private string name;
        public bool inPlace = true;

        public Shelf(string name, double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("shelf", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.name = name;
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
            this.Move(this.x, this.y, this.z);
            this.Rotate(this.rotationX, this.rotationY, this.rotationZ);
            return base.Update(tick);
        }

        public string getName()
        {
            return name;
        }

        public override bool Status()
        {
            return inPlace;
        }

        public override void updateStatus()
        {
            if (inPlace == false)
                inPlace = true;
            else
                inPlace = false;
        }
    }
}