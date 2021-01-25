using System;

namespace MIMP.OperatingSystem.Windows.Native
{
    /// <summary>
    ///     The type of hook procedure to be installed by <see cref="User32.SetWinEventHook(uint, uint, IntPtr, WindowsEventDelegate, uint, uint, uint)" />.
    /// </summary>
    public enum WindowsEventHookType : uint
    {
        /// <summary>
        ///     The range of WinEvent constant values specified by the Accessibility Interoperability Alliance (AIA) for use across the industry.
        /// </summary>
        /// <remarks>EVENT_AIA_START</remarks>
        AIAStart = 0xA000,
        /// <remarks>EVENT_AIA_END</remarks>
        AIAEnd = 0xAFFF,

        /// <summary>
        ///     The lowest possible event values.
        /// </summary>
        /// <remarks>EVENT_MIN</remarks>
        Min = 0x00000001,

        /// <summary>
        ///     The highest possible event values.
        /// </summary>
        /// <remarks>EVENT_MAX</remarks>
        Max = 0x7FFFFFFF,

        /// <summary>
        ///     An object's KeyboardShortcut property has changed. Server applications send this event
        ///     for their accessible objects.
        /// </summary>
        /// <remarks>EVENT_OBJECT_ACCELERATORCHANGE</remarks>
        ObjectAcceleratorChange = 0x8012,

        /// <summary>
        ///     A window object's scrolling has ended. Unlike <see cref="EVENT_SYSTEM_SCROLLINGEND"/>, this event is
        ///     associated with the scrolling window. Whether the scrolling is horizontal or vertical scrolling,
        ///     this event should be sent whenever the scroll action is completed.
        ///     The hwnd parameter of the WinEventProc callback function describes the scrolling window;
        ///     the idObject parameter is OBJID_CLIENT, and the idChild parameter is CHILDID_SELF.
        /// </summary>
        /// <remarks>EVENT_OBJECT_CONTENTSCROLLED</remarks>
        ObjectContentScrolled = 0x8015,

        /// <summary>
        ///     An object has been created. The system sends this event for the following user interface elements: caret,
        ///     header control, list-view control, tab control, toolbar control, tree view control, and window object.
        ///     Server applications send this event for their accessible objects.
        ///     Before sending the event for the parent object, servers must send it for all of an object's child objects.
        ///     Servers must ensure that all child objects are fully created and ready to accept IAccessible calls from
        ///     clients before the parent object sends this event.
        ///     Because a parent object is created after its child objects, clients must make sure that an object's parent
        ///     has been created before calling IAccessible::get_accParent, particularly if in-context hook functions are
        ///     used.
        /// </summary>
        /// <remarks>EVENT_OBJECT_CREATE</remarks>
        ObjectCreate = 0x8000,
        /// <remarks>EVENT_OBJECT_DEFACTIONCHANGE</remarks>
        ObjectDefactionChange = 0x8011,
        /// <remarks>EVENT_OBJECT_DESCRIPTIONCHANGE</remarks>
        ObjectDescriptionChange = 0x800D,
        /// <remarks>EVENT_OBJECT_DESTROY</remarks>
        ObjectDestroy = 0x8001,
        /// <remarks>EVENT_OBJECT_DRAGSTART</remarks>
        ObjectDragStart = 0x8021,
        /// <remarks>EVENT_OBJECT_DRAGCANCEL</remarks>
        ObjectDragCancel = 0x8022,
        /// <remarks>EVENT_OBJECT_DRAGCOMPLETE</remarks>
        ObjectDragComplete = 0x8023,
        /// <remarks>EVENT_OBJECT_DRAGENTER</remarks>
        ObjectDragEnter = 0x8024,
        /// <remarks>EVENT_OBJECT_DRAGLEAVE</remarks>
        ObjectDragLeave = 0x8025,
        /// <remarks>EVENT_OBJECT_DRAGDROPPED</remarks>
        ObjectDragDropped = 0x8026,
        /// <remarks>EVENT_OBJECT_END</remarks>
        ObjectEnd = 0x80FF,
        /// <remarks>EVENT_OBJECT_FOCUS</remarks>
        ObjectFocus = 0x8005,
        /// <remarks>EVENT_OBJECT_HELPCHANGE</remarks>
        ObjectHelpChange = 0x8010,
        /// <remarks>EVENT_OBJECT_HIDE</remarks>
        ObjectHide = 0x8003,
        /// <remarks>EVENT_OBJECT_HOSTEDOBJECTSINVALIDATED</remarks>
        ObjectHostedObjectsInvalidated = 0x8020,
        /// <remarks>EVENT_OBJECT_IME_HIDE</remarks>
        ObjectIMEHide = 0x8028,
        /// <remarks>EVENT_OBJECT_IME_SHOW</remarks>
        ObjectIMEShow = 0x8027,
        /// <remarks>EVENT_OBJECT_IME_CHANGE</remarks>
        ObjectIMEChange = 0x8029,
        /// <remarks>EVENT_OBJECT_INVOKED</remarks>
        ObjectInvoked = 0x8013,
        /// <remarks>EVENT_OBJECT_LIVEREGIONCHANGED</remarks>
        ObjectLiveRegionChanged = 0x8019,
        /// <remarks>EVENT_OBJECT_LOCATIONCHANGE</remarks>
        ObjectLocationChange = 0x800B,
        /// <remarks>EVENT_OBJECT_NAMECHANGE</remarks>
        ObjectNameChange = 0x800C,
        /// <remarks>EVENT_OBJECT_PARENTCHANGE</remarks>
        ObjectParentChange = 0x800F,
        /// <remarks>EVENT_OBJECT_REORDER</remarks>
        ObjectReorder = 0x8004,
        /// <remarks>EVENT_OBJECT_SELECTION</remarks>
        ObjectSelection = 0x8006,
        /// <remarks>EVENT_OBJECT_SELECTIONADD</remarks>
        ObjectSelectionAdd = 0x8007,
        /// <remarks>EVENT_OBJECT_SELECTIONREMOVE</remarks>
        ObjectSelectionRemove = 0x8008,
        /// <remarks>EVENT_OBJECT_SELECTIONWITHIN</remarks>
        ObjectSelectionWithin = 0x8009,
        /// <remarks>EVENT_OBJECT_SHOW</remarks>
        ObjectShow = 0x8002,
        /// <remarks>EVENT_OBJECT_STATECHANGE</remarks>
        ObjectStateChagne = 0x800A,
        /// <remarks>EVENT_OBJECT_TEXTEDIT_CONVERSIONTARGETCHANGED</remarks>
        ObjectTextEditConversionTargetChanged = 0x8030,
        /// <remarks>EVENT_OBJECT_TEXTSELECTIONCHANGED</remarks>
        ObjectTextSelectionChange = 0x8014,
        /// <remarks>EVENT_OBJECT_VALUECHANGE</remarks>
        ObjectValueChange = 0x800E,
        /// <remarks>EVENT_SYSTEM_ALERT</remarks>
        SystemAlert = 0x0002,
        /// <remarks>EVENT_SYSTEM_ARRANGMENTPREVIEW</remarks>
        SystemArrangementPreview = 0x8016,
        /// <remarks>EVENT_SYSTEM_CAPTUREEND</remarks>
        SystemCaptureEnd = 0x0009,
        /// <remarks>EVENT_SYSTEM_CAPTURESTART</remarks>
        SystemCaptureStart = 0x0008,
        /// <remarks>EVENT_SYSTEM_CONTEXTHELPEND</remarks>
        SystemContextHelpEnd = 0x000D,
        /// <remarks>EVENT_SYSTEM_CONTEXTHELPSTART</remarks>
        SystemContextHelpStart = 0x000C,
        /// <remarks>EVENT_SYSTEM_DESKTOPSWITCH</remarks>
        SystemDesktopSwitch = 0x0020,
        /// <remarks>EVENT_SYSTEM_DIALOGEND</remarks>
        SystemDialogEnd = 0x0011,
        /// <remarks>EVENT_SYSTEM_DIALOGSTART</remarks>
        SystemDialogStart = 0x0010,
        /// <remarks>EVENT_SYSTEM_DRAGDROPEND</remarks>
        SystemDragDropEnd = 0x000F,
        /// <remarks>EVENT_SYSTEM_DRAGDROPSTART</remarks>
        SystemDragDropStart = 0x000E,
        /// <remarks>EVENT_SYSTEM_END</remarks>
        SystemEnd = 0x00FF,
        /// <remarks>EVENT_SYSTEM_FOREGROUND</remarks>
        SystemForeground = 0x0003,
        /// <remarks>EVENT_SYSTEM_MENUPOPUPEND</remarks>
        SystemMenuPopUpEnd = 0x0007,
        /// <remarks>EVENT_SYSTEM_MENUPOPUPSTART</remarks>
        SystemMenuPopUpStart = 0x0006,
        /// <remarks>EVENT_SYSTEM_MENUEND</remarks>
        SystemMenuEnd = 0x0005,
        /// <remarks>EVENT_SYSTEM_MENUSTART</remarks>
        SystemMenuStart = 0x0004,
        /// <remarks>EVENT_SYSTEM_MINIMIZEEND</remarks>
        SystemMinimizeEnd = 0x0017,
        /// <remarks>EVENT_SYSTEM_MINIMIZESTART</remarks>
        SystemMinimizeStart = 0x0016,
        /// <remarks>EVENT_SYSTEM_MOVESIZEEND</remarks>
        SystemMoveSizeEnd = 0x000B,
        /// <remarks>EVENT_SYSTEM_MOVESIZESTART</remarks>
        SystemMoveSizeStart = 0x000A,
        /// <remarks>EVENT_SYSTEM_SCROLLINGEND</remarks>
        SystemScrollingEnd = 0x0013,
        /// <remarks>EVENT_SYSTEM_SCROLLINGSTART</remarks>
        SystemScrollingStart = 0x0012,
        /// <remarks>EVENT_SYSTEM_SOUND</remarks>
        SystemSound = 0x0001,
        /// <remarks>EVENT_SYSTEM_SWITCHEND</remarks>
        SystemSwitchEnd = 0x0015,
        /// <remarks>EVENT_SYSTEM_SWITCHSTART</remarks>
        SystemSwitchStart = 0x0014,
        /// <remarks>EVENT_OEM_DEFINED_START</remarks>
        OEMDefinedStart = 0x0101,
        /// <remarks>EVENT_OEM_DEFINED_END</remarks>
        OEMDefinedEnd = 0x01FF,
        /// <remarks>EVENT_UIA_EVENTID_START</remarks>
        UIAEventIDStart = 0x4E00,
        /// <remarks>EVENT_UIA_EVENTID_END</remarks>
        UIAEventIDEnd = 0x4EFF,
        /// <remarks>EVENT_UIA_PROPID_START</remarks>
        UIAPropertyIDStart = 0x7500,
        /// <remarks>EVENT_UIA_PROPID_END</remarks>
        UIAPropertyIDEnd = 0x75FF,
    }
}
