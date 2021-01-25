using MIMP.SeeSharper;
using MIMP.SeeSharper.Events;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MIMP.Window
{
    /// <summary>
    /// Represent a area where windows can display.
    /// </summary>
    public interface IScreen : INotifyPropertyChanging, INotifyPropertyChanged, IObjectEquatable<IScreen>, IDisposable, IAsyncDisposable
    {

        /// <summary>
        /// Raised if the connection to the screen is lost
        /// </summary>
        public event EventHandler Disconnect;


        /// <summary>
        /// The number of the screen
        /// </summary>
        public string ID { get; }


        /// <summary>
        /// X position of the size
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Y position of the size
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Width of the size
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Height of the size
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// The top of the screen
        /// <seealso cref="Y"/>
        /// </summary>
        public int Top => Y;

        /// <summary>
        /// The right of the screen
        /// </summary>
        public int Right => X + Width;

        /// <summary>
        /// The bottom of the screen
        /// </summary>
        public int Bottom => Y + Height;

        /// <summary>
        /// The left of the screen
        /// <seealso cref="X"/>
        /// </summary>
        public int Left => X;

        /// <summary>
        /// Return a instance of <see cref="Rectangle"/> which represent the size of the <paramref name="screen"/>.
        /// 
        /// If the rectangle modified it will not change the properties of <paramref name="screen"/>.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public Rectangle GetBounds() =>
            new Rectangle(X, Y, Width, Height);


        /// <summary>
        /// X position of the area
        /// </summary>
        public int WorkAreaX { get; }

        /// <summary>
        /// Y position of the area
        /// </summary>
        public int WorkAreaY { get; }

        /// <summary>
        /// Width of the area
        /// </summary>
        public int WorkAreaWidth { get; }

        /// <summary>
        /// Height of the area
        /// </summary>
        public int WorkAreaHeight { get; }

        /// <summary>
        /// The top of the work area
        /// </summary>
        public int WorkAreaTop => WorkAreaY;

        /// <summary>
        /// The right of the work area
        /// </summary>
        public int WorkAreaRight => WorkAreaX + WorkAreaWidth;

        /// <summary>
        /// The bottom of the work area
        /// </summary>
        public int WorkAreaBottom => WorkAreaY + WorkAreaHeight;

        /// <summary>
        /// The left of the work area
        /// </summary>
        public int WorkAreaLeft => WorkAreaX;

        /// <summary>
        /// Return a instance of <see cref="Rectangle"/> which represent the work area of the <paramref name="screen"/>.
        /// 
        /// If the rectangle modified it will not change the properties of <paramref name="screen"/>.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns>screen area</returns>
        public Rectangle GetWorkArea() =>
            new Rectangle(WorkAreaX, WorkAreaY, WorkAreaWidth, WorkAreaHeight);


        bool IObjectEquatable<IScreen>.Equals(IScreen other) =>
            other is IScreen screen && ID == screen.ID;

    }

}
