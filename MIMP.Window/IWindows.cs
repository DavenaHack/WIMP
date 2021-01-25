using MIMP.SeeSharper.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MIMP.Window
{
    /// <summary>
    /// A collection of all open windows.
    /// </summary>
    public interface IWindows : IEnumerable<IWindow>, INotifyPropertyChanging, INotifyPropertyChanged, INotifyCollectionChanged, IDisposable, IAsyncDisposable
    {

        /// <summary>
        /// Raised if a new window is created
        /// </summary>
        public event EventHandler<WindowEventArgs> Created;

        /// <summary>
        /// Raised if a window closed
        /// </summary>
        public event EventHandler<WindowEventArgs> Closed;

        /// <summary>
        /// Count of open windows
        /// </summary>
        public int Count { get; }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();


        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChangeEvents.RaisePropertyChanged(this, propertyName);

        protected virtual void RaisePropertyChanging([CallerMemberName] string propertyName = null) =>
            PropertyChangeEvents.RaisePropertyChanging(this, propertyName);

        protected virtual void RaiseCollectionChanged(NotifyCollectionChangedEventArgs eventArgs) =>
            PropertyChangeEvents.RaiseCollectionChanged(this, eventArgs);

        protected virtual void RaiseCreated(IScreen screen) =>
            Events.InvokeEvent(this, nameof(Created), new ScreenEventArgs(screen));

        protected virtual void RaiseClosed(IScreen screen) =>
            Events.InvokeEvent(this, nameof(Closed), new ScreenEventArgs(screen));

    }
}
