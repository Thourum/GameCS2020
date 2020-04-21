using System;
using System.Collections.Generic;
using System.Text;
using GameLib.Interface;

namespace GameLib.Model
{
    public class World: IWorld
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public Dictionary<Point, IObject> Map { get; set; }

        public World(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Map = new Dictionary<Point, IObject>();
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    Map.Add(new Point(x, y), null);
                }
            }
        }
    }
}
