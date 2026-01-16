using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG.Models
{
    internal class Equipment : Item
    {
        public EquipmentSlot Slot { get; private set; }
        public int AttackBonus { get; private set; }
        public int DefenseBonus { get; private set; }

        public Equipment(string name, string description, int price, EquipmentSlot slot, int attackBonus = 0, int defenseBonus = 0)
            : base(
                  name, 
                  description, 
                  price, 
                  slot == EquipmentSlot.Weapon ? ItemType.Weapon : ItemType.Armor)
        {
            Slot = slot;
            AttackBonus = attackBonus;
            DefenseBonus = defenseBonus;
        }

        public override bool Use(Player player)
        {
            /* 장비 착용 로직 구현*/
            player.EquipItem(this);
            return true;
        }


        public static Equipment CreateWeapon(string weaponType)
        {
            switch (weaponType)
            {
                case "목검":
                    return new Equipment("목검", "기본 목재로 만든 검입니다.", 100, EquipmentSlot.Weapon, attackBonus: 5);
                case "철검":
                    return new Equipment("철검", "튼튼한 철로 만든 검입니다.", 500, EquipmentSlot.Weapon, attackBonus: 15);
                case "전설검":
                    return new Equipment("전설검", "전설 무기 입니다.", 1500, EquipmentSlot.Weapon, attackBonus: 25);
                default:
                    return null;
            }
        }

        public static Equipment CreateArmor(string armorType)
        {
            switch (armorType)
            {
                case "천갑옷":
                    return new Equipment("천갑옷", "기본 방어구", 100, EquipmentSlot.Armor, 0, 5);
                case "철갑옷":
                    return new Equipment("철갑옷", "철로 만든 방어구", 500, EquipmentSlot.Armor, 0, 20);
                case "전설갑옷":
                    return new Equipment("전설갑옷", "최고급 전설 방어구", 1000, EquipmentSlot.Armor, 0, 30);
                default:
                    return null;
            }
        }
    }
}
