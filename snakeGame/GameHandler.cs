using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _1209hw
{
    public enum EDirection
    {
        RIGHT, LEFT, DOWN, UP, MAX
    }
    public class GameHandler
    {
        const int INIT_LENTH = 6;
        const int FOOD_DISAPPEAR_TIME = 15000;
        const double INIT_SLEEP_TIME = 100;
        const double MIN_SLEEP_TIME = 1;
        Random random = new Random();
        public EDirection Direction
        {
            get; set;
        }
        public bool IsGameOver
        {
            get; private set;
        }
        public int UserPoints
        {
            get; private set;
        }
        private int lastFoodTime;
        private int negativePoints;
        private double sleepTime;
        private List<GameObject> obstacles;
        private List<GameObject> snakeList;
        private GameObject food;
        private ScreenBuffer screenBuffer;
        public GameHandler()
        {
            lastFoodTime = Environment.TickCount;
            negativePoints = 0;
            sleepTime = INIT_SLEEP_TIME;
            Direction = EDirection.RIGHT;
            IsGameOver = false;
            obstacles = new List<GameObject>();
            addFood();
            for (int i = 0; i < 5; i++)
            {
                addObstacle();
            }
            snakeList = new List<GameObject>();
            for (int i = 0; i < INIT_LENTH; i++)
            {
                snakeList.Add(new GameObject(i, 1, '*'));
            }
            screenBuffer = new ScreenBuffer();
        }
        public void GameTick()
        {
            Thread.Sleep((int)sleepTime);
            reduceSleepTime(0.01);
        }
        public void UpdateGame()
        {
            addNegativePoint(1); // Add snake head
            GameObject snakeHead = snakeList.Last(); // using System.Linq;
            GameObject snakeNewHead = new GameObject(snakeHead.X, snakeHead.Y, getHeadImage(Direction));
            snakeNewHead.MoveDirection(Direction);
            // Check collision
            if (snakeList.CheckCollision(snakeNewHead) || obstacles.CheckCollision(snakeNewHead) || crossLine())

            {
                IsGameOver = true;
                return;
            }
            snakeList.Add(snakeNewHead);
            snakeList[snakeList.Count - 2].SetImage('*');
            // Feeding the snake
            if (snakeNewHead.CheckCollision(food))
            {
                addFood();
                addObstacle();
                lastFoodTime = Environment.TickCount;
                reduceSleepTime(1);
            }
            else
            {
                // Remove snake tail
                snakeList.RemoveAt(0);
            }
            // Expiring food
            if (Environment.TickCount - lastFoodTime >= FOOD_DISAPPEAR_TIME)
            {
                addNegativePoint(100);
                addFood();
                lastFoodTime = Environment.TickCount;
            }
        }
        public void Render()
        {
            screenBuffer.DrawToBackBuffer(snakeList);
            screenBuffer.DrawToBackBuffer(food);
            screenBuffer.DrawToBackBuffer(obstacles);
            drawPoints(); screenBuffer.Render();
        }
        private char getHeadImage(EDirection direction)
        {
            switch (direction)
            {
                case EDirection.RIGHT:
                    return '>';
                case EDirection.LEFT:
                    return '<';
                case EDirection.UP:
                    return '^';
                case EDirection.DOWN:
                    return 'v';
                default:
                    return '\0';
            }
        }
        private void addNegativePoint(int p)
        {
            negativePoints += p;
        }
        private void addObstacle()
        {
            GameObject obstacle;
            do
            {
                obstacle = new GameObject(random.Next(Console.WindowWidth), random.Next(Console.WindowHeight), '=');
            }
            while (snakeList.CheckCollision(obstacle) || obstacles.CheckCollision(obstacle) || food.CheckCollision(obstacle));
            obstacles.Add(obstacle);
        }
        private void addFood()
        {
            do
            {
                food = new GameObject(random.Next(Console.WindowWidth), random.Next(Console.WindowHeight), '@');
            }
            while (snakeList.CheckCollision(food) || obstacles.CheckCollision(food));
        }
        private void reduceSleepTime(double time)
        {
            if (sleepTime >= MIN_SLEEP_TIME)
            {
                sleepTime -= time;
            }
        }
        private void drawPoints()
        {
            UserPoints = (snakeList.Count - INIT_LENTH) * 1000 - negativePoints;
            UserPoints = Math.Max(UserPoints, 0);
            screenBuffer.DrawToBackBuffer(Console.WindowWidth - 20, Console.WindowHeight - 2, $"Points : {UserPoints}");
        }
        // 머리가 화면을 벗어날때
        private bool crossLine()
        {
            int x = snakeList[snakeList.Count - 1].X;
            int y = snakeList[snakeList.Count - 1].Y;
            if (x - 1 < 0 && Direction == EDirection.LEFT || x + 1 >= Console.WindowWidth && Direction == EDirection.RIGHT || y - 1 < 0 && Direction == EDirection.DOWN || y + 1 >= Console.WindowHeight && Direction == EDirection.UP)
            {
                return true;
            }
            return false;
        }
    }
}