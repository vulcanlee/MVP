using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogBenchmark
{
    public static class NLogBenchmarkHelper
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public static void WriteLog()
        {
            logger.Info("This is a log message.");
        }
    }
}
