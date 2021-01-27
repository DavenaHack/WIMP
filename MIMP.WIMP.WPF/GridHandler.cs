using MIMP.Input;
using MIMP.Log;
using MIMP.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIMP.WIMP.WPF
{
    public class GridHandler : IDisposable
    {

        private readonly App _app;

        private bool _ignore;

        private bool _windowsHandled;

        private readonly ISet<Key> _windows;
        private readonly ISet<Key> _shift;
        private readonly ISet<Key> _control;
        private readonly ISet<Key> _alt;


        private bool _upHandled;
        private bool _rightHandled;
        private bool _downHandled;
        private bool _leftHandled;

        private bool _spaceHandled;

        private ISet<Key> _suppressedKeys;


        public GridHandler(App app)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));

            _ignore = false;

            _windowsHandled = false;

            _windows = new HashSet<Key>();
            _shift = new HashSet<Key>();
            _control = new HashSet<Key>();
            _alt = new HashSet<Key>();

            _upHandled = false;
            _rightHandled = false;
            _downHandled = false;
            _leftHandled = false;

            _spaceHandled = false;

            _suppressedKeys = new HashSet<Key>();

            _app.KeyboardHandler.KeyDown += KeyDown;
            _app.KeyboardHandler.KeyUp += KeyUp;
        }

        private void KeyDown(object sender, KeyEventArgs e) // TODO check count
        {
            if (_ignore)
                return;

            try
            {
                if (e.Key.IsWindows())
                {
                    _windowsHandled = false;
                    _windows.Add(e.Key);
                    return;
                }
                if (e.Key.IsShift())
                {
                    _shift.Add(e.Key);
                    return;
                }
                if (e.Key.IsControl())
                {
                    _control.Add(e.Key);
                    return;
                }
                if (e.Key.IsAlt())
                {
                    _alt.Add(e.Key);
                    return;
                }

                if (_windows.Count > 0)
                {
                    if (e.Key == Key.Up)
                    {
                        if (_control.Count > 0)
                            return;
                        e.Suppress = true;
                        _upHandled = false;
                        _suppressedKeys.Add(e.Key);
                        return;
                    }
                    if (e.Key == Key.Right)
                    {
                        if (_control.Count > 0)
                            return;
                        e.Suppress = true;
                        _rightHandled = false;
                        _suppressedKeys.Add(e.Key);
                        return;
                    }
                    if (e.Key == Key.Down)
                    {
                        if (_control.Count > 0)
                            return;
                        e.Suppress = true;
                        _downHandled = false;
                        _suppressedKeys.Add(e.Key);
                        return;
                    }
                    if (e.Key == Key.Left)
                    {
                        if (_control.Count > 0)
                            return;
                        e.Suppress = true;
                        _leftHandled = false;
                        _suppressedKeys.Add(e.Key);
                        return;
                    }

                    if (e.Key == Key.Space)
                    {
                        if (_shift.Count > 0 || _alt.Count > 0 && _control.Count > 0)
                            return;
                        e.Suppress = true;
                        _spaceHandled = false;
                        _suppressedKeys.Add(e.Key);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _app.Logger.Log(this, Level.Error, ex.Message);
            }
        }

        private void KeyUp(object sender, KeyEventArgs e)
        {
            if (_ignore)
                return;

            try
            {
                if (e.Key.IsWindows())
                {
                    _windows.Remove(e.Key);
                    if (_windowsHandled)
                    {
                        // suppress window menu opening
                        if (_control.Count < 1)
                        {
                            e.Suppress = true;
                            _ignore = true;
                            Task.Run(() =>
                            {
                                try
                                {
                                    _app.KeyboardHandler.Down(Key.LeftControl);
                                    _app.KeyboardHandler.Up(Key.LeftControl);
                                    _app.KeyboardHandler.Up(e.Key);
                                }
                                finally
                                {
                                    _ignore = false;
                                }
                            });
                        }
                    }
                    return;
                }
                if (e.Key.IsShift())
                {
                    _shift.Remove(e.Key);
                    return;
                }
                if (e.Key.IsControl())
                {
                    _control.Remove(e.Key);
                    return;
                }
                if (e.Key.IsAlt())
                {
                    _alt.Remove(e.Key);
                    return;
                }

                if (_windows.Count > 0)
                {
                    if (e.Key == Key.Up)
                    {
                        if (!_suppressedKeys.Contains(e.Key))
                            return;
                        _suppressedKeys.Remove(e.Key);
                        if (_upHandled)
                        {
                            e.Suppress = true;
                            return;
                        }
                        if (_control.Count > 0)
                            return;
                        e.Suppress = true;
                        _windowsHandled = true;
                        _upHandled = true;
                        if (_alt.Count > 0)
                        {
                            if (_shift.Count > 0)
                            {
                                Task.Run(() => ShrinkUp());
                                return;
                            }
                            Task.Run(() => GrowUp());
                            return;
                        }
                        if (_shift.Count > 0)
                        {
                            Task.Run(() => ScreenUp());
                            return;
                        }
                        if (_suppressedKeys.Count == 1 && _suppressedKeys.All(x => x == Key.Down))
                        {
                            _downHandled = true;
                            Task.Run(() => FullHeight());
                            return;
                        }
                        Task.Run(() => MoveUp());
                        return;
                    }
                    if (e.Key == Key.Right)
                    {
                        if (!_suppressedKeys.Contains(e.Key))
                            return;
                        _suppressedKeys.Remove(e.Key);
                        if (_rightHandled)
                        {
                            e.Suppress = true;
                            return;
                        }
                        if (_control.Count > 0)
                            return;
                        e.Suppress = true;
                        _windowsHandled = true;
                        _rightHandled = true;
                        if (_alt.Count > 0)
                        {
                            if (_shift.Count > 0)
                            {
                                Task.Run(() => ShrinkRight());
                                return;
                            }
                            Task.Run(() => GrowRight());
                            return;
                        }
                        if (_shift.Count > 0)
                        {
                            Task.Run(() => ScreenRight());
                            return;
                        }
                        if (_suppressedKeys.Count == 1 && _suppressedKeys.All(x => x == Key.Left))
                        {
                            _leftHandled = true;
                            Task.Run(() => FullWidth());
                            return;
                        }
                        Task.Run(() => MoveRight());
                        return;
                    }
                    if (e.Key == Key.Down)
                    {
                        if (!_suppressedKeys.Contains(e.Key))
                            return;
                        _suppressedKeys.Remove(e.Key);
                        if (_downHandled)
                        {
                            e.Suppress = true;
                            return;
                        }
                        if (_control.Count > 0)
                            return;
                        e.Suppress = true;
                        _windowsHandled = true;
                        _downHandled = true;
                        if (_alt.Count > 0)
                        {
                            if (_shift.Count > 0)
                            {
                                Task.Run(() => ShrinkDown());
                                return;
                            }
                            Task.Run(() => GrowDown());
                            return;
                        }
                        if (_shift.Count > 0)
                        {
                            Task.Run(() => ScreenDown());
                            return;
                        }
                        if (_suppressedKeys.Count == 1 && _suppressedKeys.All(x => x == Key.Up))
                        {
                            _upHandled = true;
                            Task.Run(() => FullHeight());
                            return;
                        }
                        Task.Run(() => MoveDown());
                        return;
                    }
                    if (e.Key == Key.Left)
                    {
                        if (!_suppressedKeys.Contains(e.Key))
                            return;
                        _suppressedKeys.Remove(e.Key);
                        if (_leftHandled)
                        {
                            e.Suppress = true;
                            return;
                        }
                        if (_control.Count > 0)
                            return;
                        e.Suppress = true;
                        _windowsHandled = true;
                        _leftHandled = true;
                        if (_alt.Count > 0)
                        {
                            if (_shift.Count > 0)
                            {
                                Task.Run(() => ShrinkLeft());
                                return;
                            }
                            Task.Run(() => GrowLeft());
                            return;
                        }
                        if (_shift.Count > 0)
                        {
                            Task.Run(() => ScreenLeft());
                            return;
                        }
                        if (_suppressedKeys.Count == 1 && _suppressedKeys.All(x => x == Key.Right))
                        {
                            _rightHandled = true;
                            Task.Run(() => FullWidth());
                            return;
                        }
                        Task.Run(() => MoveLeft());
                        return;
                    }

                    if (e.Key == Key.Space)
                    {
                        if (!_suppressedKeys.Contains(e.Key))
                            return;
                        _suppressedKeys.Remove(e.Key);
                        if (_spaceHandled)
                        {
                            e.Suppress = true;
                            return;
                        }
                        if (_shift.Count > 0)
                            return;
                        if (_control.Count > 0 && _alt.Count < 1)
                        {
                            e.Suppress = true;
                            _windowsHandled = true;
                            _spaceHandled = true;
                            Task.Run(() => Maximize());
                            return;
                        }
                        if (_alt.Count > 0 && _control.Count < 1)
                        {
                            e.Suppress = true;
                            _windowsHandled = true;
                            _spaceHandled = true;
                            Task.Run(() => Minimize());
                            return;
                        }
                    }

                    _windowsHandled = false;
                }
            }
            catch (Exception ex)
            {
                _app.Logger.Log(this, Level.Error, ex.Message);
            }
        }


        #region Move


        private void MoveUp()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            var next = grid.Top(t.Item1);
            {
                var c = next;
                for (var i = 1; next != null && i < t.Item4; i++)
                    next = grid.Top(next);
                if (next != null)
                    next = c;
            }
            if (next == null)
                if (pos != t.Item2)
                    window.SetPosition(t.Item2);
                else
                {
                    var ns = _app.Screens.Top(pos, screen, false);
                    if (ns != null)
                    {
                        var g = _app.Configuration.Grid(ns.ID);
                        var s = ns.GetWorkArea();
                        var st = g.At(s, pos);
                        window.SetPosition(st.Item2);
                    }
                    else
                    {
                        ns = _app.Screens.Top(pos, screen, true);
                        if (ns != null)
                        {
                            var g = _app.Configuration.Grid(ns.ID);
                            var s = ns.GetWorkArea();
                            pos.Y = s.Bottom;
                            var st = g.At(s, pos);
                            window.SetPosition(st.Item2);
                        }
                    }
                }
            else
                window.SetPosition(grid.Position(next, size, t.Item3, t.Item4));
        }

        private void MoveRight()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            var next = grid.Right(t.Item1);
            {
                var c = next;
                for (var i = 1; next != null && i < t.Item3; i++)
                    next = grid.Right(next);
                if (next != null)
                    next = c;
            }
            if (next == null)
                if (pos != t.Item2)
                    window.SetPosition(t.Item2);
                else
                {
                    var ns = _app.Screens.Right(pos, screen, false);
                    if (ns != null)
                    {
                        var g = _app.Configuration.Grid(ns.ID);
                        var s = ns.GetWorkArea();
                        var st = g.At(s, pos);
                        window.SetPosition(st.Item2);
                    }
                    else
                    {
                        ns = _app.Screens.Right(pos, screen, true);
                        if (ns != null)
                        {
                            var g = _app.Configuration.Grid(ns.ID);
                            var s = ns.GetWorkArea();
                            pos.X = s.Left - pos.Width;
                            var st = g.At(s, pos);
                            window.SetPosition(st.Item2);
                        }
                    }
                }
            else
                window.SetPosition(grid.Position(next, size, t.Item3, t.Item4));
        }

        private void MoveDown()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            var next = grid.Bottom(t.Item1);
            if (next == null)
                if (pos != t.Item2)
                    window.SetPosition(t.Item2);
                else
                {
                    var ns = _app.Screens.Down(pos, screen, false);
                    if (ns != null)
                    {
                        var g = _app.Configuration.Grid(ns.ID);
                        var s = ns.GetWorkArea();
                        var st = g.At(s, pos);
                        window.SetPosition(st.Item2);
                    }
                    else
                    {
                        ns = _app.Screens.Down(pos, screen, true);
                        if (ns != null)
                        {
                            var g = _app.Configuration.Grid(ns.ID);
                            var s = ns.GetWorkArea();
                            pos.Y = s.Top - s.Height;
                            var st = g.At(s, pos);
                            window.SetPosition(st.Item2);
                        }
                    }
                }
            else
                window.SetPosition(grid.Position(next, size, t.Item3, t.Item4));
        }

        private void MoveLeft()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            var next = grid.Left(t.Item1);
            if (next == null)
                if (pos != t.Item2)
                    window.SetPosition(t.Item2);
                else
                {
                    var ns = _app.Screens.Left(pos, screen, false);
                    if (ns != null)
                    {
                        var g = _app.Configuration.Grid(ns.ID);
                        var s = ns.GetWorkArea();
                        var st = g.At(s, pos);
                        window.SetPosition(st.Item2);
                    }
                    else
                    {
                        ns = _app.Screens.Left(pos, screen, true);
                        if (ns != null)
                        {
                            var g = _app.Configuration.Grid(ns.ID);
                            var s = ns.GetWorkArea();
                            pos.X = s.Right;
                            var st = g.At(s, pos);
                            window.SetPosition(st.Item2);
                        }
                    }
                }
            else
                window.SetPosition(grid.Position(next, size, t.Item3, t.Item4));
        }


        #endregion Move


        #region Grow


        private void GrowUp()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            if (t.Item1.Row > 0)
                window.SetPosition(grid.Position(grid.At(t.Item1.Column, t.Item1.Row - 1), size, t.Item3, t.Item4 + 1));
        }

        private void GrowRight()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            window.SetPosition(grid.Position(t.Item1, size, t.Item3 + 1, t.Item4));
        }

        private void GrowDown()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            window.SetPosition(grid.Position(t.Item1, size, t.Item3, t.Item4 + 1));
        }

        private void GrowLeft()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            if (t.Item1.Column > 0)
                window.SetPosition(grid.Position(grid.At(t.Item1.Column - 1, t.Item1.Row), size, t.Item3 + 1, t.Item4));
        }


        #endregion Grow


        #region Shrink


        private void ShrinkUp()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            window.SetPosition(grid.Position(t.Item1, size, t.Item3, t.Item4 - 1));
        }

        private void ShrinkRight()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            window.SetPosition(grid.Position(grid.At(t.Item1.Column + t.Item1.ColumnSpan, t.Item1.Row), size, t.Item3 - 1, t.Item4));
        }

        private void ShrinkDown()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            window.SetPosition(grid.Position(grid.At(t.Item1.Column, t.Item1.Row + t.Item1.RowSpan), size, t.Item3, t.Item4 - 1));
        }

        private void ShrinkLeft()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            var grid = _app.Configuration.Grid(screen.ID);
            var size = screen.GetWorkArea();
            var pos = window.GetPosition();
            var t = grid.At(size, pos);
            if (t == null)
                return;
            window.SetPosition(grid.Position(t.Item1, size, t.Item3 - 1, t.Item4));
        }


        #endregion Shrink


        #region Full


        private void FullHeight()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            window.SetPosition(window.X, screen.Y, window.Width, screen.Height);
        }

        private void FullWidth()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var screen = window.Screen;
            window.SetPosition(screen.X, window.Y, screen.Width, window.Height);
        }


        #endregion


        #region Max-Minimize


        private void Maximize()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            if (window.State == WindowState.Maximize)
                window.Show();
            else
                window.Maximize();
        }

        private void Minimize()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            if (window.State == WindowState.Minimize)
                window.Show();
            else
                window.Minimize();
        }


        #endregion Max-Minimize


        #region Screen


        private void ScreenUp()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var pos = window.GetPosition();
            var screen = window.Screen;
            var ns = _app.Screens.Top(pos, screen, true);
            if (ns == null && ns == screen)
                return;
            window.SetPosition(
                    (window.X - screen.X) * ns.Width / screen.Width + ns.X,
                    (window.Y - screen.Y) * ns.Height / screen.Height + ns.Y,
                    window.Width * ns.Width / screen.Width,
                    window.Height * ns.Height / screen.Height
                );
        }

        private void ScreenRight()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var pos = window.GetPosition();
            var screen = window.Screen;
            var ns = _app.Screens.Right(pos, screen, true);
            if (ns == null && ns == screen)
                return;
            window.SetPosition(
                    (window.X - screen.X) * ns.Width / screen.Width + ns.X,
                    (window.Y - screen.Y) * ns.Height / screen.Height + ns.Y,
                    window.Width * ns.Width / screen.Width,
                    window.Height * ns.Height / screen.Height
                );
        }

        private void ScreenDown()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var pos = window.GetPosition();
            var screen = window.Screen;
            var ns = _app.Screens.Down(pos, screen, true);
            if (ns == null && ns == screen)
                return;
            window.SetPosition(
                    (window.X - screen.X) * ns.Width / screen.Width + ns.X,
                    (window.Y - screen.Y) * ns.Height / screen.Height + ns.Y,
                    window.Width * ns.Width / screen.Width,
                    window.Height * ns.Height / screen.Height
                );
        }

        private void ScreenLeft()
        {
            var window = _app.Apps.FirstOrDefault();
            if (window == null)
                return;
            var pos = window.GetPosition();
            var screen = window.Screen;
            var ns = _app.Screens.Left(pos, screen, true);
            if (ns == null && ns == screen)
                return;
            window.SetPosition(
                    (window.X - screen.X) * ns.Width / screen.Width + ns.X,
                    (window.Y - screen.Y) * ns.Height / screen.Height + ns.Y,
                    window.Width * ns.Width / screen.Width,
                    window.Height * ns.Height / screen.Height
                );
        }


        #endregion Screen


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _app.KeyboardHandler.KeyDown -= KeyDown;
            _app.KeyboardHandler.KeyUp -= KeyUp;
            foreach (var k in _suppressedKeys)
                _app.KeyboardHandler.Up(k);
        }

    }
}
