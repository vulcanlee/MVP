using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
