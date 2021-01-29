using MIMP.OperatingSystem.Windows.Screen;
using MIMP.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MIMP.OperatingSystem.Windows.Window
{
    public class Screen : IScreen
    {

        private static IEnumerable<string> _ChangingProperties = new[] {
            nameof(IScreen.X),
            nameof(IScreen.Y),
            nameof(IScreen.Width),
            nameof(IScreen.Height),
            nameof(IScreen.Top),
            nameof(IScreen.Right),
            nameof(IScreen.Bottom),
            nameof(IScreen.Left),
            nameof(IScreen.WorkAreaX),
            nameof(IScreen.WorkAreaY),
            nameof(IScreen.WorkAreaWidth),
            nameof(IScreen.WorkAreaHeight),
            nameof(IScreen.WorkAreaTop),
            nameof(IScreen.WorkAreaRight),
            nameof(IScreen.WorkAreaBottom),
            nameof(IScreen.WorkAreaLeft),
        };


        private static string GetID(string name) =>
            new Regex(@"\\\\\.\\DISPLAY(\d+)").Match(name).Groups[1].Value;



        public event EventHandler Disconnect;

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;


        public string ID => GetID(Info.Name);

        public int X => Info.Size.X;

        public int Y => Info.Size.Y;

        public int Width => Info.Size.Width;

        public int Height => Info.Size.Height;


        public int WorkAreaX => Info.Area.X;

        public int WorkAreaY => Info.Area.Y;

        public int WorkAreaWidth => Info.Area.Width;

        public int WorkAreaHeight => Info.Area.Height;


        public IntPtr Handle { get; }


        private ScreenInfo _info;
        protected ScreenInfo Info => _info;


        private bool _disposedValue;


        public Screen(IntPtr handle)
        {
            Handle = handle;
            _info = ScreenManager.Info(Handle);
            Disconnect += (s, e) => Dispose();
            Microsoft.Win32.SystemEvents.DisplaySettingsChanging += DisplaySettingsChanging;
            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += DisplaySettingsChanged;
        }

        ~Screen()
        {
            Dispose(false);
        }


        private void DisplaySettingsChanging(object sender, EventArgs e)
        {
            var i = ScreenManager.Info(Handle);
            if (i.IsEmpty())
            {
                Disconnect?.Invoke(this, EventArgs.Empty);
                return;
            }
            foreach (var p in _ChangingProperties)
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(p));
        }

        private void DisplaySettingsChanged(object sender, EventArgs e)
        {
            _info = ScreenManager.Info(Handle);
            foreach (var p in _ChangingProperties)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }


        public override bool Equals(object obj) =>
            ((IScreen)this).Equals(obj as IScreen);

        public override int GetHashCode() =>
            HashCode.Combine(ID);


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Microsoft.Win32.SystemEvents.DisplaySettingsChanging -= DisplaySettingsChanging;
                    Microsoft.Win32.SystemEvents.DisplaySettingsChanged -= DisplaySettingsChanged;
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
