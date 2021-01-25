namespace MIMP.OperatingSystem.Windows.Native
{
    public enum WindowLong : int
    {
        /// <summary>Sets a new extended window style.</summary>
        /// <remarks>See <see cref="ExtendedStyle"/>.</remarks>
        ExtendedStyle = -0x14, // GWL_EX_Style

        HWndParent = -8, // GWLP_HWNDPARENT

        /// <summary>Sets a new identifier of the child window.</summary>
        /// <remarks>The window cannot be a top-level window.</remarks>
        ID = -12, // GWL_ID

        /// <summary>Sets a new window style.</summary>
        Style = -16, // GWL_STYLE

        /// <summary>Sets the user data associated with the window.</summary>
        /// <remarks>This data is intended for use by the application that created the window. Its value is initially zero.</remarks>
        UserData = -21, // 

        /// <summary>Sets new extra information that is private to the application, such as handles or pointers.</summary>
        /// <remarks>Only applies to dialog boxes.</remarks>
        User = 0x8, // DWLP_USER

        /// <summary>Sets the return value of a message processed in the dialog box procedure.</summary>
        /// <remarks>Only applies to dialog boxes.</remarks>
        MessageResult = 0x0, // DWLP_MSGRESULT

        /// <summary>Sets a new address for the window procedure.</summary>
        /// <remarks>You cannot change this attribute if the window does not belong to the same process as the calling thread.</remarks>
        WindowProcedure = -4, // GWL_WNDPROC

        /// <summary>Sets a new application instance handle.</summary>
        Handle = -6, // GWLP_HINSTANCE

        /// <summary>Sets the new address of the dialog box procedure.</summary>
        /// <remarks>Only applies to dialog boxes.</remarks>
        DialogProcedure = 0x4 // DWLP_DLGPROC
    }
}
