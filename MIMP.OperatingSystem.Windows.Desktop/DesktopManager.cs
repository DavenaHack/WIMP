using MIMP.OperatingSystem.Windows.Native;
using System;
using System.Collections.Generic;

namespace MIMP.OperatingSystem.Windows.Desktop
{

    /// <summary>
    /// remember that manage not the virtual desktop from windows 10
    /// </summary>
    public static class DesktopManager
    {

        public static IntPtr Create(string name)
        {
            return User32.CreateDesktop(name, IntPtr.Zero, IntPtr.Zero, 0, (int)DesktopAccessMask.GenericAll, IntPtr.Zero);
        }

        public static bool Close(IntPtr desktop)
        {
            return User32.CloseDesktop(desktop);
        }

        public static void Switch(IntPtr desktop)
        {
            User32.SetThreadDesktop(desktop);
            User32.SwitchDesktop(desktop);
        }

        public static IntPtr Current()
        {
            return User32.GetThreadDesktop(Kernel32.GetCurrentThreadId());
        }


        public static void EnumerateDesktops(EnumerateDesktopsProcedure procedure, IntPtr param)
        {
            User32.EnumDesktops(IntPtr.Zero, procedure, param);
        }
        public static void EnumerateDesktops(EnumerateDesktopsProcedure procedure)
        {
            EnumerateDesktops(procedure, IntPtr.Zero);
        }

        public static IEnumerable<string> Desktops()
        {
            var desktops = new List<string>();
            EnumerateDesktops((string desktop, IntPtr param) =>
            {
                desktops.Add(desktop);
                return true;
            });
            return desktops;
        }

    }
}
