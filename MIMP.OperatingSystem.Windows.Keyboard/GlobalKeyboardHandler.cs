using MIMP.Input;
using MIMP.OperatingSystem.Windows.Hook;
using MIMP.OperatingSystem.Windows.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MIMP.OperatingSystem.Windows.Keyboard
{
    /// <summary>
    /// Listens keyboard globally.
    /// 
    /// <remarks>Uses WH_KEYBOARD_LL.</remarks>
    /// </summary>
    public class GlobalKeyboardHandler : IGlobalKeyboardHandler
    {

        public event EventHandler<KeyEventArgs> KeyUp;

        public event EventHandler<KeyEventArgs> KeyDown;

        /// <summary>
        /// Fired when any of the keys is pressed down.
        /// </summary>
        public event EventHandler<RawKeyEventArgs> RawKeyDown;

        /// <summary>
        /// Fired when any of the keys is released.
        /// </summary>
        public event EventHandler<RawKeyEventArgs> RawKeyUp;


        private readonly SemaphoreSlim _semaphore;
        private GlobalHookCallback _callback;


        public GlobalKeyboardHandler()
        {
            _semaphore = new SemaphoreSlim(1, 1);
            _callback = KeyboardManager.GlobalCallback(RaiseEvents);
        }

        ~GlobalKeyboardHandler()
        {
            Dispose();
        }


        private bool RaiseEvents(KeyEvent keyEvent, VirtualKey virtualKey)
        {
            try
            {
                _semaphore.Wait(); // TODO ordered waiting
                try
                {
                    var k = virtualKey.ToKey();
                    var down = keyEvent == KeyEvent.KeyDown || keyEvent == KeyEvent.SystemKeyDown;
                    var args = new KeyEventArgs(k);
                    var l = (down ? KeyDown : KeyUp)?.GetInvocationList();
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
                    var sys = keyEvent == KeyEvent.SystemKeyDown || keyEvent == KeyEvent.SystemKeyUp;
                    var raw = new RawKeyEventArgs(virtualKey, sys);
                    l = (down ? RawKeyDown : RawKeyUp)?.GetInvocationList();
                    if (l != null)
                        for (var i = l.Length; i-- > 0;)
                        {
                            try
                            {
                                var d = l[i];
                                d.Method.Invoke(d.Target, new object[] { this, raw });
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


        public void Reinitialize()
        {
            var old = _callback;
            _callback = KeyboardManager.GlobalCallback(RaiseEvents);
            old.Dispose();
        }


        public void Down(Key key)
        {
            KeyboardManager.KeyDown(key.ToVirtual());
        }

        public void Up(Key key)
        {
            KeyboardManager.KeyUp(key.ToVirtual());
        }

        public Input.KeyState GetState(Key key)
        {
            return KeyboardManager.GetKeyState(key.ToVirtual()).ToState();
        }

        public void SetState(Key key, Input.KeyState state)
        {
            KeyboardManager.SetKeyState(key.ToVirtual(), state.ToState());
        }

        public void AddState(Key key, Input.KeyState state)
        {
            KeyboardManager.AddKeyState(key.ToVirtual(), state.ToState());
        }

        public void RemoveState(Key key, Input.KeyState state)
        {
            KeyboardManager.RemoveKeyState(key.ToVirtual(), state.ToState());
        }

        public IDictionary<Key, Input.KeyState> GetStates()
        {
            return KeyboardManager.GetKeyStates()
                .ToDictionary(x => x.Key.ToKey(), x => x.Value.ToState());
        }

        public ISet<Key> GetPressedKeys()
        {
            return KeyboardManager.PressedKeys().Select(x => x.ToKey()).ToHashSet();
        }

        public ISet<Key> GetToggledKeys()
        {
            return KeyboardManager.ToggledKeys().Select(x => x.ToKey()).ToHashSet();
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
