using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using TextRPG.Models;
using TextRPG.Utils;

namespace TextRPG.Systems
{
    /* 상점 시스템 클래스 */
    internal class ShopSystem
    {        
        private List<Item>? ShopItems { get; set; }

        public ShopSystem()
        {
            ShopItems = new List<Item>();

            /* 상점 아이템 초기화 */
            InitShop();
        }

        private void InitShop()
        {
            /* 무기 아이템 추가 */
            ShopItems?.Add(Equipment.CreateWeapon("목검"));
            ShopItems?.Add(Equipment.CreateWeapon("철검"));
            ShopItems?.Add(Equipment.CreateWeapon("전설검"));

            /* 방어구 아이템 추가 */
            ShopItems?.Add(Equipment.CreateArmor("천갑옷"));
            ShopItems?.Add(Equipment.CreateArmor("철갑옷"));
            ShopItems?.Add(Equipment.CreateArmor("전설갑옷"));

            /* 소비 아이템 추가 */
            ShopItems?.Add(Consumable.CreatePotion("체력포션"));
            ShopItems?.Add(Consumable.CreatePotion("대형체력포션"));
            ShopItems?.Add(Consumable.CreatePotion("마나포션"));
            ShopItems?.Add(Consumable.CreatePotion("대형마나포션"));
        }


        /* 상점 메뉴 출력 */
        public void ShowShopMenu(Player player, InventorySystem inventory)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("|            상점 메뉴           |");
                Console.WriteLine("===================================");
                Console.WriteLine($"\n보유 골드: {player.Gold}");

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");

                Console.WriteLine("\n선택 > ");
                string? input = Console.ReadLine();

                switch(input)
                {
                    case "1":
                        /* 아이템 구매 */
                        BuyItem(player, inventory);
                        ConsoleUi.PreesAnyKey();
                        break;
                    case "2":
                        /* 아이템 판매 */
                        sellItem(player, inventory);
                        break;
                    case "0":
                        Console.WriteLine("상점을 나갑니다....");
                        ConsoleUi.PreesAnyKey();
                        return;
                    default:
                        Console.WriteLine("잘못된 선택입니다....");
                        ConsoleUi.PreesAnyKey();
                        break;
                }
            }
        }

        /* 아이템 구매 처리 */
        private void BuyItem(Player player, InventorySystem inventory)
        {
            Console.Clear();
            Console.WriteLine("\n[구매 가능한 아이템]");

            for (int i = 0; i < ShopItems?.Count; i++)
            {                
                Console.WriteLine($"{i + 1}. {ShopItems[i].Name} - 가격: {ShopItems[i].Price}골드");
            }

            Console.WriteLine("\n구매할 아이템 번호를 선택하세요. (0:취소) > ");

            if (int.TryParse(Console.ReadLine(), out var index) && index > 0 && index <= ShopItems.Count)
            {
                Item selectedItem = ShopItems[index - 1];

                if (player.Gold >= selectedItem.Price)
                {                    
                    Console.WriteLine($"{selectedItem.Name}을 {selectedItem.Price}에 구매하시겠습니까? (y/n): ");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        player.SpendGold(selectedItem.Price);            
                        Item? item = CreateItem(selectedItem);

                        if(item is Equipment equipment)
                        {
                            inventory.AddItem(equipment);
                            player.EquipItem(equipment);
                        }
                        else if (item is Consumable consumable)
                        {
                            inventory.AddItem(consumable);
                        }

                        Console.WriteLine($"{selectedItem.Name}을 구매했습니다.");                        
                    }

                }
                else
                {
                    Console.WriteLine("골드가 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("구매를 취소합니다.");
            }
        }

        /* 아이템 복제 메서드 */
        private Item? CreateItem(Item item)
        {
            /* 장착 아이템 복제 */
            if(item is Equipment equipment)
            {
                var newItem = new Equipment(
                    equipment.Name, 
                    equipment.Description, 
                    equipment.Price,                     
                    equipment.Slot, 
                    equipment.AttackBonus, 
                    equipment.DefenseBonus
                    );

                return newItem;
            }

            /* 소비 아이템 복제 */
            else if (item is Consumable consumable)
            {
                return new Consumable(
                    consumable.Name, 
                    consumable.Description, 
                    consumable.Price, 
                    consumable.HpAmount, 
                    consumable.MpAmount
                    );
            }

            return null;
        }


        private void sellItem(Player player, InventorySystem inventory)
        {
            if (inventory.Count == 0)
            {
                Console.WriteLine("\n판매할 아이템이 없습니다.");
                return;
            }

            inventory.PrintInventory();

            Console.WriteLine("\n판매할 아이템 번호를 선택하세요. (0:취소) > ");
            if (int.TryParse(Console.ReadLine(), out var index) && index > 0 && index <= inventory.Count)
            {
                Item? item = inventory.GetItem(index - 1);

                if (item != null)
                {
                    int sellPrice = item.Price / 2; // 판매 가격은 구매 가격의 절반

                    Console.WriteLine($"{item.Name}을(를) {sellPrice}골드에 판매하시겠습니까? (y/n): ");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        if (item is Equipment equipment)
                        {
                            player.UnequipItem(equipment.Slot);
                        }
                        
                        player.GainGold(sellPrice);
                        inventory.RemoveItem(item);

                        Console.WriteLine($"{item.Name}을(를) {sellPrice}골드에 판매했습니다.");
                        ConsoleUi.PreesAnyKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("판매를 취소합니다.");
            }
        }

        }
}
