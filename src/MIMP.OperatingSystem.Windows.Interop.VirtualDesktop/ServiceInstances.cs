using System;

namespace MIMP.OperatingSystem.Windows.Interop.VirtualDesktop
{
    public static class ServiceInstances
    {

        private static readonly Guid VirtualDesktopManagerInternalGUID = new Guid("C5E0CDCA-7B6E-41B2-9FC4-D93975CC467B");
        public static IVirtualDesktopManagerInternal VirtualDesktopManagerInternal => InteropManager.COMService<IVirtualDesktopManagerInternal>(VirtualDesktopManagerInternalGUID);


        private static readonly Guid VirtualDesktopManagerGUID = new Guid("AA509086-5CA9-4C25-8F95-589D3C07B48A");
        public static IVirtualDesktopManager VirtualDesktopManager { get; } = (IVirtualDesktopManager)Activator.CreateInstance(Type.GetTypeFromCLSID(VirtualDesktopManagerGUID));


        public static IApplicationViewCollection ApplicationViewCollection => InteropManager.COMService<IApplicationViewCollection>();


        private static readonly Guid VirtualDesktopPinnedAppsGUID = new Guid("B5A399E7-1C87-46B8-88E9-FC5747B171BD");
        public static IVirtualDesktopPinnedApps VirtualDesktopPinnedApps => InteropManager.COMService<IVirtualDesktopPinnedApps>(VirtualDesktopPinnedAppsGUID);

    }
}
