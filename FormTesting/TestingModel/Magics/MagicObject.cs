using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingModel.Magics
{
    public class MagicObject
    {
        /// <summary>
        /// 瞬間可以承受的表單動作請求壓力測試
        /// </summary>
        public const string TestingNodeActionPerformance = "PerformanceTesting";
        /// <summary>
        /// 將系統上的所有表單都讀取一遍
        /// </summary>
        public const string TestingNodeActionWarmingUp = "WarmingUp";
        /// <summary>
        /// 30 秒內可以承受多少的表單動作請求
        /// </summary>
        public const string TestingNodeActionDistributionTesting = "DistributionTesting";
        public const string OutputFormHtmlFolderName = "Output";
        public const string OutputReportFolderName = "Report";
        public const string FormEndpointPrefix = "/Forms/CustomForm?FormId=";
        public const string FormEndpointPost = "/Forms/CustomForm?FormId=";
    }
}
