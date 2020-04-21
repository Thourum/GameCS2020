using System;
using System.Collections.Generic;
using System.Text;
using GameLib.Interface;
using GameLib.State;

namespace GameLib.Model
{
    public abstract class Unit : IUnit
    {        
        public string Name { get; set; }
        public UnitStatus Status { get; set; } 
        public int MaxHealth { get; set; }
        public double CurrentHealth
        {
            get => CurrentHealth;
            set => CurrentHealth = value > MaxHealth ? MaxHealth : value; // Checks if it's trying to set to more then max health
        }
        public double Speed { get; set; }

    }
}
