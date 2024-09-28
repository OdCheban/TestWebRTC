using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;

namespace ConsoleTest
{
    public class F
    {
        public static ConsoleKey LClickMouse => ConsoleKey.LeftWindows;
        public static ConsoleKey RClickMouse => ConsoleKey.RightWindows;
        public static ConsoleKey MoveMouse => ConsoleKey.LeftWindows;

        public static bool EqualPosition(Point p1, Point p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }
        public static VirtualKeyCode KeyToVirtual(ConsoleKey consoleKey)
        {
            int q = (int)consoleKey;
            return (VirtualKeyCode)q;
        }
    }
}
