using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Models;
using TextRPG.Utils;

namespace TextRPG.Systems
{
    /* 상점 시스템 클래스 */
    internal class ShopSystem
    {
        /* 메뉴 선택 (구매,판매,취소) */
        private List<Item>? shopItems { get; set; }

        public ShopSystem()
        {
            shopItems = new List<Item>();

            /* 상점 아이템 초기화 */
            InitShop();
        }

        private void InitShop()
        {
            /* 무기 아이템 추가 */
            shopItems?.Add(Equipment.CreateWeapon("목검"));
            shopItems?.Add(Equipment.CreateWeapon("철검"));
            shopItems?.Add(Equipment.CreateWeapon("전설검"));

            /* 방어구 아이템 추가 */
            shopItems?.Add(Equipment.CreateArmor("천갑옷"));
            shopItems?.Add(Equipment.CreateArmor("철갑옷"));
            shopItems?.Add(Equipment.CreateArmor("전설갑옷"));

            /* 소비 아이템 추가 */
            shopItems?.Add(Consumable.CreatePotion("체력포션"));
            shopItems?.Add(Consumable.CreatePotion("대형체력포션"));
            shopItems?.Add(Consumable.CreatePotion("마나포션"));
            shopItems?.Add(Consumable.CreatePotion("대형마나포션"));
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
                        break;
                    case "2":
                        /* 아이템 판매 */
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
    }
}
