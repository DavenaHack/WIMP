using System;

namespace MIMP.OperatingSystem.Windows.Native
{
    public enum SystemCommand : int
    {
        ///<summary>Sizes the window.</summary>
        Size = 0xF000, // SC_SIZE

        ///<summary>Moves the window.</summary>
        Move = 0xF010, // SC_MOVE

        ///<summary>Minimizes the window.</summary>
        Minimize = 0xF020, // SC_MINIMIZE

        ///<summary>Maximizes the window.</summary>
        Maximize = 0xF030, // SC_MAXIMIZE

        ///<summary>Moves to the next window.</summary>
        NextWindow = 0xF040, // SC_NEXTWINDOW

        ///<summary>Moves to the previous window.</summary>
        PrevWindow = 0xF050, // SC_PREVWINDOW

        ///<summary>Closes the window.</summary>
        Close = 0xF060, // SC_CLOSE

        ///<summary>Scrolls vertically.</summary>
        VericalScroll = 0xF070, // SC_VSCROLL

        ///<summary>Scrolls horizontally.</summary>
        HorizontalScroll = 0xF080, // SC_HSCROLL

        ///<summary>Retrieves the window menu as a result of a mouse click.</summary>
        MouseMenu = 0xF090, // SC_MOUSEMENU

        ///<summary>
        ///Retrieves the window menu as a result of a keystroke.
        ///For more information, see the Remarks section.
        ///</summary>
        KeyMenu = 0xF100, // SC_KEYMENU

        ///<summary>TODO</summary>
        Arrange = 0xF110, // SC_ARRANGE

        ///<summary>Restores the window to its normal position and size.</summary>
        Restore = 0xF120, // SC_RESTORE

        ///<summary>Activates the Start menu.</summary>
        Tasklist = 0xF130, // SC_TASKLIST

        ///<summary>Executes the screen saver application specified in the [boot] section of the System.ini file.</summary>
        ScreenSave = 0xF140, // SC_SCREENSAVE

        ///<summary>
        ///Activates the window associated with the application-specified hot key.
        ///The lParam parameter identifies the window to activate.
        ///</summary>
        HotKey = 0xF150, // SC_HOTKEY

        #region Obsolete

        #region Win95
        //#if(WINVER >= 0x0400) // Win95

        ///<summary>Selects the default item; the user double-clicked the window menu.</summary>
        [Obsolete]
        Default = 0xF160, // SC_DEFAULT

        ///<summary>
        ///Sets the state of the display. This command supports devices that
        ///have power-saving features, such as a battery-powered personal computer.
        ///The lParam parameter can have the following values: -1 = the display is powering on,
        ///1 = the display is going to low power, 2 = the display is being shut off
        ///</summary>
        [Obsolete]
        MonitorPower = 0xF170, // SC_MONITORPOWER

        ///<summary>
        ///Changes the cursor to a question mark with a pointer. If the user
        ///then clicks a control in the dialog box, the control receives a WM_HELP message.
        ///</summary>
        [Obsolete]
        ContextHelp = 0xF180, // SC_CONTEXTHELP

        ///<summary>TODO</summary>
        [Obsolete]
        Separator = 0xF00F, // SC_SEPARATOR

        //#endif /* WINVER >= 0x0400 */
        #endregion Win95

        #region Vista
        //#if(WINVER >= 0x0600) // 

        ///<summary>Indicates whether the screen saver is secure.</summary>
        [Obsolete]
        IsSecure = 0x00000001, // SCF_ISSECURE

        //#endif /* WINVER >= 0x0600 */
        #endregion Vista

        #region Obsolete Names

        [Obsolete("Use " + nameof(Minimize) + " instead.")]
        Icon = Minimize, // SC_ICON

        [Obsolete("Use " + nameof(Maximize) + " instead.")]
        Zoom = Maximize, // SC_ZOOM

        #endregion Obsolete Names

        #endregion
    }
}
