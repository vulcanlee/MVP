using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingModel.Enums;
using TestingModel.Models;

namespace TestingBusiness.Services
{
    public class RemotePerformanceService
    {
        /// <summary>
        /// 列印出 HttpClient 效能量測結果
        /// </summary>
        /// <param name="testingNode"></param>
        /// <param name="performanceMeasure"></param>
        /// <param name="formInformation"></param>
        /// <returns></returns>
        public void PrintHttpClientPerformanceResult(
            TestingNodeConfiguration testingNode,
            PerformanceMeasure performanceMeasure,
            FormInformation formInformation)
        {
            #region 列印出 HttpClient 效能量測結果
            if (testingNode.HttpClientPerformanceMeasure == true)
                performanceMeasure.Output(SortEnum.TotalCost, formInformation.AllFormsTitle.ToArray());
            #endregion
        }

        /// <summary>
        /// 列印遠端伺服器上校能統計分析
        /// </summary>
        /// <param name="testingNode"></param>
        /// <param name="performanceMeasure"></param>
        /// <returns></returns>
        public async Task PrintRemotePerformanceMeasureResult(
            TestingNodeConfiguration testingNode,
            PerformanceMeasure performanceMeasure)
        {
            #region 列印遠端伺服器上校能統計分析
            if (testingNode.RemotePerformanceMeasure == true)
            {
                var performanceMeasureResult = await GetPerformanceMeasureAsync(testingNode);
                performanceMeasure.ParsePerformance(performanceMeasureResult);
                if (testingNode.RemotePerformanceMaxLatencyAnalysis)
                    performanceMeasure.OutputMaxLatencyAnalysis(performanceMeasureResult);
                if (testingNode.RemotePerformanceOutputDetail)
                    performanceMeasure.OutputDetail(performanceMeasureResult);
                if (testingNode.RemotePerformanceOutputNodeDetail)
                    performanceMeasure.OutputNodeDetail(performanceMeasureResult);
            }
            #endregion
        }

        async Task<List<PerformanceMeasureHeader>> GetPerformanceMeasureAsync(
            TestingNodeConfiguration testingNode)
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
            return result!;
        }

        /// <summary>
        /// 將伺服器上的效能統計資訊清除
        /// </summary>
        /// <param name="testingNode"></param>
        /// <returns></returns>
        public async Task CleanRemotePerformanceMeasureDataAsync(
            TestingNodeConfiguration testingNode)
        {
            #region 將伺服器上的效能統計資訊清除
            if (testingNode.RemotePerformanceMeasure == true)
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
            #endregion
        }
    }
}
