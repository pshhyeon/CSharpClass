using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1209hw
{
    static class ListExtension
    {
        public static bool CheckCollision(this List<GameObject> list, GameObject pos)
        {
            if (list == null || pos == null)
            {
                return false;
            }
            return list.Any(m => m.X == pos.X && m.Y == pos.Y);
        }
    }
}