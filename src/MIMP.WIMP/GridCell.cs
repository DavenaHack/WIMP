using MIMP.SeeSharper;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MIMP.WIMP
{
    public class GridCell : ICloneable<GridCell>, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        private int _row;
        public int Row
        {
            get => _row;
            set
            {
                if (_row == value)
                    return;
                _row = value;
                RaisePropertyChanged();
            }
        }


        private int _rowSpan = 1;
        public int RowSpan
        {
            get => _rowSpan;
            set
            {
                if (_rowSpan == value)
                    return;
                _rowSpan = value;
                RaisePropertyChanged();
            }
        }


        private int _column;
        public int Column
        {
            get => _column;
            set
            {
                if (_column == value)
                    return;
                _column = value;
                RaisePropertyChanged();
            }
        }


        private int _columnSpan = 1;
        public int ColumnSpan
        {
            get => _columnSpan;
            set
            {
                if (_columnSpan == value)
                    return;
                _columnSpan = value;
                RaisePropertyChanged();
            }
        }


        public GridCell()
        {
        }


        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public GridCell Clone()
        {
            return new GridCell
            {
                Column = Column,
                ColumnSpan = ColumnSpan,
                Row = Row,
                RowSpan = RowSpan
            };
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

    }
}
