using System;

namespace MIMP.Input
{
    [Flags]
    public enum KeyState : byte
    {
        None = 0x0,
        Pressed = 0x1 << 0,
        Toggled = 0x1 << 1,
    }

    public static class KeyStateExtensions
    {

        public static bool IsDown(this KeyState state) =>
            state.HasFlag(KeyState.Pressed);

        public static bool IsUp(this KeyState state) =>
            !state.IsDown();


        public static bool IsToggled(this KeyState state) =>
            state.HasFlag(KeyState.Toggled);

    }
}
