using MIMP.SeeSharper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MIMP.WIMP
{
    public class GridLength : ICloneable<GridLength>, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        private int _value;

        public int Value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;
                _value = value;
                RaisePropertyChanged();
            }
        }


        private GridUnit _unit;

        public GridUnit Unit
        {
            get => _unit;
            set
            {
                if (_unit == value)
                    return;
                _unit = value;
                RaisePropertyChanged();
            }
        }


        public GridLength(int value, GridUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        public GridLength() : this(1, GridUnit.Star) { }


        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GridLength Clone()
        {
            return new GridLength(Value, Unit);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

    }


    public static class GridLengthExtensions
    {

        public static IList<(int, int)> CalcSizes(this IEnumerable<GridLength> lengths, int start, int size)
        {
            var ls = lengths.ToArray();
            var sizes = new int[ls.Length];
            var j = 0;
            var ss = 0;
            var stars = new List<(int, GridLength)>();
            for (var i = 0; i < ls.Length; i++)
            {
                var r = ls[i];
                switch (r.Unit)
                {
                    case GridUnit.Pixel:
                        sizes[i] = r.Value;
                        size -= r.Value;
                        j++;
                        break;
                    case GridUnit.Star:
                        ss += r.Value;
                        stars.Add((i, r));
                        break;
                    default:
                        break;
                }
            }
            if (stars.Count > 0)
            {
                var sWidth = size / ss;
                foreach (var (i, r) in stars)
                {
                    if (j + 1 == ls.Length)
                        sizes[i] = size;
                    else
                    {
                        var w = r.Value * sWidth;
                        sizes[i] = w;
                        size -= w;
                    }
                    j++;
                }
            }
            var se = new List<(int, int)>();
            var x = start;
            foreach (var s in sizes)
            {
                var y = x + s;
                se.Add((x, y));
                x = y;
            }
            return se;
        }

    }

}
