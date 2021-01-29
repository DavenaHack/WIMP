namespace MIMP.OperatingSystem.Windows.Native
{
    public enum GetWindowCommand : uint
    {
        /// <summary>
        /// The retrieved handle identifies the window of the same type that is highest in the Z order.
        /// <para/>
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        /// <remarks>GW_HWNDFIRST</remarks>
        First = 0,
        /// <summary>
        /// The retrieved handle identifies the window of the same type that is lowest in the Z order.
        /// <para />
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        /// <remarks>GW_HWNDLAST</remarks>
        Last = 1,
        /// <summary>
        /// The retrieved handle identifies the window below the specified window in the Z order.
        /// <para />
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        /// <remarks>GW_HWNDNEXT</remarks>
        Next = 2,
        /// <summary>
        /// The retrieved handle identifies the window above the specified window in the Z order.
        /// <para />
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        /// <remarks>GW_HWNDPREV</remarks>
        Previous = 3,
        /// <summary>
        /// The retrieved handle identifies the specified window's owner window, if any.
        /// </summary>
        /// <remarks>GW_OWNER</remarks>
        Owner = 4,
        /// <summary>
        /// The retrieved handle identifies the child window at the top of the Z order,
        /// if the specified window is a parent window; otherwise, the retrieved handle is NULL.
        /// The function examines only child windows of the specified window. It does not examine descendant windows.
        /// </summary>
        /// <remarks>GW_CHILD</remarks>
        Child = 5,
        /// <summary>
        /// The retrieved handle identifies the enabled popup window owned by the specified window (the
        /// search uses the first such window found using GW_HWNDNEXT); otherwise, if there are no enabled
        /// popup windows, the retrieved handle is that of the specified window.
        /// </summary>
        /// <remarks>GW_ENABLEDPOPUP</remarks>
        EnabledPopUp = 6
    }
}
