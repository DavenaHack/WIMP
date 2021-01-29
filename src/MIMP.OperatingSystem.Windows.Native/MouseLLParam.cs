using System;
using System.Runtime.InteropServices;

namespace MIMP.OperatingSystem.Windows.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseLLParam
    {
        public Point pt;
        public int mouseData;
        public int flags;
        public int time;
        public UIntPtr dwExtraInfo;
    }
}
