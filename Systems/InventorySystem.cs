using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Models;
using TextRPG.Utils;

namespace TextRPG.Systems
{
    internal class InventorySystem
    {
        private List<Item> Items { get; set; }

        // => 읽기 전용이 된다.
        public int Count => Items.Count;
        //public int Count { get { return Items.Count; } }


        public InventorySystem()
        {
            Items = new List<Item>();
        }

        
        public void AddItem(Item item)
        {
            Items.Add(item);
            Console.WriteLine($"{item.Name} 인벤토리에 추가했습니다.");
        }
        
        public bool RemoveItem(Item item)
        {
            if (Items.Remove(item))
            {
                Console.WriteLine($"{item.Name} 인벤토리에서 삭제했습니다.");
                return true;
            }

            return false;
        }
        
        public void PrintInventory()
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("|            인벤토리            |");
            Console.WriteLine("===================================");

            if (Items.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어있습니다.");
                return;
            }

            for (int i = 0; i < Items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Items[i].Name} - {Items[i].Description}");
            }
        }

        public void ShowInventory(Player? player) 
        {
            while (true)
            {
                PrintInventory();

                Console.WriteLine("\n선택하세요.");
                Console.WriteLine("1. 아이템 사용");
                Console.WriteLine("2. 아이템 버리기");
                Console.WriteLine("0. 나가기");
                Console.Write("선택: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        UseItem(player);
                        ConsoleUi.PreesAnyKey();
                        break;
                    case "2":
                        DropItem(player);
                        ConsoleUi.PreesAnyKey();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("잘못된 선택입니다. 다시 선택하세요.");
                        break;
                }
            }
        }

        /* 아이템 사용 */
        private void UseItem(Player player)
        {
            if (Items.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어있습니다.");
                return;
            }

            Console.WriteLine("\n사용할 아이템 번호 (0: 취소) > ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index == 0)
                {
                    return; // 취소
                }
                if (index < 1 || index > Items.Count)
                {
                    Console.WriteLine("잘못된 선택입니다.");                    
                    return;
                }

                Item item = Items[index - 1];
                if (item.Use(player))
                {
                    /* 소모품일 경우 사용 후 리스트에서 제거함 */
                    if (item is Consumable)
                    {
                        RemoveItem(item);
                        Console.WriteLine($"{item.Name}을(를) 사용했습니다.");                        
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("잘못된 선택 입니다.");
            }
        }

        /* 아이템 버리기 */ 
        private void DropItem(Player player)
        {
            if (Items.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어있습니다.");
                return;
            }

            Console.WriteLine("\n버릴 아에템 번호 (0:취소) > ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index == 0)
                {
                    return; // 취소
                }
                if (index < 1 || index > Items.Count)
                {
                    Console.WriteLine("잘못된 선택입니다.");                    
                    return;
                }

                Item item = Items[index - 1];
                Console.WriteLine($"정말 {item.Name}을 버리시겠습니까? (y/n)");
                if (Console.ReadLine()?.ToLower() != "y")
                {
                    Console.WriteLine("버리기를 취소했습니다.");                    
                    return;
                }
                else
                {
                    if (item is Equipment equipment)
                    {
                        /* 장비 아이템일 경우 장착 해제 */
                        if (equipment == player.EquipedWeapon)
                        {
                            player.UnequipItem(EquipmentSlot.Weapon);
                        }
                        else if (equipment == player.EquipedArmor)
                        {
                            player.UnequipItem(EquipmentSlot.Armor);
                        }
                    }
                    RemoveItem(item);

                    Console.WriteLine($"{item.Name}을 버렸습니다.");                    
                }
            }
            else
            {
                Console.WriteLine("잘못된 선택 입니다.");                
            }
        }

    }
}
