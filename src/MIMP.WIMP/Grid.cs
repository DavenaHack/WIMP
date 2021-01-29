using MIMP.SeeSharper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MIMP.WIMP
{
    public class Grid : ICloneable<Grid>, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        private ObservableCollection<GridLength> _rows;

        public ObservableCollection<GridLength> Rows
        {
            get => _rows ?? (Rows = new ObservableCollection<GridLength>());
            set
            {
                if (_rows == value)
                    return;
                _rows = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<GridLength> _columns;

        public ObservableCollection<GridLength> Columns
        {
            get => _columns ?? (Columns = new ObservableCollection<GridLength>());
            set
            {
                if (_columns == value)
                    return;
                _columns = value;
                RaisePropertyChanged();
            }
        }



        private ObservableCollection<GridCell> _cells;

        public ObservableCollection<GridCell> Cells
        {
            get => _cells ?? (Cells = new ObservableCollection<GridCell>());
            set
            {
                if (_cells == value)
                    return;
                _cells = value;
                RaisePropertyChanged();
            }
        }


        public Grid()
        {
        }


        public GridCell Top(GridCell cell)
        {
            if (!Cells.Contains(cell))
                return null;
            return At(cell.Column, cell.Row - 1);
        }

        public GridCell Right(GridCell cell)
        {
            if (!Cells.Contains(cell))
                return null;
            return At(cell.Column + cell.ColumnSpan, cell.Row);
        }

        public GridCell Bottom(GridCell cell)
        {
            if (!Cells.Contains(cell))
                return null;
            return At(cell.Column, cell.Row + cell.RowSpan);
        }

        public GridCell Left(GridCell cell)
        {
            if (!Cells.Contains(cell))
                return null;
            return At(cell.Column - 1, cell.Row);
        }


        public GridCell At(int column, int row)
        {
            return Ats(column, row).FirstOrDefault();
        }

        public IEnumerable<GridCell> Ats(int column, int row, int columnSpan = 1, int rowSpan = 1)
        {
            foreach (var c in Cells)
                if (c.Column < column + columnSpan && c.Column + c.ColumnSpan > column
                    && c.Row < row + rowSpan && c.Row + c.RowSpan > row)
                    yield return c;
        }


        public Tuple<GridCell, Rectangle, int, int> At(Rectangle size, Rectangle search)
        {
            if (Cells.Count < 1)
                return null;
            var columns = Columns.CalcSizes(size.X, size.Width);
            var rows = Rows.CalcSizes(size.Y, size.Height);
            var sci = 0;
            var sc = columns[sci].Item1;
            var sri = 0;
            var sr = rows[sri].Item1;
            var eci = columns.Count - 1;
            var ec = columns[eci].Item2;
            var eri = rows.Count - 1;
            var er = rows[eri].Item2;
            for (var i = 0; i < columns.Count; i++)
            {
                var c = columns[i];
                if (Math.Abs(sc - search.Left) > Math.Abs(c.Item1 - search.Left))
                {
                    sc = c.Item1;
                    sci = i;
                }
                if (Math.Abs(ec - search.Right) > Math.Abs(c.Item2 - search.Right))
                {
                    ec = c.Item2;
                    eci = i;
                }
            }
            for (var i = 0; i < rows.Count; i++)
            {
                var r = rows[i];
                if (Math.Abs(sr - search.Top) > Math.Abs(r.Item1 - search.Top))
                {
                    sr = r.Item1;
                    sri = i;
                }
                if (Math.Abs(er - search.Bottom) > Math.Abs(r.Item2 - search.Bottom))
                {
                    er = r.Item2;
                    eri = i;
                }
            }
            var sg = At(sci, sri);
            var cg = At(eci, sg.Row);
            var xg = XCells(sg, cg);
            var rg = At(sg.Column, eri);
            var yg = YCells(sg, rg);
            return Tuple.Create(sg, Rectangle.FromLTRB(columns[sg.Column].Item1, rows[sg.Row].Item1, columns[cg.Column + cg.ColumnSpan - 1].Item2, rows[rg.Row + rg.RowSpan - 1].Item2), xg, yg);
        }


        public Rectangle Position(GridCell cell, Rectangle size, int xGrids = 1, int yGrids = 1)
        {
            if (Cells.Count < 1)
                return default;
            return Position(cell, Columns.CalcSizes(size.X, size.Width), Rows.CalcSizes(size.Y, size.Height), xGrids, yGrids);
        }


        protected Rectangle Position(GridCell cell, IList<(int, int)> columns, IList<(int, int)> rows, int xGrids = 1, int yGrids = 1)
        {
            var x = columns[cell.Column].Item1;
            var y = rows[cell.Row].Item1;
            var g = cell;
            var columnEnd = cell.Column + cell.ColumnSpan - 1;
            while (--xGrids > 0)
            {
                g = At(g.Column + g.ColumnSpan, cell.Row);
                if (g == null)
                    break;
                columnEnd += g.ColumnSpan;
            }
            g = cell;
            var rowEnd = cell.Row + cell.RowSpan - 1;
            while (--yGrids > 0)
            {
                g = At(cell.Column, g.Row + g.RowSpan);
                if (g == null)
                    break;
                rowEnd += g.RowSpan;
            }
            return Rectangle.FromLTRB(x, y, columns[columnEnd].Item2, rows[rowEnd].Item2);
        }


        protected int XCells(GridCell a, GridCell b)
        {
            if (a == b)
                return 1;
            var g = a;
            int x = 1;
            while (x++ < Columns.Count)
            {
                if (g == null)
                    break;
                g = At(g.Column + g.ColumnSpan, a.Row);
                if (g == b)
                    return x;
            }
            return -1;
        }

        public int YCells(GridCell a, GridCell b)
        {
            if (a == b)
                return 1;
            var g = a;
            int x = 1;
            while (x++ < Rows.Count)
            {
                g = At(a.Column, g.Row + g.RowSpan);
                if (g == null)
                    break;
                if (g == b)
                    return x;
            }
            return -1;
        }




        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Grid Clone()
        {
            return new Grid
            {
                Rows = new ObservableCollection<GridLength>(Rows.Select(x => x.Clone())),
                Columns = new ObservableCollection<GridLength>(Columns.Select(x => x.Clone())),
                Cells = new ObservableCollection<GridCell>(Cells.Select(x => x.Clone()))
            };
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
