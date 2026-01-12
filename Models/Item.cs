using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Models;

namespace TextRPG.Models
{
    internal abstract class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Price { get; protected set; }
        public ItemType Type { get; protected set; }

        protected Item(string name, string description, int price, ItemType type)
        {
            Name = name;
            Description = description;
            Price = price;
            Type = type;
        }

        public abstract bool Use(Player player);

        public virtual void PrintInfo()
        {
            Console.WriteLine($"아이템명: {Name}");
            Console.WriteLine($"설명: {Description}");
            Console.WriteLine($"가격: {Price} 골드");
            Console.WriteLine($"타입: {Type}");
        }

    }
}
