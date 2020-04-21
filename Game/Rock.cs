using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameLib.Model;
using GameLib.Logic;

namespace FallingRocks
{
    public class Rock
    {
        public static List<char> PossibleSymbols = new List<char>()
            {
                '$',
                '@',
                '#',
                '&',
                '+',
            };

        public static List<ConsoleColor> PossibleColors = new List<ConsoleColor>()
            {
                ConsoleColor.Yellow,
                ConsoleColor.White,
                ConsoleColor.Green,
                ConsoleColor.DarkYellow//,
                //ConsoleColor.Blue
            };

        public static Random random = new Random();

        public Point Position { get; set; }

        public char Symbol { get; }

        public ConsoleColor Color { get; }

        public bool Visible { get; private set; }

        public Rock()
        {
            Visible = true;

            Position = new Point(0, random.Next(0, GameSingleton.Instance.World.SizeY));

            Symbol = PossibleSymbols[random.Next(0, PossibleSymbols.Count)];
            Color = PossibleColors[random.Next(0, PossibleColors.Count)];
        }

        public Rock(int ClusterX, char ClusterSymbol, ConsoleColor ClusterColor)
        {
            Visible = true;

            Position = new Point(ClusterX, 0);

            Symbol = ClusterSymbol;
            Color = ClusterColor;
        }


        public void MoveDown()
        {
            if (Position.X + 1 < GameSingleton.Instance.World.SizeX)
            {
                Position.X++;
            }
            else
            {
                Visible = false;
            }
        }

        public void Draw()
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(Position.Y, Position.X);
            Console.Write(Symbol);
        }
    }
}