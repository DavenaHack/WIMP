using System;

namespace MIMP.OperatingSystem.Windows.Native
{
    public delegate bool EnumerateWindowsProcedure(IntPtr hWnd, IntPtr lParam);
}
