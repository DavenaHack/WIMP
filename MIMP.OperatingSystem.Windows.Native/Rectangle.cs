using System.Runtime.InteropServices;

namespace MIMP.OperatingSystem.Windows.Native
{

    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }


    public static class Rectangles
    {

        public static System.Drawing.Rectangle ToDrawing(this Rectangle r)
        {
            return System.Drawing.Rectangle.FromLTRB(r.Left, r.Top, r.Right, r.Bottom);
        }

        public static Rectangle ToNative(this System.Drawing.Rectangle r)
        {
            return new Rectangle
            {
                Left = r.Left,
                Top = r.Top,
                Right = r.Right,
                Bottom = r.Bottom,
            };
        }

    }
}
