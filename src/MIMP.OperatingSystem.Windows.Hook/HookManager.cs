using MIMP.OperatingSystem.Windows.Native;
using System;
using System.Windows.Interop;

namespace MIMP.OperatingSystem.Windows.Hook
{
    /// <summary>
    /// works only with KeyboardGlobal and MouseGlobal. All other have to hooked in unmanaged codes. // TODO
    /// </summary>
    public static class HookManager
    {

        /// <summary>
        /// If callback return true it suppress all previous hooked procedures - OS too.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static GlobalHookCallback GlobalCallback(HookType type, Func<IntPtr, IntPtr, bool> callback)
        {
            return new GlobalHookCallback(type, callback);
        }

        public static GlobalHookCallback GlobalCallback(HookType type, Action<IntPtr, IntPtr> callback)
        {
            return new GlobalHookCallback(type, (m, p) => { callback(m, p); return false; });
        }

        /// <summary>
        /// If callback return true it suppress all previous hooked procedures - OS too.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static GlobalHookCallback GlobalCallback(HookType type, Func<int, IntPtr, bool> callback)
        {
            return new GlobalHookCallback(type, (m, p) => callback(m.ToInt32(), p));
        }

        public static GlobalHookCallback GlobalCallback(HookType type, Action<int, IntPtr> callback)
        {
            return new GlobalHookCallback(type, (m, p) => { callback(m.ToInt32(), p); return false; });
        }

        /// <summary>
        /// If callback return true it suppress all previous hooked procedures - OS too.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static GlobalHookCallback GlobalCallback(HookType type, Func<WindowsMessage, IntPtr, bool> callback)
        {
            return new GlobalHookCallback(type, (m, p) => callback((WindowsMessage)m.ToInt32(), p));
        }

        public static GlobalHookCallback GlobalCallback(HookType type, Action<WindowsMessage, IntPtr> callback)
        {
            return new GlobalHookCallback(type, (m, p) => { callback((WindowsMessage)m.ToInt32(), p); return false; });
        }


        /// <summary>
        /// Make sure <param name="procedure">procedure</param> is not cleaned up by the garbage collector. Otherwise the garbage collector will clean up your hook delegate eventually, resulting in your code throwing a System.NullReferenceException.
        /// 
        /// </summary>
        /// <param name="procedure"></param>
        /// <returns></returns>
        public static IntPtr GlobalHook(HookType type, HookProcedure procedure)
        {
            return User32.SetWindowsHookEx((int)type, procedure, IntPtr.Zero, 0);
        }

        public static void GlobalUnhook(IntPtr hook)
        {
            User32.UnhookWindowsHookEx(hook);
        }



        public static WindowHookCallback WindowCallback(IntPtr hWnd, Func<int, IntPtr, IntPtr, bool> callback)
        {
            return new WindowHookCallback(hWnd, callback);
        }

        public static WindowHookCallback WindowCallback(IntPtr hWnd, Func<WindowsMessage, IntPtr, IntPtr, bool> callback)
        {
            return new WindowHookCallback(hWnd, (m, w, l) => callback((WindowsMessage)m, w, l));
        }


        public static void WindowHook(IntPtr hWnd, HwndSourceHook hook)
        {
            var source = HwndSource.FromHwnd(hWnd);
            if (source == null)
                throw new ArgumentException($"Window {hWnd} isn't a window", nameof(hWnd));
            source.AddHook(hook);
        }

        public static void WindowUnhook(IntPtr hWnd, HwndSourceHook hook)
        {
            var source = HwndSource.FromHwnd(hWnd);
            if (source != null)
                source.RemoveHook(hook);
        }

    }
}
