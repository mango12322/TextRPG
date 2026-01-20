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
                var saveData = new GameSaveData
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

        /* 저장 파일 여부 확인 */
        public static bool IsSaveFileExists()
        {
            return System.IO.File.Exists(SaveFilePath);
        }

        /* 게임 로드 메서드 */
        public static GameSaveData? LoadGame()
        {
            try
            {
                /* 저장된 JSON 파일 읽기 */
                if (!System.IO.File.Exists(SaveFilePath))
                {
                    Console.WriteLine("저장된 게임 파일이 없습니다.");
                    return null;
                }

                string jsonString = System.IO.File.ReadAllText(SaveFilePath, Encoding.UTF8);
                /* JSON 문자열 -> DTO 객체로 변환 */
                var saveData = JsonSerializer.Deserialize<GameSaveData>(jsonString, jsonOptions);
                Console.WriteLine("\n게임데이터가 로드되었습니다.");
                return saveData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"게임 로드 실패: {ex.Message}");
                return null;
            }
        }

        /* PlayerData -> Player 객체 변환 메서드 */
        public static Player LoadPlayer(PlayerData data)
        {
            var job = Enum.Parse<JobType>(data.Job);
            var player = new Player(data.Name, job);

            player.Level = data.Level;
            player.CurrentHp = data.CurrentHp;
            player.MaxHp = data.MaxHp;
            player.CurrentMp = data.CurrentMp;
            player.MaxMp = data.MaxMp;
            player.AttackPower = data.AttackPower;
            player.Defense = data.Defense;
            player.Gold = data.Gold;

            return player;
        }

        /* ItemData DTO -> Item 객체 변환 메서드 */
        public static InventorySystem LoadInventory(List<ItemData> itemDataList, Player player)
        {
            var inventory = new InventorySystem();

            foreach (var itemData in itemDataList)
            {
                Item? item = null;

                if (itemData.ItemType == "Equipment")
                {
                    var slot = Enum.Parse<EquipmentSlot>(itemData.Slot!);

                    if (slot == EquipmentSlot.Weapon)
                    {
                        // 무기 아이템 생성
                        item = Equipment.CreateWeapon(itemData.Name);
                    }
                    else if (slot == EquipmentSlot.Armor)
                    {
                        // 방어구 아이템 생성
                        item = Equipment.CreateArmor(itemData.Name);
                    }
                }
                else if (itemData.ItemType == "Consumable")
                {
                    // 소모품 아이템 생성
                    item = Consumable.CreatePotion(itemData.Name);
                }

                if (item != null)
                {
                    inventory.AddItem(item);
                }                
            }

            return inventory;
        }

        /* 저장된 장착 아이템을 복원 메서드 (무기/방어구) */
        public static void LoadEquippedItems(Player player, PlayerData data, InventorySystem inventory)
        {
            if (!string.IsNullOrEmpty(data.EquipedWeaponName))
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    var item = inventory.GetItem(i);
                    if (item != null && item.Name == data.EquipedWeaponName && item is Equipment equipment && equipment.Slot == EquipmentSlot.Weapon)
                    {
                        player.EquipItem(equipment);
                        break;
                    }
                }                
            }

            if (!string.IsNullOrEmpty(data.EquipedArmorName))
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    var item = inventory.GetItem(i);
                    if (item != null && item.Name == data.EquipedArmorName && item is Equipment equipment && equipment.Slot == EquipmentSlot.Armor)
                    {
                        player.EquipItem(equipment);
                        break;
                    }
                }                
            }

            
        }
    }
}
