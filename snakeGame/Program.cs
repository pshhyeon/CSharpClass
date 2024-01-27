using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1209hw
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = Console.WindowWidth / 2;
            Console.BufferWidth = Console.WindowWidth;
            GameHandler handler = new GameHandler();
            // Game Loop
            while (!handler.IsGameOver)
            {
                // 1. Game Tick
                handler.GameTick();
                // 2. Process Input
                if
                (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey(true);
                    if (userInput.Key == ConsoleKey.LeftArrow && handler.Direction != EDirection.RIGHT)
                    {
                        handler.Direction = EDirection.LEFT;
                    }
                    else if
                    (userInput.Key == ConsoleKey.RightArrow && handler.Direction != EDirection.LEFT)
                    {
                        handler.Direction = EDirection.RIGHT;
                    }
                    else if
                    (userInput.Key == ConsoleKey.UpArrow && handler.Direction != EDirection.DOWN)
                    {
                        handler.Direction = EDirection.UP;
                    }
                    else if
                    (userInput.Key == ConsoleKey.DownArrow && handler.Direction != EDirection.UP)
                    {
                        handler.Direction = EDirection.DOWN;
                    }
                }
                // 3. Update Game
                handler.UpdateGame();
                // 4. Render
                handler.Render();
            }
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("Game over!");
        }
    }
}