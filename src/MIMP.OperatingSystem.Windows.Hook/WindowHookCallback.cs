using System;
using System.Windows.Interop;

namespace MIMP.OperatingSystem.Windows.Hook
{
    public class WindowHookCallback : IDisposable
    {

        public IntPtr HWnd { get; }


        protected HwndSourceHook Procedure { get; }

        protected Func<int, IntPtr, IntPtr, bool> Callback { get; set; }


        public WindowHookCallback(IntPtr hWnd, Func<int, IntPtr, IntPtr, bool> callback)
        {
            Callback = callback ?? throw new ArgumentNullException(nameof(callback));
            HWnd = hWnd;
            Procedure = ProcedureMethod;
            HookManager.WindowHook(HWnd, Procedure);
        }

        ~WindowHookCallback()
        {
            Dispose();
        }


        private IntPtr ProcedureMethod(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            try
            {
                handled = Callback(msg, wParam, lParam);
            }
            catch { }
            return IntPtr.Zero;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            HookManager.WindowUnhook(HWnd, Procedure);
        }

    }
}
