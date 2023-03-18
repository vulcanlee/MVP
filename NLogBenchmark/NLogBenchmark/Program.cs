using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NLog;

namespace NLogBenchmark
{
    public class NLogBenchmarkTests
    {
        [Benchmark]
        public void TestWriteLog()
        {
            NLogBenchmarkHelper.WriteLog();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<NLogBenchmarkTests>();
        }
    }
}