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
    }
}