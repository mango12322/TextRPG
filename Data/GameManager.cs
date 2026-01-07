using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Utils;

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

        #region 게임 시작 메서드
        public void StartGame()
        {
            // TODO : 타이틀 표시
            ConsoleUi.ShowTitle();

            Console.WriteLine("RPG 게임에 오신것을 환영합니다!\n");
        }
        #endregion
    }
}
