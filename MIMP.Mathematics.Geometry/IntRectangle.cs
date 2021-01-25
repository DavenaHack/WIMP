using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MIMP.Mathematics.Geometry
{
    public class IntRectangle : INotifyPropertyChanged, IEquatable<IntRectangle>
    {

        public event PropertyChangedEventHandler PropertyChanged;


        private IntVector _position;

        public IntVector Position
        {
            get => _position;
            set
            {
                var p = _position;
                p.PropertyChanged -= PositionPropertyChanged;
                _position = value ?? IntVector.Zero();
                _position.PropertyChanged += PositionPropertyChanged;
                RaisePropertyChanged();
                if (p.X != _position.X)
                {
                    RaisePropertyChanged(nameof(X));
                    RaisePropertyChanged(nameof(Left));
                }
                if (p.Y != _position.Y)
                {
                    RaisePropertyChanged(nameof(Y));
                    RaisePropertyChanged(nameof(Top));
                }
            }
        }


        private IntVector _size;

        public IntVector Size
        {
            get => _size;
            set
            {
                var s = _size;
                s.PropertyChanged -= SizePropertyChanged;
                _size = value ?? IntVector.Zero();
                _size.PropertyChanged += SizePropertyChanged;
                RaisePropertyChanged();
                if (s.X != _size.X)
                {
                    RaisePropertyChanged(nameof(Width));
                    RaisePropertyChanged(nameof(Right));
                }
                if (s.Y != _size.Y)
                {
                    RaisePropertyChanged(nameof(Height));
                    RaisePropertyChanged(nameof(Bottom));
                }
            }
        }

        public int X
        {
            get => Position.X;
            set => Position.X = value;
        }

        public int Y
        {
            get => Position.Y;
            set => Position.Y = value;
        }

        public int Left
        {
            get => Position.X;
            set => Position.X = value;
        }

        public int Top
        {
            get => Position.Y;
            set => Position.Y = value;
        }

        public int Width
        {
            get => Size.X;
            set => Size.X = value;
        }

        public int Height
        {
            get => Size.Y;
            set => Size.Y = value;
        }

        public int Right
        {
            get => Size.X;
            set => Size.X = value;
        }

        public int Bottom
        {
            get => Size.Y;
            set => Size.Y = value;
        }


        public IntRectangle(IntVector position, IntVector size)
        {
            Position = position;
            Size = size;
        }

        public IntRectangle(System.Drawing.Point position, System.Drawing.Size size)
            : this(new IntVector(position), new IntVector(size)) { }

        public IntRectangle(int x, int y, int width, int height)
            : this(new IntVector(x, y), new IntVector(width, height)) { }

        public IntRectangle(System.Drawing.Rectangle rectangle)
            : this(rectangle.Location, rectangle.Size) { }


        public static implicit operator System.Drawing.Rectangle(IntRectangle that)
        {
            return new System.Drawing.Rectangle(that.Position, that.Size);
        }

        public static implicit operator Tuple<int, int, int, int>(IntRectangle that)
        {
            return Tuple.Create(that.X, that.Y, that.Width, that.Height);
        }

        public void Deconstruct(out int x, out int y, out int width, out int height)
        {
            x = X;
            y = Y;
            width = Width;
            height = Height;
        }

        public void Deconstruct(out IntVector position, out IntVector size)
        {
            position = Position;
            size = Size;
        }


        public bool Equals([AllowNull] IntRectangle other)
        {
            return Equals((object)other);
        }

        public override bool Equals(object obj)
        {
            return obj is IntRectangle rectangle &&
                   EqualityComparer<IntVector>.Default.Equals(_position, rectangle._position) &&
                   EqualityComparer<IntVector>.Default.Equals(_size, rectangle._size);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_position, _size);
        }

        public override string ToString()
        {
            return base.ToString();
        }


        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void SizePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IntVector.X))
            {
                RaisePropertyChanged(nameof(Width));
                RaisePropertyChanged(nameof(Right));
            }
            else if (e.PropertyName == nameof(IntVector.Y))
            {
                RaisePropertyChanged(nameof(Height));
                RaisePropertyChanged(nameof(Bottom));
            }
        }

        private void PositionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IntVector.X))
            {
                RaisePropertyChanged(nameof(X));
                RaisePropertyChanged(nameof(Left));
            }
            else if (e.PropertyName == nameof(IntVector.Y))
            {
                RaisePropertyChanged(nameof(Y));
                RaisePropertyChanged(nameof(Top));
            }
        }

    }
}
