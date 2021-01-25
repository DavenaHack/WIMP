using MIMP.SeeSharper.Events;
using System;
using System.Collections.Generic;

namespace MIMP.Input
{
    /// <summary>
    /// Listen to the events of keyboard
    /// </summary>
    public interface IGlobalKeyboardHandler : IDisposable, IAsyncDisposable
    {

        /// <summary>
        /// Raised if key pressed/down
        /// </summary>
        /// <seealso cref="KeyUp"/>
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <summary>
        /// Raised if key released/up
        /// </summary>
        /// <seealso cref="KeyDown"/>
        public event EventHandler<KeyEventArgs> KeyUp;


        /// <summary>
        /// Reinitialize the handler in this way is on the top of the listener stack and will execute at first.
        /// </summary>
        public void Reinitialize();


        /// <summary>
        /// Press the <paramref name="key"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <seealso cref="Up(Key)"/>
        public void Down(Key key);

        /// <summary>
        /// Release the <paramref name="key"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <seealso cref="Down(Key)"/>
        public void Up(Key key);


        /// <summary>
        /// Get the current state of the <paramref name="key"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <seealso cref="SetState(Key, KeyState)"/>
        /// <seealso cref="AddState(Key, KeyState)"/>
        /// <seealso cref="RemoveState(Key, KeyState)"/>
        public KeyState GetState(Key key);

        /// <summary>
        /// Set the <paramref name="state"/> of the <paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="state"></param>        
        /// <seealso cref="GetState(Key, KeyState)"/>
        /// <seealso cref="AddState(Key, KeyState)"/>
        /// <seealso cref="RemoveState(Key, KeyState)"/>
        public void SetState(Key key, KeyState state);

        /// <summary>
        /// Add the <paramref name="state"/> to the <paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="state"></param>
        /// <seealso cref="GetState(Key, KeyState)"/>
        /// <seealso cref="SetState(Key, KeyState)"/>
        /// <seealso cref="RemoveState(Key, KeyState)"/>
        public void AddState(Key key, KeyState state);

        /// <summary>
        /// Remove the <paramref name="state"/> from the <paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="state"></param>
        /// <seealso cref="GetState(Key, KeyState)"/>
        /// <seealso cref="SetState(Key, KeyState)"/>
        /// <seealso cref="AddState(Key, KeyState)"/>
        public void RemoveState(Key key, KeyState state);


        /// <summary>
        /// Get the <see cref="KeyState" /> of all <see cref="Key">keys</see>.
        /// </summary>
        /// <returns></returns>
        public IDictionary<Key, KeyState> GetStates();

        /// <summary>
        /// Returns all pressed keys. If that method is called in <see cref="KeyDown" /> event the down key is excluded on the first raised.
        /// </summary>
        /// <returns></returns>
        public ISet<Key> GetPressedKeys();

        /// <summary>
        /// Returns all toggled keys. If that method is called in <see cref="KeyDown" /> event the down key is excluded on the first raised.
        /// </summary>
        /// <returns></returns>
        public ISet<Key> GetToggledKeys();


        /// <summary>
        /// Raise the event in reversed order of the registration.
        /// </summary>
        /// <param name="args"></param>
        protected void RaiseKeyDown(KeyEventArgs args) =>
            Events.InvokeEventReverse(this, nameof(KeyDown), args, (_, _, e) => e.Suppress);

        /// <summary>
        /// Raise the event in reversed order of the registration.
        /// </summary>
        /// <param name="args"></param>
        protected void RaiseKeyUp(KeyEventArgs args) =>
            Events.InvokeEventReverse(this, nameof(KeyUp), args, (_, _, e) => e.Suppress);

    }
}