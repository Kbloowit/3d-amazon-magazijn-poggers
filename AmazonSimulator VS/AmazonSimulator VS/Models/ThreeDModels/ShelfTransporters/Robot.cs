using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : ShelfTransporters
    {
        /// <summary>
        /// Node where the robot starts and resets
        /// </summary>
        private Node robotStation;
        
        /// <summary>
        /// Constructor of the robot
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="z">Z postiion</param>
        /// <param name="rotationX">X Rotation</param>
        /// <param name="rotationY">Y Rotation</param>
        /// <param name="rotationZ">Z Rotatoin</param>
        public Robot(Node robotStation, double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("robot", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.robotStation = robotStation;
        }

        /// <summary>
        /// get the station that belongs to this robot
        /// </summary>
        /// <returns>robotStation</returns>
        public Node getRobotStation()
        {
            return robotStation;
        }
    }
}
