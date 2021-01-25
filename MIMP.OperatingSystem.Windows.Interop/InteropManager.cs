using System;

namespace MIMP.OperatingSystem.Windows.Interop
{
    public static class InteropManager
    {

        private static volatile IServiceProvider10 _ServiceProvider;

        static InteropManager()
        {
            _ServiceProvider = (IServiceProvider10)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("C2F03A33-21F5-47FA-B4BB-156362A2F239")));
        }


        public static object COMService(Guid service, Guid riid)
        {
            return _ServiceProvider.QueryService(service, riid);
        }

        public static object COMService(Guid service, Type type)
        {
            return COMService(service, type.GUID);
        }

        public static T COMService<T>(Guid service)
        {
            return (T)COMService(service, typeof(T));
        }


        public static object COMService(Type type)
        {
            return COMService(type.GUID, type);
        }

        public static T COMService<T>()
        {
            return (T)COMService(typeof(T));
        }

    }
}
