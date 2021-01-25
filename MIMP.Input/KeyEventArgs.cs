using System;

namespace MIMP.Input
{
    public class KeyEventArgs : EventArgs
    {

        public Key Key { get; }


        public bool Suppress { get; set; }


        public KeyEventArgs(Key key)
        {
            Key = key;
            Suppress = false;
        }

    }
}
