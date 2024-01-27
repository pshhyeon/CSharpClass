using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1209hw
{
    public class ScreenBuffer
    {
        private readonly int SCREEN_WIDTH;
        private readonly int SCREEN_HEIGHT;
        private char[,] frontBuffer; //현재 화면에 보여지는 곳
        private char[,] backBuffer; //화면에 앞으로 보여질 내용을 저장할 곳
        public ScreenBuffer()
        {
            SCREEN_WIDTH = Console.WindowWidth;
            SCREEN_HEIGHT = Console.WindowHeight;
            Console.CursorVisible = false;
            Console.Clear();
            frontBuffer = new char[SCREEN_HEIGHT, SCREEN_WIDTH];
            backBuffer = new char[SCREEN_HEIGHT, SCREEN_WIDTH];
        }
        public void Render()
        {
            //re-draw changed parts
            for (int y = 0; y < SCREEN_HEIGHT; y++)
            {
                for (int x = 0; x < SCREEN_WIDTH; x++)
                {
                    if (backBuffer[y, x] != frontBuffer[y, x])
                    {
                        Console.SetCursorPosition(x, SCREEN_HEIGHT - y - 1);
                        if (backBuffer[y, x] == '\0')
                        {
                            Console.Write(' ');
                        }
                        else
                        {
                            switch (backBuffer[y, x])
                            {
                                case '*':
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    break;
                                case '>':
                                case '<':
                                case '^':
                                case 'v':
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;
                                case '@':
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    break;
                                case '=':
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    break;
                            }
                            Console.Write(backBuffer[y, x]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                }
            }
            for (int y = 0; y < SCREEN_HEIGHT; y++)
            {
                for (int x = 0; x < SCREEN_WIDTH; x++)
                {
                    frontBuffer[y, x] = backBuffer[y, x];
                    backBuffer[y, x] = '\0';
                }
            }
        }
        public void DrawToBackBuffer(int x, int y, string image)
        {
            for (int index = 0; index < image.Length; index++)
            {
                backBuffer[y, x + index] = image[index];
            }
        }
        public void DrawToBackBuffer(GameObject gameObject)
        {
            backBuffer[gameObject.Y, gameObject.X] = gameObject.Image;
        }
        public void DrawToBackBuffer(List<GameObject> gameObjectList)
        {
            foreach (GameObject go in gameObjectList)
            {
                backBuffer[go.Y, go.X] = go.Image;
            }
        }
    }
}