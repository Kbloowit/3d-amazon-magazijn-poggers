﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Truck : ThreeDModels
    {
        private List<Node> destinations = new List<Node>(); //later lijst van task, kunnen checken of ze al klaar zijn
        private List<string> packlist = new List<string>();
        private bool arrived = false;
        private bool done = false;
        double deltaX;

        public Truck(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("truck", x, y, z, rotationX, rotationY, rotationZ)
        {

        }

        public override bool Update(int tick)
        {
            this.Rotate(this.rotationX, this.rotationY, this.rotationZ);
            if (Math.Round(deltaX, 1) == 0)
            {
                if (destinations.Count() != 0)
                {
                    deltaX = destinations[0].x - this.x; //waar hij naar toe moet - waar hij is
                    destinations.RemoveAt(0);
                }
                }
                if (Math.Round(deltaX, 1) > 0) // als deltaX positief is gaat hij vooruit
                {
                    this.Move(this.x + 0.20, this.y, this.z);
                    deltaX -= 0.20;
                }
                else if (Math.Round(deltaX, 1) < 0) // als deltaX negatief is gaat hij actheruit
                {
                    this.Move(this.x - 0.20, this.y, this.z);
                    deltaX += 0.20;
                }
           return base.Update(tick);
        }

        public override void AddDestination(Node d)
        {
            destinations.Add(d);
        }

        public override bool Status()
        {
            return done;
        }

        public bool Arrived()
        {
            return arrived;
        }

        public List<string> GetPacklist()
        {
            return packlist;
        }

        public void packlistRemove()
        {
            packlist.RemoveAt(0);
        }

        public void addPackage(string package)
        {
            packlist.Add(package);
        }

        public void updateDone()
        {
            if (done == true)
                done = false;
            else if (done == false)
                done = true;
        }

        public void updateArrived()
        {
            if (done == true)
                done = false;
            else if (done == false)
                done = true;
        }


    }
}