using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Domain.Helpers
{
    public class LogEntry
    {
        public string Event { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
    }

    public class Logger
    {
        private string _logPath = "C:\\Users\\User\\source\\repos\\atm-simulator\\Repository\\Data\\logs.json";

        public void Log(string eventName, string username)
        {
            var logs = LoadAll();

            logs.Add(new LogEntry
            {
                Event = eventName,
                Username = username,
                Date = DateTime.Now
            });

            string json = JsonConvert.SerializeObject(logs, Formatting.Indented);
            File.WriteAllText(_logPath, json);
        }

        private List<LogEntry> LoadAll()
        {
            if (!File.Exists(_logPath))
                return new List<LogEntry>();

            string json = File.ReadAllText(_logPath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<LogEntry>();

            return JsonConvert.DeserializeObject<List<LogEntry>>(json);
        }
    }
}