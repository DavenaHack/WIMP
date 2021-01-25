using MIMP.SeeSharper.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MIMP.Window
{
    /// <summary>
    /// A collection of all connected screens.
    /// </summary>
    public interface IScreens : IEnumerable<IScreen>, INotifyPropertyChanged, INotifyPropertyChanging, INotifyCollectionChanged, IDisposable, IAsyncDisposable
    {

        /// <summary>
        /// Raised if a new screen is connected
        /// </summary>
        public event EventHandler<ScreenEventArgs> Connect;

        /// <summary>
        /// Raised if the connection to a screen is lost
        /// </summary>
        public event EventHandler<ScreenEventArgs> Disconnect;

        /// <summary>
        /// Count of screens
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Returns the nearest screen to the <paramref name="window"/>
        /// </summary>
        /// <param name="window"></param>
        /// <returns>nearest screen</returns>
        public IScreen this[IWindow window] =>
            this[window.GetPosition()];

        /// <summary>
        /// Resturns the nearest screen to the <paramref name="position"/>
        /// </summary>
        /// <param name="position"></param>
        /// <returns>nearest screen</returns>
        public IScreen this[Rectangle position]
        {
            get
            {
                IScreen r = null;
                var d = int.MaxValue;
                foreach (var s in this)
                {
                    var sd = 0;
                    if (position.Top > s.Bottom)
                        sd += position.Top - s.Bottom;
                    if (position.Right < s.Left)
                        sd += s.Left - position.Right;
                    if (position.Bottom < s.Top)
                        sd += s.Top - position.Bottom;
                    if (position.Left > s.Right)
                        sd += position.Left - s.Right;
                    if (sd == 0)
                        return r;
                    if (sd < d)
                    {
                        d = sd;
                        r = s;
                    }
                }
                return r;
            }
        }


        /// <summary>
        /// The next top screen. 
        /// If <paramref name="loop"/> is true and <paramref name="position"/> is on the top screen the next screen will be the left screen.
        /// If there no other top screen it will return null
        /// </summary>
        /// <param name="position"></param>
        /// <param name="loop"></param>
        /// <returns>next top window</returns>
        public IScreen Top(Rectangle position, IScreen screen = null, bool loop = true)
        {
            screen ??= this[position];
            var ss = new List<IScreen>();
            IScreen r = null;
            var d = int.MaxValue;
            foreach (var s in this)
            {
                if (s.Equals(screen))
                    continue;
                if (s.Right >= position.Left && s.Left <= position.Right)
                    ss.Add(s);
                if (screen.Top != s.Bottom)
                    continue;
                var nd = 0;
                if (position.Left < s.Left)
                    nd += position.Left - s.Left;
                if (position.Right > s.Right)
                    nd += position.Right - s.Right;
                if (nd == 0)
                    return s;
                if (nd < d)
                {
                    d = nd;
                    r = s;
                }
            }
            if (r == null && loop)
            {
                d = int.MaxValue;
                var b = int.MaxValue;
                foreach (var s in ss)
                {
                    if (s.Equals(screen))
                        continue;
                    if (s.Top < screen.Bottom)
                        continue;
                    var nb = s.Top - position.Top;
                    if (nb < b)
                    {
                        var nd = 0;
                        if (position.Left < s.Left)
                            nd += position.Left - s.Left;
                        if (position.Right > s.Right)
                            nd += position.Right - s.Right;
                        if (nd < d)
                        {
                            b = nb;
                            d = nd;
                            r = s;
                        }
                    }
                }
            }
            return r;
        }

        /// <summary>
        /// The next right screen. 
        /// If <paramref name="loop"/> is true and <paramref name="position"/> is on the right screen the next screen will be the left screen.
        /// If there no other right screen it will return null
        /// </summary>
        /// <param name="position"></param>
        /// <param name="loop"></param>
        /// <returns>next right window</returns>
        public IScreen Right(Rectangle position, IScreen screen = null, bool loop = true)
        {
            screen ??= this[position];
            var ss = new List<IScreen>();
            IScreen r = null;
            var d = int.MaxValue;
            foreach (var s in this)
            {
                if (s.Equals(screen))
                    continue;
                if (s.Bottom >= position.Top && s.Top <= position.Bottom)
                    ss.Add(s);
                if (screen.Right != s.Left)
                    continue;
                var nd = 0;
                if (position.Top < s.Top)
                    nd += position.Top - s.Top;
                if (position.Bottom > s.Bottom)
                    nd += position.Bottom - s.Bottom;
                if (nd == 0)
                    return s;
                if (nd < d)
                {
                    d = nd;
                    r = s;
                }
            }
            if (r == null && loop)
            {
                d = int.MaxValue;
                var b = int.MaxValue;
                foreach (var s in ss)
                {
                    if (s.Equals(screen))
                        continue;
                    if (s.Right > screen.Left)
                        continue;
                    var nb = s.Right - position.Right;
                    if (nb < b)
                    {
                        var nd = 0;
                        if (position.Top < s.Top)
                            nd += position.Top - s.Top;
                        if (position.Bottom > s.Bottom)
                            nd += position.Bottom - s.Bottom;
                        if (nd < d)
                        {
                            b = nb;
                            d = nd;
                            r = s;
                        }
                    }
                }
            }
            return r;
        }


        /// <summary>
        /// The next down screen. 
        /// If <paramref name="loop"/> is true and <paramref name="position"/> is on the down screen the next screen will be the left screen.
        /// If there no other down screen it will return null
        /// </summary>
        /// <param name="position"></param>
        /// <param name="loop"></param>
        /// <returns>next down window</returns>
        public IScreen Down(Rectangle position, IScreen screen = null, bool loop = true)
        {
            screen ??= this[position];
            var ss = new List<IScreen>();
            IScreen r = null;
            var d = int.MaxValue;
            foreach (var s in this)
            {
                if (s.Equals(screen))
                    continue;
                if (s.Right >= position.Left && s.Left <= position.Right)
                    ss.Add(s);
                if (screen.Bottom != s.Top)
                    continue;
                var nd = 0;
                if (position.Left < s.Left)
                    nd += position.Left - s.Left;
                if (position.Right > s.Right)
                    nd += position.Right - s.Right;
                if (nd == 0)
                    return s;
                if (nd < d)
                {
                    d = nd;
                    r = s;
                }
            }
            if (r == null && loop)
            {
                d = int.MaxValue;
                var b = int.MaxValue;
                foreach (var s in ss)
                {
                    if (s.Equals(screen))
                        continue;
                    if (s.Bottom > screen.Top)
                        continue;
                    var nb = s.Bottom - position.Bottom;
                    if (nb < b)
                    {
                        var nd = 0;
                        if (position.Left < s.Left)
                            nd += position.Left - s.Left;
                        if (position.Right > s.Right)
                            nd += position.Right - s.Right;
                        if (nd < d)
                        {
                            b = nb;
                            d = nd;
                            r = s;
                        }
                    }
                }
            }
            return r;
        }


        /// <summary>
        /// The next left screen. 
        /// If <paramref name="loop"/> is true and <paramref name="position"/> is on the left screen the next screen will be the left screen.
        /// If there no other left screen it will return null
        /// </summary>
        /// <param name="position"></param>
        /// <param name="loop"></param>
        /// <returns>next left window</returns>
        public IScreen Left(Rectangle position, IScreen screen = null, bool loop = true)
        {
            screen ??= this[position];
            var ss = new List<IScreen>();
            IScreen r = null;
            var d = int.MaxValue;
            foreach (var s in this)
            {
                if (s.Equals(screen))
                    continue;
                if (s.Bottom >= position.Top && s.Top <= position.Bottom)
                    ss.Add(s);
                if (screen.Left != s.Right)
                    continue;
                var nd = 0;
                if (position.Top < s.Top)
                    nd += position.Top - s.Top;
                if (position.Bottom > s.Bottom)
                    nd += position.Bottom - s.Bottom;
                if (nd == 0)
                    return s;
                if (nd < d)
                {
                    d = nd;
                    r = s;
                }
            }
            if (r == null && loop)
            {
                d = int.MaxValue;
                var b = int.MaxValue;
                foreach (var s in ss)
                {
                    if (s.Equals(screen))
                        continue;
                    if (s.Left < screen.Right)
                        continue;
                    var nb = s.Left - position.Left;
                    if (nb < b)
                    {
                        var nd = 0;
                        if (position.Top < s.Top)
                            nd += position.Top - s.Top;
                        if (position.Bottom > s.Bottom)
                            nd += position.Bottom - s.Bottom;
                        if (nd < d)
                        {
                            b = nb;
                            d = nd;
                            r = s;
                        }
                    }
                }
            }
            return r;
        }


        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

    }
}
