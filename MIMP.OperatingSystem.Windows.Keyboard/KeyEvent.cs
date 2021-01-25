using MIMP.OperatingSystem.Windows.Native;

namespace MIMP.OperatingSystem.Windows.Keyboard
{
    public enum KeyEvent : int
    {
        /// <summary>
        /// Key down
        /// </summary>
        KeyDown = (int)WindowsMessage.KeyDown,

        /// <summary>
        /// Key up
        /// </summary>
        KeyUp = (int)WindowsMessage.KeyUp,

        /// <summary>
        /// System key down
        /// </summary>
        SystemKeyDown = (int)WindowsMessage.SystemKeyDown,

        /// <summary>
        /// System key up
        /// </summary>
        SystemKeyUp = (int)WindowsMessage.SystemKeyUp,
    }
}
