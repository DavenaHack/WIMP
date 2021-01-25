namespace MIMP.Log
{
    public enum Level : uint
    {
        All = uint.MinValue,
        Debug = 0x1 << 2,
        Verbose = 0x1 << 3,
        Info = 0x1 << 4,
        Warning = 0x1 << 5,
        Error = 0x1 << 6,
        Fatal = 0x1 << 7,
        None = uint.MaxValue,
    }
}
