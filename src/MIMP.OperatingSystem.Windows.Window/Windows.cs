using MIMP.Window;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MIMP.OperatingSystem.Windows.Window
{
    public class Windows : IWindows
    {

        private static volatile IWindows _Instance;
        private static readonly object _Lock = new object();

        public static IWindows Instance
        {
            get
            {
                if (_Instance == null)
                    lock (_Lock)
                        if (_Instance == null)
                            _Instance = new Windows();
                return _Instance;
            }
        }


        public event EventHandler<WindowEventArgs> Created
        {
            add { throw new NotSupportedException(); }
            remove { throw new NotSupportedException(); }
        }

        public event EventHandler<WindowEventArgs> Closed
        {
            add { throw new NotSupportedException(); }
            remove { throw new NotSupportedException(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public event NotifyCollectionChangedEventHandler CollectionChanged;


        private IDictionary<IntPtr, IWindow> _windows =>
            WindowManager.AppsForward().Select(x => (IWindow)new Window(x))
                .ToDictionary(x => (x as Window).Handle);


        public int Count => _windows.Count;


        private bool _disposedValue;


        protected Windows()
        {
        }


        ~Windows()
        {
            Dispose(false);
        }


        public IEnumerator<IWindow> GetEnumerator()
        {
            return _windows.Values.GetEnumerator();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync() =>
            await Task.Run(() => Dispose());

    }
}
