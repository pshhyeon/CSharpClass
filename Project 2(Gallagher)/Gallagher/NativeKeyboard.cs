using System;
using System.Collections.Generic;
using System.Text;

namespace _20180955project3
{
    enum EKeyCode
    {
        Space = 0x20,
        Left = 0x25,
        Up,
        Right,
        Down,
    }
    static class NativeKeyboard
    {
        private const int KEY_PRESSED = 0x8000;
        public static bool IsKeyDown(EKeyCode key)
        {
            return (GetKeyState((int)key) & KEY_PRESSED) != 0;
        }        
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetKeyState(int key);
    }
}
