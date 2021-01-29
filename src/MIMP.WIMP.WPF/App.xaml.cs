using MIMP.Input;
using MIMP.Log;
using MIMP.OperatingSystem.Windows.Keyboard;
using MIMP.Window;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MIMP.WIMP.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ILogger Logger { get; private set; }

        public IGlobalKeyboardHandler KeyboardHandler { get; private set; }

        //public IGlobalMouseHandler MouseHandler { get; private set; }

        public GridHandler GridHandler { get; private set; }

        public IWIMPConfigurationService ConfigurationService { get; private set; }

        private WIMPConfiguration _configuration;
        public WIMPConfiguration Configuration
        {
            get => _configuration ?? (Configuration = new WIMPConfiguration
            {
                DefaultGrid = new Grid
                {
                    Rows = new ObservableCollection<GridLength> { new GridLength(1, GridUnit.Star), new GridLength(1, GridUnit.Star) },
                    Columns = new ObservableCollection<GridLength> { new GridLength(1, GridUnit.Star), new GridLength(1, GridUnit.Star) },
                    Cells = new ObservableCollection<GridCell>
                    {
                        new GridCell{ Row = 0, Column = 0 },
                        new GridCell{ Row = 0, Column = 1 },
                        new GridCell{ Row = 1, Column = 0 },
                        new GridCell{ Row = 1, Column = 1 },
                    }
                }
            });
            set
            {
                if (_configuration == value)
                    return;
                _configuration = value;
                RaisePropertyChanged();
            }
        }


        public IWindows Apps { get; private set; }

        public IScreens Screens { get; private set; }



        public App()
        {
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 1)
                Process.GetCurrentProcess().Kill();
            ConfigurationService = new WIMPConfigurationServiceFactory().Get();
            Configuration = ConfigurationService.Load();
            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(Configuration))
                    ConfigurationService.Save(Configuration);
            };
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Logger = new BufferLogger();
            Apps = OperatingSystem.Windows.Window.Windows.Instance;
            Screens = OperatingSystem.Windows.Window.Screens.Instance;
            InitHandlers();
            InitNotifyIcon();
        }


        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (Apps != null)
            {
                Apps?.Dispose();
                Apps = null;
            }
            DisposeNotifyIcon();
            DisposeHandlers();
        }


        #region Dispatch


        public void Dispatch(Action action)
        {
            if (Thread.CurrentThread.ManagedThreadId == Dispatcher.Thread.ManagedThreadId)
                action();
            else
                Dispatcher.Invoke(action);
        }

        public async Task DispatchAsync(Action action)
        {
            if (Thread.CurrentThread.ManagedThreadId == Dispatcher.Thread.ManagedThreadId)
                action();
            else
                await Dispatcher.BeginInvoke(action);
        }


        #endregion Dispath


        #region GlobalListener


        private void InitHandlers()
        {
            DisposeHandlers();
            //MouseHandler = new GlobalMouseHandler();
            KeyboardHandler = new GlobalKeyboardHandler();
            GridHandler = new GridHandler(this);
        }

        private void DisposeHandlers()
        {
            if (GridHandler != null)
            {
                GridHandler?.Dispose();
                GridHandler = null;
            }
            if (KeyboardHandler != null)
            {
                KeyboardHandler?.Dispose();
                KeyboardHandler = null;
            }
            //if (MouseHandler != null)
            //{
            //    MouseHandler?.Dispose();
            //    MouseHandler = null;
            //}
        }


        #endregion GlobalListener


        #region NotifyIcon


        private System.Windows.Forms.NotifyIcon _notifyIcon;

        private void InitNotifyIcon()
        {
            DisposeNotifyIcon();
            _notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Text = "WIMP"
            };
            var icon = (System.Windows.Controls.Image)FindResource("NotifyIcon");
            var source = icon.Source as BitmapSource;
            using (Bitmap bmp = new Bitmap(source.PixelWidth, source.PixelHeight, PixelFormat.Format32bppPArgb))
            {
                BitmapData data = bmp.LockBits(new Rectangle(System.Drawing.Point.Empty, bmp.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
                source.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
                bmp.UnlockBits(data);
                _notifyIcon.Icon = Icon.FromHandle(bmp.GetHicon());
            }
            _notifyIcon.DoubleClick += (s, e) =>
            {
                MainWindow.IsEnabled = true;
                MainWindow.Visibility = Visibility.Visible;
                MainWindow.Activate();
            };

            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            var configuration = _notifyIcon.ContextMenuStrip.Items.Add("Configuration");
            configuration.Click += (s, e) =>
            {
                MainWindow.IsEnabled = true;
                MainWindow.Visibility = Visibility.Visible;
                MainWindow.Activate();
            };
            var close = _notifyIcon.ContextMenuStrip.Items.Add("Exit");
            close.Click += (s, e) =>
            {
                Shutdown();
            };

            _notifyIcon.Visible = true;
        }

        private void DisposeNotifyIcon()
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
        }

        #endregion

    }
}