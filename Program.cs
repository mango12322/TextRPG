namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // TODO : 타이틀 표시
            // TODO : 저장된 게임 존재 여부 확인
            // TODO : 게임 로드 및 새 게임 시작
            TextRPG.Utils.ConsoleUi.ShowTitle();
        }
    }
}
