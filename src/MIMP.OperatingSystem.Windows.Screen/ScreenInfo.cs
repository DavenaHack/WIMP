using MIMP.OperatingSystem.Windows.Native;
using System.Runtime.InteropServices;
using Rectangle = System.Drawing.Rectangle;

namespace MIMP.OperatingSystem.Windows.Screen
{

    [StructLayout(LayoutKind.Sequential)]
    public struct ScreenInfo
    {
        public string Name;
        public MonitorInfoFlag Flags;
        public Rectangle Size;
        public Rectangle Area;
    }

    public static class ScreenInfoExtensions
    {

        public static bool IsPrimary(this ScreenInfo screen)
        {
            return screen.Flags.HasFlag(MonitorInfoFlag.Primary);
        }

        public static bool IsEmpty(this ScreenInfo screen) =>
            string.IsNullOrEmpty(screen.Name);

    }

}
