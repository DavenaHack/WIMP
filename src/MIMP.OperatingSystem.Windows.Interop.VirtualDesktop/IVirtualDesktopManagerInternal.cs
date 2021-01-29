using System;
using System.Runtime.InteropServices;

namespace MIMP.OperatingSystem.Windows.Interop.VirtualDesktop
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("F31574D6-B682-4CDC-BD56-1827860ABEC6")]
    public interface IVirtualDesktopManagerInternal
    {
        int GetCount();

        void MoveViewToDesktop(IApplicationView pView, IVirtualDesktop desktop);

        bool CanViewMoveDesktops(IApplicationView pView);

        IVirtualDesktop GetCurrentDesktop();

        IObjectArray GetDesktops();

        IVirtualDesktop GetAdjacentDesktop(IVirtualDesktop pDesktopReference, AdjacentDesktop uDirection);

        void SwitchDesktop(IVirtualDesktop desktop);

        IVirtualDesktop CreateDesktopW();

        void RemoveDesktop(IVirtualDesktop pRemove, IVirtualDesktop pFallbackDesktop);

        IVirtualDesktop FindDesktop(ref Guid desktopId);
    }
}
