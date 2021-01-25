using System;
using System.Runtime.InteropServices;

namespace MIMP.OperatingSystem.Windows.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int x;
        public int y;
    }

    public static class Points
    {
        public static void Deconstruct(this Point p, out int x, out int y)
        {
            x = p.x;
            y = p.y;
        }

        public static System.Drawing.Point ToDrawing(this Point p)
        {
            return new System.Drawing.Point(p.x, p.y);
        }

        public static Point ToNative(this System.Drawing.Point p)
        {
            return new Point { x = p.X, y = p.Y };
        }
    }
}
