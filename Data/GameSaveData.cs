using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Models;

namespace TextRPG.Data
{
    internal class GameSaveData
    {
        public PlayerData Player { get; set; }
        public List<ItemData> InventoryItems { get; set; } = new List<ItemData>();
    }

    public class PlayerData
    {
        public string Name { get; set; }
        public string Job { get; set; }
        
        public int Level { get; set; }
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public int CurrentMp { get; set; }
        public int MaxMp { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }
        public int Gold { get; set; }

        public string? EquipedWeaponName { get; set; }
        public string? EquipedArmorName { get; set; }
    }

    public class ItemData
    {
        public string ItemType { get; set; }
        public string Name { get; set; }
        public string? Slot { get; set; }
    }
}
