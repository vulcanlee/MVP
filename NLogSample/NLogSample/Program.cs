using NLog;
using NLogSample.Helpers;

namespace NLogSample
{
    internal class Program
    {
        public static Logger logger =
            LogManager.GetCurrentClassLogger();
        public static LoggerHelper loggerHelper = new LoggerHelper();
        static void Main(string[] args)
        {
            string 讀卡機操作工程模式用之日誌事件 = "讀卡機操作 工程模式用之日誌事件";
            string 呼叫WebAPI工程模式用之日誌事件 = "呼叫WebAPI 工程模式用之日誌事件";
            模擬產生相關日誌事件("使用預設變數LogLevel",
                讀卡機操作工程模式用之日誌事件, 呼叫WebAPI工程模式用之日誌事件);

            啟用工程模式();
            模擬產生相關日誌事件("啟用 工程模式",
                讀卡機操作工程模式用之日誌事件, 呼叫WebAPI工程模式用之日誌事件);

            關閉工程模式();
            模擬產生相關日誌事件("關閉 工程模式",
                讀卡機操作工程模式用之日誌事件, 呼叫WebAPI工程模式用之日誌事件);

            啟用工程模式且僅限讀卡機操作();
            模擬產生相關日誌事件("啟用工程模式且僅限讀卡機操作",
                讀卡機操作工程模式用之日誌事件, 呼叫WebAPI工程模式用之日誌事件);

            啟用工程模式且指定呼叫WebAPI與讀卡機操作();
            模擬產生相關日誌事件("啟用工程模式且指定呼叫WebAPI與讀卡機操作",
                讀卡機操作工程模式用之日誌事件, 呼叫WebAPI工程模式用之日誌事件);

            啟用工程模式且沒有指定任何條件();
            模擬產生相關日誌事件("啟用工程模式且沒有指定任何條件",
                讀卡機操作工程模式用之日誌事件, 呼叫WebAPI工程模式用之日誌事件);
        }

        private static void 啟用工程模式且沒有指定任何條件()
        {
            啟用工程模式();
            loggerHelper.ClearEngineerMode();
        }

        private static void 啟用工程模式且指定呼叫WebAPI與讀卡機操作()
        {
            啟用工程模式();
            loggerHelper.ClearEngineerMode();
            loggerHelper.SetEngineerMode(EngineerModeCodeEnum.讀卡機操作);
            loggerHelper.AddEngineerMode(EngineerModeCodeEnum.呼叫WebAPI);
        }

        private static void 啟用工程模式且僅限讀卡機操作()
        {
            啟用工程模式();
            loggerHelper.ClearEngineerMode();
            loggerHelper.SetEngineerMode(EngineerModeCodeEnum.讀卡機操作);
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

        private static void 模擬產生相關日誌事件(string message1, string message2, string message3)
        {
            WriteAllLevelLog(message1);
            WriteAll讀卡機操作Log(message2);
            WriteAll呼叫WebAPILog(message3);
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

        private static void WriteAll呼叫WebAPILog(string message)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(message);
            loggerHelper.SendLog(() =>
            {
                logger.Trace("呼叫WebAPI 追蹤:Trace");
            }, EngineerModeCodeEnum.呼叫WebAPI);
            loggerHelper.SendLog(() =>
            {
                logger.Debug("呼叫WebAPI 我是偵錯:Debug");
            }, EngineerModeCodeEnum.呼叫WebAPI);
            loggerHelper.SendLog(() =>
            {
                logger.Info("呼叫WebAPI 我是資訊:Info");
            }, EngineerModeCodeEnum.呼叫WebAPI);

            logger.Warn("呼叫WebAPI 我是警告:Warn");
            logger.Error("呼叫WebAPI 我是錯誤:error");
            logger.Fatal("呼叫WebAPI 我是致命錯誤:Fatal");
        }

        private static void WriteAll讀卡機操作Log(string message)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(message);
            loggerHelper.SendLog(() =>
            {
                logger.Trace("讀卡機操作 追蹤:Trace");
            }, EngineerModeCodeEnum.讀卡機操作);
            loggerHelper.SendLog(() =>
            {
                logger.Debug("讀卡機操作 我是偵錯:Debug");
            }, EngineerModeCodeEnum.讀卡機操作);
            loggerHelper.SendLog(() =>
            {
                logger.Info("讀卡機操作 我是資訊:Info");
            }, EngineerModeCodeEnum.讀卡機操作);

            logger.Warn("讀卡機操作 我是警告:Warn");
            logger.Error("讀卡機操作 我是錯誤:error");
            logger.Fatal("讀卡機操作 我是致命錯誤:Fatal");
        }
    }
}