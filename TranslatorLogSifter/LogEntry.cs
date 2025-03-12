using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorLogSifter
{
    public class LogEntry
    {
     
        public static LogEntry BuildLogEntry(string s)
        {
            var logEntry = new LogEntry();
            DateTime result;
            if (s.Length > 24 && DateTime.TryParse(s.Substring(0, 24), out result))
                logEntry.LogEntryDate = result;
            logEntry.LogLine = s;
            string[] strArray = s.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length <= 4)
            {
                logEntry.Type = "E";
            }
            else
            {
                logEntry.Type = strArray[2];
            }
            return logEntry;
        }

        public DateTime LogEntryDate { get; set; }

        public string LogLine { get; set; }

        public string Type { get; set; }
    }
}
