using System;

namespace _20180955prj3
{
    class Program
    {
        enum EPlayer
        {
            user,
            com
        };
        static void Main(string[] args)
        {
            string[,] board = new string[19, 19];
            string again = "y";
            Random random = new Random();
            int firstPlayer = (int)EPlayer.user; // 0

            while (true)
            {
                if (again == "y")
                {
                    Console.Clear();
                    BoardLine(board);
                    Omokpan(board);

                    int x = 0, y = 0, comx = 0, comy = 0;
                    int turn = firstPlayer;
                    while (true)
                    {
                        if (turn == (int)EPlayer.com)
                        {
                            comx = random.Next(0, 19);
                            comy = random.Next(0, 19);
                            if (board[comx, comy] != StoneColor(EPlayer.user) && board[comx, comy] != StoneColor(EPlayer.com))
                            {
                                board[comx, comy] = StoneColor(EPlayer.com);
                                Console.Clear();
                                Omokpan(board);
                                if (WinningFormula(board, StoneColor(EPlayer.com)))
                                {
                                    Console.WriteLine($"{StoneColor(EPlayer.com)} Win.");
                                    firstPlayer = (int)EPlayer.user;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"{StoneColor(EPlayer.com)}'s X.Y: {comx + 1},{comy + 1} ");
                                    turn--;
                                }
                            }
                        }
                        else
                        {
                            Console.Write($"{StoneColor(EPlayer.user)}'s X.Y: ");
                            string[] input = Console.ReadLine().Split();
                            x = int.Parse(input[0]);
                            y = int.Parse(input[1]);
                            if (board[x - 1, y - 1] == StoneColor(EPlayer.user) || board[x - 1, y - 1] == StoneColor(EPlayer.com))
                            {
                                Console.WriteLine("Can not put there. Try again");
                                Console.WriteLine($"{StoneColor(EPlayer.com)}'s X.Y: {comx + 1},{comy + 1} ");
                            }
                            else
                            {
                                if (x != 1)
                                {
                                    board[x - 2, y - 1] = board[x - 2, y - 1].Replace("━", " ");
                                }
                                board[x - 1, y - 1] = StoneColor(EPlayer.user);
                                Console.Clear();
                                Omokpan(board);
                                if (WinningFormula(board, StoneColor(EPlayer.user)))
                                {
                                    Console.WriteLine($"{StoneColor(EPlayer.user)} Win.");
                                    firstPlayer = (int)EPlayer.com;
                                    break;
                                }
                                else
                                {
                                    turn++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
                Console.Write("Play agian? (y/n): ");
                again = Console.ReadLine();
            }
        }

        // 보드판 초기 배열값 지정
        static void BoardLine(string[,] board)
        {
            board[0, 0] = "┏━";
            board[0, 18] = "┗━";
            board[18, 0] = "┓";
            board[18, 18] = "┛";
            for (int i = 1; i < 18; i++)
            {
                for (int j = 1; j < 18; j++)
                {
                    board[i, j] = "╋━";
                }
                board[i, 0] = "┳━";
                board[0, i] = "┣━";
                board[18, i] = "┫";
                board[i, 18] = "┻━";
            }
        }
        // 게임판 출력
        static void Omokpan(string[,] board)
        {
            Console.WriteLine("{0, 35}\n", "====== Let's  Play Omok ======");
            for (int columnNum = 1; columnNum < 20; columnNum++)
            {
                if (columnNum < 2)
                {
                    Console.Write("{0, 4}", columnNum);
                }
                else
                {
                    Console.Write("{0,2}", columnNum);
                }
            }
            for (int a = 0; a < 19; a++)
            {
                Console.Write("\n{0,2}", (a + 1));
                for (int b = 0; b < 19; b++)
                {
                    Console.Write($"{board[b, a]}");
                }
            }
            Console.WriteLine("\n");
        }
        // 바둑돌 색지정
        static string StoneColor(EPlayer player)
        {
            string color = "";
            switch (player)
            {
                case EPlayer.user:
                    color = "○";
                    break;
                case EPlayer.com:
                    color = "●";
                    break;
            }
            return color;
        }
        // 승리공식
        static bool WinningFormula(string[,] board, string stone)
        {
            for (int x = 0; x < 19; x++)
            {
                for (int y = 0; y < 19; y++)
                {
                    if (x < 15 && board[x, y] == stone && board[x + 1, y] == stone && board[x + 2, y] == stone && board[x + 3, y] == stone && board[x + 4, y] == stone) // 가로
                    {
                        return true;
                    }
                    else if (y < 15 && board[x, y] == stone && board[x, y + 1] == stone && board[x, y + 2] == stone && board[x, y + 3] == stone && board[x, y + 4] == stone)// 세로
                    {
                        return true;
                    }
                    else if (x < 15 && y < 15 && board[x, y] == stone && board[x + 1, y + 1] == stone && board[x + 2, y + 2] == stone && board[x + 3, y + 3] == stone && board[x + 4, y + 4] == stone) // 대각선\
                    {
                        return true;
                    }
                    else if (x > 3 && y < 15 && board[x, y] == stone && board[x - 1, y + 1] == stone && board[x - 2, y + 2] == stone && board[x - 3, y + 3] == stone && board[x - 4, y + 4] == stone)// 대각선/
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}