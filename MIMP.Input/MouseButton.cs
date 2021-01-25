namespace MIMP.Input
{

    public enum MouseButton : byte
    {
        Left = 0,
        Middle = Auxiliary,
        Auxiliary = 1,
        Wheel = Auxiliary,
        Right = Secondary,
        Secondary = 2,
        Back = Fourth,
        Fourth = 3,
        Forward = Fifth,
        Fifth = 4,
    }

    public static class MouseButtons
    {

        public static string GetName(this MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => "Left Button",
                MouseButton.Middle => "Middle Button",
                MouseButton.Right => "Right Button",
                MouseButton.Back => "Backward Button",
                MouseButton.Forward => "Forward Button",
                _ => $"Button {(int)button + 1}",
            };
        }

    }

}
