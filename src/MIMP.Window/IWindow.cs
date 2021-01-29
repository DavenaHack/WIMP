using MIMP.SeeSharper;
using MIMP.SeeSharper.Events;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MIMP.Window
{
    /// <summary>
    /// Represent a UI
    /// </summary>
    public interface IWindow : INotifyPropertyChanging, INotifyPropertyChanged, IObjectEquatable<IWindow>, IDisposable, IAsyncDisposable
    {

        /// <summary>
        /// Raised if the window is moved
        /// </summary>
        public event EventHandler Moved;

        /// <summary>
        /// Raised if the window is resized
        /// </summary>
        public event EventHandler Resized;

        /// <summary>
        /// Raised if the window is closed
        /// </summary>
        public event EventHandler Closed;


        /// <summary>
        /// The title of the window
        /// </summary>
        public string Title { get; }


        /// <summary>
        /// The window state
        /// </summary>
        public WindowState State { get; set; }


        /// <summary>
        /// X position of the window
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y position of the window
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Width of the window
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the window
        /// </summary>
        public int Height { get; set; }


        /// <summary>
        /// Top of the window
        /// </summary>
        public int Top
        {
            get => Y;
            set
            {
                RaisePropertyChanging();
                if (Y != value)
                    Y = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Right of the window
        /// </summary>
        public int Right
        {
            get => X + Width;
            set
            {
                RaisePropertyChanging();
                value -= X;
                if (Width != value)
                    Width = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Bottom of the window
        /// </summary>
        public int Bottom
        {
            get => Y + Height;
            set
            {
                RaisePropertyChanging();
                value -= Y;
                if (Height != value)
                    Height = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Left of the window
        /// </summary>
        public int Left
        {
            get => X;
            set
            {
                RaisePropertyChanging();
                if (X != value)
                    X = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// The nearest screen
        /// </summary>
        public IScreen Screen { get; }


        /// <summary>
        /// Return a instance of <see cref="Rectangle"/> which represent the size of the window.
        /// 
        /// If the rectangle modified it will not change the properties of <paramref name="screen"/>.
        /// </summary>
        /// <returns></returns>
        public Rectangle GetPosition() =>
            new Rectangle(X, Y, Width, Height);

        /// <summary>
        /// Set the position of the window and redraw it.
        /// If the window is maximized it will automatically show as normal.
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(Rectangle position);

        /// <summary>
        /// Set the position of the window and redraw it.
        /// If the window is maximized it will automatically show as normal.
        /// </summary>
        public void SetPosition(int x, int y, int width, int height);


        /// <summary>
        /// Move the window and redraw it.
        /// If the window is maximized it will automatically show as normal.
        /// </summary>
        /// <param name="position"></param>
        public void Move(Point position);

        /// <summary>
        /// Move the window and redraw it.
        /// If the window is maximized it will automatically show as normal.
        /// </summary>
        public void Move(int x, int y);


        /// <summary>
        /// Resize the window and redraw it.
        /// If the window is maximized it will automatically show as normal.
        /// </summary>
        public void Resize(Size size);

        /// <summary>
        /// Resize the window and redraw it.
        /// If the window is maximized it will automatically show as normal.
        /// </summary>
        public void Resize(int width, int height);


        /// <summary>
        /// Hide the window.
        /// </summary>
        public void Hide();

        /// <summary>
        /// Show the window as normal window.
        /// </summary>
        public void Show();

        /// <summary>
        /// Maximize the window.
        /// </summary>
        public void Maximize();

        /// <summary>
        /// Minimize the window.
        /// </summary>
        public void Minimize();


        protected void RaisePropertyChanging([CallerMemberName] string propertyName = null) =>
            PropertyChangeEvents.RaisePropertyChanging(this, propertyName);

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChangeEvents.RaisePropertyChanged(this, propertyName);

        protected void RaiseMoved() =>
            Events.InvokeEvent(this, nameof(Moved));

        protected void RaiseResized() =>
            Events.InvokeEvent(this, nameof(Resized));

        protected void RaiseClosed() =>
            Events.InvokeEvent(this, nameof(Closed));

    }
}
