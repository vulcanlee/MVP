using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TestingBusiness.Helpers;
using TestingModel.Magics;
using TestingModel.Models;

namespace TestingBusiness.Services;

public class FormService
{
    private readonly ILogger<FormsStressTesting> logger;
    private readonly ConsoleHelper consoleHelper;
    private readonly IOptions<TestingTargetConfiguration> targetOption;
    private readonly IOptions<List<TestingNodeConfiguration>> testingNodeOption;

    public FormService(ILogger<FormsStressTesting> logger,
        ConsoleHelper consoleHelper,
        IOptions<TestingTargetConfiguration> TargetOption,
        IOptions<List<TestingNodeConfiguration>> TestingNodeOption)
    {
        this.logger = logger;
        this.consoleHelper = consoleHelper;
        targetOption = TargetOption;
        testingNodeOption = TestingNodeOption;
    }

    /// <summary>
    /// 取得 "Target" > "TestingNode" 所宣告的測試節點
    /// </summary>
    /// <returns></returns>
    public TestingNodeConfiguration? GetCurrentFormConfigurationNode()
    {
        TestingNodeConfiguration? node = null;
        foreach (var item in testingNodeOption.Value)
        {
            if (targetOption.Value.TestingNode.ToLower() ==
                item.Title.ToLower())
            {
                node = item;
                return node;
            }
        }
        return null;
    }

    /// <summary>
    /// 建立需要測試的表單清單 URL
    /// </summary>
    /// <param name="testingNode"></param>
    /// <param name="formInformation"></param>
    /// <returns></returns>
    public List<string> MakeFormUrl(TestingNodeConfiguration testingNode,
        FormInformation formInformation)
    {
        #region 建立需要測試的表單清單 URL
        formInformation.AllForms.Clear();
        foreach (var item in formInformation.FormIds)
        {
            string formUrl = $"{testingNode.Host.ConnectHost}" +
                $"{testingNode.FormEndpointPrefix}{item}{testingNode.FormEndpointPost}";
            formInformation.AllForms.Add(formUrl);
        }
        ThreadPool.SetMinThreads(formInformation.NumberOfRequests + 250, formInformation.NumberOfRequests + 250);

        return formInformation.AllForms;
        #endregion
    }

    /// <summary>
    /// 登入到系統且建立需要用到的 HttpClient 物件集合
    /// </summary>
    /// <param name="testingNode"></param>
    /// <param name="formInformation"></param>
    /// <param name="performanceMeasureHeader"></param>
    /// <returns></returns>
    public async Task<List<HttpClient>> MakeHasLoginHttpClient(
        TestingNodeConfiguration testingNode,
        FormInformation formInformation,
        PerformanceMeasureHeader performanceMeasureHeader)
    {
        #region 登入到系統且建立需要用到的 HttpClient 物件集合
        List<Task<HttpClient>> clientsTask = new List<Task<HttpClient>>();
        List<HttpClient> clients = new List<HttpClient>();

        Console.WriteLine($"Building {formInformation.MaxHttpClients} HttpClients");
        Stopwatch stopwatch = Stopwatch.StartNew();
        stopwatch.Restart();
        stopwatch.Start();

        for (int i = 0; i < formInformation.MaxHttpClients; i++)
        {
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
        var client = new HttpClient(handler)
        {
            Timeout = new TimeSpan(0, 5, 0)
        };
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
                string strResult = await response.Content.ReadAsStringAsync();
                Console.Write("-");
                return client;
            }
        }
        return null;
    }

    public async Task WarmingUpForms(TestingNodeConfiguration testingNode,
        PerformanceMeasureHeader performanceMeasureHeader,
        FormInformation formInformation, List<HttpClient> clients)
    {
        List<Task<string>> tasks = new List<Task<string>>();
        formInformation.AllFailureForm.Clear();

        Stopwatch allWeakup = new Stopwatch();
        Stopwatch stopwatch = new Stopwatch();
        allWeakup.Start();

        Console.WriteLine($"強制休息 {testingNode.ForceSleepMilliSecond / 1000} 秒");
        await Task.Delay(testingNode.ForceSleepMilliSecond);

        Console.WriteLine($"Opening {formInformation.AllForms.Count} Forms");
        stopwatch.Restart();
        stopwatch.Start();
        int start = 0;
        object locker = new();

        formInformation.AllFormsTitle.Clear();
        for (int i = 0; i < formInformation.AllForms.Count; i++) formInformation.AllFormsTitle.Add("");
        for (int i = start; i < formInformation.AllForms.Count; i++)
        {
            if (i % formInformation.MaxHttpClients == 0)
                tasks.Clear();

            int idx = i;

            if (formInformation.AllForms[i].Contains("e0f53420-f6fa-40b9-a557-743987f38cec") ||
                formInformation.AllForms[i].Contains("e611e336-c7f3-48a9-b0f4-349ebe51d2da") ||
                formInformation.AllForms[i].Contains("e0f53420-f6fa-40b9-a557-743987f38cec") ||
                formInformation.AllForms[i].Contains("e0f53420-f6fa-40b9-a557-743987f38cec"))
                continue;

            var client = clients[i % formInformation.MaxHttpClients];
            var task = Task.Run(async () =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                stopwatch.Start();

                var resultTitle = await NetGetFormAsync(performanceMeasureHeader,
                          client, formInformation.AllForms[idx % formInformation.FormIdsCount],
                          idx, formInformation.DistributionTesting,
                          testingNode.HttpClientPerformanceMeasure, testingNode);

                if (resultTitle.Contains("並未將物件參考設定為物件的執行個體") ||
                resultTitle.Contains("編譯錯誤"))
                {
                    formInformation.AllFailureForm.Add(new FailureFormItem()
                    {
                        FormId = testingNode.FormIds[idx],
                        Title = resultTitle,
                    });
                }

                stopwatch.Stop();
                lock (locker)
                {
                    Console.WriteLine($"Form {formInformation.AllForms.Count}/{idx}  {resultTitle}");
                    Console.Write($"Elapsed time of Opening Forms:  {formInformation.AllForms[idx]}  ");
                    if (stopwatch.ElapsedMilliseconds > 2000)
                    {
                        consoleHelper.Output($"{stopwatch.ElapsedMilliseconds}", ConsoleColor.White, ConsoleColor.Red);
                    }
                    else
                    {
                        consoleHelper.Output($"{stopwatch.ElapsedMilliseconds}", ConsoleColor.White, ConsoleColor.Green);
                    }
                    Console.WriteLine($" ms");
                }
                formInformation.AllFormsTitle[idx] = resultTitle;
                return resultTitle;
            });
            tasks.Add(task);

            if (i % formInformation.MaxHttpClients == formInformation.MaxHttpClients - 1)
            {
                await Task.WhenAll(tasks);
            }

        }

        await Task.WhenAll(tasks);

        allWeakup.Stop();
        Console.WriteLine($"第一次初始化耗時 {allWeakup.Elapsed}");

        PrintFailureFormResult(testingNode, formInformation);

        PrintNormalFormId(testingNode, formInformation);
    }

    public void PrintNormalFormId(TestingNodeConfiguration testingNode,
    FormInformation formInformation)
    {
        if (testingNode.RecordToFileWarmingUpFailureForm)
        {
            var dataFolder = Path.Combine(Directory.GetCurrentDirectory(),
                MagicObject.LogReportFolderName);
            if (Directory.Exists(dataFolder) == false) Directory.CreateDirectory(dataFolder);
            var now = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var filename = Path.Combine(dataFolder, $"{now}_Normal.log");

            var stream = consoleHelper.SetConsoleOutputToFile(filename);

            #region 印出正常的表單 ID
            if (formInformation.AllFailureForm.Count > 0)
            {
                var foo = testingNode.FormIds;
                foreach (var item in formInformation.AllFailureForm)
                    foo.Remove(item.FormId);

                foreach (var item in foo)
                    Console.WriteLine($"\"{item}\",");
            }
            #endregion

            consoleHelper.ResetConsoleOutput(stream);
        }
    }

    public void PrintFailureFormResult(TestingNodeConfiguration testingNode,
    FormInformation formInformation)
    {
        if (testingNode.RecordToFileWarmingUpFailureForm)
        {
            var foo = Directory.GetCurrentDirectory();
            var dataFolder = Path.Combine(Directory.GetCurrentDirectory(),
                MagicObject.LogReportFolderName);
            if (Directory.Exists(dataFolder) == false) Directory.CreateDirectory(dataFolder);
            var now = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var filename = Path.Combine(dataFolder, $"{now}_FailureForm.log");

            var stream = consoleHelper.SetConsoleOutputToFile(filename);
            PrintFailureFormResultToConsole(testingNode, formInformation);
            consoleHelper.ResetConsoleOutput(stream);
        }

        if (testingNode.WarmingUpFailureForm)
        {
            PrintFailureFormResultToConsole(testingNode, formInformation);
        }
    }

    public void PrintFailureFormResultToConsole(TestingNodeConfiguration testingNode,
   FormInformation formInformation)
    {
        Console.WriteLine();
        Console.WriteLine($"Open fail form id list");
        foreach (var header in formInformation.AllFailureForm)
        {
            Console.WriteLine($"{header.FormId}  {header.Title}");
        }
    }

    public async Task StressPerformanceForms(TestingNodeConfiguration testingNode,
        PerformanceMeasureHeader performanceMeasureHeader,
        FormInformation formInformation, List<HttpClient> clients)
    {
        #region 進行表單壓力測試
        Stopwatch stopwatch = Stopwatch.StartNew();
        List<Task<string>> tasks = new List<Task<string>>();

        Console.WriteLine($"強制休息 {testingNode.ForceSleepMilliSecond / 1000} 秒");
        await Task.Delay(testingNode.ForceSleepMilliSecond);

        Console.WriteLine($"Opening {formInformation.NumberOfRequests} Forms");
        stopwatch.Restart();
        stopwatch.Start();
        formInformation.AllFormsTitle.Clear();
        for (int i = 0; i < formInformation.NumberOfRequests; i++) formInformation.AllFormsTitle.Add("");
        for (int i = 0; i < formInformation.NumberOfRequests; i++)
        {
            int idx = i;
            var client = clients[i % formInformation.MaxHttpClients];
            var task = Task.Run(async () =>
            {
                var fooi = idx;
                var resultTitle = await NetGetFormAsync(performanceMeasureHeader,
                     client, formInformation.AllForms[idx % formInformation.FormIdsCount],
                     idx, formInformation.DistributionTesting,
                     testingNode.HttpClientPerformanceMeasure, testingNode);

                resultTitle = $"{resultTitle}    {testingNode.FormIds[fooi]}";

                if (resultTitle.Contains("編譯"))
                {
                    //int fbar = 1;
                }
                return resultTitle;
            });
            tasks.Add(task);
        }

        var allTitle = await Task.WhenAll(tasks);
        formInformation.AllFormsTitle = allTitle.ToList();
        stopwatch.Stop();
        Console.WriteLine();
        Console.WriteLine($"Elapsed time of Opening Forms: {stopwatch.ElapsedMilliseconds} ms");
        #endregion
    }

    async Task<string> NetGetFormAsync(PerformanceMeasureHeader measure,
        HttpClient client, string formEndPoint, int index, bool distributionTesting,
        bool performanceMeasureAction, TestingNodeConfiguration testingNode)
    {
        Random random = new Random();
        int timeRange = 30 * 1000;
        PerformanceMeasureNode? measureItem = null;
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
                measureItem!.Title = $"{measureItem.Title} > {title} ";
            #endregion

            if (performanceMeasureAction == true)
                measure.EndMeasure(measureItem!, performanceMeasureAction);

            #region 將此次讀取到的網頁儲存到本機 Data 目錄下
            if (testingNode.RecordRawHtmlContent)
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

}
