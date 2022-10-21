using NckuhRepair.Models;
using NckuhRepair.ViewModels;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NckuhRepair.Helpers
{
    public class FormIOVerifyHelper
    {
        private readonly MagicHelper magicHelper;

        public FormIOVerifyHelper(MagicHelper magicHelper)
        {
            this.magicHelper = magicHelper;
        }
        public string CheckRequiredByComponent(Models.Component component)
        {
            var result = "";

            if (component.validate != null)
            {
                if (component.validate.required == true)
                {
                    #region 這裡必須要有值
                    component.Value = component.Value?.Trim();
                    if (string.IsNullOrEmpty(component.Value?.Trim()))
                    {
                        result = $"{component.tooltip} 必須要有輸入值";
                        return result;
                    }
                    #endregion
                }
            }
            return result;
        }
        public string CheckRequired(FormIOModel formIOModel)
        {
            var result = "";

            #region 必填欄位檢查
            var form = formIOModel;

            foreach (Models.Component componentParent in form.components)
            {
                if (componentParent.type == magicHelper.FormIOPanel)
                {
                    foreach (Models.Component componentChild in componentParent.components)
                    {
                        result = CheckRequiredByComponent(componentChild);
                        if (string.IsNullOrEmpty(result) == false)
                        {
                            return result;
                        }
                    }
                }
                else
                {
                    result = CheckRequiredByComponent(componentParent);
                    if(string.IsNullOrEmpty(result) == false)
                    {
                        return result;
                    }
                }
            }

            #endregion
            return result;
        }
    }
}
