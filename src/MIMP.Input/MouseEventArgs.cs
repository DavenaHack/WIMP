using System;

namespace MIMP.Input
{
    public class MouseEventArgs : EventArgs
    {

        public int X { get; }

        public int Y { get; }


        public bool Suppress { get; set; }


        public MouseEventArgs(int x, int y)
        {
            X = x;
            Y = y;
            Suppress = false;
        }


        public override string ToString()
        {
            return $"({X},{Y})";
        }

    }
}
