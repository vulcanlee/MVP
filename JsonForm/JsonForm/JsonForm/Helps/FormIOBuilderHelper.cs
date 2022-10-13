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
                    Padding = new Thickness(10, 10, 10, 10),
                };

                border.Margin(new Thickness(0, 0, 0, 30));

                VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
                border.Content = verticalStackLayout;

                generateView = border;
            }
            #endregion
            #endregion

            #region 基礎檢視控制項 

            #region textfield 文字輸入盒
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

            #region password 密碼 文字輸入盒
            if (component.type == magicHelper.FormIOPassword)
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
                    IsPassword = true,
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

            #region number 數值 文字輸入盒
            if (component.type == magicHelper.FormIONumber)
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
                    Keyboard = Keyboard.Numeric,
                }
                .Text(component.Value)
                .Margin(new Thickness(0, 0, 0, 20));

                #region 綁定變更事件
                entry.TextChanged += (s, e) =>
                {
                    if (string.IsNullOrEmpty(e.NewTextValue))
                    {
                        entry.Text = "0";
                        component.Value = "0";
                        return;
                    }

                    double inputNumber = 0;
                    bool isNumber = double.TryParse(e.NewTextValue, out inputNumber);
                    if (isNumber == true)
                    {
                        component.Value = e.NewTextValue;
                    }
                    else
                    {
                        entry.Text = component.Value;
                        return;
                    }
                };
                #endregion

                verticalStackLayout.Children.Add(entry);
                generateView = verticalStackLayout;
            }
            #endregion

            #region textarea 多行文字輸入盒
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

                // Todo 需要有片語輸入功能
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

            #region select 下拉選單
            if (component.type == magicHelper.FormIOSelect)
            {
                VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
                if (string.IsNullOrEmpty(component.tooltip) == false)
                {
                    #region Dropdown 下拉選單 的 前置說明文字
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

                Picker picker = new Picker()
                {
                    ClassId = component.key,
                    BackgroundColor = magicHelper.FormViewBackgroundColor,
                    HorizontalOptions = LayoutOptions.Fill,
                }
                .Margin(new Thickness(0, 0, 0, 20));

                picker.SelectedIndexChanged += (s, e) =>
                {
                    var callbackPicker = (Picker)s;
                    int selectedIndex = callbackPicker.SelectedIndex;

                    if (selectedIndex != -1)
                    {
                        component.Value = (string)picker.ItemsSource[selectedIndex];
                    }
                };

                var allOptions = new List<string>();
                foreach (var item in component?.data?.values)
                {
                    allOptions.Add(item.label);
                }
                picker.ItemsSource = allOptions;

                verticalStackLayout.Children.Add(picker);
                generateView = verticalStackLayout;
            }
            #endregion

            #region checkbox 檢查輸入盒
            if (component.type == magicHelper.FormIOCheckbox)
            {
                Grid grid = new Grid();
                grid.RowDefinitions = Rows.Define(Auto);
                grid.ColumnDefinitions = Columns.Define(30, Stars(1));
                grid.Margin(new Thickness(0, 0, 0, 20));

                CheckBox checkBox = new CheckBox()
                {
                    ClassId = component.key,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start,
                }
                .Margin(new Thickness(0, 0, 0, 0));

                checkBox.CheckedChanged += (s, e) =>
                {
                    component.Value = e.Value.ToString();
                };
                grid.Add(checkBox, 0, 0);

                if (string.IsNullOrEmpty(component.tooltip) == false)
                {
                    #region 文字輸入盒 的 前置說明文字
                    Label label = new Label()
                    {
                        ClassId = component.tooltip,
                        VerticalOptions = LayoutOptions.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Start,
                        LineBreakMode = LineBreakMode.WordWrap,
                    }
                    .Text(component.tooltip)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(magicHelper.DefaultFontSize)
                    .Bold()
                    .TextColor(Color.FromArgb("dd888888"));
                    grid.Add(label, 1, 0);
                    #endregion
                }

                generateView = grid;
            }
            #endregion

            #region selectboxes 群組多個檢查輸入盒
            if (component.type == magicHelper.FormIOSelectboxes)
            {
                StackLayout stackLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };
                stackLayout.Margin(new Thickness(0, 0, 0, 20));

                stackLayout.Children.Add(new Label()
                {
                    ClassId = component.tooltip,
                }
                    .Text(component.tooltip)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(magicHelper.DefaultFontSize)
                    .Bold()
                    .TextColor(magicHelper.FormEntryBackgroundColor));

                foreach (var item in component.values)
                {
                    Grid grid = new Grid();
                    grid.RowDefinitions = Rows.Define(Auto);
                    grid.ColumnDefinitions = Columns.Define(30, Stars(1));
                    grid.Margin(new Thickness(0, 0, 0, 0));

                    CheckBox checkBox = new CheckBox()
                    {
                        ClassId = component.key,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                    }
                    .Margin(new Thickness(0, 0, 0, 0));

                    checkBox.CheckedChanged += (s, e) =>
                    {
                        if (e.Value == true)
                        {
                            item.select = item.value;
                        }
                        else
                        {
                            item.select = "";
                        }
                        #region 將多個檢查盒的輸入結果，組合成為一個物件
                        component.Value = "";
                        foreach (var itemReview in component.values)
                        {
                            if (string.IsNullOrEmpty(itemReview.select) == false)
                            {
                                if (string.IsNullOrEmpty(component.Value))
                                {
                                    component.Value = $"{itemReview.select}";
                                }
                                else
                                {
                                    component.Value = $"{component.Value} ; {itemReview.select}";
                                }
                            }
                        }
                        #endregion
                    };
                    grid.Add(checkBox, 0, 0);

                    if (string.IsNullOrEmpty(component.tooltip) == false)
                    {
                        #region 文字輸入盒 的 前置說明文字
                        Label label = new Label()
                        {
                            ClassId = item.label,
                            VerticalOptions = LayoutOptions.Center,
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Start,
                            LineBreakMode = LineBreakMode.WordWrap,
                        }
                        .Text(item.label)
                        .Margin(new Thickness(0, 0, 0, 0))
                        .FontSize(magicHelper.DefaultFontSize)
                        .Bold()
                        .TextColor(Color.FromArgb("dd888888"));
                        grid.Add(label, 1, 0);
                        #endregion
                    }
                    stackLayout.Children.Add(grid);
                }
                generateView = stackLayout;
            }
            #endregion

            #region radio 收音機按鈕 輸入盒
            if (component.type == magicHelper.FormIORadio)
            {
                StackLayout stackLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };
                stackLayout.Margin(new Thickness(0, 0, 0, 20));

                foreach (var optionsItem in component.values)
                {
                    RadioButton radioButton = new RadioButton()
                    {
                        ClassId = component.key,
                        Content = optionsItem.label,
                    }
                    .Margin(new Thickness(0, 0, 15, 0));

                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.NumberOfTapsRequired = 1;
                    tapGestureRecognizer.Tapped += (s, e) =>
                    {
                        if (radioButton.IsChecked == false)
                        {
                            radioButton.IsChecked = true;
                            component.Value = optionsItem.value;
                        }
                    };
                    TapGestureRecognizer tapGestureRecognizer2 = new TapGestureRecognizer();
                    tapGestureRecognizer2.NumberOfTapsRequired = 2;
                    tapGestureRecognizer2.Tapped += (s, e) =>
                    {
                        if (radioButton.IsChecked == true)
                        {
                            radioButton.IsChecked = false;
                            component.Value = "";
                        }
                    };
                    radioButton.GestureRecognizers.Add(tapGestureRecognizer);
                    radioButton.GestureRecognizers.Add(tapGestureRecognizer2);

                    stackLayout.Children.Add(radioButton);
                }

                generateView = stackLayout;
            }
            #endregion

            #endregion

            return generateView;
        }
    }
}
