using MIMP.OperatingSystem.Windows.Hook;
using MIMP.OperatingSystem.Windows.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MIMP.OperatingSystem.Windows.Keyboard
{
    public static class KeyboardManager
    {

        #region Callback


        /// <summary>
        /// Make sure <param name="procedure">procedure</param> is not cleaned up by the garbage collector. Otherwise the garbage collector will clean up your hook delegate eventually, resulting in your code throwing a System.NullReferenceException.
        /// 
        /// </summary>
        /// <param name="procedure"></param>
        /// <returns></returns>
        public static IntPtr HookGlobal(HookProcedure procedure)
        {
            return HookManager.GlobalHook(HookType.KeyboardGlobal, procedure);
        }

        public static void UnhookGlobal(IntPtr hook)
        {
            HookManager.GlobalUnhook(hook);
        }

        /// <summary>
        /// If callback return true it suppress all previous hooked procedures - OS too.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static GlobalHookCallback GlobalCallback(Func<KeyEvent, VirtualKey, bool> callback)
        {
            return HookManager.GlobalCallback(HookType.KeyboardGlobal, (e, k) => callback((KeyEvent)e.ToInt32(), (VirtualKey)Marshal.ReadInt32(k)));
        }

        public static GlobalHookCallback GlobalCallback(Action<KeyEvent, VirtualKey> callback)
        {
            return HookManager.GlobalCallback(HookType.KeyboardGlobal, (e, k) => callback((KeyEvent)e.ToInt32(), (VirtualKey)Marshal.ReadInt32(k)));
        }


        #endregion Callback


        #region Key


        #region KeyState

        public static KeyState GetKeyState(VirtualKey virtualKey)
        {
            return (KeyState)User32.GetKeyState((int)virtualKey);
        }

        public static bool IsKeyPressed(VirtualKey virtualKey)
        {
            return GetKeyState(virtualKey).HasFlag(KeyState.Pressed);
        }

        public static bool IsKeyToggled(VirtualKey virtualKey)
        {
            return GetKeyState(virtualKey).HasFlag(KeyState.Toggled);
        }


        public static IDictionary<VirtualKey, KeyState> GetKeyStates()
        {
            var r = new Dictionary<VirtualKey, KeyState>();
            var s = new byte[256];
            User32.GetKeyboardState(s);
            foreach (var k in Enum.GetValues<VirtualKey>().Distinct())
                r.Add(k, (KeyState)s[(int)k]);
            return r;
        }

        public static IEnumerable<VirtualKey> PressedKeys()
        {
            return GetKeyStates()
                .Where(x => x.Value.HasFlag(KeyState.Pressed))
                .Select(x => x.Key);
        }

        public static IEnumerable<VirtualKey> ToggledKeys()
        {
            return GetKeyStates()
                .Where(x => x.Value.HasFlag(KeyState.Toggled))
                .Select(x => x.Key);
        }


        public static void SetKeyState(VirtualKey virtualKey, KeyState state)
        {
            var s = new byte[256];
            User32.GetKeyboardState(s);
            s[(int)virtualKey] = (byte)state;
            User32.SetKeyboardState(s);
        }

        public static void AddKeyState(VirtualKey virtualKey, KeyState state)
        {
            SetKeyState(virtualKey, GetKeyState(virtualKey) | state);
        }

        public static void RemoveKeyState(VirtualKey virtualKey, KeyState state)
        {
            SetKeyState(virtualKey, GetKeyState(virtualKey) & ~state);
        }


        #endregion KeyState


        #region SendKey


        public static void KeyDown(VirtualKey virtualKey)
        {
            User32.keybd_event((byte)virtualKey, virtualKey.ToScanCode(), (uint)(KeyboardEventFlag.ExtendedKey), UIntPtr.Zero);
        }

        public static void KeyUp(VirtualKey virtualKey)
        {
            User32.keybd_event((byte)virtualKey, virtualKey.ToScanCode(), (uint)(KeyboardEventFlag.ExtendedKey | KeyboardEventFlag.KeyUp), UIntPtr.Zero);
        }


        #endregion SendKey


        #endregion Key


        #region Keyboard


        public static IntPtr GetKeyboardLayout(IntPtr handle = default)
        {
            if (handle == IntPtr.Zero)
                handle = User32.GetDesktopWindow();
            var thread = User32.GetWindowThreadProcessId(handle, out var _);
            return User32.GetKeyboardLayout(thread);
        }

        #endregion

    }

    public static class KeyboardExtensions
    {

        public static Input.Key ToKey(this VirtualKey virtualKey)
        {
            return (Input.Key)System.Windows.Input.KeyInterop.KeyFromVirtualKey((int)virtualKey);
        }

        public static VirtualKey ToVirtual(this Input.Key key)
        {
            return (VirtualKey)System.Windows.Input.KeyInterop.VirtualKeyFromKey((System.Windows.Input.Key)key);
        }

        public static byte ToScanCode(this VirtualKey virtualKey)
        {
            return (byte)User32.MapVirtualKeyEx((uint)virtualKey, (uint)MapVirtualKeyMapTypes.VirtualKey2ScanCode, KeyboardManager.GetKeyboardLayout());
        }

        public static Input.KeyState ToState(this KeyState state)
        {
            var r = Input.KeyState.None;
            if (state == KeyState.None)
                return r;
            if (state.HasFlag(KeyState.Pressed))
                r |= Input.KeyState.Pressed;
            if (state.HasFlag(KeyState.Toggled))
                r |= Input.KeyState.Toggled;
            return r;
        }

        public static KeyState ToState(this Input.KeyState state)
        {
            var r = KeyState.None;
            if (state == Input.KeyState.None)
                return r;
            if (state.HasFlag(Input.KeyState.Pressed))
                r |= KeyState.Pressed;
            if (state.HasFlag(Input.KeyState.Toggled))
                r |= KeyState.Toggled;
            return r;
        }

    }
}
