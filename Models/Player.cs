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
            maxHp: 100,
            maxMp: 50,
            attackPower: 20,
            defense: 10,
            level: 1)
        {
            Job = job;
            Gold = 1000;
        }
        #endregion


    }
}
