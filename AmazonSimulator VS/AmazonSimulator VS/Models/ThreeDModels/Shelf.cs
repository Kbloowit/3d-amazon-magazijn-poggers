using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Shelf : ThreeDModels
    {
        /// <summary>
        /// Bool shelf in place to pickup
        /// </summary>
        private bool inPlace = true;

        /// <summary>
        /// Constructor of the Shelf
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="z">Z postiion</param>
        /// <param name="rotationX">X Rotation</param>
        /// <param name="rotationY">Y Rotation</param>
        /// <param name="rotationZ">Z Rotatoin</param>
        public Shelf(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("shelf", x, y, z, rotationX, rotationY, rotationZ)
        {
        }

        /// <summary>
        /// Get the status of the shelf
        /// </summary>
        /// <returns>InPlace</returns>
        public override bool Status()
        {
            return inPlace;
        }

        /// <summary>
        /// Update the status of the shelf (inPlace or not)
        /// </summary>
        public void updateStatus()
        {
            if (inPlace == false)
                inPlace = true;
            else
                inPlace = false;
        }
    }
}