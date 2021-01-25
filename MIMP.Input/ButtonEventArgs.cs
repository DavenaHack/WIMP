namespace MIMP.Input
{
    public class ButtonEventArgs : MouseEventArgs
    {

        public MouseButton Button { get; }


        public ButtonEventArgs(int x, int y, MouseButton button) : base(x, y)
        {
            Button = button;
        }


        public override string ToString()
        {
            return $"{Button.GetName()} {base.ToString()}";
        }

    }
}
