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
            List<LoggingRule> rules = new List<LoggingRule>();

            #region 儲存所有 LoggingRule
            rules = LogManager.Configuration.LoggingRules.ToList();
            #endregion

            停用所有的NLogRule(rules);
            WriteAllLevelLog("沒有套用任何 Rule，正常使用的 Log 輸出");

            加入所有設定檔案內的所有Rule(rules);
            await Task.Delay(2000);
            WriteAllLevelLog("套用所有 Rule，正常使用的 Log 輸出");

            變更FileRule僅接受指定LogLevel以上Log(rules, LogLevel.Trace);
            await Task.Delay(2000);
            WriteAllLevelLog("變更 FileRule 僅接受 Trace 以上 Log 輸出");

            變更FileRule僅接受指定LogLevel以上Log(rules, LogLevel.Warn);
            await Task.Delay(2000);
            WriteAllLevelLog("變更 FileRule 僅接受 Warning 以上 Log 輸出");

            await Task.Delay(2000);
            new SomeClass().WriteAllLevelLog("在 SomeClass 內寫入 Log");

        }

        private static void 變更FileRule僅接受指定LogLevel以上Log(List<LoggingRule> rules,
            LogLevel logLevel)
        {
            #region 變更FileRule僅接受Warning以上Log
            var config = LogManager.Configuration;
            foreach (var rule in rules)
            {
                var ruleItem = config.LoggingRules
                    .FirstOrDefault(x => x.RuleName == rule.RuleName);
                if (ruleItem != null)
                {
                    #region 將透過 Levels 屬性宣告的 LogLevel 全部停用
                    foreach (var itemLogLevel in LogLevel.AllLevels)
                    {
                        ruleItem.DisableLoggingForLevel(itemLogLevel);
                    }
                    #endregion

                    ruleItem.SetLoggingLevels(logLevel, LogLevel.Fatal);
                }
            }
            LogManager.Configuration = config;
            #endregion
        }

        private static void 加入所有設定檔案內的所有Rule(List<LoggingRule> rules)
        {
            #region 加入所有設定檔案內的所有Rule
            var config = LogManager.Configuration;
            foreach (var rule in rules)
            {
                var ruleItem = config.LoggingRules
                    .FirstOrDefault(x => x.RuleName == rule.RuleName);
                if (ruleItem == null)
                {
                    config.LoggingRules.Add(rule);
                }
            }
            LogManager.Configuration = config;
            #endregion
        }

        private static void 停用所有的NLogRule(List<LoggingRule> rules)
        {
            #region 停用所有的 NLog Rule
            foreach (var rule in rules)
            {
                LogManager.Configuration.LoggingRules.Remove(rule);
            }
            #endregion
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