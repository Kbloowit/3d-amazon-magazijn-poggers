using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Forklift : ShelfTransporters
    {
        /// <summary>
        /// Constructor of the forklift
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="z">Z postiion</param>
        /// <param name="rotationX">X Rotation</param>
        /// <param name="rotationY">Y Rotation</param>
        /// <param name="rotationZ">Z Rotatoin</param>
        public Forklift(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("forklift", x, y, z, rotationX, rotationY, rotationZ)
        {

        }

        
    }
}
