using NLog;
using NLog.Config;
using NLog.Targets;
using System.Reflection.Emit;

namespace NetNlog
{
    internal class Program
    {
        public static Logger logger =
            LogManager.GetCurrentClassLogger();
        static async Task Main(string[] args)
        {
            使用預設變數LogLevel();
            await Task.Delay(2000);
            WriteAllLevelLog("使用預設變數LogLevel");

            使用預設變數LogLevel指定為Trace();
            await Task.Delay(2000);
            WriteAllLevelLog("使用預設變數LogLevel指定為Trace");

        }

        private static void 使用預設變數LogLevel指定為Trace()
        {
            // 取得目前的 NLog 設定
            var config = LogManager.Configuration;

            // 設定要變更的變數值
            config.Variables["myVar"] = "myValue";
            config.Variables["DetectLogLevel"] = $"trace";

            // 變更完畢後，重新載入 NLog 設定
            LogManager.Configuration = config;

            // 使用變更後的變數值進行 logging
            logger.Info("The value of myVar is ${var:myVar}");


            //var config = LogManager.Configuration;
            //config.Variables["DetectLogLevel"] = $"trace";
            //LogManager.Configuration = config;
            //// 立即重新載入設定
            ////LogManager.ReconfigExistingLoggers();

            //logger.Info("My variable is ${DetectLogLevel}");
        }

        private static void 使用預設變數LogLevel()
        {
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

    public class SomeClass
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public void WriteAllLevelLog(string message)
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