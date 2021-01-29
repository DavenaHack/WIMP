using Microsoft.Win32;
using System;
using WinRegistry = Microsoft.Win32.Registry;

namespace MIMP.OperatingSystem.Windows.Registry
{
    public static class RegistryManager
    {


        #region Key

        public static (string, string) NextKey(string key)
        {
            if (key == null)
                return (null, null);
            var i = key.IndexOf('\\');
            if (i < 0)
                return (key, null);
            return (key.Substring(0, i), key.Substring(i + 1));
        }

        public static (RegistryKey, string) Root(string key)
        {
            var (p, k) = NextKey(key);
            return p.ToUpper() switch
            {
                "HKEY_CLASSES_ROOT" => (WinRegistry.ClassesRoot, k),
                "HKEY_CURRENT_USER" => (WinRegistry.CurrentUser, k),
                "HKEY_LOCAL_MACHINE" => (WinRegistry.LocalMachine, k),
                "HKEY_USERS" => (WinRegistry.Users, k),
                "HKEY_PERFORMANCE_DATA" => (WinRegistry.PerformanceData, k),
                "HKEY_CURRENT_CONFIG" => (WinRegistry.CurrentConfig, k),
                _ => throw new NotSupportedException("Unknown root registry key: " + p),
            };
        }

        public static RegistryKey Root(RegistryHive hive) =>
            hive switch
            {
                RegistryHive.ClassesRoot => WinRegistry.ClassesRoot,
                RegistryHive.CurrentUser => WinRegistry.CurrentUser,
                RegistryHive.LocalMachine => WinRegistry.LocalMachine,
                RegistryHive.Users => WinRegistry.Users,
                RegistryHive.PerformanceData => WinRegistry.PerformanceData,
                RegistryHive.CurrentConfig => WinRegistry.CurrentConfig,
                _ => throw new NotImplementedException(),
            };


        #endregion Key


        #region RegistryKey

        public static RegistryKey GetRegistryKey(string key)
        {
            var (r, k) = Root(key);
            return GetRegistryKey(r, k);
        }

        public static RegistryKey GetRegistryKey(RegistryHive hive, string key)
        {
            return GetRegistryKey(Root(hive), key);
        }

        public static RegistryKey GetRegistryKey(RegistryKey parent, string key)
        {
            if (parent == null)
                return null;
            string n;
            (n, key) = NextKey(key);
            var r = parent;
            while (n != null)
            {
                var p = r.OpenSubKey(n, true);
                if (p == null)
                {
                    var nu = n.ToUpper();
                    foreach (var k in r.GetSubKeyNames())
                        if (k.ToUpper() == nu)
                        {
                            p = r.OpenSubKey(k, true);
                            break;
                        }
                    if (p == null)
                        return null;
                }
                if (r != parent)
                    r.Dispose();
                r = p;
                (n, key) = NextKey(key);
            }
            return r;
        }


        public static RegistryKey GetOrAddRegistryKey(string key)
        {
            var (r, k) = Root(key);
            return GetOrAddRegistryKey(r, k);
        }

        public static RegistryKey GetOrAddRegistryKey(RegistryHive hive, string key)
        {
            return GetOrAddRegistryKey(Root(hive), key);
        }

        public static RegistryKey GetOrAddRegistryKey(RegistryKey parent, string key)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
            string n;
            (n, key) = NextKey(key);
            var r = parent;
            while (n != null)
            {
                var p = r.OpenSubKey(n, true);
                if (p == null)
                {
                    var nu = n.ToUpper();
                    foreach (var k in r.GetSubKeyNames())
                        if (k.ToUpper() == nu)
                        {
                            p = r.OpenSubKey(k, true);
                            break;
                        }
                    if (p == null)
                        p = r.CreateSubKey(n, true);
                }
                if (r != parent)
                    r.Dispose();
                r = p;
                (n, key) = NextKey(key);
            }
            return r;
        }

        public static bool Delete(string key)
        {
            var (r, k) = Root(key);
            return Delete(r, k);
        }

        public static bool Delete(RegistryHive hive, string key)
        {
            return Delete(Root(hive), key);
        }


        public static bool Delete(RegistryKey parent, string key)
        {
            //string n;
            //(n, key) = NextKey(key);
            //while (key != null)
            //{
            //    var p = parent.OpenSubKey(n);
            //    if (p == null)
            //    {
            //        var nu = n.ToUpper();
            //        foreach (var k in parent.GetSubKeyNames())
            //            if (k.ToUpper() == nu)
            //            {
            //                p = parent.OpenSubKey(k);
            //                break;
            //            }
            //        if (p == null)
            //            return false;
            //    }
            //    parent = p;
            //    (n, key) = NextKey(key);
            //}
            try
            {
                parent.DeleteSubKeyTree(key, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion RegistryKey


        #region Value


        public static string String(string key, string name, string @default = default)
        {
            return (string)GetRegistryKey(key)?.GetValue(name, @default) ?? @default;
        }

        public static void SetString(string key, string name, string value)
        {
            Set(key, name, value, r => r.SetValue(name, value, RegistryValueKind.String));
        }


        private static void Set<T>(RegistryHive hive, string key, string name, T value, Action<RegistryKey> set)
        {
            if (value == null)
            {
                using var k = GetRegistryKey(hive, key);
                if (k != null)
                    k.DeleteValue(name);
            }
            else
            {
                using var k = GetOrAddRegistryKey(hive, key);
                set(k);
            }
        }

        private static void Set<T>(string key, string name, T value, Action<RegistryKey> set)
        {
            if (value == null)
            {
                using var k = GetRegistryKey(key);
                if (k != null)
                    k.DeleteValue(name);
            }
            else
            {
                using var k = GetOrAddRegistryKey(key);
                set(k);
            }
        }


        #endregion Value

    }
}