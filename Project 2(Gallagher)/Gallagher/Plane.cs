using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace _20180955project3
{
    class Plane
    {
        public string shape;
        public ConsoleColor color;
        public int dx;
        public int dy;
        public int width;
        public bool alive;
        public bool collision; // collision 추가
        public int x;
        public int y;
        public EDirection eDirection;

        // 생성자 collision 추가
        public Plane(string _shape, ConsoleColor _color)
        {
            shape = _shape;
            width = _shape.Length;
            alive = true;
            collision = false;
            color = _color;
        }
        public Bullet Shoot()
        {
            return new Bullet(x, y, width);
        }
        public void Clear()
        {
            Console.SetCursorPosition(x, y);
            for (int i = 0; i < width; i++)
            {
                Console.Write(" ");
            }
        }
        public void Move(int _dx, int _dy)
        {
            dx = _dx;
            dy = _dy;
        }
        // 함수 오버로딩으로 적기 움직임 함수 생성
        public void Move(EDirection direction, bool down)
        {
            if (direction == EDirection.left)
            {
                eDirection = direction;
                dx = -1;
            }
            else if (direction == EDirection.right)
            {
                eDirection = direction;
                dx = 1;
            }
            if (down)
            {
                dy = 1;
            }
        }
        public void Show()
        {
            if (dx != 0 || dy != 0)
            {
                Clear();
                x += dx;
                y += dy;
                dx = dy = 0;
            }
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(shape);
        }
        public bool Contact(Bullet shooting_bullet)
        {
            if (y != shooting_bullet.y)
            {
                return false;
            }
            int Lx = x, Rx = x + width;
            int shootingbullet_Lx = shooting_bullet.x, shootingbullet_Rx = shooting_bullet.x + shooting_bullet.width;

            if (Lx > shootingbullet_Rx || Rx < shootingbullet_Lx)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // 비행기 충돌 판단 함수
        public bool PlaneCollision (Plane player)
        {            
            if (player.y != y)
            {
                return false;
            }            
            if (player.x > x + width ||  player.x + player.width < x)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
