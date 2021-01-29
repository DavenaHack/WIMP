namespace MIMP.OperatingSystem.Windows.Native
{
    /// <summary>
    /// The set of valid MapTypes used in MapVirtualKey
    /// </summary>
    public enum MapVirtualKeyMapTypes : uint
    {
        /// <summary>
        /// uCode is a virtual-key code and is translated into a scan code.
        /// If it is a virtual-key code that does not distinguish between left- and
        /// right-hand keys, the left-hand scan code is returned.
        /// If there is no translation, the function returns 0.
        /// </summary>
        VirtualKey2ScanCode = 0, // MAPVK_VK_TO_VSC

        /// <summary>
        /// uCode is a scan code and is translated into a virtual-key code that
        /// does not distinguish between left- and right-hand keys. If there is no
        /// translation, the function returns 0.
        /// </summary>
        ScanCode2VirtualKey = 1, // MAPVK_VSC_TO_VK

        /// <summary>
        /// uCode is a virtual-key code and is translated into an unshifted
        /// character value in the low-order word of the return value. Dead keys (diacritics)
        /// are indicated by setting the top bit of the return value. If there is no
        /// translation, the function returns 0.
        /// </summary>
        VirtualKey2Character = 2, // MAPVK_VK_TO_CHAR

        /// <summary>
        /// Windows NT/2000/XP: uCode is a scan code and is translated into a
        /// virtual-key code that distinguishes between left- and right-hand keys. If
        /// there is no translation, the function returns 0.
        /// </summary>
        VSC_TO_VK_EX = 3, // MAPVK_VSC_TO_VK_EX

        /// <summary>
        /// Not currently documented
        /// </summary>
        VK_TO_VSC_EX = 0x04 // MAPVK_VK_TO_VSC_EX
    }
}
