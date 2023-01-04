using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingModel.Magics;
using TestingModel.Models;

namespace TestingBusiness.Helpers
{
    public static class FormHelper
    {
        public static void ConvertConfigurationToFormInformation(this FormInformation formInformation,
            TestingNodeConfiguration testingNode)
        {
            formInformation.NumberOfRequests = testingNode.NumberOfRequests;
            formInformation.MaxHttpClients = testingNode.MaxHttpClients;
            formInformation.FormIds = testingNode.FormIds;
            formInformation.FormIdsCount = formInformation.FormIds.Count;

            if (testingNode.Mode == MagicObject.TestingNodeActionPerformance)
                formInformation.Mode = TestingModel.Enums.TestingModeEnum.壓力測試;
            else if (testingNode.Mode == MagicObject.TestingNodeActionWarmingUp)
                formInformation.Mode = TestingModel.Enums.TestingModeEnum.表單暖機預先載入;
            else if (testingNode.Mode == MagicObject.TestingNodeActionDistributionTesting)
                formInformation.Mode = TestingModel.Enums.TestingModeEnum.時間內吞吐量測試;
        }
    }
}