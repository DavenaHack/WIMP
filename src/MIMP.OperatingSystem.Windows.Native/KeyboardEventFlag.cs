using System;

namespace MIMP.OperatingSystem.Windows.Native
{
    [Flags]
    public enum KeyboardEventFlag : byte
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>KEYEVENTF_EXTENDEDKEY</remarks>
        ExtendedKey = 0x1,
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>KEYEVENTF_KEYUP</remarks>
        KeyUp = 0x2
    }
}
