namespace MIMP.OperatingSystem.Windows.Native
{
    public enum HookType : int
    {
        JournalRecord = 0, // WH_JOURNALRECORD
        JournalPlayback = 1, //WH_JOURNALPLAYBACK
        Keyboard = 2, // WH_KEYBOARD
        GetMessage = 3, // WH_GETMESSAGE
        CallWindowProcedure = 4, // WH_CALLWNDPROC
        ComputerBasedTraining = 5, // WH_CBT
        SystemMessageFilter = 6, // WH_SYSMSGFILTER
        Mouse = 7, // WH_MOUSE
        Hardware = 8, // WH_HARDWARE
        Debug = 9, // WH_DEBUG
        Shell = 10, // WH_SHELL
        ForegroundIdle = 11, // WH_FOREGROUNDIDLE
        CallWindowProcedureReturn = 12, // WH_CALLWNDPROCRET
        KeyboardGlobal = 13, // WH_KEYBOARD_LL
        MouseGlobal = 14 // WH_MOUSE_LL
    }
}
