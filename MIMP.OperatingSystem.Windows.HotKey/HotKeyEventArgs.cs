using MIMP.OperatingSystem.Windows.Native;
using System;

namespace MIMP.OperatingSystem.Windows.HotKey
{
    public class HotKeyEventArgs : EventArgs
    {

        public KeyModifier Modifiers { get; }

        public VirtualKey Key { get; }


        public HotKeyEventArgs(KeyModifier modifiers, VirtualKey key)
        {
            Modifiers = modifiers;
            Key = key;
        }

    }
}
