using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestingModel.Magics;
using TestingModel.Models;

namespace TestingBusiness.Helpers
{
    public class FormHelper
    {
        private readonly PerformanceMeasure performanceMeasure;
        private readonly ILogger<FormsStressTesting> logger;
        private readonly IOptions<TestingTargetConfiguration> targetOption;
        private readonly IOptions<List<TestingNodeConfiguration>> testingNodeOption;

        public FormHelper(PerformanceMeasure performanceMeasure,
            ILogger<FormsStressTesting> logger,
            IOptions<TestingTargetConfiguration> TargetOption,
            IOptions<List<TestingNodeConfiguration>> TestingNodeOption)
        {
            this.performanceMeasure = performanceMeasure;
            this.logger = logger;
            targetOption = TargetOption;
            testingNodeOption = TestingNodeOption;
        }

        public TestingNodeConfiguration GetFormConfigurationNode()
        {
            TestingNodeConfiguration node = null;
            foreach (var item in testingNodeOption.Value)
            {
                if (targetOption.Value.TestingNode.ToLower() ==
                    item.Title.ToLower())
                {
                    node = item;
                    return node;
                }
            }
            return node;
        }

        public List<string> MakeFormUrl(TestingNodeConfiguration testingNode,
            int numberOfRequests)
        {
            List<string> allForms = new List<string>();

            foreach (var item in testingNode.FormIds)
            {
                string formUrl = $"{testingNode.Host.ConnectHost}" +
                    $"{testingNode.FormEndpointPrefix}{item}{testingNode.FormEndpointPost}";
                allForms.Add(formUrl);
            }
            ThreadPool.SetMinThreads(numberOfRequests + 250, numberOfRequests + 250);

            return allForms;
        }

        public async Task<List<HttpClient>> MakeHasLoginHttpClient(
            TestingNodeConfiguration testingNode,
            int maxHttpClients, int totalForms,
            PerformanceMeasureHeader performanceMeasureHeader, List<string> allForms)
        {
            #region 登入到系統且建立需要用到的 HttpClient 物件集合
            List<Task<HttpClient>> clientsTask = new List<Task<HttpClient>>();
            List<HttpClient> clients = new List<HttpClient>();
            await Task.Delay(1000);
            Console.WriteLine($"Building {maxHttpClients} HttpClients");
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Restart();
            stopwatch.Start();
            int index = 0;

            for (int i = 0; i < maxHttpClients; i++)
            {
                var form = allForms[index % totalForms];
                int cc = i;
                var task = NetLoginAsync(testingNode, performanceMeasureHeader, cc);
                clientsTask.Add(task);
            }
            await Task.WhenAll(clientsTask);
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine($"Elapsed time of Building HttpClients : {stopwatch.ElapsedMilliseconds} ms");

            foreach (var item in clientsTask)
            {
                clients.Add(item.Result);
            }
            #endregion
            return clients;
        }

        async Task<HttpClient> NetLoginAsync(TestingNodeConfiguration testingNode,
            PerformanceMeasureHeader measure, int idx)
        {
            string account = testingNode.Host.Account;
            string password = testingNode.Host.Password;
            string loginEndPoint = $"{testingNode.Host.ConnectHost}/Account/Login";
            string keywordStart = "<form action=\"/Account/Login\" class=\"form-horizontal\" method=\"post\" role=\"form\"><input name=\"__RequestVerificationToken\" type=\"hidden\" value=\"";
            string keywordEnd = "\" />";

            Console.Write(".");

            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var client = new HttpClient(handler);
            client.Timeout = new TimeSpan(0, 5, 0);
            var beforeResponse = await client.GetAsync(loginEndPoint);
            var html = await beforeResponse.Content.ReadAsStringAsync();
            Console.Write("*");

            var index = html.IndexOf(keywordStart);
            var indexEnd = html.IndexOf(keywordEnd, index);
            var tokenValue = html.Substring(index + keywordStart.Length, indexEnd - index - keywordStart.Length);

            // 方法一： 使用字串名稱用法
            var formData = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("__RequestVerificationToken", tokenValue),
                new KeyValuePair<string, string>("UserName", account),
                new KeyValuePair<string, string>("Password", password)
            });

            var response = await client.PostAsync(loginEndPoint, formData);
            if (response != null)
            {
                if (response.IsSuccessStatusCode == true)
                {
                    // 取得呼叫完成 API 後的回報內容
                    String strResult = await response.Content.ReadAsStringAsync();
                    Console.Write("-");
                    return client;
                }
            }
            return null;
        }

        public async Task WarmingUpForms(TestingNodeConfiguration testingNode,
            List<string> allForms, List<string> allFormsTitle,
            List<string> allFailureForm, bool distributionTesting,
            PerformanceMeasureHeader performanceMeasureHeader,
            int maxHttpClients, int totalForms, List<HttpClient> clients)
        {
            List<Task<string>> tasks = new List<Task<string>>();
            allFailureForm.Clear();

            Stopwatch allWeakup = new Stopwatch();
            Stopwatch stopwatch = new Stopwatch();
            allWeakup.Start();

            Console.WriteLine($"強制休息 {testingNode.ForceSleepMilliSecond / 1000} 秒");
            await Task.Delay(testingNode.ForceSleepMilliSecond);

            Console.WriteLine($"Opening {allForms.Count} Forms");
            stopwatch.Restart();
            stopwatch.Start();
            int index = 0;
            int start = 0;
            object locker = new object();

            allFormsTitle.Clear();
            for (int i = 0; i < allForms.Count; i++) allFormsTitle.Add("");
            for (int i = start; i < allForms.Count; i++)
            {
                if (i % maxHttpClients == 0)
                    tasks.Clear();

                int idx = i;

                if (allForms[i].Contains("e0f53420-f6fa-40b9-a557-743987f38cec") ||
                    allForms[i].Contains("e611e336-c7f3-48a9-b0f4-349ebe51d2da") ||
                    allForms[i].Contains("e0f53420-f6fa-40b9-a557-743987f38cec") ||
                    allForms[i].Contains("e0f53420-f6fa-40b9-a557-743987f38cec"))
                    continue;

                var client = clients[i % maxHttpClients];
                var task = Task.Run(async () =>
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Restart();
                    stopwatch.Start();

                    var resultTitle = await NetGetFormAsync(performanceMeasureHeader,
                              client, allForms[idx % totalForms], idx, distributionTesting,
                              testingNode.HttpClientPerformanceMeasure, testingNode);

                    if (resultTitle.Contains("並未將物件參考設定為物件的執行個體") ||
                    resultTitle.Contains("編譯錯誤"))
                    {
                        allFailureForm.Add(testingNode.FormIds[idx]);
                    }

                    stopwatch.Stop();
                    lock (locker)
                    {
                        Console.WriteLine($"Form {allForms.Count}/{idx}  {resultTitle}");
                        Console.Write($"Elapsed time of Opening Forms:  {allForms[idx]}  ");
                        if (stopwatch.ElapsedMilliseconds > 2000)
                        {
                            Output($"{stopwatch.ElapsedMilliseconds}", ConsoleColor.White, ConsoleColor.Red);
                        }
                        else
                        {
                            Output($"{stopwatch.ElapsedMilliseconds}", ConsoleColor.White, ConsoleColor.Green);
                        }
                        Console.WriteLine($" ms");
                    }
                    allFormsTitle[idx] = resultTitle;
                    return resultTitle;
                });
                tasks.Add(task);

                if (i % maxHttpClients == (maxHttpClients - 1))
                {
                    await Task.WhenAll(tasks);
                }

            }

            await Task.WhenAll(tasks);

            allWeakup.Stop();
            Console.WriteLine($"第一次初始化耗時 {allWeakup.Elapsed}");


            if (allFailureForm.Count > 0)
            {
                var foo = testingNode.FormIds;
                foreach (var item in allFailureForm)
                {
                    Console.WriteLine(item);
                    foo.Remove(item);
                }

                var bar = JsonConvert.SerializeObject(foo);
                Console.WriteLine(bar);
            }
        }

        async Task<string> NetGetFormAsync(PerformanceMeasureHeader measure,
            HttpClient client, string formEndPoint, int index, bool distributionTesting,
            bool performanceMeasureAction, TestingNodeConfiguration testingNode)
        {
            Random random = new Random();
            int timeRange = 30 * 1000;
            PerformanceMeasureNode measureItem = null;
            string title = "";

            try
            {
                if (distributionTesting)
                {
                    int waitMS = random.Next(timeRange);
                    await Task.Delay(waitMS);
                }

                if (performanceMeasureAction == true)
                    measureItem = measure
                        .BeginMeasure($"Get Form Content Page {index}", performanceMeasureAction);
                var beforeResponse = await client.GetAsync(formEndPoint);
                var html = await beforeResponse.Content.ReadAsStringAsync();

                #region 取得 Title
                var beginIndex = html.IndexOf("<title>");
                var endIndex = html.IndexOf("</title>");
                title = html.Substring(beginIndex, endIndex - beginIndex).Replace("<title>", "");
                //Console.Write(title+"  ");
                if (performanceMeasureAction == true)
                    measureItem.Title = $"{measureItem.Title} > {title} ";
                #endregion

                if (performanceMeasureAction == true)
                    measure.EndMeasure(measureItem!, performanceMeasureAction);

                #region 將此次讀取到的網頁儲存到本機 Data 目錄下
                if (testingNode.LogFormRawHtml)
                {
                    var foo = Directory.GetCurrentDirectory();
                    var dataFolder = Path.Combine(Directory.GetCurrentDirectory(),
                        MagicObject.OutputFormHtmlFolderName);
                    if (Directory.Exists(dataFolder) == false) Directory.CreateDirectory(dataFolder);
                    var filename = Path.Combine(dataFolder, $"forms_{index}.html");
                    await File.WriteAllTextAsync(filename, html);
                }
                #endregion
            }
            catch (Exception ex)
            {
                logger!.LogError(ex, $"Access URL : {formEndPoint}");
                Console.WriteLine(ex.Message);
            }
            return title;
        }
        
        void Output(string msg, ConsoleColor needForegroundColor, ConsoleColor needBackgroundColor)
        {
            var foregroundColor = Console.ForegroundColor;
            var backgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = needForegroundColor;
            Console.BackgroundColor = needBackgroundColor;
            Console.Write(msg);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
        }
    }
}
