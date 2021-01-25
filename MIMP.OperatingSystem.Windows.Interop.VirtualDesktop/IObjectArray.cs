using System;
using System.Runtime.InteropServices;

namespace MIMP.OperatingSystem.Windows.Interop.VirtualDesktop
{

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("92CA9DCD-5622-4BBA-A805-5E9F541BD8C9")]
    public interface IObjectArray
    {

        void GetCount(out int count);

        void GetAt(int index, ref Guid iid, [MarshalAs(UnmanagedType.Interface)] out object obj);

    }
}
