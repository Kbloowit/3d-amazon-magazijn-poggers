using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public abstract class ThreeDModels : IUpdatable
    {
        public abstract void Move(double x, double y, double z);
        public abstract void Rotate(double rotationX, double rotationY, double rotationZ);
        public abstract bool Update(int tick);
    }
}