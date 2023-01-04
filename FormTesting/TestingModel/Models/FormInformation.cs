using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingModel.Enums;

namespace TestingModel.Models
{
    public class FormInformation
    {
        /// <summary>
        /// 每次需要產生多少的讀取表單動作
        /// </summary>
        public int NumberOfRequests { get; set; } = 0;
        /// <summary>
        /// 需要準備多少個 HttpClient 物件
        /// </summary>
        public int MaxHttpClients { get; set; } = 0;
        /// <summary>
        /// 設定檔案內提供的表單數量
        /// </summary>
        public int FormIdsCount { get; set; } = 0;
        /// <summary>
        /// 此次程式要執行的運作模式
        /// </summary>
        public TestingModeEnum   Mode { get; set; }
        public bool DistributionTesting { get
            {
                if (Mode == TestingModeEnum.時間內吞吐量測試) return true;
                else return false;
            }
        }
        public List<string> AllForms { get; set; } = new List<string>();
        public List<string> FormIds { get; set; } = new List<string>();
        public List<string> AllFormsTitle { get; set; } = new List<string>();
        public List<string> AllFailureForm { get; set; } = new List<string>();
    }
}
