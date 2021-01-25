using System;

namespace MIMP.OperatingSystem.Windows.Native
{
    [Flags]
    public enum KeyModifier
    {
        None = 0x0,
        Alt = 0x1 << 0,
        Control = 0x1 << 1,
        Shift = 0x1 << 2,
        Windows = 0x1 << 3,
    }
}
