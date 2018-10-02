using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Train : TransportVehicle
    {
        public Train(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("train", x, y, z, rotationX, rotationY, rotationZ)
        {

        }
    }
}