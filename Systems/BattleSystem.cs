using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Models;

namespace TextRPG.Systems
{
    internal class BattleSystem
    {
        #region 던전 입장 - 전투 실행
        // 전투 시작 메서드
        // 반환값 : 전투 승리 여부
        public bool StartBattle(Player player, Enemy enemy)
        {
            Console.Clear();            
            Console.WriteLine("===================================");
            Console.WriteLine("|            전투 시작            |");
            Console.WriteLine("===================================");

            // 등장한 적 캐릭터 정보 출력
            enemy.PrintInfo();

            // 턴 변수 정의
            int turn = 1;

            // 전투 루프
            while (player.IsAlive && enemy.IsAlive)
            {
                Console.WriteLine($"\n=======턴 {turn} =======");
                // TODO: 플레이어 턴
                PlayerTurn(player, enemy);
                // TODO: 적 사망여부 판단
                // TODO: 적 턴
                turn++;
            }

            // 전투 결과 반환
            return player.IsAlive;
        }
        #endregion

        #region 플레이어 턴
        // 플레이어 턴 메서드 (1. 공격, 2. 스킬, 3. 도망)
        private void PlayerTurn(Player player, Enemy enemy)
        {
            Console.WriteLine($"\n {player.Name}");
            Console.WriteLine($"HP: {player.CurrentHp}/{player.MaxHp} | MP: {player.CurrentMp}/{player.MaxMp}");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 도망");
            
            
            while(true)
            {
                Console.Write("\n-> 선택 (1~3): ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        // 일반 공격
                        break;
                    case "2":
                        // 스킬 공격
                        break;
                    case "3":
                        // 도망 시도
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요!");
                        continue;
                }
            }
        }
        #endregion

        #region 적 턴
        #endregion
    }
}
