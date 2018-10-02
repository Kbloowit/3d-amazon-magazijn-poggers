using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Truck : TransportVehicle
    {
        public Truck(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("truck", x, y, z, rotationX, rotationY, rotationZ)
        {

        }
    }
}