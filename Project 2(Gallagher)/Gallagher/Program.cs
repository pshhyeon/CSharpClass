using System;
using System.Threading;

namespace _20180955project3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool start = true; // 전체게임을 초기화하는 bool type 변수 생성
            while (start)
            {
                GameManager mgr = new GameManager(60, 20);
                while (mgr.PlayGame())
                {
                    mgr.Situation();
                    Thread.Sleep(50);
                }
                start = mgr.Restart(); // 멤버함수를 통해 반환 받기
            }
        }
    }
}
