using System;

namespace MIMP.OperatingSystem.Windows.Native
{
    /// <summary>
    /// Window Styles.
    /// The following styles can be specified wherever a window style is required. After the control has been created, these styles cannot be modified, except as noted.
    /// </summary>
    [Flags()]
    public enum WindowStyle : uint
    {
        /// <summary>The window has a thin-line border.</summary>
        Border = 0x800000, // WS_BORDER

        /// <summary>The window has a title bar (includes the WS_BORDER style).</summary>
        Caption = 0xc00000, // WS_CAPTION

        /// <summary>The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.</summary>
        Child = 0x40000000, // WS_CHILD

        /// <summary>Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.</summary>
        ClipChildren = 0x2000000, // WS_CLIPCHILDREN

        /// <summary>
        /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.
        /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
        /// </summary>
        ClipSiblings = 0x4000000, // WS_CLIPSIBLINGS

        /// <summary>The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.</summary>
        Disabled = 0x8000000, // WS_DISABLED

        /// <summary>The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.</summary>
        DialogFrame = 0x400000, // WS_DLGFRAME

        /// <summary>
        /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style.
        /// The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
        /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
        /// </summary>
        Group = 0x20000, // WS_GROUP

        /// <summary>The window has a horizontal scroll bar.</summary>
        HorizontalScroll = 0x100000, // WS_HSCROLL

        /// <summary>The window is initially maximized.</summary>
        Maximazie = 0x1000000, // WS_MAXIMIZE

        /// <summary>The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
        MaximizeBox = 0x10000, // WS_MAXIMIZEBOX

        /// <summary>The window is initially minimized.</summary>
        Minimize = 0x20000000, // WS_MINIMIZE

        /// <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
        MinimizeBox = 0x20000, // WS_MINIMIZEBOX

        /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>
        Overlapped = 0x00000000, // WS_OVERLAPPED

        /// <summary>The window is an overlapped window.</summary>
        OverlappedWindow = Overlapped | Caption | SysMenu | SizeFrame | MinimizeBox | MaximizeBox, // WS_OVERLAPPEDWINDOW

        /// <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style.</summary>
        PopUp = 0x80000000u, // WS_POPUP

        /// <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.</summary>
        PopUpWindow = PopUp | Border | SysMenu, // WS_POPUPWINDOW

        /// <summary>The window has a sizing border.</summary>
        SizeFrame = 0x40000, // WS_SIZEFRAME

        /// <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified.</summary>
        SysMenu = 0x80000, // WS_SYSMENU

        /// <summary>
        /// The window is a control that can receive the keyboard focus when the user presses the TAB key.
        /// Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.  
        /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
        /// For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
        /// </summary>
        TabStop = 0x10000, // WS_TABSTOP

        /// <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.</summary>
        Visible = 0x10000000, // WS_VISIBLE

        /// <summary>The window has a vertical scroll bar.</summary>
        VerticalScroll = 0x200000, // WS_VSCROLL 
    }
}
