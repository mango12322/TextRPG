using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Models;

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
                        break;
                    case "2":
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
                    }
                }
            }
            else
            {
                Console.WriteLine("잘못된 선택 입니다.");
            }
        }
    }
}
