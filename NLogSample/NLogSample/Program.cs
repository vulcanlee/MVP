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
            啟用工程模式();
            WriteAllLevelLog("啟用 工程模式");
            關閉工程模式();
            WriteAllLevelLog("關閉 工程模式");
        }

        private static void 啟用工程模式()
        {
            // 取得目前的 NLog 設定
            var config = LogManager.Configuration;

            // 設定要變更的變數值
            config.Variables["EngineerLogLevel"] = $"debug";

            // 變更完畢後，重新載入 NLog 設定
            LogManager.Configuration = config;
        }

        private static void 關閉工程模式()
        {
            // 取得目前的 NLog 設定
            var config = LogManager.Configuration;

            // 設定要變更的變數值
            config.Variables["EngineerLogLevel"] = $"off";

            // 變更完畢後，重新載入 NLog 設定
            LogManager.Configuration = config;
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