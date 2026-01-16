using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG.Models
{
    internal class Consumable : Item
    {
        public int HpAmount { get; private set; }
        public int MpAmount { get; private set; }

        public Consumable(string name, string description, int price, int hpAmount = 0, int mpAmount = 0) 
            : base(
                  name, 
                  description, 
                  price, 
                  ItemType.Potion)
        {
            HpAmount = hpAmount;
            MpAmount = mpAmount;
        }

        public override bool Use(Player player)
        {
            // 플레이어의 HP/MP를 회복시키는 로직 구현
            bool isUsed = false;
            
            if (HpAmount > 0)
            {
                int healedHp = player.HealHp(HpAmount);
                if (healedHp > 0)
                {
                    Console.WriteLine($"{player.Name}의 HP가 {healedHp}만큼 회복되었습니다.");
                    isUsed = true;
                }
                else
                {
                    Console.WriteLine($"{player.Name}의 HP가 이미 최대치입니다.");
                }
            }

            if (MpAmount > 0)
            {
                int healedMp = player.HealMp(MpAmount);
                if (healedMp > 0)
                {
                    Console.WriteLine($"{player.Name}의 MP가 {healedMp}만큼 회복되었습니다.");
                    isUsed = true;
                }
                else
                {
                    Console.WriteLine($"{player.Name}의 MP가 이미 최대치입니다.");
                }                
            }

            return isUsed;
        }

        public static Consumable CreatePotion(string potionType)
        {
            switch (potionType)
            {
                case "체력포션":
                    return new Consumable(
                        name: "체력포션",
                        description: "HP를 50만큼 회복시켜주는 포션입니다.",
                        price: 100,
                        hpAmount: 50);                    
                case "대형체력포션":
                    return new Consumable(
                        name: "대형체력포션",
                        description: "HP를 150만큼 회복시켜주는 포션입니다.",
                        price: 300,
                        hpAmount: 150);                    
                case "마나포션":
                    return new Consumable(
                        name: "마나포션",
                        description: "MP를 30만큼 회복시켜주는 포션입니다.",
                        price: 150,
                        mpAmount: 50);                    
                case "대형 마나포션":
                    return new Consumable(
                        name: "대형마나포션",
                        description: "MP를 100만큼 회복시켜주는 포션입니다.",
                        price: 300,
                        mpAmount: 100);                    
                default:
                    return null!;
            }
        }
    }
}
