using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingModel.Enums;

namespace TestingModel.Models
{
    public class PerformanceMeasure
    {
        static AutoResetEvent resetEvent = new AutoResetEvent(true);
        public List<PerformanceMeasureHeader> Header { get; set; } =
           new List<PerformanceMeasureHeader>();

        public PerformanceMeasureHeader NewHeader()
        {
            PerformanceMeasureHeader header = new PerformanceMeasureHeader();
            resetEvent.WaitOne();
            Header.Add(header);
            resetEvent.Set();
            return header;
        }

        public List<PerformanceMeasureHeader> GetHeads()
        {
            return Header;
        }

        public void Reset()
        {
            Header = new List<PerformanceMeasureHeader>();
        }

        public void Output(SortEnum sortEnum, string[] allFormsTitle)
        {
            foreach (var header in Header)
            {
                if (header.Nodes.Count == 0) continue;
                int index = 0;
                foreach (var item in header.Nodes)
                {
                    string formTitle = allFormsTitle[index++];
                    item.Title = formTitle;
                }
                List<PerformanceMeasureNode> temp = new List<PerformanceMeasureNode>();
                var tempList = header.Nodes;
                if (sortEnum == SortEnum.TotalCost)
                {
                    temp = tempList.OrderBy(x => x.EstimatedTime).ToList();
                }
                else
                {
                    temp = tempList.OrderBy(x => x.End).ToList();
                }
                var first = temp.OrderBy(x => x.Begin).First().Begin;
                var last = temp.OrderByDescending(x => x.End).First().End;
                foreach (var node in temp)
                {
                    var delay = (int)(node.Begin - first).TotalMilliseconds;
                    Console.WriteLine($"{node.Title}");
                    Console.Write($"Execute cost : ");
                    Output($"{node.EstimatedTime}", ConsoleColor.White, ConsoleColor.Green);
                    Console.Write($"   Delay ");
                    Output($"{delay}", ConsoleColor.White, ConsoleColor.Blue);
                    Console.WriteLine();
                    Console.WriteLine($"From [{node.Begin}] to [{node.End}]");
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine($"From:{first} - End:{last}");
            }
        }

        static void Output(string msg, ConsoleColor needForegroundColor, ConsoleColor needBackgroundColor)
        {
            var foregroundColor = Console.ForegroundColor;
            var backgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = needForegroundColor;
            Console.BackgroundColor = needBackgroundColor;
            Console.Write(msg);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.ResetColor();
        }

        /// <summary>
        /// 顯示出在後端伺服器程式碼，在執行哪個區塊段落的時候，將會耗費最多的時間與要執行該區段程式碼前延遲了多久？
        /// </summary>
        /// <param name="headers"></param>
        public void OutputDetail(List<PerformanceMeasureHeader> headers)
        {
            foreach (var header in headers)
            {
                Console.WriteLine($"{header.HeaderTitle}");
                foreach (var item in header.Nodes)
                {
                    if (item.Title.Contains("CustomFormsLib.GetFormAsync")) continue;
                    if(item.Latency == 0)
                    {
                        Console.Write($"【{item.Title} ({item.EstimatedTime}) > [");
                        Output($"{item.Latency}", ConsoleColor.Gray, ConsoleColor.Black);
                        Console.WriteLine($"] {item.LatencyDetail}】 ");
                        Console.Write($"");
                    }
                    else
                    {
                        Console.Write($"【{item.Title} ({item.EstimatedTime}) > [");
                        Output($"{item.Latency}", ConsoleColor.Yellow, ConsoleColor.Black);
                        Console.WriteLine($"] {item.LatencyDetail}】 ");
                        Console.Write($"");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 另外一種模式來顯示每個後端程式碼區段的總共執行耗時多久與延誤多久明細
        /// </summary>
        /// <param name="headers"></param>
        public void OutputNodeDetail(List<PerformanceMeasureHeader> headers)
        {
            Console.WriteLine();
            foreach (var header in headers)
            {
                Console.WriteLine($"{header.HeaderTitle}");
                foreach (var item in header.Nodes)
                {
                    if (item.Title.Contains("CustomFormsLib.GetFormAsync")) continue;
                    var title = item.Title.Trim().Split(" ")[0];
                    if (title == "CustomForm")
                    {
                        Console.Write($"{title,30} B:");
                        Output($"{item.Begin:mm:ss.ffff}", ConsoleColor.Cyan, ConsoleColor.Black);
                        Console.WriteLine($" F:{item.End:mm:ss.ffff} E:{item.EstimatedTime} ");
                    }
                    else
                        Console.WriteLine($"{title,30} B:{item.Begin:mm:ss.ffff} F:{item.End:mm:ss.ffff} E:{item.EstimatedTime} ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 顯示出在後端伺服器程式碼，在執行哪個區塊段落的時候，將會耗費最多的時間與要執行該區段程式碼前延遲了多久？
        /// </summary>
        /// <param name="headers"></param>
        public void OutputMaxLatencyAnalysis(List<PerformanceMeasureHeader> headers)
        {
            Console.WriteLine();
            foreach (var header in headers)
            {
                Console.WriteLine(header.HeaderTitle);
                Console.Write($"Request Estimated : ");
                Output($"{header.RequestEstimated}", ConsoleColor.White, ConsoleColor.Green);
                Console.WriteLine();
                Console.Write($"Max Node Estimated : ");
                Output($"{header.NodeEstimated}", ConsoleColor.White, ConsoleColor.Blue);
                Console.WriteLine($" - {header.NodeEstimatedDetail}");
                Console.Write($"Max Latency : ");
                Output($"{header.MaxLatency}", ConsoleColor.White, ConsoleColor.Red);
                Console.WriteLine($" - {header.LatencyDetail}");
                Console.WriteLine();
            }
        }
        public void ParsePerformance(List<PerformanceMeasureHeader> headers)
        {
            foreach (var headerItem in headers)
            {
                headerItem.MaxLatency = 0;
                headerItem.NodeEstimated = 0;
                string previousName = "";
                DateTime previousTime = default(DateTime);
                foreach (var nodeItem in headerItem.Nodes)
                {
                    if (nodeItem.Title.Contains("CustomFormsLib.GetFormAsync")) continue;
                    if (!nodeItem.Title.Contains("Application_BeginRequest"))
                    {
                        if (headerItem.NodeEstimated < nodeItem.EstimatedTime.TotalMilliseconds)
                        {
                            headerItem.NodeEstimated = Convert
                                .ToInt32(nodeItem.EstimatedTime.TotalMilliseconds);
                            headerItem.NodeEstimatedDetail = nodeItem.Title;
                        }
                    }
                    else
                    {
                        headerItem.RequestEstimated = Convert
                            .ToInt32(nodeItem.EstimatedTime.TotalMilliseconds);
                    }

                    if (!string.IsNullOrEmpty(previousName))
                    {
                        nodeItem.Latency = Convert
                            .ToInt32((nodeItem.Begin - previousTime).TotalMilliseconds);
                        if (nodeItem.Latency < 0)
                        {
                            //int foo1 = 1;
                        }
                        if (headerItem.MaxLatency < nodeItem.Latency)
                        {
                            headerItem.MaxLatency = nodeItem.Latency;
                            headerItem.LatencyDetail = $"{previousName} > {nodeItem.Title}";
                        }
                    }
                    previousName = nodeItem.Title;
                    if (!nodeItem.Title.Contains("Application_BeginRequest"))
                        previousTime = nodeItem.End;
                    else
                        previousTime = nodeItem.Begin;
                    nodeItem.LatencyDetail = "";
                }
            }
        }
    }
}
