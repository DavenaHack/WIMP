using System;

namespace MIMP.OperatingSystem.Windows.Keyboard
{
    public class StringKeyboardEventArgs : EventArgs
    {

        public string Value { get; }


        public StringKeyboardEventArgs(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

    }
}
