using MIMP.OperatingSystem.Windows.Native;
using MIMP.Window;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace MIMP.OperatingSystem.Windows.Window
{
    // TODO events - hook on message and send it through the events
    public class Window : IWindow
    {

        public event EventHandler Moved
        {
            add { throw new NotSupportedException(); }
            remove { throw new NotSupportedException(); }
        }

        public event EventHandler Resized
        {
            add { throw new NotSupportedException(); }
            remove { throw new NotSupportedException(); }
        }

        public event EventHandler Closed
        {
            add { throw new NotSupportedException(); }
            remove { throw new NotSupportedException(); }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;


        public string Title => WindowManager.Title(Handle);


        public WindowState State
        {
            get => Placement.ShowCommand switch
            {
                ShowWindowCommand.Normal or ShowWindowCommand.ShowNormal => WindowState.Normal,
                ShowWindowCommand.Maximize or ShowWindowCommand.ShowMaximized => WindowState.Maximize,
                ShowWindowCommand.Minimize or ShowWindowCommand.ShowMinimized => WindowState.Minimize,
                _ => WindowState.Hide
            };
            set
            {
                switch (value)
                {
                    case WindowState.Normal:
                        Show();
                        break;
                    case WindowState.Minimize:
                        Minimize();
                        break;
                    case WindowState.Maximize:
                        Maximize();
                        break;
                    case WindowState.Hide:
                        Hide();
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }


        public int X
        {
            get => Placement.ShowCommand switch
            {
                ShowWindowCommand.Normal or ShowWindowCommand.ShowNormal => Rectangle.X,
                ShowWindowCommand.Maximize or ShowWindowCommand.ShowMaximized => Placement.MaxPosition.X,
                _ => -1
            };
            set => Move(value, Y);
        }

        public int Y
        {
            get => Placement.ShowCommand switch
            {
                ShowWindowCommand.Normal or ShowWindowCommand.ShowNormal => Rectangle.Y,
                ShowWindowCommand.Maximize or ShowWindowCommand.ShowMaximized => Placement.MaxPosition.Y,
                _ => -1
            };
            set => Move(X, value);
        }

        public int Width
        {
            get => Placement.ShowCommand switch
            {
                ShowWindowCommand.Normal or ShowWindowCommand.ShowNormal => Rectangle.Width,
                ShowWindowCommand.Maximize or ShowWindowCommand.ShowMaximized => Screen.WorkAreaWidth,
                _ => -1
            }; set => Resize(value, Height);
        }

        public int Height
        {
            get => Placement.ShowCommand switch
            {
                ShowWindowCommand.Normal or ShowWindowCommand.ShowNormal => Rectangle.Height,
                ShowWindowCommand.Maximize or ShowWindowCommand.ShowMaximized => Screen.WorkAreaHeight,
                _ => -1
            }; set => Resize(Width, value);
        }


        public IScreen Screen =>
            Screens.Instance[this]; // TODO ScreenFactory;


        public IntPtr Handle { get; }


        protected Placement Placement =>
            WindowManager.GetPlacement(Handle);


        protected Rectangle Rectangle =>
            WindowManager.GetPosition(Handle);


        private bool _disposedValue;


        public Window(IntPtr hWnd)
        {
            Handle = hWnd != IntPtr.Zero ? hWnd : throw new ArgumentNullException(nameof(hWnd));
        }

        ~Window()
        {
            Dispose(false);
        }


        public void SetPosition(Rectangle position) // TODO find a solution which move once but dont change normalPosition
        {
            SetPosition(position.X, position.Y, position.Width, position.Height);
        }

        public void SetPosition(int x, int y, int width, int height)
        {
            if (Placement.ShowCommand != ShowWindowCommand.ShowNormal)
                WindowManager.Show(Handle, ShowWindowCommand.ShowNormalNoActivate);
            WindowManager.Move(Handle, x, y, width, height, true);
        }

        public void Move(Point position)
        {
            Move(position.X, position.Y);
        }

        public void Move(int x, int y)
        {
            SetPosition(x, y, Width, Height);
        }

        public void Resize(Size size)
        {
            Resize(size.Width, size.Height);
        }

        public void Resize(int width, int height)
        {
            SetPosition(X, Y, width, height);
        }


        protected void Show(ShowWindowCommand showCommand)
        {
            WindowManager.Show(Handle, showCommand);
        }

        public void Hide()
        {
            Show(ShowWindowCommand.Hide);
        }

        public void Show()
        {
            Show(ShowWindowCommand.ShowNormal);
        }

        public void Maximize()
        {
            Show(ShowWindowCommand.ShowMaximized);
        }

        public void Minimize()
        {
            Show(ShowWindowCommand.ShowMinimized);
        }


        public override bool Equals(object obj)
        {
            return obj is Window window &&
                   Handle.Equals(window.Handle);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Handle);
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
