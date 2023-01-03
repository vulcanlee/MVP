using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            int maxHttpClients,int totalForms,
            PerformanceMeasureHeader performanceMeasureHeader,List<string> allForms)
        {
            #region 登入到系統且建立需要用到的 HttpClient 物件集合
            List<Task<HttpClient>> clientsTask = new List<Task<HttpClient>>();
            List<HttpClient> clients = new List<HttpClient>();
            await Task.Delay(1000);
            Console.WriteLine($"Building {maxHttpClients} HttpClients");
            Stopwatch stopwatch= Stopwatch.StartNew();
            stopwatch.Restart();
            stopwatch.Start();
            int index = 0;

            for (int i = 0; i < maxHttpClients; i++)
            {
                var form = allForms[index % totalForms];
                int cc = i;
                var task = NetLoginAsync(testingNode,performanceMeasureHeader, cc);
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
    }
}
