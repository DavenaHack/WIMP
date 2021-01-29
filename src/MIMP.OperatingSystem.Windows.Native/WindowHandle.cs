namespace MIMP.OperatingSystem.Windows.Native
{
    public enum WindowHandle : int
    {
        /// <summary>
        /// To create a message-only window, use the SetParent function to set the parent of a window to HWND_MESSAGE, or use HWND_MESSAGE as the parent in the CreateWindow or CreateWindowEx function.
        /// </summary>
        /// <remarks>HWND_MESSAGE</remarks>
        Message = -3,
        /// <summary>
        ///     Places the window at the top of the Z order.
        /// </summary>
        /// <remarks>HWND_TOP</remarks>
        Top = 0,
        /// <summary>
        ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
        /// </summary>
        /// <remarks>HWND_BOTTOM</remarks>
        Bottom = 1,
        /// <summary>
        ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
        /// </summary>
        /// <remarks>HWND_TOPMOST</remarks>
        TopMost = -1,
        /// <summary>
        ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
        /// </summary>
        /// <remarks>HWND_NOTOPMOST</remarks>
        NoTopMost = -2
    }
}
