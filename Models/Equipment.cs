using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG.Models
{
    internal class Equipment : Item
    {


        public Equipment(string name, string description, int price, ItemType type)
            : base(
                  name, 
                  description, 
                  price, 
                  type)
        {

        }

        public override bool Use(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
