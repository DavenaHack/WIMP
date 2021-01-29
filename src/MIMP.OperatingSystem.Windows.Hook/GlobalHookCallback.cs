using MIMP.OperatingSystem.Windows.Native;
using System;
using System.Runtime.CompilerServices;

namespace MIMP.OperatingSystem.Windows.Hook
{
    public class GlobalHookCallback : IDisposable
    {

        private static readonly IntPtr _SuppressPrevious = new IntPtr(1);

        public HookType Type { get; set; }

        public IntPtr Hook { get; private set; }


        protected HookProcedure Procedure { get; }

        protected Func<IntPtr, IntPtr, bool> Callback { get; }


        public GlobalHookCallback(HookType type, Func<IntPtr, IntPtr, bool> callback)
        {
            Callback = callback ?? throw new NullReferenceException(nameof(callback));
            Procedure = ProcedureMethod;
            Hook = HookManager.GlobalHook(type, Procedure);
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private IntPtr ProcedureMethod(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < -1)
                return User32.CallNextHookEx(Hook, nCode, wParam, lParam);
            try
            {
                if (Callback(wParam, lParam))
                    return _SuppressPrevious;
            }
            catch { }
            return User32.CallNextHookEx(Hook, nCode, wParam, lParam);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (Hook != IntPtr.Zero)
                lock (this)
                {
                    Hook = IntPtr.Zero;
                    HookManager.GlobalUnhook(Hook);
                }
        }

    }
}
