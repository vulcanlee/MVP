using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingModel.Magics;

namespace TestingModel.Models
{
    public class TestingNodeConfiguration
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Mode { get; set; } = MagicObject.TestingNodeActionPerformance;
        public HostConnectionInformation Host { get; set; } = new();
        public List<string> FormEndPoints { get; set; } = new();
        public int NumberOfRequests { get; set; } = 1;
        public int MaxHttpClients { get; set; } = 10;
        public bool PerformanceMeasure { get; set; } = false;
        public string ResetRemotePerformanceMeasureEndpoint { get; set; } = "";
        public string GetRemotePerformanceMeasureEndpoint { get; set; } = "";
    }
}
