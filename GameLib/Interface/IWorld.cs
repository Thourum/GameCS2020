using GameLib.Model;
using System.Collections.Generic;

namespace GameLib.Interface
{
    public interface IWorld
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public Dictionary<Point, IObject> Map { get; set; }
    }
}
