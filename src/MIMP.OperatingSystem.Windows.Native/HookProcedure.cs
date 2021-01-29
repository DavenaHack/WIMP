using System;

namespace MIMP.OperatingSystem.Windows.Native
{
    public delegate IntPtr HookProcedure(int code, IntPtr wParam, IntPtr lParam);
}
