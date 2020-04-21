using System;
using System.Collections.Generic;
using System.Text;
using GameLib.Model;
using GameLib.Interface;

namespace GameLib.Logic
{
    public class Game : IGame
    {
        public IWorld World { get; set; }
        public Player Player { get; set; }
        public double Speed { get; set; }
        public Game()
        {

        }
    }
}
