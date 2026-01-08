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
                JobType.Warrial => 20,
                JobType.Archer => 25,
                JobType.Wizard => 15,
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
        #endregion
    }
}
