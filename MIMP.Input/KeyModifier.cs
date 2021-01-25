using System;
using System.Collections.Generic;

namespace MIMP.Input
{
    [Flags]
    public enum KeyModifier : byte
    {
        None = 0x0,
        Shift = 0x1 << 0,
        Control = 0x1 << 1,
        Alt = 0x1 << 2,
        Windows = 0x1 << 3,
    }


    public static class KeyModifiers
    {

        public static KeyModifier GetKeyModifiers(bool shift = false, bool control = false, bool alt = false, bool windows = false)
        {
            var r = KeyModifier.None;
            if (shift)
                r |= KeyModifier.Shift;
            if (control)
                r |= KeyModifier.Control;
            if (alt)
                r |= KeyModifier.Alt;
            if (windows)
                r |= KeyModifier.Windows;
            return r;
        }

        public static IDictionary<KeyModifier, IEnumerable<Key>> GetKeys(this KeyModifier modifiers)
        {
            var r = new Dictionary<KeyModifier, IEnumerable<Key>>();
            if (modifiers == KeyModifier.None)
                return r;
            if (modifiers.HasFlag(KeyModifier.Shift))
                r[KeyModifier.Shift] = new HashSet<Key> { Key.LeftShift, Key.RightShift };
            if (modifiers.HasFlag(KeyModifier.Control))
                r[KeyModifier.Control] = new HashSet<Key> { Key.LeftControl, Key.RightControl };
            if (modifiers.HasFlag(KeyModifier.Alt))
                r[KeyModifier.Alt] = new HashSet<Key> { Key.LeftAlt, Key.RightAlt };
            if (modifiers.HasFlag(KeyModifier.Windows))
                r[KeyModifier.Windows] = new HashSet<Key> { Key.LeftWindows, Key.RightWindows };
            return r;
        }


        public static bool HasShift(this KeyModifier modifiers)
        {
            return modifiers.HasFlag(KeyModifier.Shift);
        }

        public static bool HasControl(this KeyModifier modifiers)
        {
            return modifiers.HasFlag(KeyModifier.Control);
        }

        public static bool HasAlt(this KeyModifier modifiers)
        {
            return modifiers.HasFlag(KeyModifier.Alt);
        }

        public static bool HasWindows(this KeyModifier modifiers)
        {
            return modifiers.HasFlag(KeyModifier.Windows);
        }

    }

}
