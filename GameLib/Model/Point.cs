using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib.Model
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y) => (X, Y) = (x, y);

        public static bool operator ==(Point first, Point second)
        {
            return first.X == second.X && first.Y == second.Y;
        }
        public static bool operator !=(Point first, Point second)
        {
            return first.X != second.X || first.Y != second.Y;
        }
        public static Point operator -(Point x, Point y)
        {
            return new Point(x.X - y.X, x.Y - y.Y);
        }
    }
}
