using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Models;

namespace TextRPG.Models
{
    internal class Enemy : Character
    {
        #region 프로퍼티
        public int GoldReward { get; private set; }
        #endregion


        #region 생성자
        public Enemy(string name, int maxHp, int maxMp, int attackPower, int defense, int level, int goldReward) :
            base(name, maxHp, maxMp, attackPower, defense, level)
        {
            GoldReward = goldReward;
        }
        
        #endregion


        #region 메서드
        // 적 생성 메서드
        public static Enemy CreateEnemy(int playerLevel)
        {
            Random random = new Random();
            // 적 캐릭터의 레벨 (플레이어 레벨 ±1)
            int enemyLevel = Math.Max(1, playerLevel + random.Next(-1, 2));  // -1, 0, +1 
            // 적 캐릭터 랜덤 생성
            string[] enumyType = { "고블린", "오크", "트롤" };
            string enemyName = enumyType[random.Next(0, enumyType.Length)];

            // 적 캐릭터의 스텟 (레벨에 비례)
            int maxHp = 50 + (enemyLevel - 1) * 30;
            int maxMp = 20 + (enemyLevel - 1) * 10;
            int attackPower = 10 + (enemyLevel - 1) * 5;
            int defence = 5 + (enemyLevel - 1) * 3;
            int goldReward = 20 + (enemyLevel - 1) * 10;

            return new Enemy(enemyName, maxHp, maxMp, attackPower, defence, enemyLevel, goldReward);
        }

        public override void PrintInfo()
        {
            Console.Clear();
            Console.WriteLine($"====== {Name} ======");
            Console.WriteLine($"레벨: {Level}");
            Console.WriteLine($"체력: {CurrentHp}/{MaxHp}");            
            Console.WriteLine($"공격력: {AttackPower}");
            Console.WriteLine($"방어력: {Defense}");
        }

        #endregion
    }
}
