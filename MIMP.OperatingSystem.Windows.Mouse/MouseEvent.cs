using MIMP.OperatingSystem.Windows.Native;

namespace MIMP.OperatingSystem.Windows.Mouse
{
    public enum MouseEvent : int
    {
        MouseMove = (int)WindowsMessage.MouseMove,
        LeftButtonDown = (int)WindowsMessage.LeftButtonDown,
        LeftButtonUp = (int)WindowsMessage.LeftButtonUp,
        LeftButtonDoubleClick = (int)WindowsMessage.LeftButtonDoubleClick,
        RightButtonDown = (int)WindowsMessage.RightButtonDown,
        RightButtonUp = (int)WindowsMessage.RightButtonUp,
        RightButtonDoubleClick = (int)WindowsMessage.RightButtonDoubleClick,
        MiddleButtonDown = (int)WindowsMessage.MiddleButtonDown,
        MiddleButtonUp = (int)WindowsMessage.MiddleButtonUp,
        MiddleButtonDoubleClick = (int)WindowsMessage.MiddleButtonDoubleClick,
        XButtonDown = (int)WindowsMessage.XButtonDown,
        XButtonUp = (int)WindowsMessage.XButtonUp,
        XButtonDoubleClick = (int)WindowsMessage.XButtonDoubleClick,
        MouseWheel = (int)WindowsMessage.MouseWheel,
        MouseHorizontalWheel = (int)WindowsMessage.MouseHorizontalWheel
    }
}
