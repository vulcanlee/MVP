using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogSample.Helpers
{
    public class LoggerHelper
    {
        private EngineerModeCodeEnum engineerMode = EngineerModeCodeEnum.None;

        public void SendLog(Action loggerHandler, EngineerModeCodeEnum engineerMode)
        {
            var actual = (this.engineerMode & engineerMode) == engineerMode;
            if (actual) { loggerHandler(); }
        }

        /// <summary>
        /// 加入某個工程模式
        /// </summary>
        /// <param name="engineerMode">指定之工程模式列舉值</param>
        public void AddEngineerMode(EngineerModeCodeEnum engineerMode)
        {
            this.engineerMode = this.engineerMode | engineerMode;
        }

        /// <summary>
        /// 移除某個工程模式
        /// </summary>
        /// <param name="engineerMode">指定之工程模式列舉值</param>
        public void RemoveEngineerMode(EngineerModeCodeEnum engineerMode)
        {
            this.engineerMode = this.engineerMode & ~engineerMode;
        }

        /// <summary>
        /// 強制設定某個工程模式
        /// </summary>
        /// <param name="engineerMode">指定之工程模式列舉值</param>
        public void SetEngineerMode(EngineerModeCodeEnum engineerMode)
        {
            this.engineerMode = engineerMode;
        }

        /// <summary>
        /// 清除所有工程模式
        /// </summary>
        /// <param name="engineerMode">指定之工程模式列舉值</param>
        public void ClearEngineerMode()
        {
            this.engineerMode = EngineerModeCodeEnum.None;
        }


    }

    [Flags]
    public enum EngineerModeCodeEnum
    {
        None = 0,
        登出登入 = 1,
        資料庫存取 = 2,
        讀卡機操作 = 4,
        MQTT = 8,
        呼叫WebAPI = 16,
        All = 登出登入 | 資料庫存取 | 讀卡機操作 | MQTT | 呼叫WebAPI
    }
}
