using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MIMP.Mathematics.Geometry
{
    public class IntVector : INotifyPropertyChanged, IEquatable<IntVector>
    {

        public static IntVector Zero() => new IntVector(0);


        public event PropertyChangedEventHandler PropertyChanged;


        private int _x;
        public int X
        {
            get => _x;
            set
            {
                if (_x == value)
                    return;
                _x = value;
                RaisePropertyChanged();
            }
        }


        private int _y;

        public int Y
        {
            get => _y;
            set
            {
                if (_y == value)
                    return;
                _y = value;
                RaisePropertyChanged();
            }
        }


        public IntVector(int xy)
            : this(xy, xy) { }

        public IntVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IntVector(System.Drawing.Point point)
            : this(point.X, point.Y) { }

        public IntVector(System.Drawing.Size size)
            : this(size.Width, size.Height) { }
        public IntVector(Tuple<int, int> pair)
            : this(pair.Item1, pair.Item2) { }


        public override bool Equals(object obj)
        {
            return obj is IntVector vector &&
                   _x == vector._x &&
                   _y == vector._y;
        }

        public bool Equals([AllowNull] IntVector other)
        {
            return Equals((object)other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y);
        }


        public override string ToString()
        {
            return $"({X},{Y})";
        }


        public static bool operator ==(IntVector left, IntVector right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(IntVector left, IntVector right)
        {
            return !(left == right);
        }

        public static IntVector operator +(IntVector left, IntVector right)
        {
            return new IntVector(left?._x ?? 0 + right?._x ?? 0, left?._y ?? 0 + right?._y ?? 0);
        }

        public static IntVector operator -(IntVector left, IntVector right)
        {
            return new IntVector(left?._x ?? 0 - right?._x ?? 0, left?._y ?? 0 - right?._y ?? 0);
        }

        public static IntVector operator *(IntVector left, IntVector right)
        {
            return new IntVector(left?._x ?? 0 * right?._x ?? 0, left?._y ?? 0 * right?._y ?? 0);
        }

        public static IntVector operator /(IntVector left, IntVector right)
        {
            return new IntVector(left?._x ?? 0 / right?._x ?? 0, left?._y ?? 0 / right?._y ?? 0);
        }

        public static IntVector operator +(IntVector left, int right)
        {
            return new IntVector(left?._x ?? 0 + right, left?._y ?? 0 + right);
        }

        public static IntVector operator -(IntVector left, int right)
        {
            return new IntVector(left?._x ?? 0 - right, left?._y ?? 0 - right);
        }

        public static IntVector operator *(IntVector left, int right)
        {
            return new IntVector(left?._x ?? 0 * right, left?._y ?? 0 * right);
        }

        public static IntVector operator /(IntVector left, int right)
        {
            return new IntVector(left?._x ?? 0 / right, left?._y ?? 0 / right);
        }


        public static implicit operator System.Drawing.Point(IntVector that)
        {
            return new System.Drawing.Point(that._x, that._y);
        }

        public static implicit operator System.Drawing.Size(IntVector that)
        {
            return new System.Drawing.Size(that._x, that._y);
        }

        public static implicit operator Tuple<int, int>(IntVector that)
        {
            return Tuple.Create(that._x, that._y);
        }


        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }


        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
