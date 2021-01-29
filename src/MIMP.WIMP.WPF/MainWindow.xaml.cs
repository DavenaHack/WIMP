using MIMP.Window;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MIMP.WIMP.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        protected App App => (App)Application.Current;

        private WIMPConfiguration _tempConfiguration;
        public WIMPConfiguration TempConfiguration
        {
            get => _tempConfiguration;
            set
            {
                if (_tempConfiguration == value)
                    return;
                _tempConfiguration = value;
                RaisePropertyChanged();
                _grids = null;
                RaisePropertyChanged(nameof(Grids));
            }
        }


        #region Grid


        public IEnumerable<GridUnit> GridUnits => Enum.GetValues<GridUnit>();


        private IDictionary<IScreen, Button> _gridViews;

        private ObservableScreenGrids _grids;
        public ObservableScreenGrids Grids
        {
            get => _grids ?? (Grids = new ObservableScreenGrids(App.Screens, TempConfiguration));
            set
            {
                if (_grids == value)
                    return;
                if (_grids != null)
                {
                    _grids.CollectionChanged -= GridCollectionChanged;
                    _grids.PropertyChanged -= GridPropertyChanged;
                }
                _grids = value;
                if (_grids != null)
                {
                    _grids.CollectionChanged += GridCollectionChanged;
                    _grids.PropertyChanged += GridPropertyChanged;
                }
                RaisePropertyChanged();
                SelectedScreen = SelectedScreen;
            }
        }

        private int _minX;
        private int _maxX;
        private int _minY;
        private int _maxY;


        private IScreen _selectedScreen;
        public IScreen SelectedScreen
        {
            get => _selectedScreen;
            set
            {
                if (!App.Screens.Contains(value))
                    return;
                _selectedScreen = value;
                RaisePropertyChanged();
                if (_selectedScreen != null)
                    SelectedGrid = TempConfiguration.ScreenGrid[_selectedScreen.ID] = TempConfiguration.Grid(_selectedScreen.ID).Clone();
                else
                    SelectedGrid = null;
            }
        }

        private Grid _selectedGrid;
        public Grid SelectedGrid
        {
            get => _selectedGrid;
            set
            {
                if (_selectedGrid == value)
                    return;
                _selectedGrid = value;
                RaisePropertyChanged();
            }
        }


        private bool _gridChanged;

        public bool GridChanged
        {
            get => _gridChanged;
            set
            {
                if (_gridChanged == value)
                    return;
                _gridChanged = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        public MainWindow()
        {
            InitializeComponent();
            TempConfiguration = App.Configuration.Clone();

            _gridViews = new Dictionary<IScreen, Button>();
            SelectedScreen = App.Screens.FirstOrDefault();
            InitGrid();

            GridCanvas.SizeChanged += (_, _) => SetScreenSizes();
            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(Grids))
                    InitGrid();
                if (e.PropertyName == nameof(SelectedScreen))
                {
                    foreach (var (s, b) in _gridViews)
                        b.Style = FindResource(s.Equals(SelectedScreen) ? "SelectedScreenButton" : "ScreenButton") as Style;
                }
            };
        }


        #region Grid


        private void InitGrid()
        {
            lock (_gridViews)
            {
                _gridViews.Clear();
                GridCanvas.Children.Clear();
                if (Grids == null || Grids.Count < 1)
                    return;
                _minX = int.MaxValue;
                _maxX = int.MinValue;
                _minY = int.MaxValue;
                _maxY = int.MinValue;
                if (!App.Screens.Contains(SelectedScreen))
                    SelectedScreen = App.Screens.FirstOrDefault();
                foreach (var (s, g) in Grids)
                {
                    var button = new Button
                    {
                        Style = FindResource("ScreenButton") as Style,
                    };
                    button.Click += (_, _) =>
                    {
                        SelectedScreen = s;
                    };
                    if (s.Equals(SelectedScreen))
                        button.Style = FindResource("SelectedScreenButton") as Style;
                    GridCanvas.Children.Add(button);
                    _gridViews[s] = button;
                    var grid = new System.Windows.Controls.Grid();
                    button.Content = grid;
                    foreach (var r in g.Rows)
                        grid.RowDefinitions.Add(new RowDefinition());
                    foreach (var c in g.Columns)
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                    foreach (var c in g.Cells)
                    {
                        var bc = new Button
                        {
                            Style = FindResource("GridButton") as Style,
                        };
                        grid.Children.Add(bc);
                        System.Windows.Controls.Grid.SetColumn(bc, c.Column);
                        System.Windows.Controls.Grid.SetColumnSpan(bc, c.ColumnSpan);
                        System.Windows.Controls.Grid.SetRow(bc, c.Row);
                        System.Windows.Controls.Grid.SetRowSpan(bc, c.RowSpan);

                        var menu = bc.ContextMenu = new ContextMenu();
                        var joinTop = new MenuItem
                        {
                            Header = "Join with top cell",
                            IsEnabled = c.Row > 0,
                        };
                        joinTop.Click += (_, _) =>
                        {
                            JoinCells(g, c, new[] { g.At(c.Column, c.Row - 1) });
                            InitGrid();
                            GridChanged = true;
                        };
                        menu.Items.Add(joinTop);
                        var joinRight = new MenuItem
                        {
                            Header = "Join with right cell",
                            IsEnabled = c.Column + c.ColumnSpan < g.Columns.Count,
                        };
                        joinRight.Click += (_, _) =>
                        {
                            JoinCells(g, c, new[] { g.At(c.Column + c.ColumnSpan, c.Row) });
                            InitGrid();
                            GridChanged = true;
                        };
                        menu.Items.Add(joinRight);
                        var joinButton = new MenuItem
                        {
                            Header = "Join with bottom cell",
                            IsEnabled = c.Row + c.RowSpan < g.Rows.Count,
                        };
                        joinButton.Click += (_, _) =>
                        {
                            JoinCells(g, c, new[] { g.At(c.Column, c.Row + c.RowSpan) });
                            InitGrid();
                            GridChanged = true;
                        };
                        menu.Items.Add(joinButton);
                        var joinLeft = new MenuItem
                        {
                            Header = "Join with left cell",
                            IsEnabled = c.Row + c.RowSpan < g.Rows.Count,
                        };
                        joinLeft.Click += (_, _) =>
                        {
                            JoinCells(g, c, new[] { g.At(c.Column, c.Row + c.RowSpan) });
                            InitGrid();
                            GridChanged = true;
                        };
                        menu.Items.Add(joinLeft);
                        menu.Items.Add(new Separator());
                        var splitVertical = new MenuItem
                        {
                            Header = "Split vertical",
                            IsEnabled = c.ColumnSpan % 2 == 0,
                        };
                        splitVertical.Click += (_, _) =>
                        {
                            c.ColumnSpan /= 2;
                            g.Cells.Add(new GridCell
                            {
                                Column = c.Column + c.ColumnSpan,
                                ColumnSpan = c.ColumnSpan,
                                Row = c.Row,
                                RowSpan = c.RowSpan,
                            });
                            InitGrid();
                            GridChanged = true;
                        };
                        menu.Items.Add(splitVertical);
                        var splitHorizontal = new MenuItem
                        {
                            Header = "Split vertical",
                            IsEnabled = c.RowSpan % 2 == 0,
                        };
                        splitHorizontal.Click += (_, _) =>
                        {
                            c.RowSpan /= 2;
                            g.Cells.Add(new GridCell
                            {
                                Row = c.Row + c.RowSpan,
                                RowSpan = c.RowSpan,
                                Column = c.Column,
                                ColumnSpan = c.ColumnSpan,
                            });
                            InitGrid();
                            GridChanged = true;
                        };
                        menu.Items.Add(splitHorizontal);
                    }
                    var x = s.X + s.Width;
                    if (s.X < _minX)
                        _minX = s.X;
                    if (x > _maxX)
                        _maxX = x;
                    var y = s.Y + s.Height;
                    if (s.Y < _minY)
                        _minY = s.Y;
                    if (y > _maxY)
                        _maxY = y;
                }
                SetScreenSizes();
            }
        }

        private void JoinCells(Grid grid, GridCell source, IEnumerable<GridCell> cells)
        {
            foreach (var c in cells)
            {
                if (c == source)
                    continue;
                var co = Math.Min(source.Column, c.Column);
                source.ColumnSpan = Math.Max(source.Column + source.ColumnSpan, c.Column + c.ColumnSpan) - co;
                source.Column = co;
                var ro = Math.Min(source.Row, c.Row);
                source.RowSpan = Math.Max(source.Row + source.RowSpan, c.Row + c.RowSpan) - ro;
                source.Row = ro;
                grid.Cells.Remove(c);
            }
            var next = grid.Ats(source.Column, source.Row, source.ColumnSpan, source.RowSpan)
                .Where(x => x != source).ToList();
            if (next.Count > 0)
                JoinCells(grid, source, next);
        }

        private void SetScreenSizes()
        {
            if (Grids == null || Grids.Count < 1 || _grids.Count < 1)
                return;
            double w = GridCanvas.ActualWidth;
            double h = GridCanvas.ActualHeight;
            if (w == 0 || h == 0)
                return;
            double dx = _maxX - _minX;
            double dy = _maxY - _minY;
            var rr = w / h;
            var sr = dx / dy;
            double mx = 0;
            double my = 0;
            if (sr > rr)
                my = (h - w / sr) / 2;
            else if (rr > sr)
                mx = (w - h * sr) / 2;
            double fx = (w - mx * 2) / dx;
            double fy = (h - my * 2) / dy;
            foreach (var (s, g) in Grids)
            {
                var border = _gridViews[s];
                Canvas.SetLeft(border, mx + (s.X - _minX) * fx);
                Canvas.SetTop(border, my + (s.Y - _minY) * fy);
                border.Width = s.Width * fx;
                border.Height = s.Height * fy;
                var grid = border.Content as System.Windows.Controls.Grid;
                grid.RowDefinitions.Clear();
                foreach (var r in g.Rows)
                    grid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = To(r, fx)
                    });
                grid.ColumnDefinitions.Clear();
                foreach (var c in g.Columns)
                    grid.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = To(c, fy)
                    });
            }
        }


        private void GridCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InitGrid();
        }

        private void GridPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InitGrid();
        }


        private System.Windows.GridLength To(GridLength r, double factor) => r.Unit switch
        {
            GridUnit.Star => new System.Windows.GridLength(r.Value, GridUnitType.Star),
            GridUnit.Pixel => new System.Windows.GridLength(r.Value * factor, GridUnitType.Pixel),
            _ => System.Windows.GridLength.Auto,
        };


        #endregion


        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected void Dispatch(Action action)
        {
            if (Thread.CurrentThread.ManagedThreadId == Dispatcher.Thread.ManagedThreadId)
                action();
            else
                Dispatcher.Invoke(action);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            GridChanged = false;
            TempConfiguration = App.Configuration.Clone();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            GridChanged = false;
            App.Configuration = TempConfiguration.Clone();
        }

        private void RowGridUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox cb)
                return;
            if (e.RemovedItems.Count < 1)
                return;
            if (e.RemovedItems[0] is not GridUnit u)
                return;
            if (cb.DataContext is not GridLength l)
                return;
            SetGridValue(l, u, SelectedGrid.Rows, SelectedScreen.Y, SelectedScreen.Height);
        }

        private void ColumnGridUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox cb)
                return;
            if (e.RemovedItems.Count < 1)
                return;
            if (e.RemovedItems[0] is not GridUnit u)
                return;
            if (cb.DataContext is not GridLength l)
                return;
            SetGridValue(l, u, SelectedGrid.Columns, SelectedScreen.X, SelectedScreen.Width);
        }

        private void SetGridValue(GridLength length, GridUnit oldUnit, IEnumerable<GridLength> lengths, int start, int size)
        {
            var ls = lengths.ToList();
            var i = ls.IndexOf(length);
            ls[i] = new GridLength(length.Value, oldUnit);
            switch (oldUnit)
            {
                case GridUnit.Pixel:
                    length.Value = 1; // TODO calc
                    break;
                case GridUnit.Star:
                    var v = ls.CalcSizes(start, size)[i];
                    length.Value = v.Item2 - v.Item1;
                    break;
                default:
                    break;
            }
        }

        private void GridLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validation.GetHasError(sender as TextBox))
                return;
            InitGrid();
            GridChanged = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
            IsEnabled = false;
        }

        private void RowDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button b)
                return;
            if (b.DataContext is not GridLength l)
                return;
            var grid = SelectedGrid;
            var i = grid.Rows.IndexOf(l);
            grid.Rows.Remove(l);
            foreach (var c in grid.Cells)
            {
                if (c.Row == i)
                    if (c.RowSpan < 2)
                        grid.Cells.Remove(c);
                    else if (c.Row >= grid.Rows.Count)
                        grid.Cells.Remove(c);
                    else
                        c.RowSpan--;
                else if (c.Row < i)
                    c.RowSpan--;
                else
                    c.Row--;
            }
            InitGrid();
            GridChanged = true;
        }

        private void ColumnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button b)
                return;
            if (b.DataContext is not GridLength l)
                return;
            var grid = SelectedGrid;
            var i = grid.Columns.IndexOf(l);
            grid.Columns.Remove(l);
            foreach (var c in grid.Cells.ToList())
            {
                if (c.Column == i)
                    if (c.ColumnSpan < 2)
                        grid.Cells.Remove(c);
                    else if (c.Column >= grid.Columns.Count)
                        grid.Cells.Remove(c);
                    else
                        c.ColumnSpan--;
                else if (c.Column < i)
                {
                    if (c.Column + c.ColumnSpan > i)
                        c.ColumnSpan--;
                }
                else
                    c.Column--;
            }
            InitGrid();
            GridChanged = true;
        }

        private void RowAdd_Click(object sender, RoutedEventArgs e)
        {
            var grid = SelectedGrid;
            grid.Rows.Add(new GridLength(1, GridUnit.Star));
            for (var i = 0; i < grid.Columns.Count; i++)
                grid.Cells.Add(new GridCell
                {
                    Row = grid.Rows.Count - 1,
                    Column = i
                });
            InitGrid();
            GridChanged = true;
        }

        private void ColumnAdd_Click(object sender, RoutedEventArgs e)
        {
            var grid = SelectedGrid;
            grid.Columns.Add(new GridLength(1, GridUnit.Star));
            for (var i = 0; i < grid.Rows.Count; i++)
                grid.Cells.Add(new GridCell
                {
                    Column = grid.Columns.Count - 1,
                    Row = i
                });
            InitGrid();
            GridChanged = true;
        }

    }
}
