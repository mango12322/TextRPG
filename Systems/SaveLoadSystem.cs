using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Models;
using TextRPG.Data;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace TextRPG.Systems
{
    internal class SaveLoadSystem
    {
        /* 게임 경로 및 파일명 */
        private const string SaveFilePath = "gamesave.json";

        /* JSON 직렬화 옵션 */
        private static readonly JsonSerializerOptions jsonOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping  // 한글 지원
        };


        /* 게임 저장 메서드 */
        public static bool SaveGame(Player player, InventorySystem inventory)
        {
            try
            {
                /* 게임 객체(클래스) -> DTO(Data Transfer Object) 변환 */
                var saveData = new
                {
                    Player = ConvertToPlayerData(player),
                    Inventory = ConvertToItemDataList(inventory),
                };

                /* DTO 객체 -> JSON 문자열로 변환 */
                string jsonString = JsonSerializer.Serialize(saveData, jsonOptions);

                /* JSON 문자열을 파일로 저장 */
                System.IO.File.WriteAllText(SaveFilePath, jsonString, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"게임 저장 실패: {ex.Message}");
                return false;
            }
        }

        /* Player -> PlayerData로 변환 */
        private static PlayerData ConvertToPlayerData(Player player)
        {
            var playerData = new PlayerData
            {
                Name = player.Name,
                Job = player.Job.ToString(),
                Level = player.Level,
                CurrentHp = player.CurrentHp,
                MaxHp = player.MaxHp,
                CurrentMp = player.CurrentMp,
                MaxMp = player.MaxMp,
                AttackPower = player.AttackPower,
                Defense = player.Defense,
                Gold = player.Gold,
                EquipedWeaponName = player.EquipedWeapon?.Name,
                EquipedArmorName = player.EquipedArmor?.Name
            };
            return playerData;
        }

        /* InventorySystem -> List<ItemData>로 변환 */
        private static List<ItemData> ConvertToItemDataList(InventorySystem inventory)
        {
            var itemDataList = new List<ItemData>();

            for (int i = 0; i < inventory.Count; i++)
            {
                var item = inventory.GetItem(i);
                if (item != null)
                {
                    var itemData = new ItemData
                    {                        
                        Name = item.Name,                       
                    };

                    if (item is Equipment equipment)
                    {
                        itemData.ItemType = "Equipment";
                        itemData.Slot = equipment.Slot.ToString();
                    }
                    else                    
                        itemData.ItemType = "Consumable";                        

                    itemDataList.Add(itemData);
                }
            }
            
            return itemDataList;
        }
    }
}
