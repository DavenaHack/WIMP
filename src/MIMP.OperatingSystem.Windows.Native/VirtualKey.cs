using System;

namespace MIMP.OperatingSystem.Windows.Native
{
    public enum VirtualKey : byte
    {
        None = 0x00,

        LeftButton = 0x01, // VK_LBUTTON
        RightButton = 0x02, // VK_RBUTTON
        Cancel = 0x03, // VK_CANCEL
        MiddleButton = 0x04, // VK_MBUTTON
        XButton1 = 0x05, // VK_XBUTTON1
        XButton2 = 0x06, // VK_XBUTTON2

        Back = 0x08, // VK_BACK
        Tab = 0x09, // VK_TAB

        Clear = 0x0C, // VK_CLEAR
        Return = 0x0D, // VK_RETURN

        Shift = 0x10, // VK_SHIFT
        LeftShift = 0xA0, // VK_LSHIFT
        RightShift = 0xA1, // VK_RSHIFT

        Control = 0x11, // VK_CONTROL
        LeftControl = 0xA2, // VK_LCONTROL
        RightControl = 0xA3, // VK_RCONTROL

        Alt = 0x12, // VK_MENU
        LeftAlt = 0xA4, // VK_LMENU
        RightAlt = 0xA5, // VK_RMENU

        Pause = 0x13, // VK_PAUSE
        CapsLock = Capital,
        Capital = 0x14, // VK_CAPITAL

        Escape = 0x1B, // VK_ESCAPE

        Space = 0x20, // VK_SPACE
        PageUp = Prior,
        Prior = 0x21,// VK_PRIOR
        PageDown = Next,
        Next = 0x22, // VK_NEXT
        End = 0x23, // VK_END
        Pos1 = Home,
        Home = 0x24, // VK_HOME

        #region Arrows

        Left = 0x25, // VK_LEFT
        Up = 0x26, // VK_UP
        Right = 0x27, // VK_RIGHT
        Down = 0x28, // VK_DOWN

        #endregion

        Select = 0x29, // VK_SELECT
        Print = 0x2A, // VK_PRINT
        Execute = 0x2B, // VK_EXECUTE
        Snapshot = 0x2C, // VK_SNAPSHOT
        Insert = 0x2D, // VK_INSERT
        Delete = 0x2E, // VK_DELETE
        Help = 0x2F, // VK_HELP

        #region Digits

        D0 = 0x30,
        D1 = 0x31,
        D2 = 0x32,
        D3 = 0x33,
        D4 = 0x34,
        D5 = 0x35,
        D6 = 0x36,
        D7 = 0x37,
        D8 = 0x38,
        D9 = 0x39,

        #endregion

        #region Alphabet

        A = 0x41,
        B = 0x42,
        C = 0x43,
        D = 0x44,
        E = 0x45,
        F = 0x46,
        G = 0x47,
        H = 0x48,
        I = 0x49,
        J = 0x4A,
        K = 0x4B,
        L = 0x4C,
        M = 0x4D,
        N = 0x4E,
        O = 0x4F,
        P = 0x50,
        Q = 0x51,
        R = 0x52,
        S = 0x53,
        T = 0x54,
        U = 0x55,
        V = 0x56,
        W = 0x57,
        X = 0x58,
        Y = 0x59,
        Z = 0x5A,

        #endregion

        LeftWindows = 0x5B, // VK_LWIN
        RightWindows = 0x5C, // VK_RWIN

        Apps = 0x5D, // VK_APPS
        Sleep = 0x5F, // VK_SLEEP

        #region Numpad

        Numpad0 = 0x60, // VK_NUMPAD0
        Numpad1 = 0x61, // VK_NUMPAD1
        Numpad2 = 0x62, // VK_NUMPAD2
        Numpad3 = 0x63, // VK_NUMPAD3
        Numpad4 = 0x64, // VK_NUMPAD4
        Numpad5 = 0x65, // VK_NUMPAD5
        Numpad6 = 0x66, // VK_NUMPAD6
        Numpad7 = 0x67, // VK_NUMPAD7
        Numpad8 = 0x68, // VK_NUMPAD8
        Numpad9 = 0x69, // VK_NUMPAD9
        Multiply = 0x6A, // VK_MULTIPLY
        Add = 0x6B, // VK_ADD
        Seperator = 0x6C, // VK_SEPARATOR
        Subtract = 0x6D, // VK_SUBTRACT
        Decimal = 0x6E, // VK_DECIMAL
        Divide = 0x6F, // VK_DIVIDE

        NumLock = 0x90, // VK_NUMLOCK

        #endregion

        #region F

        F1 = 0x70, // VK_F1
        F2 = 0x71, // VK_F2
        F3 = 0x72, // VK_F3
        F4 = 0x73, // VK_F4
        F5 = 0x74, // VK_F5
        F6 = 0x75, // VK_F6
        F7 = 0x76, // VK_F7
        F8 = 0x77, // VK_F8
        F9 = 0x78, // VK_F9
        F10 = 0x79, // VK_F10
        F11 = 0x7A, // VK_F11
        F12 = 0x7B, // VK_F12
        F13 = 0x7C, // VK_F13
        F14 = 0x7D, // VK_F14
        F15 = 0x7E, // VK_F15
        F16 = 0x7F, // VK_F16
        F17 = 0x80, // VK_F17
        F18 = 0x81, // VK_F18
        F19 = 0x82, // VK_F19
        F20 = 0x83, // VK_F20
        F21 = 0x84, // VK_F21
        F22 = 0x85, // VK_F22
        F23 = 0x86, // VK_F23
        F24 = 0x87, // VK_F24

        #endregion

        Scroll = 0x91, // VK_SCROLL

        #region IME

        Kana = 0x15, // VK_KANA
        [Obsolete("Old name. Use " + nameof(Kana))]
        Hangeul = 0x15, // VK_HANGEUL
        Hangul = 0x15, // VK_HANGUL
        ImeOn = 0x16, // VK_IME_ON
        Junja = 0x17, // VK_JUNJA
        Final = 0x18, // VK_FINAL
        Hanja = 0x19, // VK_HANJA
        Kanji = 0x19, // VK_KANJI
        ImeOff = 0x1A, // VK_IME_OFF
        Convert = 0x1C, // VK_CONVERT
        NonConvert = 0x1D, // VK_NONCONVERT
        Accept = 0x1E, // VK_ACCEPT
        ModeChange = 0x1F, // VK_MODECHANGE
        ProcessKey = 0xE5, // VK_PROCESSKEY

        #endregion

        #region OEM


        /// <summary>
        /// '=' key on numpad
        /// </summary>
        OemNecEqual = 0x92, // VK_OEM_NEC_EQUAL

        //
        /// <summary>
        /// 'Dictionary' key
        /// </summary>
        OemFjJisho = 0x92, // VK_OEM_FJ_JISHO

        /// <summary>
        /// 'Unregister word' key
        /// </summary>
        OemFjMasshou = 0x93, // VK_OEM_FJ_MASSHOU

        /// <summary>
        /// // 'Register word' key
        /// </summary>
        OemFjTouroku = 0x94, // VK_OEM_FJ_TOUROKU

        /// <summary>
        /// 'Left OYAYUBI' key
        /// </summary>
        OemFjLoya = 0x95, // VK_OEM_FJ_LOYA

        /// <summary>
        /// 'Right OYAYUBI' key
        /// </summary>
        OemFjRoya = 0x96, // VK_OEM_FJ_ROYA

        /// <summary>
        /// ';:' for US
        /// </summary>
        Oem1 = 0xBA, // VK_OEM_1
        /// <summary>
        /// '+' any country
        /// </summary>
        OemPlus = 0xBB, // VK_OEM_PLUS
        /// <summary>
        ///  ',' any country
        /// </summary>
        OemComma = 0xBC,  // VK_OEM_COMMA
        /// <summary>
        /// '-' any country
        /// </summary>
        OemMinus = 0xBD, // VK_OEM_MINUS
        /// <summary>
        /// '.' any country
        /// </summary>
        OemPeriod = 0xBE, // VK_OEM_PERIOD
        /// <summary>
        /// '/?' for US
        /// </summary>
        Oem2 = 0xBF, // VK_OEM_2
        OemQuestion = Oem2,
        /// <summary>
        /// '`~' for US
        /// </summary>
        Oem3 = 0xC0, // VK_OEM_3

        /// <summary>
        /// '[{' for US
        /// </summary>
        Oem4 = 0xDB, // VK_OEM_4
        /// <summary>
        /// '\|' for US
        /// </summary>
        Oem5 = 0xDC, // VK_OEM_5
        /// <summary>
        /// ']}' for US
        /// </summary>
        Oem6 = 0xDD, // VK_OEM_6
        /// <summary>
        /// ''"' for US
        /// </summary>
        Oem7 = 0xDE,  // VK_OEM_7
        OemQuotes = Oem7,

        Oem8 = 0xDF, // VK_OEM_8

        /// <summary>
        /// 'AX' key on Japanese AX kbd
        /// </summary>
        OemAX = 0xE1, // VK_OEM_AX

        /// <summary>
        /// "<>" or "\|" on RT 102-key kbd.
        /// </summary>
        Oem102 = 0xE2,// VK_OEM_102
        OemBackspace = Oem102,

        OemReset = 0xE9, // VK_OEM_RESET
        OemJump = 0xEA, // VK_OEM_JUMP
        OemPa1 = 0xEB, // VK_OEM_PA1
        OemPa2 = 0xEC, // VK_OEM_PA2
        OemPa3 = 0xED, // VK_OEM_PA3
        OemWSCTRL = 0xEE, // VK_OEM_WSCTRL
        OemCUSEL = 0xEF, // VK_OEM_CUSEL
        OemATTN = 0xF0, // VK_OEM_ATTN
        OemFinish = 0xF1, // VK_OEM_FINISH
        OemCopy = 0xF2, // VK_OEM_COPY
        OemAuto = 0xF3, // VK_OEM_AUTO
        OemENLW = 0xF4, // VK_OEM_ENLW
        OemBackTab = 0xF5, // VK_OEM_BACKTAB

        OemClear = 0xFE, // VK_OEM_CLEAR


        #endregion

        #region Browser

        BrowserBack = 0xA6, // VK_BROWSER_BACK
        BrowserForward = 0xA7, // VK_BROWSER_FORWARD
        BrowserRefresh = 0xA8, // VK_BROWSER_REFRESH
        BrowserStop = 0xA9, // VK_BROWSER_STOP
        BrowserSearch = 0xAA, // VK_BROWSER_SEARCH
        BrowserFavorites = 0xAB, // VK_BROWSER_FAVORITES
        BrowserHome = 0xAC, // VK_BROWSER_HOME

        #endregion

        #region Sound

        VolumeMute = 0xAD, // VK_VOLUME_MUTE
        VolumeDown = 0xAE, // VK_VOLUME_DOWN
        VolumeUp = 0xAF, // VK_VOLUME_UP
        MediaNextTrack = 0xB0, // VK_MEDIA_NEXT_TRACK
        MediaPreviousTrack = 0xB1, // VK_MEDIA_PREV_TRACK
        MediaStop = 0xB2, // VK_MEDIA_STOP
        MediaPlayPause = 0xB3, // VK_MEDIA_PLAY_PAUSE

        #endregion

        #region Apps

        LaunchMail = 0xB4, // VK_LAUNCH_MAIL
        LaunchMediaSelect = 0xB5, // VK_LAUNCH_MEDIA_SELECT
        LaunchApp1 = 0xB6, // VK_LAUNCH_APP1
        LaunchApp2 = 0xB7, // VK_LAUNCH_APP2

        #endregion

        /// <summary>
        /// Help key on ICO
        /// </summary>
        IcoHelp = 0xE3, // VK_ICO_HELP
        /// <summary>
        /// 00 key on ICO
        /// </summary>
        Ico00 = 0xE4, // VK_ICO_00


        IcoClear = 0xE6, // VK_ICO_CLEARrio

        Packet = 0xE7, // VK_PACKET

        Attn = 0xF6, // VK_ATTN
        CrSel = 0xF7, // VK_CRSEL
        ExSel = 0xF8, // VK_EXSEL
        EraseEOF = 0xF9, // VK_EREOF
        Play = 0xFA, // VK_PLAY
        Zoom = 0xFB, // VK_ZOOM
        NoName = 0xFC, // VK_NONAME
        Pa1 = 0xFD, // VK_PA1
    }
}
