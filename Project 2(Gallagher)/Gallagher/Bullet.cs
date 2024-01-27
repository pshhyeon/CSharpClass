using System;
using System.Collections.Generic;
using System.Text;

namespace _20180955project3
{
    class Bullet
    {
        public string shape;
        public ConsoleColor color;
        public int dx;
        public int dy;
        public int width;
        public bool alive;
        public int x;
        public int y;

        public Bullet(int plane_x, int plane_y, int plane_width)
        {
            shape = "!";
            width = shape.Length;
            alive = true;

            x = plane_x + plane_width / 2;
            y = plane_y - 2;
            color = ConsoleColor.Red;
        }

        public void BulletMove()
        {
            Move(0, -1);
        }

        public void Clear()
        {
            Console.SetCursorPosition(x, y);
            for (int i = 0; i < width; i++)
            {
                Console.Write(" ");
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
        public void Move(int _dx, int _dy)
        {
            dx = _dx;
            dy = _dy;
        }
    }
}
