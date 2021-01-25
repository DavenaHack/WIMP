using MIMP.OperatingSystem.Windows.Native;
using MIMP.OperatingSystem.Windows.Screen;
using MIMP.Window;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MIMP.OperatingSystem.Windows.Window
{
    public class Screens : IScreens
    {

        private static volatile Screens _Instance;
        private static readonly object _Lock = new object();

        public static Screens Instance
        {
            get
            {
                if (_Instance == null)
                    lock (_Lock)
                        if (_Instance == null)
                            _Instance = new Screens();
                return _Instance;
            }
        }


        public event EventHandler<ScreenEventArgs> Connect;

        public event EventHandler<ScreenEventArgs> Disconnect;

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public event NotifyCollectionChangedEventHandler CollectionChanged;


        public int Count => _screens.Count;


        private IDictionary<IntPtr, IScreen> _screens;


        private bool _disposedValue;


        protected Screens()
        {
            _screens = new Dictionary<IntPtr, IScreen>();
            foreach (var s in ScreenManager.Screens().Select(s => new Screen(s)))
                AddScreen(s);
            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += DisplaySettingsChanged;
        }

        ~Screens()
        {
            Dispose(false);
        }


        public IScreen this[IWindow key] =>
            _screens[ScreenManager.WindowScreen((key as Window).Handle, MonitorFlag.DefaultToNearest)];


        public IEnumerator<IScreen> GetEnumerator() =>
            _screens.Values.GetEnumerator();


        private void AddScreen(Screen screen)
        {
            if (!_screens.ContainsKey(screen.Handle))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(Count)));
                screen.Disconnect += Screen_Disconnect;
                _screens[screen.Handle] = screen;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, screen));
                Connect?.Invoke(this, new ScreenEventArgs(screen));
            }
        }

        private void Screen_Disconnect(object sender, EventArgs e)
        {
            if (sender is not Screen s)
                return;
            RemoveScreen(s);
        }

        private void RemoveScreen(Screen screen)
        {
            if (_screens.ContainsKey(screen.Handle))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(Count)));
                _screens.Remove(screen.Handle);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, screen));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
                Disconnect?.Invoke(this, new ScreenEventArgs(screen));
            }
        }


        private void DisplaySettingsChanged(object sender, EventArgs e)
        {
            foreach (var s in ScreenManager.Screens())
                if (!_screens.ContainsKey(s))
                    AddScreen(new Screen(s));
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync() =>
            await Task.Run(() => Dispose());

    }
}
