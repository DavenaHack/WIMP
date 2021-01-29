using System;

namespace MIMP.Window
{
    public class WindowEventArgs : EventArgs
    {

        public IWindow Window { get; }


        public WindowEventArgs(IWindow window)
        {
            Window = window;
        }

    }
}
