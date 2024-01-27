using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1209hw
{
    public class GameObject
    {
        public int X
        {
            get; private set;
        }
        public int Y
        {
            get; private set;
        }
        public char Image
        {
            get; private set;
        }
        public GameObject(int x, int y, char c)
        {
            X = x;
            Y = y;
            Image = c;
        }
        public void SetImage(char c)
        {
            Image = c;
        }
        public bool CheckCollision(GameObject pos)
        {
            if (pos == null)
            {
                return false;
            }
            return (X == pos.X && Y == pos.Y);
        }
        public void MoveDirection(EDirection direction)
        {
            switch (direction)
            {
                case EDirection.LEFT:
                    MoveLeft();
                    break;
                case EDirection.RIGHT:
                    MoveRight();
                    break;
                case EDirection.UP:
                    MoveUp();
                    break;
                case EDirection.DOWN:
                    MoveDown();
                    break;
            }
        }
        private void MoveLeft()             // => 연습문제 정답
        {
            X--;
        }
        private void MoveRight()
        {
            X++;
        }
        private void MoveUp()
        {
            Y++;
        }
        private void MoveDown()
        {
            Y--;
        }
    }
}
