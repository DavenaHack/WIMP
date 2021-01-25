using MIMP.OperatingSystem.Windows.Native;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MIMP.OperatingSystem.Windows.Screen
{
    public static class ScreenManager
    {

        public static void Enumerate(EnumerateMonitorProcedure procedure)
        {
            Enumerate(procedure, IntPtr.Zero);
        }

        public static void Enumerate(EnumerateMonitorProcedure procedure, IntPtr param)
        {
            User32.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, procedure, param);
        }

        public static IEnumerable<IntPtr> Screens()
        {
            var r = new List<IntPtr>();
            Enumerate((IntPtr hMonitor, IntPtr hdcMonitor, ref Rectangle lprcMonitor, IntPtr dwData) =>
            {
                r.Add(hMonitor);
                return true;
            });
            return r;
        }

        public static IEnumerable<ScreenInfo> Infos()
        {
            var r = new List<ScreenInfo>();
            Enumerate((IntPtr hMonitor, IntPtr hdcMonitor, ref Rectangle lprcMonitor, IntPtr dwData) =>
            {
                r.Add(Info(hMonitor));
                return true;
            });
            return r;
        }

        public static ScreenInfo Info(IntPtr handle)
        {
            var info = new MonitorInfoExtended
            {
                Size = Marshal.SizeOf<MonitorInfoExtended>(),
            };
            User32.GetMonitorInfo(handle, ref info);
            return new ScreenInfo
            {
                Name = info.DeviceName,
                Flags = (MonitorInfoFlag)info.Flags,
                Size = info.Monitor.ToDrawing(),
                Area = info.WorkArea.ToDrawing(),
            };
        }


        public static IntPtr WindowScreen(IntPtr handle, MonitorFlag flag)
        {
            return User32.MonitorFromWindow(handle, (uint)flag);
        }

    }
}
