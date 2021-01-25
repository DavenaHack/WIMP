using System;

namespace MIMP.Input
{
    public interface IGlobalMouseHandler : IDisposable, IAsyncDisposable
    {

        /// <summary>
        /// Raised if mouse moved
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMove;

        /// <summary>
        /// Raised if any button down
        /// <seealso cref="MouseButton"/>
        /// <seealso cref="ButtonUp"/>
        /// <seealso cref="ButtonDoubleClick"/>
        /// </summary>
        public event EventHandler<ButtonEventArgs> ButtonDown;

        /// <summary>
        /// Raised if any button up
        /// <seealso cref="MouseButton"/>
        /// <seealso cref="MouseButton"/>
        /// <seealso cref="ButtonDown"/>
        /// <seealso cref="ButtonDoubleClick"/>
        /// </summary>
        public event EventHandler<ButtonEventArgs> ButtonUp;

        /// <summary>
        /// Raised if any button double click
        /// <seealso cref="MouseButton"/>
        /// <seealso cref="MouseButton"/>
        /// <seealso cref="ButtonDown"/>
        /// <seealso cref="ButtonDoubleClick"/>
        /// </summary>
        public event EventHandler<ButtonEventArgs> ButtonDoubleClick;

        /// <summary>
        /// Raised if vertical wheel scroll
        /// </summary>
        public event EventHandler<WheelEventArgs> Wheel;

        /// <summary>
        /// Raised if horizontal wheel scroll
        /// </summary>
        public event EventHandler<WheelEventArgs> HorizontalWheel;

    }
}
