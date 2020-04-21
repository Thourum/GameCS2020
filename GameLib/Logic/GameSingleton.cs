using System;
using System.Collections.Generic;
using System.Text;
using GameLib.Model;
using GameLib.Interface;

namespace GameLib.Logic
{
    public sealed class GameSingleton
    {
        private static readonly Lazy<Game> lazy = new Lazy<Game> (() => new Game());

        public static Game Instance => lazy.Value;
    }
}
