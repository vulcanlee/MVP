using CommunityToolkit.Maui.Markup;
using JsonForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonForm.Helps
{
    public class FormBuilderHelper
    {
        private readonly MagicHelper magicHelper;

        public FormBuilderHelper(MagicHelper magicHelper)
        {
            this.magicHelper = magicHelper;
        }

        public void GenerateView(VerticalStackLayout hostContainer, Row rowItem)
        {
            #region Label 文字
            if (rowItem.Type == magicHelper.FormLabel)
            {
                #region 文字輸入盒 的 前置說明文字
                hostContainer.Children.Add(new Label()
                {
                    ClassId = rowItem.Title,
                }
                .Text(rowItem.Text)
                .Margin(new Thickness(0, 0, 0, 20))
                .FontSize(12)
                .Bold()
                .TextColor(Color.FromArgb("bb888888")));
                #endregion
            }
            #endregion

        }
    }
}
