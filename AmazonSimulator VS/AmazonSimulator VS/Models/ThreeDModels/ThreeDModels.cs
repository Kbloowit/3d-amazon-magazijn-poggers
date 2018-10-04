using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public abstract class ThreeDModels : IUpdatable
    {
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;
        private double _rX = 0;
        private double _rY = 0;
        private double _rZ = 0;

        public string type { get; }
        public Guid guid { get; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public double rotationX { get { return _rX; } }
        public double rotationY { get { return _rY; } }
        public double rotationZ { get { return _rZ; } }

        public bool needsUpdate = true;

        /// <summary>
        /// Constructor of ThreeDModels
        /// </summary>
        /// <param name="type">Type of the ThreeDObject</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="z">Z postiion</param>
        /// <param name="rotationX">X Rotation</param>
        /// <param name="rotationY">Y Rotation</param>
        /// <param name="rotationZ">Z Rotatoin</param>
        public ThreeDModels(string type, double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            this.type = type;
            this.guid = Guid.NewGuid();

            this._x = x;
            this._y = y;
            this._z = z;

            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;
        }

        /// <summary>
        /// Move the ThreeDModel
        /// </summary>
        /// <param name="x">Move to this X</param>
        /// <param name="y">Move to this Y</param>
        /// <param name="z">Move to this Z</param>
        public virtual void Move(double x, double y, double z)
        {
            this._x = x;
            this._y = y;
            this._z = z;

            needsUpdate = true;
        }

        /// <summary>
        /// Rotate the ThreeDModel
        /// </summary>
        /// <param name="rotationX">Rotation on X</param>
        /// <param name="rotationY">Rotation on Y</param>
        /// <param name="rotationZ">Rotation on Z</param>
        public virtual void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;

            needsUpdate = true;
        }

        /// <summary>
        /// Updates the ThreeDModel
        /// </summary>
        /// <param name="tick">Tick time (50 = 20 times per second)</param>
        /// <returns>Update(Tick)</returns>
        public virtual bool Update(int tick)
        {
            if (needsUpdate)
            {
                needsUpdate = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Status of the ThreeDModel
        /// </summary>
        /// <returns>Status</returns>
        public abstract bool Status();
    }
}