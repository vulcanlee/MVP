﻿using System;
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
        public List<string> FormIds { get; set; } = new();
        public int NumberOfRequests { get; set; } = 1;
        public int MaxHttpClients { get; set; } = 10;
        public int ForceSleepMilliSecond { get; set; } = 6000;
        public string FormEndpointPrefix { get; set; } = "";
        public string FormEndpointPost { get; set; } = "";
        public bool RecordRawHtmlContent { get; set; } = false;
        public bool HttpClientPerformanceMeasure { get; set; } = false;
        public bool RecordToFileHttpClientPerformanceMeasure { get; set; } = false;
        public bool RemotePerformanceMeasure { get; set; } = false;
        public bool RecordToFileRemotePerformanceMeasure { get; set; } = false;
        public bool RemotePerformanceMaxLatencyAnalysis { get; set; } = false;
        public bool RemotePerformanceOutputDetail { get; set; } = false;
        public bool RemotePerformanceOutputNodeDetail { get; set; } = false;
        public bool WarmingUpFailureForm { get; set; } = false;
        public bool RecordToFileWarmingUpFailureForm { get; set; } = false;
        public string ResetRemotePerformanceMeasureEndpoint { get; set; } = "";
        public string GetRemotePerformanceMeasureEndpoint { get; set; } = "";
         public string GetFormsEndPoint { get; set; } = "";
        public bool UsingGetForms { get; set; }
    }
}
