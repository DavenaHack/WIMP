using System;

namespace MIMP.Window
{
    public class ScreenEventArgs : EventArgs
    {

        public IScreen Screen { get; }


        public ScreenEventArgs(IScreen screen)
        {
            Screen = screen ?? throw new ArgumentNullException(nameof(screen));
        }

    }
}
