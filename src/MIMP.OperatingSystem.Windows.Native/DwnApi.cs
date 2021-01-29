using System;
using System.Runtime.InteropServices;

namespace MIMP.OperatingSystem.Windows.Native
{
    public static class DwnApi
    {

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, uint dwAttribute, out bool pvAttribute, int cbAttribute);

    }
}
