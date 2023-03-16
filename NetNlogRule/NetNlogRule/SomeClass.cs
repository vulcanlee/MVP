using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNamespace.Database
{
    public class SomeClass
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public void WriteAllLog(string message)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(message);
            logger.Trace("我是追蹤 DB :Trace");
            logger.Debug("我是偵錯 DB :Debug");
            logger.Info("我是資訊 DB :Info");
            logger.Warn("我是警告 DB :Warn");
            logger.Error("我是錯誤 DB :error");
            logger.Fatal("我是致命錯誤 DB :Fatal");
        }
    }
}
