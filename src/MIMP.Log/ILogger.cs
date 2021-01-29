namespace MIMP.Log
{
    public interface ILogger
    {

        public ILogStrategy Strategy { get; set; }

        public Level Level { get; set; }


        public void Log(object sender, Level level, string message);


        public void Debug(object sender, string message) =>
            Log(sender, Level.Debug, message);

        public void Verbose(object sender, string message) =>
            Log(sender, Level.Verbose, message);

        public void Info(object sender, string message) =>
            Log(sender, Level.Info, message);

        public void Warning(object sender, string message) =>
            Log(sender, Level.Warning, message);

        public void Error(object sender, string message) =>
            Log(sender, Level.Error, message);

        public void Fatal(object sender, string message) =>
            Log(sender, Level.Fatal, message);

    }
}
