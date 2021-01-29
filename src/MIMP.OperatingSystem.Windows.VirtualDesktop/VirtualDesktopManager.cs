using MIMP.OperatingSystem.Windows.Interop.VirtualDesktop;
using MIMP.OperatingSystem.Windows.Native;
using MIMP.OperatingSystem.Windows.Registry;
using MIMP.OperatingSystem.Windows.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MIMP.OperatingSystem.Windows.VirtualDesktop
{
    public static class VirtualDesktopManager
    {

        public static IEnumerable<Guid> VirtualDesktops()
        {
            return Use(() => ServiceInstances.VirtualDesktopManagerInternal.GetDesktops(), ds =>
            {
                var r = new List<Guid>();
                ds.GetCount(out var c);
                for (var i = 0; i < c; i++)
                    r.Add(GetID(() =>
                    {
                        ds.GetAt(i, typeof(IVirtualDesktop).GUID, out object v);
                        return (IVirtualDesktop)v;
                    }));
                return r;
            });
        }

        public static int Count()
        {
            return ServiceInstances.VirtualDesktopManagerInternal.GetCount();
        }


        public static Guid Current()
        {
            return GetID(ServiceInstances.VirtualDesktopManagerInternal.GetCurrentDesktop);
        }

        public static Guid Previous(Guid? virtualDesktop = null)
        {
            virtualDesktop ??= Current();
            return Use(() => GetDesktop(virtualDesktop.Value), d => GetID(() => ServiceInstances.VirtualDesktopManagerInternal.GetAdjacentDesktop(d, AdjacentDesktop.Left)));
        }

        public static Guid Next(Guid? virtualDesktop = null)
        {
            virtualDesktop ??= Current();
            return Use(() => GetDesktop(virtualDesktop.Value), d => GetID(() => ServiceInstances.VirtualDesktopManagerInternal.GetAdjacentDesktop(d, AdjacentDesktop.Right)));
        }


        public static int Index(Guid? virtualDesktop = null)
        {
            virtualDesktop ??= Current();
            var i = 0;
            foreach (var d in VirtualDesktops())
                if (d == virtualDesktop)
                    return i;
                else
                    i++;
            return -1;
        }


        public static Guid At(int index)
        {
            int count = ServiceInstances.VirtualDesktopManagerInternal.GetCount();
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));
            return Use(() => ServiceInstances.VirtualDesktopManagerInternal.GetDesktops(), ds =>
            {
                return GetID(() =>
                {
                    ds.GetAt(index, typeof(IVirtualDesktop).GUID, out object v);
                    return (IVirtualDesktop)v;
                });
            });
        }


        public static void Switch(Guid virtualDesktop)
        {
            Use(() => GetDesktop(virtualDesktop), d => ServiceInstances.VirtualDesktopManagerInternal.SwitchDesktop(d));
        }

        public static Guid Create()
        {
            return GetID(() => ServiceInstances.VirtualDesktopManagerInternal.CreateDesktopW());
        }

        public static void Remove(Guid virutalDesktop) // TODO handle auto selection of next desktop
        {
            Use(() => GetDesktop(virutalDesktop), v =>
            {
                Use(() => GetDesktop(Index(virutalDesktop) == 0 ? At(1) : At(0)), d => ServiceInstances.VirtualDesktopManagerInternal.RemoveDesktop(v, d));
            });
        }


        public static string Name(Guid virtualDesktop)
        {
            try
            {
                return RegistryManager.String(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\VirtualDesktops\Desktops\{" + virtualDesktop + "}", "Name");
            }
            catch
            {
                return null;
            }
        }

        public static void SetName(Guid virtualDesktop, string name)
        {
            var k = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\VirtualDesktops\Desktops\{" + virtualDesktop + "}";
            if (name == null)
                RegistryManager.Delete(k);
            else
                RegistryManager.SetString(k, "Name", name);
        }


        #region Window

        public static Guid WindowDesktop(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                throw new ArgumentNullException(nameof(hWnd));
            return ServiceInstances.VirtualDesktopManager.GetWindowDesktopId(hWnd);
        }

        public static void MoveWindow(IntPtr hWnd, Guid? virtualDeskotp = null)
        {
            // TODO
        }

        public static bool IsWindowOnCurrent(IntPtr hWnd)
        {
            return ServiceInstances.VirtualDesktopManager.IsWindowOnCurrentVirtualDesktop(hWnd);
        }

        public static void PinWindow(IntPtr hWnd)
        {
            WindowManager.PinVirtualDesktop(hWnd);
        }

        public static void UnpinWindow(IntPtr hWnd)
        {
            WindowManager.UnpinVirtualDesktop(hWnd);
        }

        #endregion Window



        private static IVirtualDesktop GetDesktop(Guid id)
        {
            return ServiceInstances.VirtualDesktopManagerInternal.FindDesktop(id);
        }

        private static Guid GetID(Func<IVirtualDesktop> com) =>
            Use(com, v => v.GetID());


        private static R Use<C, R>(Func<C> com, Func<C, R> func)
        {
            var c = com();
            var r = func(c);
            if (c != null)
                Marshal.ReleaseComObject(c);
            return r;
        }

        private static void Use<C>(Func<C> com, Action<C> func) =>
            Use<C, object>(com, c => { func(c); return null; });

    }
}
