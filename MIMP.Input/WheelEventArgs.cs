namespace MIMP.Input
{
    public class WheelEventArgs : MouseEventArgs // TODO delta
    {

        public bool Forward { get; }


        public WheelEventArgs(int x, int y, bool forward) : base(x, y)
        {
            Forward = forward;
        }


        public override string ToString()
        {
            return $"{(Forward ? "forward" : "backward")} {base.ToString()}";
        }

    }
}
