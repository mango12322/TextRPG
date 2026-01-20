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

        
        public void StartGame(bool loadedGame = false)
        {            
            ConsoleUi.ShowTitle();

            Console.WriteLine("RPG 게임에 오신것을 환영합니다!\n");
            
            if (!loadedGame)
            {
                CreateCharacter();            
            
                Inventory = new InventorySystem();            
                SetupInitialItems();
            }

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

            JobType job = JobType.Worrial;            

            while(true)
            {
                Console.WriteLine("선택 (1-3): ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        job = JobType.Worrial;
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
                    Rest();
                    break;
                case "6":
                    /* 게임저장 */
                    SaveGame();
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

            Enemy enemy = Enemy.CreateEnemy(player.Level);
            ConsoleUi.PreesAnyKey();

            enemy.PrintInfo();

            BattleSystem.StartBattle(player, enemy);

            Console.WriteLine("\n던전 탐험을 마치고 마을로 돌아갑니다.");
            ConsoleUi.PreesAnyKey();
        }        

        /* 휴식 */
        private void Rest()
        {
            const int restCost = 50;

            Console.Clear();
            Console.WriteLine($"\n휴식을 취합니다.");
            Console.WriteLine($"\n비용 : {restCost} 골드");

            if (player.Gold < restCost)
            {
                Console.WriteLine("골드가 부족합니다. 휴식을 취할 수 없습니다.");
                ConsoleUi.PreesAnyKey();
                return;
            }

            Console.Write("\n휴식을 취하겠습니까? (y/n)");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                player.SpendGold(restCost);
                player.HealHp(player.MaxHp);
                player.HealMp(player.MaxMp);
                Console.WriteLine("\n휴식을 취했습니다. HP와 MP가 모두 회복되었습니다.");
            }
            else
            {
                Console.WriteLine("\n휴식을 취하지 않았습니다.");
            }
        }

        /* 저장 기능 */
        public void SaveGame()
        {
            if (player == null || Inventory == null)
            {
                Console.WriteLine("\n저장할 게임 데이터가 없습니다.");
                ConsoleUi.PreesAnyKey();
                return;
            }

            if (SaveLoadSystem.SaveGame(player, Inventory))
            {
                Console.WriteLine("\n정상적으로 게임이 저장되었습니다.");
                ConsoleUi.PreesAnyKey();
            }
        }

        /* 게임 로드 */
        public bool LoadGame()
        {
            var saveData = SaveLoadSystem.LoadGame(); 
            if (saveData == null)
            {
                Console.WriteLine("\n저장된 게임 데이터가 없습니다.");
                ConsoleUi.PreesAnyKey();
                return false;
            }

            player = SaveLoadSystem.LoadPlayer(saveData.Player);
            Inventory = SaveLoadSystem.LoadInventory(saveData.Inventory, player);
            SaveLoadSystem.LoadEquippedItems(player, saveData.Player, Inventory);

            Console.WriteLine("\n게임 데이터를 불러왔습니다.");
            ConsoleUi.PreesAnyKey();
            return true;
        }


    }
}
