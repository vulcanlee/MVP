using Newtonsoft.Json;
using System;
using System.Diagnostics;
using TestingModel.Enums;
using TestingModel.Magics;
using TestingModel.Models;

namespace TestingBusiness
{
    public class FormsStressTesting
    {
        private readonly PerformanceMeasure performanceMeasure;
        private TestingNodeConfiguration testingNode;
        List<string> allForms = new List<string>();
        List<HttpClient> clients = new List<HttpClient>();
        List<Task<HttpClient>> clientsTask = new List<Task<HttpClient>>();
        List<Task<string>> tasks = new List<Task<string>>();
        int totalForms = 0;
        int numberOfRequests = 0;
        int maxHttpClients = 0;
        bool distributionTesting = false;
        bool performanceMeasureAction = false;
        Stopwatch stopwatch = new Stopwatch();

        public FormsStressTesting(PerformanceMeasure performanceMeasure)
        {
            this.performanceMeasure = performanceMeasure;
        }
        public async Task NETFormsAsync(TestingNodeConfiguration testingNodeConfiguration)
        {
            this.testingNode = testingNodeConfiguration;

            #region 設定這個方法會用到的相關欄位值
            numberOfRequests = testingNode.NumberOfRequests;
            maxHttpClients = testingNode.MaxHttpClients;
            performanceMeasureAction = testingNode.PerformanceMeasure;
            #endregion

            #region 建立需要測試的表單清單 URL
            foreach (var item in testingNode.FormEndPoints)
            {
                string formUrl = $"{testingNode.Host.ConnectHost}{item}";
                allForms.Add(formUrl);
            }
            totalForms = allForms.Count;
            ThreadPool.SetMinThreads(numberOfRequests + 250, numberOfRequests + 250);
            #endregion

            #region 將伺服器上的效能統計資訊清除
            if (testingNodeConfiguration.PerformanceMeasure == true)
            {
                await ResetRemotePerformanceMeasureAsync();
            }
            #endregion

            #region 登入到系統且建立需要用到的 HttpClient 物件集合
            await Task.Delay(1000);
            Console.WriteLine($"Building {maxHttpClients} HttpClients");
            stopwatch.Restart();
            stopwatch.Start();
            int index = 0;

            PerformanceMeasureHeader performanceMeasureHeader =
                performanceMeasure.NewHeader();

            for (int i = 0; i < maxHttpClients; i++)
            {
                var form = allForms[index % totalForms];
                int cc = i;
                var task = NetLoginAsync(performanceMeasureHeader, cc);
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

            #region 進行表單壓力測試
            Console.WriteLine($"強制休息 10 秒");
            await Task.Delay(6000);

            Console.WriteLine($"Opening {numberOfRequests} Forms");
            stopwatch.Restart();
            stopwatch.Start();
            index = 0;
            for (int i = 0; i < numberOfRequests; i++)
            {
                int idx = i;
                var client = clients[i % maxHttpClients];
                var task = Task.Run(async () =>
                {
                    var resultTitle = await NetGetFormAsync(performanceMeasureHeader,
                         client, allForms[idx % totalForms], idx, distributionTesting,
                         performanceMeasureAction);
                    return resultTitle;
                });
                tasks.Add(task);
            }

            var allFormsTitle = await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine($"Elapsed time of Opening Forms: {stopwatch.ElapsedMilliseconds} ms");
            #endregion

            #region 列印出效能量測結果
            performanceMeasure.Output(SortEnum.TotalCost, allFormsTitle);
            #endregion


            #region 列印效能統計分析
            var performanceMeasureResult = await GetPerformanceMeasureAsync();
            performanceMeasure.ParsePerformance(performanceMeasureResult);
            performanceMeasure.OutputAnalysis(performanceMeasureResult);
            performanceMeasure.OutputDetail(performanceMeasureResult);
            performanceMeasure.OutputNodeDetail(performanceMeasureResult);
            #endregion

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();

            return;
        }

        async Task ResetRemotePerformanceMeasureAsync()
        {
            var endPoint = $"{testingNode.Host.ConnectHost}" +
                $"{testingNode.ResetRemotePerformanceMeasureEndpoint}";
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            HttpClient client = new HttpClient(handler);
            await client.GetAsync(endPoint);
        }

        async Task<List<PerformanceMeasureHeader>> GetPerformanceMeasureAsync()
        {
            var endPoint = $"{testingNode.Host.ConnectHost}" +
                $"{testingNode.GetRemotePerformanceMeasureEndpoint}";
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            HttpClient client = new HttpClient(handler);
            var content = await client.GetStringAsync(endPoint);
            var result = JsonConvert.DeserializeObject<List<PerformanceMeasureHeader>>(content);
            return result;
        }

        async Task<HttpClient> NetLoginAsync(PerformanceMeasureHeader measure, int idx)
        {
            string account = testingNode.Host.Account;
            string password = testingNode.Host.Password;
            //account = "exentric";
            //password = "kmuh!100";
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

        static async Task<string> NetGetFormAsync(PerformanceMeasureHeader measure,
        HttpClient client, string formEndPoint, int index, bool distributionTesting = true,
       bool performanceMeasureAction = false)
        {
            Random random = new Random();
            int timeRange = 30 * 1000;

            if (distributionTesting)
            {
                int waitMS = random.Next(timeRange);
                await Task.Delay(waitMS);
            }

            var measureItem = measure
                .BeginMeasure($"Get Form Content Page {index}", performanceMeasureAction);
            var beforeResponse = await client.GetAsync(formEndPoint);
            var html = await beforeResponse.Content.ReadAsStringAsync();

            #region 取得 Title
            var beginIndex = html.IndexOf("<title>");
            var endIndex = html.IndexOf("</title>");
            var title = html.Substring(beginIndex, endIndex - beginIndex).Replace("<title>", "");
            //Console.Write(title+"  ");
            #endregion

            measure.EndMeasure(measureItem, performanceMeasureAction);

            //if (!html.Contains("20.75麻醉同意書")) throw new Exception("表單不正確");

            #region 將此次讀取到的網頁儲存到本機 Data 目錄下
            var foo = Directory.GetCurrentDirectory();
            var dataFolder = Path.Combine(Directory.GetCurrentDirectory(),
                MagicObject.OutputFormHtmlFolderName);
            if (Directory.Exists(dataFolder) == false) Directory.CreateDirectory(dataFolder);
            var filename = Path.Combine(dataFolder, $"forms_{index}.html");
            await File.WriteAllTextAsync(filename, html);
            #endregion

            return title;
        }

    }
}