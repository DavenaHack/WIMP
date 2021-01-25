namespace MIMP.Window
{
    public enum WindowState : byte
    {
        /// <summary>
        /// Window is hidden from the user.
        /// </summary>
        Hide = 0,
        /// <summary>
        /// Window show as normal window.
        /// </summary>
        Normal = 1 << 0,
        /// <summary>
        /// Window is minimized.
        /// </summary>
        Minimize = 1 << 1,
        /// <summary>
        /// window is maximized
        /// </summary>
        Maximize = 1 << 2,
    }
}
