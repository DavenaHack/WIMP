namespace MIMP.Log
{
    public interface ILogStrategy
    {

        public bool Log(Level logLevel, Level level);

    }
}
