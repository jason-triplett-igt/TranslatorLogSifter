using TranslatorLogSifter;


string currentDirectory = Environment.CurrentDirectory;
Console.WriteLine("Current Directory:");
Console.WriteLine(currentDirectory);
LinkedList<LogEntry> logEntriesW = new LinkedList<LogEntry>();
LinkedList<LogEntry> logEntriesE = new LinkedList<LogEntry>();
LinkedList<LogEntry> logEntriesHttp = new LinkedList<LogEntry>();
string[] files = Directory.GetFiles(currentDirectory, "*.log");
string[] strArray = files.Length != 0 ? files : throw new ArgumentException("No .log files in that location");
for (int index = 0; index < strArray.Length; ++index)
{
    using (StreamReader reader = new StreamReader(strArray[index]))
    {
        while (((TextReader)reader).Peek() > -1)
        {
            LogEntry logEntry = LogEntry.BuildLogEntry(await ((TextReader)reader).ReadLineAsync());
            if (logEntry.LogEntryDate != DateTime.MinValue)
            {
                if (logEntry.Type == "E")
                    logEntriesE.AddLast(logEntry);
                else if (logEntry.Type == "W")
                    logEntriesW.AddLast(logEntry);
                if (logEntry.LogLine.Contains("HTTPSocket"))
                    logEntriesHttp.AddLast(logEntry);
            }
        }
    }
}
strArray = (string[])null;
Task task = File.WriteAllLinesAsync(Environment.CurrentDirectory + "\\outputE.log", (IEnumerable<string>)logEntriesE.AsParallel<LogEntry>().OrderBy<LogEntry, DateTime>((Func<LogEntry, DateTime>)(o => o.LogEntryDate)).Select<LogEntry, string>((Func<LogEntry, string>)(x => x.LogLine)), new CancellationToken());
Task wTask = File.WriteAllLinesAsync(Environment.CurrentDirectory + "\\outputW.log", (IEnumerable<string>)logEntriesW.AsParallel<LogEntry>().OrderBy<LogEntry, DateTime>((Func<LogEntry, DateTime>)(o => o.LogEntryDate)).Select<LogEntry, string>((Func<LogEntry, string>)(x => x.LogLine)), new CancellationToken());
Task hTask = File.WriteAllLinesAsync(Environment.CurrentDirectory + "\\outputHttp.log", (IEnumerable<string>)logEntriesHttp.AsParallel<LogEntry>().OrderBy<LogEntry, DateTime>((Func<LogEntry, DateTime>)(o => o.LogEntryDate)).Select<LogEntry, string>((Func<LogEntry, string>)(x => x.LogLine)), new CancellationToken());
await task;
await wTask;
await hTask;
