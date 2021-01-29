namespace MIMP.OperatingSystem.Windows.Native
{
    public enum ShowWindowCommand : uint
    {
        /// <summary>
        /// Hides the window and activates another window.
        /// </summary>
        Hide = 0, // SW_HIDE

        /// <summary>
        /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
        /// </summary>
        ShowNormal = 1, // SW_SHOWNORMAL

        /// <summary>
        /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
        /// </summary>
        Normal = 1, // SW_NORMAL 

        /// <summary>
        /// Activates the window and displays it as a minimized window.
        /// </summary>
        ShowMinimized = 2, // SW_SHOWMINIMIZED

        /// <summary>
        /// Activates the window and displays it as a maximized window.
        /// </summary>
        ShowMaximized = 3, // SW_SHOWMAXIMIZED

        /// <summary>
        /// Maximizes the specified window.
        /// </summary>
        Maximize = 3, // SW_MAXIMIZE

        /// <summary>
        /// Displays a window in its most recent size and position. This value is similar to <see cref="ShowWindowCommand.ShowNormal"/>, except the window is not activated.
        /// </summary>
        ShowNormalNoActivate = 4, // SW_SHOWNOACTIVATE

        /// <summary>
        /// Activates the window and displays it in its current size and position.
        /// </summary>
        Show = 5, // SW_SHOW

        /// <summary>
        /// Minimizes the specified window and activates the next top-level window in the z-order.
        /// </summary>
        Minimize = 6, // SW_MINIMIZE

        /// <summary>
        /// Displays the window as a minimized window. This value is similar to <see cref="ShowWindowCommand.ShowMinimized"/>, except the window is not activated.
        /// </summary>
        ShowMinNoActivate = 7, // SW_SHOWMINNOACTIVE 

        /// <summary>
        /// Displays the window in its current size and position. This value is similar to <see cref="ShowWindowCommand.Show"/>, except the window is not activated.
        /// </summary>
        ShowNoActivate = 8, // SW_SHOWNA

        /// <summary>
        /// Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
        /// </summary>
        Restore = 9, // SW_RESTORE
    }
}
