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
        public bool StartBattle(Player player, Enemy enemy)
        {
            Console.Clear();            
            Console.WriteLine("===================================");
            Console.WriteLine("|            전투 시작            |");
            Console.WriteLine("===================================");
            
            enemy.PrintInfo();
            
            int turn = 1;

            // 전투 루프
            while (player.IsAlive && enemy.IsAlive)
            {
                Console.WriteLine($"\n=======턴 {turn} =======");
                
                if (!PlayerTurn(player, enemy))
                {
                    Console.WriteLine("\n전투에서 도망쳤습니다...");
                    return false;
                };
                
                if (!enemy.IsAlive)
                {
                    break;
                }

                EnemyTurn(player, enemy);
                turn++;
            }

            // 전투 결과 반환
            if (player.IsAlive)
            {
                int gainGold = enemy.GoldReward;
                Console.WriteLine("\n전투에서 승리했습니다.");
                Console.WriteLine($"\n{gainGold} 골드를 획득했습니다.");

                player.GainGold(gainGold);
                return true;
            }
            else
            {
                Console.WriteLine("\n전투에서 패배했습니다.");
                return false;
            }
        }
        #endregion

        #region 플레이어 턴
        // 플레이어 턴 메서드 (1. 공격, 2. 스킬, 3. 도망)
        private bool PlayerTurn(Player player, Enemy enemy)
        {
            Console.WriteLine($"\n {player.Name}");
            Console.WriteLine($"HP: {player.CurrentHp}/{player.MaxHp} | MP: {player.CurrentMp}/{player.MaxMp}");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 도망");
            
            
            while(true)
            {
                Console.Write("\n-> 선택 (1~3): ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        // 일반 공격
                        int damage = player.Attack(enemy);
                        Console.WriteLine("\n=====================================");
                        Console.WriteLine($"{player.Name}의 공격! {enemy.Name}에게 {damage}의 피해를 입혔습니다.");
                        Console.WriteLine($"{enemy.Name}의 남은 HP: {enemy.CurrentHp}/{enemy.MaxHp}");
                        return true;
                    case "2":
                        // 스킬 사용 전에 MP 체크
                        if (player.CurrentMp < 15)
                        {
                            Console.WriteLine("MP가 부족합니다.");
                            continue;
                        }

                        // 스킬 발동
                        int skillDamage = player.SkillAttack(enemy);
                        Console.WriteLine("\n=====================================");
                        Console.WriteLine($"{player.Name}의 스킬 공격! {enemy.Name}에게 {skillDamage}의 피해를 입혔습니다.");
                        Console.WriteLine($"{enemy.Name}의 남은 HP: {enemy.CurrentHp}/{enemy.MaxHp}");
                        return true;
                    case "3":
                        // 도망 시도
                        Random rand = new Random();
                        if (rand.NextDouble() < 0.5)
                        {
                            Console.WriteLine("\n 도망에 성공했습니다!");
                            return false;
                        }
                        else
                        {
                            Console.WriteLine("\n도망에 실패했습니다.");
                            return true;
                        }
                            default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요!");
                        continue;
                }
            }
        }
        #endregion

        #region 적 턴
        private void EnemyTurn(Player player, Enemy enemy)
        {
            Console.WriteLine("\n=====================================");
            Console.WriteLine($"{enemy.Name}의 턴!");

            int damage = enemy.Attack(player);
            Console.WriteLine($"{enemy.Name}의 공격! {player.Name}에게 {damage}를 입혔습니다.");
            Console.WriteLine($"{player.Name}의 남은 HP: {player.CurrentHp}/{player.MaxHp}");
        }
        #endregion
    }
}
