using MIMP.OperatingSystem.Windows.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace MIMP.OperatingSystem.Windows.Window
{
    public static class WindowManager
    {

        private static readonly IntPtr _ShellWindow = User32.GetShellWindow();


        public static bool IsAppWindow(IntPtr handle)
        {
            if (_ShellWindow == handle)
                return false;

            var style = Style(handle);
            if (!style.HasFlag(WindowStyle.Visible) || style.HasFlag(WindowStyle.Disabled))
                return false;

            var exStyle = ExtendedStyle(handle);
            if (exStyle.HasFlag(ExtendedWindowStyle.Transparent))
                return false;
            if (exStyle.HasFlag(ExtendedWindowStyle.NoRedirectionBitmap))
                return false;

            if (exStyle.HasFlag(ExtendedWindowStyle.NoActivate) && !exStyle.HasFlag(ExtendedWindowStyle.AppWindow))
                return false;

            if (exStyle.HasFlag(ExtendedWindowStyle.Layered))
                return false;

            var title = Title(handle); // TODO find a better method as no title
            if (string.IsNullOrEmpty(title))
                return false;

            var placement = GetPlacement(handle);
            if (placement.ShowCommand == ShowWindowCommand.Hide)
                return false;

            return true;
        }


        public static bool IsVisible(IntPtr handle)
        {
            return User32.IsWindowVisible(handle);
        }


        #region Enumerate

        public static void EnumerateWindows(EnumerateWindowsProcedure procedure, IntPtr param)
        {
            User32.EnumWindows(procedure, param);
        }

        public static void EnumerateWindows(EnumerateWindowsProcedure procedure)
        {
            EnumerateWindows(procedure, IntPtr.Zero);
        }

        public static IEnumerable<IntPtr> Windows()
        {
            var windows = new List<IntPtr>();
            EnumerateWindows((IntPtr handle, IntPtr param) =>
            {
                windows.Add(handle);
                return true;
            });
            return windows;
        }


        public static void EnumerateChildren(IntPtr parent, EnumerateWindowsProcedure procedure, IntPtr param)
        {
            User32.EnumChildWindows(parent, procedure, param);
        }

        public static void EnumerateChildren(IntPtr parent, EnumerateWindowsProcedure procedure)
        {
            User32.EnumChildWindows(parent, procedure, IntPtr.Zero);
        }

        public static IEnumerable<IntPtr> Children(IntPtr parent)
        {
            var windows = new List<IntPtr>();
            EnumerateChildren(parent, (IntPtr handle, IntPtr param) =>
            {
                windows.Add(handle);
                return true;
            });
            return windows;
        }


        #endregion


        #region GetWindow

        public static IntPtr Current()
        {
            return User32.GetForegroundWindow();
        }

        public static void SetCurrent(IntPtr handle)
        {
            User32.SetForegroundWindow(handle);
        }


        public static IntPtr First()
        {
            return First(Current());
        }

        public static IntPtr First(IntPtr handle)
        {
            return User32.GetWindow(handle, (int)GetWindowCommand.First);
        }


        public static IntPtr FirstApp()
        {
            return FirstApp(Current());
        }

        public static IntPtr FirstApp(IntPtr handle)
        {
            handle = First(handle);
            while (handle != IntPtr.Zero && !IsAppWindow(handle))
                handle = Next(handle);
            return handle;
        }


        public static IntPtr Next()
        {
            return Next(Current());
        }

        public static IntPtr Next(IntPtr handle)
        {
            return User32.GetWindow(handle, (int)GetWindowCommand.Next);
        }

        public static IntPtr NextApp()
        {
            return NextApp(Current());
        }

        public static IntPtr NextApp(IntPtr handle)
        {
            do
                handle = Next(handle);
            while (handle != IntPtr.Zero && !IsAppWindow(handle));
            return handle;
        }


        public static IntPtr Previous()
        {
            return Previous(Current());
        }

        public static IntPtr Previous(IntPtr handle)
        {
            return User32.GetWindow(handle, (int)GetWindowCommand.Previous);
        }

        public static IntPtr PreviousApp()
        {
            return PreviousApp(Current());
        }

        public static IntPtr PreviousApp(IntPtr handle)
        {
            do
                handle = Previous(handle);
            while (handle != IntPtr.Zero && !IsAppWindow(handle));
            return handle;
        }


        public static IntPtr Last()
        {
            return Last(Current());
        }

        public static IntPtr Last(IntPtr handle)
        {
            return User32.GetWindow(handle, (int)GetWindowCommand.Last);
        }

        public static IntPtr LastApp()
        {
            return LastApp(Current());
        }

        public static IntPtr LastApp(IntPtr handle = default)
        {
            handle = Last(handle);
            while (handle != IntPtr.Zero && !IsAppWindow(handle))
                handle = Previous(handle);
            return handle;
        }


        public static IEnumerable<IntPtr> AppsForward()
        {
            return AppsForward(Current());
        }

        public static IEnumerable<IntPtr> AppsForward(IntPtr handle)
        {
            if (!IsAppWindow(handle))
                handle = NextApp(handle);
            if (handle == IntPtr.Zero)
                yield break;
            do
            {
                yield return handle;
                handle = NextApp(handle);
            } while (handle != IntPtr.Zero);
        }


        public static IEnumerable<IntPtr> AppsBackward()
        {
            return AppsBackward(Current());
        }

        public static IEnumerable<IntPtr> AppsBackward(IntPtr handle)
        {
            if (!IsAppWindow(handle))
                handle = PreviousApp(handle);
            if (handle == IntPtr.Zero)
                yield break;
            do
            {
                yield return handle;
                handle = PreviousApp(handle);
            } while (handle != IntPtr.Zero);
        }

        #endregion GetWindow


        public static string Title(IntPtr handle)
        {
            var length = User32.GetWindowTextLength(handle);
            if (length < 1)
                return string.Empty;
            var builder = new StringBuilder(length);
            User32.GetWindowText(handle, builder, length + 1);
            return builder.ToString();
        }


        public static string ModuleFileName(IntPtr handle)
        {
            User32.GetWindowThreadProcessId(handle, out var id);
            var p = Process.GetProcessById((int)id);
            return p.MainModule.FileName;
        }


        #region Position

        public static void Switch(IntPtr handle)
        {
            User32.SwitchToThisWindow(handle, true);
        }


        public static void Move(IntPtr handle, int x, int y, int width, int height, bool repaint = true)
        {
            User32.MoveWindow(handle, x, y, width, height, repaint);
        }


        public static void Show(IntPtr handle, ShowWindowCommand showWindowCommand)
        {
            User32.ShowWindow(handle, (int)showWindowCommand);
        }


        public static Placement GetPlacement(IntPtr handle)
        {
            WindowPlacement placement = new WindowPlacement
            {
                length = Marshal.SizeOf<WindowPlacement>()
            };
            User32.GetWindowPlacement(handle, ref placement);
            return new Placement
            {
                Flags = (WindowPlacementFlag)placement.flags,
                ShowCommand = (ShowWindowCommand)placement.showCmd,
                MaxPosition = placement.ptMaxPosition.ToDrawing(),
                MinPosition = placement.ptMinPosition.ToDrawing(),
                NormalPosition = placement.rcNormalPosition.ToDrawing(),
            };
        }


        public static void SetPlacement(IntPtr handle, Placement placement)
        {
            var p = new WindowPlacement
            {
                length = Marshal.SizeOf<WindowPlacement>(),
                flags = (int)placement.Flags,
                showCmd = (int)placement.ShowCommand,
                ptMaxPosition = placement.MaxPosition.ToNative(),
                ptMinPosition = placement.MinPosition.ToNative(),
                rcNormalPosition = placement.NormalPosition.ToNative()
            };
            User32.SetWindowPlacement(handle, ref p);
        }


        public static System.Drawing.Rectangle GetPosition(IntPtr handle)
        {
            User32.GetWindowRect(handle, out var r);
            return r.ToDrawing();
        }

        #endregion Position


        #region Style


        public static WindowStyle Style(IntPtr handle)
        {
            return (WindowStyle)User32.GetWindowLong(handle, (int)WindowLong.Style);
        }

        public static void SetStyle(IntPtr handle, WindowStyle style)
        {
            User32.SetWindowLong(handle, (int)WindowLong.Style, new IntPtr((int)style));
        }

        public static void AddStyle(IntPtr handle, WindowStyle style)
        {
            SetStyle(handle, Style(handle) | style);
        }

        public static void RemoveStyle(IntPtr handle, WindowStyle style)
        {
            SetStyle(handle, Style(handle) & ~style);
        }

        #endregion


        #region ExtendedStyle


        public static ExtendedWindowStyle ExtendedStyle(IntPtr handle)
        {
            return (ExtendedWindowStyle)User32.GetWindowLong(handle, (int)WindowLong.ExtendedStyle);
        }

        public static void SetExtendedStyle(IntPtr handle, ExtendedWindowStyle style)
        {
            User32.SetWindowLong(handle, (int)WindowLong.ExtendedStyle, new IntPtr((int)style));
        }

        public static void AddExtendedStyle(IntPtr handle, ExtendedWindowStyle style)
        {
            SetExtendedStyle(handle, ExtendedStyle(handle) | style);
        }

        public static void RemoveExtendedStyle(IntPtr handle, ExtendedWindowStyle style)
        {
            SetExtendedStyle(handle, ExtendedStyle(handle) & ~style);
        }



        public static void PinVirtualDesktop(IntPtr handle)
        {
            AddExtendedStyle(handle, ExtendedWindowStyle.ToolWindow);
        }

        public static void UnpinVirtualDesktop(IntPtr handle)
        {
            RemoveExtendedStyle(handle, ExtendedWindowStyle.ToolWindow);
        }


        public static void HideTaskbar(IntPtr handle)
        {
            RemoveExtendedStyle(handle, ExtendedWindowStyle.AppWindow);
            AddExtendedStyle(handle, ExtendedWindowStyle.NoActivate);
        }

        public static void ShowTaskbar(IntPtr handle)
        {
            AddExtendedStyle(handle, ExtendedWindowStyle.AppWindow);
            RemoveExtendedStyle(handle, ExtendedWindowStyle.NoActivate);
        }


        #endregion ExtendedStyle


    }
}
