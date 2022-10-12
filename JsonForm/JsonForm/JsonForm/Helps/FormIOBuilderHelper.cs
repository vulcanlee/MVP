using CommunityToolkit.Maui.Markup;
using JsonForm.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using static System.Net.Mime.MediaTypeNames;

namespace JsonForm.Helps
{
    public class FormIOBuilderHelper
    {
        private readonly MagicHelper magicHelper;

        public FormIOBuilderHelper(MagicHelper magicHelper)
        {
            this.magicHelper = magicHelper;
        }

        public IView GenerateView(Models.Component component)
        {
            IView generateView = null;

            #region Layout 版面配置 
            #region Panel - Border
            if (component.type == magicHelper.FormIOPanel)
            {
                Border border = new Border()
                {
                    BackgroundColor = Color.FromArgb("FFf5f5f5"),
                    StrokeShape = new RoundRectangle()
                    {
                        CornerRadius = new CornerRadius(5, 5, 5, 5)
                    },
                    Padding = new Thickness(10,10,10,10),
                };

                border.Margin(new Thickness(0, 0, 0, 30));

                VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
                border.Content = verticalStackLayout;

                generateView = border;
            }
            #endregion
            #endregion

            #region 基礎檢視控制項

            #region Textbox 文字輸入盒
            if (component.type == magicHelper.FormIOTextfield)
            {
                VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
                if (string.IsNullOrEmpty(component.tooltip) == false)
                {
                    #region 文字輸入盒 的 前置說明文字
                    verticalStackLayout.Children.Add(new Label()
                    {
                        ClassId = component.tooltip,
                    }
                    .Text(component.tooltip)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(magicHelper.DefaultFontSize)
                    .Bold()
                    .TextColor(magicHelper.FormEntryBackgroundColor));
                    #endregion
                }

                Entry entry = new Entry()
                {
                    ClassId = component.key,
                    BackgroundColor = magicHelper.FormViewBackgroundColor,
                }
                .Text(component.Value)
                .Margin(new Thickness(0, 0, 0, 20));

                #region 綁定變更事件
                entry.TextChanged += (s, e) =>
                {
                    component.Value = e.NewTextValue;
                };
                #endregion

                verticalStackLayout.Children.Add(entry);
                generateView = verticalStackLayout;
            }
            #endregion

            #region Textarea 多行文字輸入盒
            if (component.type == magicHelper.FormIOTextarea)
            {
                VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
                if (string.IsNullOrEmpty(component.tooltip) == false)
                {
                    #region 文字輸入盒 的 前置說明文字
                    verticalStackLayout.Children.Add(new Label()
                    {
                        ClassId = component.tooltip,
                    }
                    .Text(component.tooltip)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(magicHelper.DefaultFontSize)
                    .Bold()
                    .TextColor(magicHelper.FormEntryBackgroundColor));
                    #endregion
                }

                string requireMessage = "";
                if (component.validate?.required == true)
                    requireMessage = " ，請注意這個欄位為必填";
                Editor entry = new Editor()
                {
                    ClassId = component.key,
                    BackgroundColor = magicHelper.FormViewBackgroundColor,
                    HorizontalOptions = LayoutOptions.Fill,
                    HeightRequest = 150,
                    Placeholder = $"請在此填入 {component.tooltip} {requireMessage}",
                }
                .Margin(new Thickness(0, 0, 0, 20));

                entry.TextChanged += (s, e) =>
                {
                    component.Value = e.NewTextValue;
                };

                verticalStackLayout.Children.Add(entry);
                generateView = verticalStackLayout;
            }
            #endregion

            #endregion

            return generateView;
        }
    }
}
