using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Utils;
using TextRPG.Models;
using TextRPG.Systems;

namespace TextRPG.Data
{
    internal class GameManager
    {                       
        private static GameManager _instance;
        
        public static GameManager Instance 
        {
            get
            {                
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        public Player? player { get; private set; }
        public BattleSystem BattleSystem { get; private set; }
        public bool IsRunning { get; private set; } = true;
        public InventorySystem Inventory { get; private set; }
        public ShopSystem Shop { get; private set; }

        private GameManager()
        {            
            BattleSystem = new BattleSystem();
            Shop = new ShopSystem();
        }        

        
        public void StartGame()
        {            
            ConsoleUi.ShowTitle();

            Console.WriteLine("RPG 게임에 오신것을 환영합니다!\n");
            
            CreateCharacter();            
            
            Inventory = new InventorySystem();            
            SetupInitialItems();

            IsRunning = true;
            while (IsRunning)
                ShowMainMenu();

            if (!IsRunning)
                ConsoleUi.ShowGameOver();


        }        
        
        private void CreateCharacter()
        {            
            Console.Write("캐릭터명을 입력하세요: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                name = "무명용사";
            }

            Console.WriteLine($"{name}님, 환영합니다!");
            
            Console.WriteLine("직업을 선택해주세요: ");
            Console.WriteLine("1: 전사");
            Console.WriteLine("2: 궁수");
            Console.WriteLine("3: 마법사");

            JobType job = JobType.Warrial;            

            while(true)
            {
                Console.WriteLine("선택 (1-3): ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        job = JobType.Warrial;
                        break;
                    case "2":
                        job = JobType.Archer;
                        break;
                    case "3":
                        job = JobType.Wizard;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요!");
                        continue;
                }

                break;
            }

            /* 캐릭터 생성 */
            player = new Player(name, job);
            Console.WriteLine($"{name}님, {job}으로 캐릭터가 생성되었습니다.");

            player.PrintInfo();

            ConsoleUi.PreesAnyKey();
        }

        private void SetupInitialItems()
        {
            var weapon = Equipment.CreateWeapon("목검");
            var armor = Equipment.CreateArmor("천갑옷");

            Inventory.AddItem(weapon);
            Inventory.AddItem(armor);

            Inventory.AddItem(Consumable.CreatePotion("체력포션"));
            Inventory.AddItem(Consumable.CreatePotion("체력포션"));
            Inventory.AddItem(Consumable.CreatePotion("마나포션"));

            player.EquipItem(weapon);
            player.EquipItem(armor);

            Console.WriteLine("\n초기 장비를 지급하였습니다.");
            ConsoleUi.PreesAnyKey();
        }

        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("====================");
            Console.WriteLine("|     메인 메뉴    |");
            Console.WriteLine("====================");

            Console.WriteLine("\n1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장 (전투)");
            Console.WriteLine("5. 휴식 (HP/MP 회복)");
            Console.WriteLine("6. 게임저장");
            Console.WriteLine("0. 종료");

            Console.Write("1~6 선택: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    /* 상태 보기 */
                    player?.PrintInfo();
                    ConsoleUi.PreesAnyKey();
                    break;
                case "2":
                    /* 인벤토리 */
                    Inventory.ShowInventory(player);
                    break;
                case "3":
                    /* 상점 */
                    Shop.ShowShopMenu(player, Inventory);
                    break;
                case "4":
                    /* 던전 입장 */
                    EnterDungeon();
                    break;
                case "5":
                    /* 휴식 */
                    break;
                case "6":
                    /* 게임저장 */
                    break;
                case "0":
                    IsRunning = false;
                    Console.WriteLine("게임이 종료됩니다!");
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다. 다시 선택해 주세요!");
                    ConsoleUi.PreesAnyKey();
                    break;                    
            }

        }

        /* 던전 입장 */
        public void EnterDungeon()
        {
            Console.Clear();
            Console.WriteLine("\n던전에 입장합니다...");

            // 적 캐릭터 생성
            Enemy enemy = Enemy.CreateEnemy(player.Level);
            ConsoleUi.PreesAnyKey();

            enemy.PrintInfo();

            // 전투 시스템
            BattleSystem battleSystem = new BattleSystem();
            battleSystem.StartBattle(player, enemy);

            Console.WriteLine("\n던전 탐험을 마치고 마을로 돌아갑니다.");
            ConsoleUi.PreesAnyKey();
        }        
    }
}
