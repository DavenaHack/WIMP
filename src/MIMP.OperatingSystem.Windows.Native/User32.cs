using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MIMP.OperatingSystem.Windows.Native
{
    public static class User32
    {

        #region Message


        [DllImport("user32.dll")]
        public static extern bool GetMessage(out Message msg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(ref Message lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref Message lpMsg);

        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage([In] ref Message lpmsg);

        [DllImport("user32.dll")]
        public static extern uint MsgWaitForMultipleObjectsEx(uint nCount, IntPtr[] pHandles, uint dwMilliseconds, uint dwWakeMask, uint dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, ref IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SendMessageTimeout(IntPtr hWnd, int uMsg, int wParam, int lParam, int fuFlags, int uTimeout, out int lpdwResult);


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);


        #endregion Message


        #region GetClass

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder buf, int nMaxCount);


        public static IntPtr GetClassLong(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
                return GetClassLongPtr64(hWnd, nIndex);
            else
                return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
        }

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        private static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        private static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        #endregion GetClass


        #region Desktop


        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateDesktop(string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, int dwFlags, int dwDesiredAccess, IntPtr lpsa);


        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseDesktop(IntPtr handle);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SwitchDesktop(IntPtr hDesktop);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetThreadDesktop(IntPtr hDesktop);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetThreadDesktop(uint dwThreadId);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumDesktops(IntPtr hwinsta, EnumerateDesktopsProcedure lpEnumFunc, IntPtr lParam);


        #endregion Desktop


        #region Monitor


        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, EnumerateMonitorProcedure lpfnEnum, IntPtr dwData);


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfoExtended lpmi);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);


        #endregion


        #region Window

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        ///     Retrieves a handle to the Shell's desktop window.
        ///     <para>
        ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633512%28v=vs.85%29.aspx for more
        ///     information
        ///     </para>
        /// </summary>
        /// <returns>
        ///     C++ ( Type: HWND )<br />The return value is the handle of the Shell's desktop window. If no Shell process is
        ///     present, the return value is NULL.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetShellWindow();

        /// <summary>
        /// enumarator on all desktop windows
        /// </summary>
        /// <param name="hDesktop"></param>
        /// <param name="lpEnumCallbackFunction"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumerateWindowsProcedure lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumerateWindowsProcedure lpEnumFunc, IntPtr lParam);


        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr parent, EnumerateWindowsProcedure lpEnumFunc, IntPtr lParam);

        /// <summary>
        /// Retrieves a handle to a window that has the specified relationship (Z-Order or owner) to the specified window.
        /// </summary>
        /// <remarks>The EnumChildWindows function is more reliable than calling GetWindow in a loop. An application that
        /// calls GetWindow to perform this task risks being caught in an infinite loop or referencing a handle to a window
        /// that has been destroyed.</remarks>
        /// <param name="hWnd">A handle to a window. The window handle retrieved is relative to this window, based on the
        /// value of the uCmd parameter.</param>
        /// <param name="uCmd">The relationship between the specified window and the window whose handle is to be
        /// retrieved.</param>
        /// <returns>
        /// If the function succeeds, the return value is a window handle. If no window exists with the specified relationship
        /// to the specified window, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetTopWindow(IntPtr hWnd);


        #region WindowPlacement

        /// <summary>
        /// Retrieves the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="lpwndpl">
        /// A pointer to the WINDOWPLACEMENT structure that receives the show state and position information.
        /// <para>
        /// Before calling GetWindowPlacement, set the length member to sizeof(WINDOWPLACEMENT). GetWindowPlacement fails if lpwndpl-> length is not set correctly.
        /// </para>
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// <para>
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </para>
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

        /// <summary>
        /// Sets the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="lpwndpl">
        /// A pointer to a WINDOWPLACEMENT structure that specifies the new show state and window positions.
        /// <para>
        /// Before calling SetWindowPlacement, set the length member of the WINDOWPLACEMENT structure to sizeof(WINDOWPLACEMENT). SetWindowPlacement fails if the length member is not set correctly.
        /// </para>
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// <para>
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </para>
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WindowPlacement lpwndpl);

        #endregion WindowPlacement


        #region WindowLong

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr window, int nIndex);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size > 4)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLongPtr32(hWnd, nIndex, dwNewLong));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLongPtr32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        #endregion WindowLong


        #region Parent

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        #endregion Parent


        /// <summary>
        ///     The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the
        ///     position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative
        ///     to the upper-left corner of the parent window's client area.
        ///     <para>
        ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633534%28v=vs.85%29.aspx for more
        ///     information
        ///     </para>
        /// </summary>
        /// <param name="hWnd">C++ ( hWnd [in]. Type: HWND )<br /> Handle to the window.</param>
        /// <param name="X">C++ ( X [in]. Type: int )<br />Specifies the new position of the left side of the window.</param>
        /// <param name="Y">C++ ( Y [in]. Type: int )<br /> Specifies the new position of the top of the window.</param>
        /// <param name="nWidth">C++ ( nWidth [in]. Type: int )<br />Specifies the new width of the window.</param>
        /// <param name="nHeight">C++ ( nHeight [in]. Type: int )<br />Specifies the new height of the window.</param>
        /// <param name="bRepaint">
        ///     C++ ( bRepaint [in]. Type: bool )<br />Specifies whether the window is to be repainted. If this
        ///     parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This
        ///     applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the
        ///     parent window uncovered as a result of moving a child window.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.<br /> If the function fails, the return value is zero.
        ///     <br />To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);


        /// <summary>
        ///     Changes the size, position, and Z order of a child, pop-up, or top-level window. These windows are ordered
        ///     according to their appearance on the screen. The topmost window receives the highest rank and is the first window
        ///     in the Z order.
        ///     <para>See https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545%28v=vs.85%29.aspx for more information.</para>
        /// </summary>
        /// <param name="hWnd">C++ ( hWnd [in]. Type: HWND )<br />A handle to the window.</param>
        /// <param name="hWndInsertAfter">
        ///     C++ ( hWndInsertAfter [in, optional]. Type: HWND )<br />A handle to the window to precede the positioned window in
        ///     the Z order. This parameter must be a window handle or one of the following values.
        ///     <list type="table">
        ///     <itemheader>
        ///         <term>HWND placement</term><description>Window to precede placement</description>
        ///     </itemheader>
        ///     <item>
        ///         <term>HWND_BOTTOM ((HWND)1)</term>
        ///         <description>
        ///         Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost
        ///         window, the window loses its topmost status and is placed at the bottom of all other windows.
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>HWND_NOTOPMOST ((HWND)-2)</term>
        ///         <description>
        ///         Places the window above all non-topmost windows (that is, behind all topmost windows). This
        ///         flag has no effect if the window is already a non-topmost window.
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>HWND_TOP ((HWND)0)</term><description>Places the window at the top of the Z order.</description>
        ///     </item>
        ///     <item>
        ///         <term>HWND_TOPMOST ((HWND)-1)</term>
        ///         <description>
        ///         Places the window above all non-topmost windows. The window maintains its topmost position
        ///         even when it is deactivated.
        ///         </description>
        ///     </item>
        ///     </list>
        ///     <para>For more information about how this parameter is used, see the following Remarks section.</para>
        /// </param>
        /// <param name="X">C++ ( X [in]. Type: int )<br />The new position of the left side of the window, in client coordinates.</param>
        /// <param name="Y">C++ ( Y [in]. Type: int )<br />The new position of the top of the window, in client coordinates.</param>
        /// <param name="cx">C++ ( cx [in]. Type: int )<br />The new width of the window, in pixels.</param>
        /// <param name="cy">C++ ( cy [in]. Type: int )<br />The new height of the window, in pixels.</param>
        /// <param name="uFlags">
        ///     C++ ( uFlags [in]. Type: UINT )<br />The window sizing and positioning flags. This parameter can be a combination
        ///     of the following values.
        ///     <list type="table">
        ///     <itemheader>
        ///         <term>HWND sizing and positioning flags</term>
        ///         <description>Where to place and size window. Can be a combination of any</description>
        ///     </itemheader>
        ///     <item>
        ///         <term>SWP_ASYNCWINDOWPOS (0x4000)</term>
        ///         <description>
        ///         If the calling thread and the thread that owns the window are attached to different input
        ///         queues, the system posts the request to the thread that owns the window. This prevents the calling
        ///         thread from blocking its execution while other threads process the request.
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_DEFERERASE (0x2000)</term>
        ///         <description>Prevents generation of the WM_SYNCPAINT message. </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_DRAWFRAME (0x0020)</term>
        ///         <description>Draws a frame (defined in the window's class description) around the window.</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_FRAMECHANGED (0x0020)</term>
        ///         <description>
        ///         Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message
        ///         to the window, even if the window's size is not being changed. If this flag is not specified,
        ///         WM_NCCALCSIZE is sent only when the window's size is being changed
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_HIDEWINDOW (0x0080)</term><description>Hides the window.</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOACTIVATE (0x0010)</term>
        ///         <description>
        ///         Does not activate the window. If this flag is not set, the window is activated and moved to
        ///         the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
        ///         parameter).
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOCOPYBITS (0x0100)</term>
        ///         <description>
        ///         Discards the entire contents of the client area. If this flag is not specified, the valid
        ///         contents of the client area are saved and copied back into the client area after the window is sized or
        ///         repositioned.
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOMOVE (0x0002)</term>
        ///         <description>Retains the current position (ignores X and Y parameters).</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOOWNERZORDER (0x0200)</term>
        ///         <description>Does not change the owner window's position in the Z order.</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOREDRAW (0x0008)</term>
        ///         <description>
        ///         Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies
        ///         to the client area, the nonclient area (including the title bar and scroll bars), and any part of the
        ///         parent window uncovered as a result of the window being moved. When this flag is set, the application
        ///         must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOREPOSITION (0x0200)</term><description>Same as the SWP_NOOWNERZORDER flag.</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOSENDCHANGING (0x0400)</term>
        ///         <description>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOSIZE (0x0001)</term>
        ///         <description>Retains the current size (ignores the cx and cy parameters).</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOZORDER (0x0004)</term>
        ///         <description>Retains the current Z order (ignores the hWndInsertAfter parameter).</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_SHOWWINDOW (0x0040)</term><description>Displays the window.</description>
        ///     </item>
        ///     </list>
        /// </param>
        /// <returns><c>true</c> or nonzero if the function succeeds, <c>false</c> or zero otherwise or if function fails.</returns>
        /// <remarks>
        ///     <para>
        ///     As part of the Vista re-architecture, all services were moved off the interactive desktop into Session 0.
        ///     hwnd and window manager operations are only effective inside a session and cross-session attempts to manipulate
        ///     the hwnd will fail. For more information, see The Windows Vista Developer Story: Application Compatibility
        ///     Cookbook.
        ///     </para>
        ///     <para>
        ///     If you have changed certain window data using SetWindowLong, you must call SetWindowPos for the changes to
        ///     take effect. Use the following combination for uFlags: SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER |
        ///     SWP_FRAMECHANGED.
        ///     </para>
        ///     <para>
        ///     A window can be made a topmost window either by setting the hWndInsertAfter parameter to HWND_TOPMOST and
        ///     ensuring that the SWP_NOZORDER flag is not set, or by setting a window's position in the Z order so that it is
        ///     above any existing topmost windows. When a non-topmost window is made topmost, its owned windows are also made
        ///     topmost. Its owners, however, are not changed.
        ///     </para>
        ///     <para>
        ///     If neither the SWP_NOACTIVATE nor SWP_NOZORDER flag is specified (that is, when the application requests that
        ///     a window be simultaneously activated and its position in the Z order changed), the value specified in
        ///     hWndInsertAfter is used only in the following circumstances.
        ///     </para>
        ///     <list type="bullet">
        ///     <item>Neither the HWND_TOPMOST nor HWND_NOTOPMOST flag is specified in hWndInsertAfter. </item>
        ///     <item>The window identified by hWnd is not the active window. </item>
        ///     </list>
        ///     <para>
        ///     An application cannot activate an inactive window without also bringing it to the top of the Z order.
        ///     Applications can change an activated window's position in the Z order without restrictions, or it can activate
        ///     a window and then move it to the top of the topmost or non-topmost windows.
        ///     </para>
        ///     <para>
        ///     If a topmost window is repositioned to the bottom (HWND_BOTTOM) of the Z order or after any non-topmost
        ///     window, it is no longer topmost. When a topmost window is made non-topmost, its owners and its owned windows
        ///     are also made non-topmost windows.
        ///     </para>
        ///     <para>
        ///     A non-topmost window can own a topmost window, but the reverse cannot occur. Any window (for example, a
        ///     dialog box) owned by a topmost window is itself made a topmost window, to ensure that all owned windows stay
        ///     above their owner.
        ///     </para>
        ///     <para>
        ///     If an application is not in the foreground, and should be in the foreground, it must call the
        ///     SetForegroundWindow function.
        ///     </para>
        ///     <para>
        ///     To use SetWindowPos to bring a window to the top, the process that owns the window must have
        ///     SetForegroundWindow permission.
        ///     </para>
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        #region FindWindow

        /// <summary>
        ///     Retrieves a handle to the top-level window whose class name and window name match the specified strings. This
        ///     function does not search child windows. This function does not perform a case-sensitive search. To search child
        ///     windows, beginning with a specified child window, use the
        ///     <see cref="!:https://msdn.microsoft.com/en-us/library/windows/desktop/ms633500%28v=vs.85%29.aspx">FindWindowEx</see>
        ///     function.
        ///     <para>
        ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633499%28v=vs.85%29.aspx for FindWindow
        ///     information or https://msdn.microsoft.com/en-us/library/windows/desktop/ms633500%28v=vs.85%29.aspx for
        ///     FindWindowEx
        ///     </para>
        /// </summary>
        /// <param name="lpClassName">
        ///     C++ ( lpClassName [in, optional]. Type: LPCTSTR )<br />The class name or a class atom created by a previous call to
        ///     the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the
        ///     high-order word must be zero.
        ///     <para>
        ///     If lpClassName points to a string, it specifies the window class name. The class name can be any name
        ///     registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
        ///     </para>
        ///     <para>If lpClassName is NULL, it finds any window whose title matches the lpWindowName parameter.</para>
        /// </param>
        /// <param name="lpWindowName">
        ///     C++ ( lpWindowName [in, optional]. Type: LPCTSTR )<br />The window name (the window's
        ///     title). If this parameter is NULL, all window names match.
        /// </param>
        /// <returns>
        ///     C++ ( Type: HWND )<br />If the function succeeds, the return value is a handle to the window that has the
        ///     specified class name and window name. If the function fails, the return value is NULL.
        ///     <para>To get extended error information, call GetLastError.</para>
        /// </returns>
        /// <remarks>
        ///     If the lpWindowName parameter is not NULL, FindWindow calls the <see cref="M:GetWindowText" /> function to
        ///     retrieve the window name for comparison. For a description of a potential problem that can arise, see the Remarks
        ///     for <see cref="M:GetWindowText" />.
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        /// </summary>
        /// <param name="ZeroOnly"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(IntPtr ZeroOnly, string lpWindowName);

        #endregion FindWindow


        #region ForegroundWindow

        /// <summary>
        ///     Retrieves a handle to the foreground window (the window with which the user is currently working). The system
        ///     assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
        ///     <para>See https://msdn.microsoft.com/en-us/library/windows/desktop/ms633505%28v=vs.85%29.aspx for more information.</para>
        /// </summary>
        /// <returns>
        ///     C++ ( Type: Type: HWND )<br /> The return value is a handle to the foreground window. The foreground window
        ///     can be NULL in certain circumstances, such as when a window is losing activation.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        ///     Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is
        ///     directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher
        ///     priority to the thread that created the foreground window than it does to other threads.
        ///     <para>See for https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539%28v=vs.85%29.aspx more information.</para>
        /// </summary>
        /// <param name="hWnd">
        ///     C++ ( hWnd [in]. Type: HWND )<br />A handle to the window that should be activated and brought to the foreground.
        /// </param>
        /// <returns>
        ///     <c>true</c> or nonzero if the window was brought to the foreground, <c>false</c> or zero If the window was not
        ///     brought to the foreground.
        /// </returns>
        /// <remarks>
        ///     The system restricts which processes can set the foreground window. A process can set the foreground window only if
        ///     one of the following conditions is true:
        ///     <list type="bullet">
        ///     <listheader>
        ///         <term>Conditions</term><description></description>
        ///     </listheader>
        ///     <item>The process is the foreground process.</item>
        ///     <item>The process was started by the foreground process.</item>
        ///     <item>The process received the last input event.</item>
        ///     <item>There is no foreground process.</item>
        ///     <item>The process is being debugged.</item>
        ///     <item>The foreground process is not a Modern Application or the Start Screen.</item>
        ///     <item>The foreground is not locked (see LockSetForegroundWindow).</item>
        ///     <item>The foreground lock time-out has expired (see SPI_GETFOREGROUNDLOCKTIMEOUT in SystemParametersInfo).</item>
        ///     <item>No menus are active.</item>
        ///     </list>
        ///     <para>
        ///     An application cannot force a window to the foreground while the user is working with another window.
        ///     Instead, Windows flashes the taskbar button of the window to notify the user.
        ///     </para>
        ///     <para>
        ///     A process that can set the foreground window can enable another process to set the foreground window by
        ///     calling the AllowSetForegroundWindow function. The process specified by dwProcessId loses the ability to set
        ///     the foreground window the next time the user generates input, unless the input is directed at that process, or
        ///     the next time a process calls AllowSetForegroundWindow, unless that process is specified.
        ///     </para>
        ///     <para>
        ///     The foreground process can disable calls to SetForegroundWindow by calling the LockSetForegroundWindow
        ///     function.
        ///     </para>
        /// </remarks>
        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion ForegroundWindow


        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [Obsolete]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint GetWindowModuleFileName(IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);


        #region WindowText


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);


        /// <summary>
        ///     Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is a
        ///     control, the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in another
        ///     application.
        ///     <para>
        ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633520%28v=vs.85%29.aspx  for more
        ///     information
        ///     </para>
        /// </summary>
        /// <param name="hWnd">
        ///     C++ ( hWnd [in]. Type: HWND )<br />A <see cref="IntPtr" /> handle to the window or control containing the text.
        /// </param>
        /// <param name="lpString">
        ///     C++ ( lpString [out]. Type: LPTSTR )<br />The <see cref="StringBuilder" /> buffer that will receive the text. If
        ///     the string is as long or longer than the buffer, the string is truncated and terminated with a null character.
        /// </param>
        /// <param name="nMaxCount">
        ///     C++ ( nMaxCount [in]. Type: int )<br /> Should be equivalent to
        ///     <see cref="StringBuilder.Length" /> after call returns. The <see cref="int" /> maximum number of characters to copy
        ///     to the buffer, including the null character. If the text exceeds this limit, it is truncated.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the length, in characters, of the copied string, not including
        ///     the terminating null character. If the window has no title bar or text, if the title bar is empty, or if the window
        ///     or control handle is invalid, the return value is zero. To get extended error information, call GetLastError.<br />
        ///     This function cannot retrieve the text of an edit control in another application.
        /// </returns>
        /// <remarks>
        ///     If the target window is owned by the current process, GetWindowText causes a WM_GETTEXT message to be sent to the
        ///     specified window or control. If the target window is owned by another process and has a caption, GetWindowText
        ///     retrieves the window caption text. If the window does not have a caption, the return value is a null string. This
        ///     behavior is by design. It allows applications to call GetWindowText without becoming unresponsive if the process
        ///     that owns the target window is not responding. However, if the target window is not responding and it belongs to
        ///     the calling application, GetWindowText will cause the calling application to become unresponsive. To retrieve the
        ///     text of a control in another process, send a WM_GETTEXT message directly instead of calling GetWindowText.<br />For
        ///     an example go to
        ///     <see cref="!:https://msdn.microsoft.com/en-us/library/windows/desktop/ms644928%28v=vs.85%29.aspx#sending">
        ///     Sending a
        ///     Message.
        ///     </see>
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);


        #endregion WindowText


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);


        #region Icon


        [DllImport("user32.dll", EntryPoint = "DestroyIcon")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon([In()] IntPtr hIcon);


        #endregion Icon


        #endregion Window


        #region Hook

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProcedure lpfn, IntPtr hMod, uint dwThreadId);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        ///     Passes the hook information to the next hook procedure in the current hook chain. A hook procedure can call this
        ///     function either before or after processing the hook information.
        ///     <para>
        ///     See [ https://msdn.microsoft.com/en-us/library/windows/desktop/ms644974%28v=vs.85%29.aspx ] for more
        ///     information.
        ///     </para>
        /// </summary>
        /// <param name="hhk">C++ ( hhk [in, optional]. Type: HHOOK )<br />This parameter is ignored. </param>
        /// <param name="nCode">
        ///     C++ ( nCode [in]. Type: int )<br />The hook code passed to the current hook procedure. The next
        ///     hook procedure uses this code to determine how to process the hook information.
        /// </param>
        /// <param name="wParam">
        ///     C++ ( wParam [in]. Type: WPARAM )<br />The wParam value passed to the current hook procedure. The
        ///     meaning of this parameter depends on the type of hook associated with the current hook chain.
        /// </param>
        /// <param name="lParam">
        ///     C++ ( lParam [in]. Type: LPARAM )<br />The lParam value passed to the current hook procedure. The
        ///     meaning of this parameter depends on the type of hook associated with the current hook chain.
        /// </param>
        /// <returns>
        ///     C++ ( Type: LRESULT )<br />This value is returned by the next hook procedure in the chain. The current hook
        ///     procedure must also return this value. The meaning of the return value depends on the hook type. For more
        ///     information, see the descriptions of the individual hook procedures.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///     Hook procedures are installed in chains for particular hook types. <see cref="CallNextHookEx" /> calls the
        ///     next hook in the chain.
        ///     </para>
        ///     <para>
        ///     Calling CallNextHookEx is optional, but it is highly recommended; otherwise, other applications that have
        ///     installed hooks will not receive hook notifications and may behave incorrectly as a result. You should call
        ///     <see cref="CallNextHookEx" /> unless you absolutely need to prevent the notification from being seen by other
        ///     applications.
        ///     </para>
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WindowsEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        #endregion Hook


        #region Key


        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetKeyboardState(byte[] lpKeyState);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern short GetKeyState(int nVirtKey);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetKeyboardLayout(uint dwLayout);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetKeyNameText(int lParam, [Out] StringBuilder lpString, int nSize);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);


        #region HotKey


        /// <summary>
        /// Define a system-wide hot key.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window that will receive WM_HOTKEY messages generated by the
        /// hot key. If this parameter is NULL, WM_HOTKEY messages are posted to the
        /// message queue of the calling thread and must be processed in the message loop.
        /// </param>
        /// <param name="id">
        /// The identifier of the hot key. If the hWnd parameter is NULL, then the hot
        /// key is associated with the current thread rather than with a particular
        /// window.
        /// </param>
        /// <param name="fsModifiers">
        /// The keys that must be pressed in combination with the key specified by the
        /// uVirtKey parameter in order to generate the WM_HOTKEY message. The fsModifiers
        /// parameter can be a combination of the following values.
        /// MOD_ALT     0x0001
        /// MOD_CONTROL 0x0002
        /// MOD_SHIFT   0x0004
        /// MOD_WIN     0x0008
        /// </param>
        /// <param name="vk">The virtual-key code of the hot key.</param>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        /// <summary>
        /// Frees a hot key previously registered by the calling thread.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window associated with the hot key to be freed. This parameter
        /// should be NULL if the hot key is not associated with a window.
        /// </param>
        /// <param name="id">
        /// The identifier of the hot key to be freed.
        /// </param>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        #endregion


        #endregion Key


        #region Thread


        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);


        #endregion

    }
}
