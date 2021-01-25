using MIMP.OperatingSystem.Windows.Hook;
using MIMP.OperatingSystem.Windows.Native;
using System;

namespace HookConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using var hook = HookManager.GlobalCallback(HookType.KeyboardGlobal, (WindowsMessage m, IntPtr p) =>
            {
                Console.WriteLine(m);
                if (m == WindowsMessage.KeyUp)
                {
                    Environment.Exit(0); //Once you have completed all the task, use this piece of code to exit the console application otherwise, the console will always wait for more messages.
                }
            });

            while ((!User32.GetMessage(out Message msg, IntPtr.Zero, 0, 0)))
            {
                User32.TranslateMessage(ref msg);
                User32.DispatchMessage(ref msg);
                Microsoft.Win32.SystemEvents.DisplaySettingsChanging += (s, e) =>
                {
                };
            }
        }
    }
}
