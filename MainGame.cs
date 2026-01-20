using TextRPG.Utils;
using TextRPG.Data;
using TextRPG.Systems;
using TextRPG.Models;

namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 콘솔 인코딩 설정 (한글 지원)
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // TODO : 저장된 게임 존재 여부 확인
            if (SaveLoadSystem.IsSaveFileExists())
            {
                /* 메뉴 오픈 (새 게임, 이어서하기, 종료) */
                ShowStartMenu();
            }
            else 
                GameManager.Instance.StartGame();


            // TODO : 게임 로드 및 새 게임 시작
            
        }

        static void ShowStartMenu()
        {
            Console.Clear();
            ConsoleUi.ShowTitle();
            Console.WriteLine("|================================|");
            Console.WriteLine("        게임 시작        \n");            
            Console.WriteLine("|================================|");

            Console.WriteLine("\n1. 새 게임");
            Console.WriteLine("2. 이어서 하기");
            Console.WriteLine("3. 종료");

            while (true)
            {
                Console.Write("\n선택 > ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        GameManager.Instance.StartGame();
                        return;
                    case "2":
                        if (GameManager.Instance.LoadGame())
                        {
                            GameManager.Instance.StartGame(true);
                            
                        }                        
                        return;
                    case "3":
                        Console.WriteLine("게임을 종료합니다.");
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                        break;

                }
            }

        }
    }
}
