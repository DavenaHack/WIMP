using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MIMP.SeeSharper.Events
{
    public static class Events
    {

        public static Delegate GetRaiseDelegate(object sender, string name, BindingFlags flags)
        {
            var t = sender.GetType();
            var e = t.GetEvent(name, flags);
            if (e is not null)
                return (Delegate)t.GetField(name, flags).GetValue(sender);
            throw new ArgumentException($"Unknown event {name} of type {t}", nameof(name));
        }

        public static Delegate GetRaiseDelegate(object sender, string name) =>
            GetRaiseDelegate(sender, name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);


        public static void InvokeEvent(object sender, string name, object[] parameters, BindingFlags flags, Func<object, string, object[], bool> cancel = null)
        {
            var e = GetRaiseDelegate(sender, name, flags);
            if (e is null)
                return;
            if (cancel == null)
                e?.Method.Invoke(e.Target, parameters);
            else
                foreach (var d in e.GetInvocationList())
                {
                    d.Method.Invoke(d.Target, parameters);
                    if (cancel(sender, name, parameters))
                        break;
                }
        }

        public static void InvokeEvent(object sender, string name, object[] paramters, Func<object, string, object[], bool> cancel = null) =>
            InvokeEvent(sender, name, paramters, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, cancel);

        public static void InvokeEvent<T, E>(T sender, string name, E eventArgs, Func<T, string, E, bool> cancel = null) =>
            InvokeEvent((object)sender, name, new object[] { sender, eventArgs }, cancel == null ? null : (_, n, p) => cancel((T)p[0], n, (E)p[1]));

        public static void InvokeEvent<T>(T sender, string name, Func<T, string, bool> cancel = null) =>
            InvokeEvent(sender, name, EventArgs.Empty, cancel == null ? null : (s, n, _) => cancel(s, n));


        public static void InvokeEventReverse(object sender, string name, object[] parameters, BindingFlags flags, Func<object, string, object[], bool> cancel = null)
        {
            var e = GetRaiseDelegate(sender, name, flags);
            if (e is null)
                return;
            var l = e.GetInvocationList();
            for (var i = l.Length; i-- > 0;)
            {
                var d = l[i];
                d.Method.Invoke(d.Target, parameters);
                if (cancel is not null && cancel(sender, name, parameters))
                    break;
            }
        }

        public static void InvokeEventReverse(object sender, string name, object[] paramters, Func<object, string, object[], bool> cancel = null) =>
            InvokeEventReverse(sender, name, paramters, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, cancel);

        public static void InvokeEventReverse<T, E>(T sender, string name, E eventArgs, Func<T, string, E, bool> cancel = null) =>
            InvokeEventReverse((object)sender, name, new object[] { sender, eventArgs }, cancel == null ? null : (_, n, p) => cancel((T)p[0], n, (E)p[1]));

        public static void InvokeEventReverse<T>(T sender, string name, Func<T, string, bool> cancel = null) =>
            InvokeEventReverse(sender, name, EventArgs.Empty, cancel == null ? null : (s, n, _) => cancel(s, n));


        public static void InvokeEventParallel(object sender, string name, object[] parameters, BindingFlags flags)
        {
            var e = GetRaiseDelegate(sender, name, flags);
            if (e is null)
                return;
            foreach (var d in e.GetInvocationList().AsParallel())
                d.Method.Invoke(d.Target, parameters);
        }

        public static void InvokeEventParallel(object sender, string name, object[] paramters) =>
            InvokeEventParallel(sender, name, paramters, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        public static void InvokeEventParallel<T, E>(T sender, string name, E eventArgs) =>
            InvokeEventParallel((object)sender, name, new object[] { sender, eventArgs });

        public static void InvokeEventParallel<T>(T sender, string name) =>
            InvokeEventParallel(sender, name, EventArgs.Empty);


        public static IAsyncResult InvokeEventAsync(object sender, string name, object[] parameters, BindingFlags flags, Func<object, string, object[], bool> cancel = null) =>
            Task.Run(() => InvokeEvent(sender, name, parameters, flags, cancel));

        public static IAsyncResult InvokeEventAsync(object sender, string name, object[] paramters, Func<object, string, object[], bool> cancel = null) =>
            InvokeEventAsync(sender, name, paramters, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, cancel);

        public static IAsyncResult InvokeEventAsync<T, E>(T sender, string name, E eventArgs, Func<T, string, E, bool> cancel = null) =>
            InvokeEventAsync((object)sender, name, new object[] { sender, eventArgs }, cancel == null ? null : (_, n, p) => cancel((T)p[0], n, (E)p[1]));

        public static IAsyncResult InvokeEventAsync<T>(T sender, string name, Func<T, string, bool> cancel = null) =>
            InvokeEventAsync(sender, name, EventArgs.Empty, cancel == null ? null : (s, n, _) => cancel(s, n));


        public static IAsyncResult InvokeEventReverseAsync(object sender, string name, object[] parameters, BindingFlags flags, Func<object, string, object[], bool> cancel = null) =>
            Task.Run(() => InvokeEventReverse(sender, name, parameters, flags, cancel));

        public static IAsyncResult InvokeEventReverseAsync(object sender, string name, object[] paramters, Func<object, string, object[], bool> cancel = null) =>
            InvokeEventReverseAsync(sender, name, paramters, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, cancel);

        public static IAsyncResult InvokeEventReverseAsync<T, E>(T sender, string name, E eventArgs, Func<T, string, E, bool> cancel = null) =>
            InvokeEventReverseAsync((object)sender, name, new object[] { sender, eventArgs }, cancel == null ? null : (_, n, p) => cancel((T)p[0], n, (E)p[1]));

        public static IAsyncResult InvokeEventReverseAsync<T>(T sender, string name, Func<T, string, bool> cancel = null) =>
            InvokeEventReverseAsync(sender, name, EventArgs.Empty, cancel == null ? null : (s, n, _) => cancel(s, n));

    }
}
