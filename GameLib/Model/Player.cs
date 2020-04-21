using System;
using System.Collections.Generic;
using System.Text;
using GameLib.Interface;
using GameLib.State;

namespace GameLib.Model
{
    public class Player : IPlayer
    {
        public Point Position { get; set; }
        public string Name { get; set; }
        public UnitStatus Status { get; set; }
        public PlayerType PlayerType { get; set; }
        public int MaxHealth { get; set; }
        public double CurrentHealth
        {
            get => CurrentHealth;
            set => CurrentHealth = value > MaxHealth ? MaxHealth : value; // Checks if it's trying to set to more then max health
        }
        public double Speed { get; set; }
        public int Score { get; set; }

        public Player()
        {
            Status = UnitStatus.Alive;
        }
    }
}
