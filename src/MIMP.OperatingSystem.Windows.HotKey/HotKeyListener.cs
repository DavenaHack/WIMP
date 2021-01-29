using MIMP.OperatingSystem.Windows.Hook;
using MIMP.OperatingSystem.Windows.Native;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace MIMP.OperatingSystem.Windows.HotKey
{
    public class HotKeyListener : IDisposable
    {

        public event EventHandler<HotKeyEventArgs> Pressed;


        public IntPtr HWnd { get; }

        public int ID { get; private set; }


        protected WindowHookCallback Callback { get; }


        protected IDictionary<uint, HotKeyEventArgs> HotKeys { get; }

        public HotKeyListener(IntPtr hWnd)
        {
            HWnd = hWnd;
            HotKeys = new ConcurrentDictionary<uint, HotKeyEventArgs>();

            string atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + GetType().FullName;
            ID = Kernel32.GlobalAddAtom(atomName);

            Callback = HookManager.WindowCallback(hWnd, (WindowsMessage message, IntPtr wParam, IntPtr lParam) =>
            {
                if (message == WindowsMessage.HotKey &&
                    wParam.ToInt32() == ID)
                {
                    var hk = (uint)lParam.ToInt32();
                    if (HotKeys.ContainsKey(hk))
                        try
                        {
                            Pressed?.Invoke(this, HotKeys[hk]);
                        }
                        catch { }
                    return false;
                }
                return false;
            });
        }

        ~HotKeyListener()
        {
            Dispose();
        }


        public bool HotKey(KeyModifier modifiers, VirtualKey key)
        {
            HotKeys[(uint)modifiers | (((uint)key) << 16)] = new HotKeyEventArgs(modifiers, key);
            return HotKeyManager.Register(HWnd, ID, modifiers, key);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (ID != 0)
            {
                HotKeyManager.Unregister(HWnd, ID);
                Kernel32.GlobalDeleteAtom((ushort)ID);
                ID = 0;
            }
            Callback.Dispose();
        }

    }
}
