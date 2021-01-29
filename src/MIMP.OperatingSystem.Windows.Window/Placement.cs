using MIMP.OperatingSystem.Windows.Native;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace MIMP.OperatingSystem.Windows.Window
{
    public struct Placement
    {
        public WindowPlacementFlag Flags;
        public ShowWindowCommand ShowCommand;
        public Point MinPosition;
        public Point MaxPosition;
        public Rectangle NormalPosition;
    }
}
