using MIMP.OperatingSystem.Windows.Native;
using System;

namespace MIMP.OperatingSystem.Windows.Keyboard
{
    /// <summary>
    /// Raw KeyEvent arguments.
    /// </summary>
    public class RawKeyEventArgs : EventArgs
    {

        /// <summary>
        /// VKCode of the key.
        /// </summary>
        public VirtualKey VirtualKey { get; }

        /// <summary>
        /// Is the hitted key system key.
        /// </summary>
        public bool IsSystemKey { get; }


        public bool Suppress { get; set; }


        /// <summary>
        /// Create raw keyevent arguments.
        /// </summary>
        /// <param name="virtualCode"></param>
        /// <param name="isSystemKey"></param>
        /// <param name="character">Character</param>
        public RawKeyEventArgs(VirtualKey virtualCode, bool isSystemKey)
        {
            VirtualKey = virtualCode;
            IsSystemKey = isSystemKey;
            Suppress = false;
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>Returns string representation of this key, if not possible empty string is returned.</returns>
        public override string ToString()
        {
            return $"{Enum.GetName(VirtualKey) ?? VirtualKey.ToString()} - {IsSystemKey}";
        }

    }
}
