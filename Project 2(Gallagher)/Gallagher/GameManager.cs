using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Linq;

namespace _20180955project3
{
    //EDirection 으로 적비행기의 방향 설정
    public enum EDirection
    {
        right, left
    }
    class GameManager
    {
        const int enemyMoveSpeed = 3;         // 반복 속도 조절 변수 _ 스피드 값이 클수록 속도가 느려짐 최소 1 이상이어야함
        private int delayCount = 0;             // 반복 속도 조절 변수
        private int width;
        private int height;
        private int count;
        private Plane player;
        private Plane[] enemies;
        private List<Bullet> bullets;

        public GameManager(int consoleWidth, int consleHeight)
        {
            width = consoleWidth;
            height = consleHeight;
            SetGame();
            GameBoard();
        }

        public void SetGame()
        {
            Console.WindowHeight = height;
            Console.WindowWidth = width;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            Console.CursorVisible = false;
            player = new Plane(">-0-<", ConsoleColor.Yellow);
            enemies = new Plane[8];
            bullets = new List<Bullet>();
            player.x = width / 2 - 2;
            player.y = height - 4;
            for (int i = 0; i < 8; i++)
            {
                int a = i / 4;
                int b = i % 4;
                enemies[i] = new Plane("[XUX]", ConsoleColor.Green);
                enemies[i].x = a * 4 + 5 + 10 * b;
                enemies[i].y = 5 + a * 3;
            }
        }

        public void GameBoard()
        {
            for (int i = 0; i < height - 1; i++)
            {
                Console.Write("|");
                for (int j = 0; j < width - 2; j++)
                {
                    if (i < height - 2 && i != 1)
                    {
                        Console.Write(" ");
                    }
                    else if (i == 1)
                    {
                        Console.Write("_");
                    }
                    else
                    {
                        Console.Write("_");
                    }
                }
                Console.WriteLine("|");
            }
        }

        public void InputKey()
        {
            int dx = 0;
            int dy = 0;
            if (NativeKeyboard.IsKeyDown(EKeyCode.Left) && player.x != 1)
            {
                dx = -1;
            }
            if (NativeKeyboard.IsKeyDown(EKeyCode.Right) && player.x != width - player.width - 1)
            {
                dx = 1;
            }
            if (NativeKeyboard.IsKeyDown(EKeyCode.Up) && player.y != 0)
            {
                dy = -1;
            }
            if (NativeKeyboard.IsKeyDown(EKeyCode.Down) && player.y != height - 3)
            {
                dy = 1;
            }
            if (NativeKeyboard.IsKeyDown(EKeyCode.Space) && player.y > 2)
            {
                Bullet bullet = player.Shoot();
                bullets.Add(bullet);
            }
            player.Move(dx, dy);
            Thread.Sleep(10);
        }
        // score 표시
        public void printScore()
        {
            Console.SetCursorPosition(width / 2, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Score : {count}");
        }

        //적비행기 움직임함수
        public void EnemyMove()
        {
            EDirection direction = EDirection.right;
            bool down = false;
            bool leftMoveEnemy = enemies.Any(m => m.x == 2);
            bool rightMoveEnemy = enemies.Any(m => m.x + m.width == width - 2);
            if (delayCount == enemyMoveSpeed)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    Plane enemy = enemies[i];
                    {
                        if (!enemy.alive)
                        {
                            continue;
                        }
                        if (enemy.x + enemy.width < width && enemy.y < height - 3)
                        {
                            if (leftMoveEnemy)
                            {
                                direction = EDirection.right;
                                down = true;
                            }
                            else if (rightMoveEnemy)
                            {
                                direction = EDirection.left;
                                down = true;
                            }
                            else
                            {
                                if (enemy.eDirection == EDirection.right)
                                {
                                    direction = EDirection.right;
                                }
                                else
                                {
                                    direction = EDirection.left;
                                }
                            }
                            enemy.Move(direction, down);
                            down = false;
                            delayCount = 0;
                        }
                    }
                }
            }
            delayCount++;
        }
        public void Situation()
        {
            InputKey();
            for (int i = 0; i < bullets.Count; i++)
            {
                Bullet bullet = bullets[i];
                if (bullet.alive)
                {
                    bullet.Show();
                }
            }
            for (int i = 0; i < enemies.Length; i++)
            {
                Plane enemy = enemies[i];
                if (enemy.alive)
                {
                    enemy.Show();
                }
            }
            player.Show();
            for (int i = 0; i < enemies.Length; i++)
            {
                Plane enemy = enemies[i];
                for (int j = 0; j < bullets.Count; j++)
                {
                    Bullet bullet = bullets[j];
                    if (enemy.alive && bullet.alive && enemy.Contact(bullet))
                    {
                        if (enemy.collision)
                        {
                            enemy.alive = false;
                            bullet.alive = false;
                            enemy.Clear();
                            bullet.Clear();
                            count++;
                            break;
                        }
                        else
                        {
                            bullet.alive = false;
                            enemy.collision = true;
                            enemy.Clear();
                            enemy.shape = "{xux}";      //한발 맞으면 모양 변경
                            bullet.Clear();
                            count++;
                            break;
                        }
                    }
                }
                if (enemy.alive && enemy.PlaneCollision(player))        //플레이어와 적기 충돌시
                {
                    enemy.alive = false;
                    player.alive = false;
                    enemy.Clear();
                    player.Clear();
                    break;
                }

            }
            for (int i = 0; i < bullets.Count; i++)
            {
                Bullet bullet = bullets[i];
                if (!bullet.alive)
                {
                    continue;
                }
                bullet.BulletMove();
                if (bullet.x + bullet.dx < 1 || bullet.x + bullet.dx >= width - 2 || bullet.y + bullet.dy < 2 || bullet.y + bullet.dy >= height - 4) // 총알 이동영역 한칸줄이기
                {
                    bullet.alive = false;
                    bullet.Clear();
                }
            }
            EnemyMove();        // 적기 움직임 및 속도 조절
            printScore();       // 점수 표시
        }
        // 게임dl 진행중일때 true이면서 승리 또는 패배시  false 반환
        public bool PlayGame()
        {
            bool end = true;
            if (count >= enemies.Length * 2)
            {
                end = false;
                ResultMessage("WIN");
            }
            if (!player.alive)
            {
                end = false;
                ResultMessage("LOOSE");
            }
            return end;
        }
        // 결과창 생성
        public void ResultMessage(string result)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n\n{"",15} YOU {result}!!");
            Console.WriteLine($"\n{"",15} Score: {count}\n");
            Console.Write($"\n{"",15} Play Again? (y/n):");
        }
        // 게임을 계속 할껀지 키를 입력받고 true 또는 flase 반환
        public bool Restart()
        {
            ConsoleKeyInfo input;
            while (true)
            {
                input = Console.ReadKey(true);
                if (input.Key == ConsoleKey.Y)
                {
                    return true;
                }
                if (input.Key == ConsoleKey.N)
                {
                    return false;
                }
            }
        }
    }
}

