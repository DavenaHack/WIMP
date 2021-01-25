using MIMP.Input;
using MIMP.OperatingSystem.Windows.Mouse;
using MIMP.OperatingSystem.Windows.Native;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MIMP.OperatingSystem.Windows.Keyboard
{
    /// <summary>
    /// Listens keyboard globally.
    /// 
    /// <remarks>Uses WH_KEYBOARD_LL.</remarks>
    /// </summary>
    public class GlobalMouseHandler : IGlobalMouseHandler
    {

        public event EventHandler<MouseEventArgs> MouseMove;

        public event EventHandler<ButtonEventArgs> ButtonDown;

        public event EventHandler<ButtonEventArgs> ButtonUp;

        public event EventHandler<ButtonEventArgs> ButtonDoubleClick;

        public event EventHandler<WheelEventArgs> Wheel;

        public event EventHandler<WheelEventArgs> HorizontalWheel;


        private readonly SemaphoreSlim _semaphore;

        private readonly IDisposable _callback;


        public GlobalMouseHandler()
        {
            _semaphore = new SemaphoreSlim(1, 1);
            _callback = MouseManager.GlobalCallback(RaiseEvents);
        }

        ~GlobalMouseHandler()
        {
            Dispose();
        }


        private bool RaiseEvents(MouseEvent mouseEvent, MouseLLParam param)
        {
            try
            {
                _semaphore.Wait();
                try
                {
                    var (x, y) = param.pt;
                    MulticastDelegate e = null;
                    MouseEventArgs args = null;
                    switch (mouseEvent)
                    {
                        case MouseEvent.MouseMove:
                            e = MouseMove;
                            args = new MouseEventArgs(x, y);
                            break;
                        case MouseEvent.LeftButtonDown:
                            e = ButtonDown;
                            args = new ButtonEventArgs(x, y, MouseButton.Left);
                            break;
                        case MouseEvent.LeftButtonUp:
                            e = ButtonUp;
                            args = new ButtonEventArgs(x, y, MouseButton.Left);
                            break;
                        case MouseEvent.LeftButtonDoubleClick:
                            e = ButtonDoubleClick;
                            args = new ButtonEventArgs(x, y, MouseButton.Left);
                            break;
                        case MouseEvent.RightButtonDown:
                            e = ButtonDown;
                            args = new ButtonEventArgs(x, y, MouseButton.Right);
                            break;
                        case MouseEvent.RightButtonUp:
                            e = ButtonUp;
                            args = new ButtonEventArgs(x, y, MouseButton.Right);
                            break;
                        case MouseEvent.RightButtonDoubleClick:
                            e = ButtonDoubleClick;
                            args = new ButtonEventArgs(x, y, MouseButton.Right);
                            break;
                        case MouseEvent.MiddleButtonDown:
                            e = ButtonDown;
                            args = new ButtonEventArgs(x, y, MouseButton.Middle);
                            break;
                        case MouseEvent.MiddleButtonUp:
                            e = ButtonUp;
                            args = new ButtonEventArgs(x, y, MouseButton.Middle);
                            break;
                        case MouseEvent.MiddleButtonDoubleClick:
                            e = ButtonDoubleClick;
                            args = new ButtonEventArgs(x, y, MouseButton.Middle);
                            break;
                        case MouseEvent.XButtonDown:
                            e = ButtonDown;
                            args = new ButtonEventArgs(x, y, MouseButton.Fourth + (byte)(param.mouseData / 0x10000) - 1);
                            break;
                        case MouseEvent.XButtonUp:
                            e = ButtonUp;
                            args = new ButtonEventArgs(x, y, MouseButton.Fourth + (byte)(param.mouseData / 0x10000) - 1);
                            break;
                        case MouseEvent.XButtonDoubleClick:
                            e = ButtonDoubleClick;
                            args = new ButtonEventArgs(x, y, MouseButton.Fourth + (byte)(param.mouseData / 0x10000) - 1);
                            break;
                        case MouseEvent.MouseWheel:
                            e = Wheel;
                            args = new WheelEventArgs(x, y, param.mouseData > 0);
                            break;
                        case MouseEvent.MouseHorizontalWheel:
                            e = HorizontalWheel;
                            args = new WheelEventArgs(x, y, param.mouseData > 0);
                            break;
                        default:
                            break;
                    }
                    var l = e?.GetInvocationList();
                    if (l != null)
                        for (var i = l.Length; i-- > 0;)
                        {
                            try
                            {
                                var d = l[i];
                                d.Method.Invoke(d.Target, new object[] { this, args });
                            }
                            catch { }
                            if (args.Suppress)
                                return true;
                        }
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            catch (ObjectDisposedException) { }
            return false;
        }


        public void Dispose()
        {
            _callback.Dispose();
            _semaphore.Dispose();
        }

        public async ValueTask DisposeAsync() =>
            await Task.Run(() => Dispose());

    }
}
