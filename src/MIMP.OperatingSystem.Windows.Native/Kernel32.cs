using System;
using System.Runtime.InteropServices;

namespace MIMP.OperatingSystem.Windows.Native
{
    public static class Kernel32
    {

        #region Thread


        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();


        #endregion


        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);


        #region Atom


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern ushort GlobalAddAtom(string lpString);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern ushort GlobalDeleteAtom(ushort nAtom);

        #endregion

    }
}
