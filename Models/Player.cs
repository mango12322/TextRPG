using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Models;

namespace TextRPG.Models
{
    internal class Player :Character
    {        
        #region 프로퍼티        

        public JobType Job { get; private set; }        
        public int Gold { get; private set; }
        // TODO: 장착 무기, 장착 방어구
        #endregion


        #region 생성자
        public Player(string name, JobType job) : base(
            name: name,
            maxHp: GetInitHp(job),
            maxMp: GetInitMp(job),
            attackPower: GetInitAttack(job),
            defense: GetInitDefense(job),
            level: 1)
        {
            Job = job;
            Gold = 1000;
        }
        #endregion


        #region 직업별 스텟 초기화
        private static int GetInitHp(JobType job)
        {
            switch (job)
            {
                case JobType.Warrial: return 150;
                case JobType.Archer: return 100;
                case JobType.Wizard: return 80;
                default: return 100;
            }
        }

        private static int GetInitMp(JobType job)
        {
            switch (job)
            {
                case JobType.Warrial: return 30;
                case JobType.Archer: return 50;
                case JobType.Wizard: return 100;
                default: return 30;
            }
        }

        private static int GetInitAttack(JobType job) =>
            job switch
            {
                JobType.Warrial => 15,
                JobType.Archer => 20,
                JobType.Wizard => 25,
                _ => 20,
            };

        private static int GetInitDefense(JobType job)
        {
            switch (job)
            {
                case JobType.Warrial: return 15;
                case JobType.Archer: return 10;
                case JobType.Wizard: return 5;
                default: return 10;
            }

        }
        #endregion

        #region 메서드
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine("================================");
        }

        // 기본 공격 메서드
        public override int Attack(Character target)
        {
            // TODO: 장착무기 또는 방어구에 따른 추가 데미지 계산
            int attackDamage = AttackPower;

            return target.TakeDamage(attackDamage);
        }

        // 스킬 공격 (MP 소모) : Player 전용 메서드
        public int SkillAttack(Character target)
        {
            int mpCost = 15;

            // 스킬 공격 = 기본 공격 * 1.5
            int totalDamage = AttackPower;
            totalDamage = (int)(totalDamage * 1.5);

            // MP 소모
            CurrentMp -= mpCost;

            // 데미지 전달
            return target.TakeDamage(totalDamage);            
        }

        // 골드 획득 메서드
        public void GainGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"\n{amount} 골드를 획득했습니다! 현재 골드: {Gold}");
        }

        #endregion
    }
}
