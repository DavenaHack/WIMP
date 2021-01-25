using System;

namespace MIMP.OperatingSystem.Windows.Keyboard
{
    [Flags]
    public enum KeyState : byte
    {
        None = 0x0,
        Toggled = 0x1 << 0,
        Pressed = 0x1 << 7,
    }
}
