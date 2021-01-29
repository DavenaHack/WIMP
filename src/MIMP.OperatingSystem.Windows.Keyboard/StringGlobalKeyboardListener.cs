using MIMP.OperatingSystem.Windows.Native;
using System;
using System.Text;

namespace MIMP.OperatingSystem.Windows.Keyboard
{
    public class StringGlobalKeyboardListener : IDisposable // TODO paste and copy
    {

        private readonly GlobalKeyboardHandler _listener;


        public event EventHandler<StringKeyboardEventArgs> Typed;


        public StringGlobalKeyboardListener(GlobalKeyboardHandler listener = null)
        {
            _listener = listener ?? new GlobalKeyboardHandler();
            _listener.RawKeyDown += (s, e) => RaiseTyped(e.VirtualKey, true);
            _listener.RawKeyUp += (s, e) => RaiseTyped(e.VirtualKey, false);
        }


        protected void RaiseTyped(VirtualKey virtualKey, bool keyDown)
        {
            var value = VirtualKey2String((uint)virtualKey, keyDown);
            if (!string.IsNullOrEmpty(value))
                Typed?.Invoke(this, new StringKeyboardEventArgs(value));
        }

        #region Convert VKCode to string
        // Note: Sometimes single VKCode represents multiple chars, thus string. 
        // E.g. typing "^1" (notice that when pressing 1 the both characters appear, 
        // because of this behavior, "^" is called dead key)

        private uint _lastVKCode = 0;
        private uint _lastScanCode = 0;
        private byte[] _lastKeyState = new byte[255];
        private bool _lastIsDead = false;


        /// <summary>
        /// Convert VKCode to Unicode.
        /// <remarks>isKeyDown is required for because of keyboard state inconsistencies!</remarks>
        /// </summary>
        /// <param name="VKCode">VKCode</param>
        /// <param name="isKeyDown">Is the key down event?</param>
        /// <returns>String representing single unicode character.</returns>
        protected string VirtualKey2String(uint VKCode, bool isKeyDown)
        {
            // ToUnicodeEx needs StringBuilder, it populates that during execution.
            var sbString = new StringBuilder(5);

            byte[] bKeyState = new byte[255];
            bool bKeyStateStatus;
            bool isDead = false;

            // Gets the current windows window handle, threadID, processID
            IntPtr currentHWnd = User32.GetForegroundWindow();
            uint currentWindowThreadID = User32.GetWindowThreadProcessId(currentHWnd, out uint currentProcessID);

            // This programs Thread ID
            uint thisProgramThreadId = Kernel32.GetCurrentThreadId();

            // Attach to active thread so we can get that keyboard state
            if (User32.AttachThreadInput(thisProgramThreadId, currentWindowThreadID, true))
            {
                // Current state of the modifiers in keyboard
                bKeyStateStatus = User32.GetKeyboardState(bKeyState);

                // Detach
                User32.AttachThreadInput(thisProgramThreadId, currentWindowThreadID, false);
            }
            else
            {
                // Could not attach, perhaps it is this process?
                bKeyStateStatus = User32.GetKeyboardState(bKeyState);
            }

            // On failure we return empty string.
            if (!bKeyStateStatus)
                return "";

            // Gets the layout of keyboard
            IntPtr HKL = User32.GetKeyboardLayout(currentWindowThreadID);

            // Maps the virtual keycode
            uint lScanCode = User32.MapVirtualKeyEx(VKCode, (uint)MapVirtualKeyMapTypes.VirtualKey2ScanCode, HKL);

            // Keyboard state goes inconsistent if this is not in place. In other words, we need to call above commands in UP events also.
            if (!isKeyDown)
                return "";

            // Converts the VKCode to unicode
            int relevantKeyCountInBuffer = User32.ToUnicodeEx(VKCode, lScanCode, bKeyState, sbString, sbString.Capacity, (uint)0, HKL);

            var result = "";

            switch (relevantKeyCountInBuffer)
            {
                // Dead keys (^,`...)
                case -1:
                    isDead = true;

                    // We must clear the buffer because ToUnicodeEx messed it up, see below.
                    ClearKeyboardBuffer(VKCode, lScanCode, HKL);
                    break;

                case 0:
                    break;

                // Single character in buffer
                case 1:
                    result = sbString[0].ToString();
                    break;

                // Two or more (only two of them is relevant)
                case 2:
                default:
                    result = sbString.ToString().Substring(0, 2);
                    break;
            }

            // We inject the last dead key back, since ToUnicodeEx removed it.
            // More about this peculiar behavior see e.g: 
            //   http://www.experts-exchange.com/Programming/System/Windows__Programming/Q_23453780.html
            //   http://blogs.msdn.com/michkap/archive/2005/01/19/355870.aspx
            //   http://blogs.msdn.com/michkap/archive/2007/10/27/5717859.aspx
            if (_lastVKCode != 0 && _lastIsDead)
            {
                var sbTemp = new StringBuilder(5);
                User32.ToUnicodeEx(_lastVKCode, _lastScanCode, _lastKeyState, sbTemp, sbTemp.Capacity, 0, HKL);
                _lastVKCode = 0;

                return result;
            }

            // Save these
            _lastScanCode = lScanCode;
            _lastVKCode = VKCode;
            _lastIsDead = isDead;
            _lastKeyState = (byte[])bKeyState.Clone();

            return result;
        }

        private void ClearKeyboardBuffer(uint vk, uint sc, IntPtr hkl)
        {
            var sb = new StringBuilder(10);
            int rc;
            do
            {
                byte[] lpKeyStateNull = new byte[255];
                rc = User32.ToUnicodeEx(vk, sc, lpKeyStateNull, sb, sb.Capacity, 0, hkl);
            } while (rc < 0);
        }

        #endregion Convert VKCode to string


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _listener.Dispose();
        }

    }
}
