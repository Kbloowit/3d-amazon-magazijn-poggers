using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    
    public class Vertex
    {
        private double _x;
        private double _y;
        private double _z;
        private string _name;

        public Vertex(string name, double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }



    }

    
}
