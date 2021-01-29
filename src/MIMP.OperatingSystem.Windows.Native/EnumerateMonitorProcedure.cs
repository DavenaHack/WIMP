using System;

namespace MIMP.OperatingSystem.Windows.Native
{
    public delegate bool EnumerateMonitorProcedure(IntPtr hMonitor, IntPtr hdcMonitor, ref Rectangle lprcMonitor, IntPtr dwData);
}
