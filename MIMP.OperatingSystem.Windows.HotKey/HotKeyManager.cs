using MIMP.OperatingSystem.Windows.Native;
using System;

namespace MIMP.OperatingSystem.Windows.HotKey
{
    public static class HotKeyManager
    {

        public static bool Register(IntPtr hWnd, int id, KeyModifier modifiers, VirtualKey key)
        {
            if (key == VirtualKey.None || modifiers == KeyModifier.None)
                throw new ArgumentException("The key or modifiers could not be None.");

            var isKeyRegisterd = User32.RegisterHotKey(hWnd, id, (uint)modifiers, (uint)key);
            if (!isKeyRegisterd)
            {
                Unregister(IntPtr.Zero, id);
                isKeyRegisterd = User32.RegisterHotKey(hWnd, id, (uint)modifiers, (uint)key);
            }

            return isKeyRegisterd;
        }

        public static void Unregister(IntPtr hWnd, int id)
        {
            User32.UnregisterHotKey(hWnd, id);
        }

    }
}
