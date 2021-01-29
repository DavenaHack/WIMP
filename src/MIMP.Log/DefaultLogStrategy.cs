namespace MIMP.Log
{
    public class DefaultLogStrategy : ILogStrategy
    {

        public bool Log(Level logLevel, Level level) =>
            level >= logLevel;

    }
}
