using System;
using System.Collections.Generic;
using System.Text;
using GameLib.Interface;
using GameLib.Logic;
using GameLib.State;

namespace GameLib.Model
{
    public class Item : IObject
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public IEffect Effect { get; set; }
        public bool Equipped { get; set; }
        public Point Position { get; set; }
        public Item()
        {

        }

        public bool IsEuiptable() => Type == ItemType.Shield || Type == ItemType.Sword;
        public void Drop() 
        {
            Inventory.Instance.Drop(this);
            Position = GameSingleton.Instance.Player.Position;
        }
    }
}
