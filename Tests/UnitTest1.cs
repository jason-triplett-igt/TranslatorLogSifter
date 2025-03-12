using TranslatorLogSifter;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidErrorParse()
        {
            var teststring = "2024/10/20 19:25:38.161 E S2SClientController TicketPrintedTransaction::Process(): Node or Device [BE2 03bf44b7 BC 0.002] are not ready, will try EVT_TICKET_PRINTED later!\r\n";
            var logentry = LogEntry.BuildLogEntry(teststring);
            Assert.AreEqual(teststring, logentry.LogLine);
            Assert.AreEqual("E", logentry.Type);
            Assert.AreEqual(new DateTime(2024, 10, 20, 19, 25, 38, 161), logentry.LogEntryDate);

        }
    }
}