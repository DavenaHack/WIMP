using System;

namespace MIMP.OperatingSystem.Windows.Native
{
    public delegate void WindowsEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
}
