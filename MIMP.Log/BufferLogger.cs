using MIMP.Log;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MIMP.WIMP.WPF
{
    public class BufferLogger : ILogger
    {

        public ILogStrategy Strategy { get; set; }

        public Level Level { get; set; }

        public int Capacity { get; }

        public ObservableCollection<string> Logs { get; }


        public BufferLogger(Level level = Level.All, ILogStrategy strategy = null, int capacity = 1024)
        {
            Logs = new ObservableCollection<string>(new LinkedList<string>());
            Level = level;
            Strategy = strategy ?? new DefaultLogStrategy();
            Capacity = capacity;
        }


        public void Log(object sender, Level level, string message)
        {
            if (Strategy.Log(Level, level))
            {
                Logs.Add($"{DateTime.Now:O} [{level}]: {sender} - {message}");
                if (Logs.Count > Capacity)
                    Logs.RemoveAt(0);
            }
        }

    }
}
