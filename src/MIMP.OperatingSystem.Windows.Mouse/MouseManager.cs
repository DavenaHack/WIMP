using MIMP.OperatingSystem.Windows.Hook;
using MIMP.OperatingSystem.Windows.Native;
using System;
using System.Runtime.InteropServices;

namespace MIMP.OperatingSystem.Windows.Mouse
{
    public static class MouseManager
    {

        /// <summary>
        /// Make sure <param name="procedure">procedure</param> is not cleaned up by the garbage collector. Otherwise the garbage collector will clean up your hook delegate eventually, resulting in your code throwing a System.NullReferenceException.
        /// 
        /// </summary>
        /// <param name="procedure"></param>
        /// <returns></returns>
        public static IntPtr HookGlobal(HookProcedure procedure)
        {
            return HookManager.GlobalHook(HookType.MouseGlobal, procedure);
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
        public static GlobalHookCallback GlobalCallback(Func<MouseEvent, MouseLLParam, bool> callback)
        {
            return HookManager.GlobalCallback(HookType.MouseGlobal, (e, p) => callback((MouseEvent)e.ToInt32(), Marshal.PtrToStructure<MouseLLParam>(p)));
        }

        public static GlobalHookCallback GlobalCallback(Action<MouseEvent, MouseLLParam> callback)
        {
            return HookManager.GlobalCallback(HookType.MouseGlobal, (e, p) => callback((MouseEvent)e.ToInt32(), Marshal.PtrToStructure<MouseLLParam>(p)));
        }

    }
}
