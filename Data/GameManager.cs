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
        #region 싱글톤 패턴
        // 싱글톤 인스턴스 (내부 접근 용 전용: 필드)
        private static GameManager _instance;

        // 외부에서 인스턴스에 접근 할 수 있는 프로퍼티
        public static GameManager Instance 
        {
            get
            {
                // 인스턴스가 없으면 새로 생성
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        private GameManager()
        {
            // 클래스가 생성될 때 초기화 작업 수행

            BattleSystem = new BattleSystem();

        }
        #endregion

        #region 프로퍼티
        public Player? player { get; private set; }

        public BattleSystem BattleSystem { get; private set; }

        public bool IsRunning { get; private set; } = true;
        #endregion

        #region 게임 시작 메서드
        public void StartGame()
        {
            // 타이틀 표시
            ConsoleUi.ShowTitle();

            Console.WriteLine("RPG 게임에 오신것을 환영합니다!\n");

            // 캐릭터 생성
            CreateCharacter();

            // 메인 게임 루프
            IsRunning = true;
            while (IsRunning)
                ShowMainMenu();

            if (!IsRunning)
                ConsoleUi.ShowGameOver();

            // TODO : 인벤토리 초기화
            // TODO : 초기 아이템 지급
        }
        #endregion

        #region 캐릭터 생성
        private void CreateCharacter()
        {
            // 이름 입력
            Console.Write("캐릭터명을 입력하세요: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                name = "무명용사";
            }

            Console.WriteLine($"{name}님, 환영합니다!");

            // 직업 선택
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

            // 입력한 이름과 선택한 직업으로 플레이서 캐릭터 생성
            player = new Player(name, job);
            Console.WriteLine($"{name}님, {job}으로 캐릭터가 생성되었습니다.");

            player.PrintInfo();

            ConsoleUi.PreesAnyKey();
        }
        #endregion

        #region 메인메뉴
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
                    player?.PrintInfo();
                    ConsoleUi.PreesAnyKey();
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    EnterDungeon();
                    break;
                case "5":
                    break;
                case "6":
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
        #endregion

        #region 메뉴 기능
        // 던전 입장
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
        #endregion
    }
}
