using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : ThreeDModels
    {

        private List<Node> destinations; //later lijst van task, kunnen checken of ze al klaar zijn

        //private double speed;

        public Robot(string type, double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("robot", x, y, z, rotationX, rotationY, rotationZ)
        {

        }

        public override bool Update(int tick)
        {
            double deltaX;
            double deltaZ;

            if(destinations.Count() != 0)
            {
                for (int i = 0; i < destinations.Count(); i++)
                {
                    if(destinations[i].x != destinations[i+1].x)
                        deltaX = Math.Abs(destinations[i].x - destinations[i + 1].x);
                    if (destinations[i].z != destinations[i + 1].z)
                        deltaZ = Math.Abs(destinations[i].z - destinations[i + 1].z);

                    
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
