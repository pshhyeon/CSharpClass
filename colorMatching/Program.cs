using System;
using System.Threading;

namespace _0603hw
{
    public enum EColors
    {
        Green,
        Red,
        Blue,
        Yellow,
        Cyan,
        Max
    };
    class Program
    {
        const int MAX_ROUNDS = 10;
        const int BLOCK_WIDTH = 7;
        static void Main(string[] args)
        {
            int round = 0;
            EColors[] colors = new EColors[MAX_ROUNDS];
            do
            {
                MakeNextRound(colors, ref round);
                ShowRound(colors, round);
            }
            while
            (MakeGuess(colors, round));
            Console.WriteLine("\nWrong!\nGame over");
        }
        static bool MakeGuess(EColors[] colors, int round)
        {
            Console.WriteLine($"Round {round}:");
            Console.WriteLine($"Enter color (R/G/B/Y/C): ");
            for (int i = 0; i < round; i++)
            {
                string inputString = Console.ReadKey(true).Key.ToString();
                EColors inputColor;
                switch (inputString)
                {
                    case "Y":
                        inputColor = EColors.Yellow;
                        break;
                    case "B":
                        inputColor = EColors.Blue;
                        break;
                    case "G":
                        inputColor = EColors.Green;
                        break;
                    case "R":
                        inputColor = EColors.Red;
                        break;
                    case "C":
                        inputColor = EColors.Cyan;
                        break;
                    default:
                        inputColor = EColors.Max;
                        break;
                }
                if (colors[i] != inputColor)
                {
                    ResetConsoleColor();
                    return false;
                }
                SetBackgroundColor(inputColor);
                Console.Write($"{" ",BLOCK_WIDTH}");
                Thread.Sleep(1000);
            }
            ResetConsoleColor();
            Console.WriteLine("\nCorrect.");
            Thread.Sleep(1000);
            return true;
        }
        static void ResetConsoleColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        static void MakeNextRound(EColors[] colors, ref int round)
        {
            Random rand = new Random();
            round++;
            for (int i = 0; i < round; i++)
            {
                colors[i] = (EColors)(rand.Next((int)EColors.Max));
            }
        }
        static void SetBackgroundColor(EColors color)
        {
            switch (color)
            {
                case EColors.Green:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case EColors.Red:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case EColors.Blue:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case EColors.Yellow:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                case EColors.Cyan:
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
            }
        }
        static void ShowRound(EColors[] colors, int round)
        {
            ResetConsoleColor();
            Console.Clear();
            for (int i = 0; i < round; i++)
            {
                Console.WriteLine($"Round {round}:");
                Thread.Sleep(1000);
                for (int j = 0; j < i; j++)
                {
                    Console.Write($"{"",BLOCK_WIDTH}");
                }
                SetBackgroundColor(colors[i]);
                Console.Write($"Q{i + 1,-BLOCK_WIDTH}");
                Thread.Sleep(1000);
                ResetConsoleColor();
                Console.Clear();
            }
        }
    }
}