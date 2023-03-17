using NLog;

namespace NLogSample
{
    internal class Program
    {
        public static Logger logger =
            LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            WriteAllLevelLog("使用預設變數LogLevel");
        }

        private static void WriteAllLevelLog(string message)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(message);
            logger.Trace("我是追蹤:Trace");
            logger.Debug("我是偵錯:Debug");
            logger.Info("我是資訊:Info");
            logger.Warn("我是警告:Warn");
            logger.Error("我是錯誤:error");
            logger.Fatal("我是致命錯誤:Fatal");
        }
    }
}