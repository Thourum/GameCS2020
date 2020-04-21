using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLib.Model;

namespace GameLib.Logic
{

    public sealed class Inventory
    {
        private static readonly Lazy<Inventory> lazy = new Lazy<Inventory>(() => new Inventory());

        public static Inventory Instance => lazy.Value;

        public int Size { get; set; }
        private List<Item> _inventory = new List<Item>();

        public void Add(Item item)
        {
            if (_inventory.Count() >= Size) return;
            _inventory.Add(item);
        }

        public void Drop(Item item)
        {
            if (!_inventory.Contains(item)) return;
            _inventory.Remove(item);
        }

        public Item[] List(Item item) => _inventory.ToArray();
    }
}
