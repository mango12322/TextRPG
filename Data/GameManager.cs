using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Utils;
using TextRPG.Models;

namespace TextRPG.Data
{
    internal class GameManager
    {
        // 싱글톤 패턴 (Singleton Pattern) 구현

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

        }
        #endregion

        #region 프로퍼티
        public Player? player { get; private set; }
        #endregion

        #region 게임 시작 메서드
        public void StartGame()
        {
            // 타이틀 표시
            ConsoleUi.ShowTitle();

            Console.WriteLine("RPG 게임에 오신것을 환영합니다!\n");

            // 캐릭터 생성
            CreateCharacter();
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

            Console.WriteLine($"Player Hp: {player.CurrentHp}");
            Console.WriteLine($"Player Mp: {player.CurrentMp}");
            Console.WriteLine($"Player Atk: {player.AttackPower}");
            Console.WriteLine($"Player Def: {player.Defense}");

        }
        #endregion
    }
}
