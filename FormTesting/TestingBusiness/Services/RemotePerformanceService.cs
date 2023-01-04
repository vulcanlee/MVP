using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingModel.Models;

namespace TestingBusiness.Services
{
    public class RemotePerformanceService
    {
        public async Task CleanRemotePerformanceMeasureDataAsync(TestingNodeConfiguration testingNode)
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
