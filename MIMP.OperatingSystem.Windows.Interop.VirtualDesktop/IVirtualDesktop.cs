using System;
using System.Runtime.InteropServices;

namespace MIMP.OperatingSystem.Windows.Interop.VirtualDesktop
{

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("FF72FFDD-BE7E-43FC-9C03-AD81681E88E4")]
    public interface IVirtualDesktop
    {
        bool IsViewVisible(object pView);

        Guid GetID();
    }
}
