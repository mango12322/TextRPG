using System;
using System.Collections.Generic;
using System.Text;

// 콘솔 관련 UI 유틸리티를 담당
namespace TextRPG.Utils
{
    internal class ConsoleUi
    {
        // 타이틀 표시 메서드
        public static void ShowTitle()
        {
                       Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("        Welcome to TextRPG        ");
            Console.WriteLine("===================================");
            Console.WriteLine();
        }
    }
}
