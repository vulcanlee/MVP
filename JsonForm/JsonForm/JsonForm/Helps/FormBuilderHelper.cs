using CommunityToolkit.Maui.Markup;
using JsonForm.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace JsonForm.Helps
{
    public class FormBuilderHelper
    {
        private readonly MagicHelper magicHelper;

        public FormBuilderHelper(MagicHelper magicHelper)
        {
            this.magicHelper = magicHelper;
        }

        public IView GenerateView(Row rowItem, bool insideColumnOfGrid = false)
        {
            IView generateView = null;

            #region 基礎檢視控制項
            #region Label 文字
            if (rowItem.Type == magicHelper.FormLabel)
            {
                #region 文字輸入盒 的 前置說明文字
                generateView = new Label()
                {
                    ClassId = rowItem.Title,
                }
                .Text(rowItem.Text)
                .Margin(new Thickness(0, 0, 0, 20))
                .FontSize(magicHelper.DefaultFontSize)
                .Bold()
                .TextColor(Color.FromArgb("bb888888"));
                #endregion
            }
            #endregion

            #region Textbox 文字輸入盒
            if (rowItem.Type == magicHelper.FormTexBox)
            {
                VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
                if (string.IsNullOrEmpty(rowItem.Title) == false)
                {
                    #region 文字輸入盒 的 前置說明文字
                    verticalStackLayout.Children.Add(new Label()
                    {
                        ClassId = rowItem.Title,
                    }
                    .Text(rowItem.Title)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(magicHelper.DefaultFontSize)
                    .Bold()
                    .TextColor(Color.FromArgb("bb888888")));
                    #endregion
                }

                Entry entry = new Entry()
                {
                    ClassId = rowItem.Name,
                    BackgroundColor = magicHelper.FormViewBackgroundColor
                }
                .Text(rowItem.Value)
                .Margin(new Thickness(0, 0, 0, 20));

                verticalStackLayout.Children.Add(entry);
                generateView = verticalStackLayout;
            }
            #endregion

            #region Textarea 多行文字輸入盒
            if (rowItem.Type == magicHelper.Textarea)
            {
                VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
                if (string.IsNullOrEmpty(rowItem.Title) == false)
                {
                    #region 文字輸入盒 的 前置說明文字
                    verticalStackLayout.Children.Add(new Label()
                    {
                        ClassId = rowItem.Title,
                    }
                    .Text(rowItem.Title)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(magicHelper.DefaultFontSize)
                    .Bold()
                    .TextColor(Color.FromArgb("bb888888")));
                    #endregion
                }

                Editor entry = new Editor()
                {
                    ClassId = rowItem.Name,
                    BackgroundColor = magicHelper.FormViewBackgroundColor,
                    HorizontalOptions = LayoutOptions.Fill,
                    HeightRequest = 150,
                }
                .Margin(new Thickness(0, 0, 0, 20));

                verticalStackLayout.Children.Add(entry);
                generateView = verticalStackLayout;
            }
            #endregion

            #region Dropdown 下拉選單
            if (rowItem.Type == magicHelper.Dropdown)
            {
                VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
                if (string.IsNullOrEmpty(rowItem.Title) == false)
                {
                    #region Dropdown 下拉選單 的 前置說明文字
                    verticalStackLayout.Children.Add(new Label()
                    {
                        ClassId = rowItem.Title,
                    }
                    .Text(rowItem.Title)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(magicHelper.DefaultFontSize)
                    .Bold()
                    .TextColor(Color.FromArgb("bb888888")));
                    #endregion
                }

                Picker picker = new Picker()
                {
                    ClassId = rowItem.Name,
                    BackgroundColor = magicHelper.FormViewBackgroundColor,
                    HorizontalOptions = LayoutOptions.Fill,
                }
                .Margin(new Thickness(0, 0, 0, 20));

                var allOptions = new List<string>();
                foreach (var item in rowItem.Options)
                {
                    allOptions.Add(item.Value);
                }
                picker.ItemsSource = allOptions;

                verticalStackLayout.Children.Add(picker);
                generateView = verticalStackLayout;
            }
            #endregion

            #region Checkbox 檢查輸入盒
            if (rowItem.Type == magicHelper.Checkbox)
            {
                Grid grid = new Grid();
                grid.RowDefinitions = Rows.Define(Auto);
                grid.ColumnDefinitions = Columns.Define(50, Stars(1));
                if (insideColumnOfGrid)
                    grid.Margin(new Thickness(0, 0, 0, 0));
                else
                    grid.Margin(new Thickness(0, 0, 0, 20));

                CheckBox checkBox = new CheckBox()
                {
                    ClassId = rowItem.Name,
                    VerticalOptions = LayoutOptions.Start,
                }
                .Margin(new Thickness(0, 0, 0, 0));
                grid.Add(checkBox, 0, 0);

                if (string.IsNullOrEmpty(rowItem.Text) == false)
                {
                    #region 文字輸入盒 的 前置說明文字
                    Label label = new Label()
                    {
                        ClassId = rowItem.Text,
                        VerticalOptions = LayoutOptions.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Start,
                        LineBreakMode = LineBreakMode.WordWrap,
                    }
                    .Text(rowItem.Text)
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

            #region Selector (RadioButton) 檢查輸入盒
            if (rowItem.Type == magicHelper.Selector)
            {
                StackLayout stackLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };
                if (insideColumnOfGrid)
                    stackLayout.Margin(new Thickness(0, 0, 0, 0));
                else
                    stackLayout.Margin(new Thickness(0, 0, 0, 20));

                foreach (var optionsItem in rowItem.Options)
                {
                    RadioButton radioButton = new RadioButton()
                    {
                        ClassId = rowItem.Name,
                        Content = optionsItem,
                    }
                    .Margin(new Thickness(0, 0, 15, 0));
                    stackLayout.Children.Add(radioButton);
                }

                if (string.IsNullOrEmpty(rowItem.Text) == false)
                {
                    #region Selector (RadioButton) 檢查輸入盒 的 前置說明文字
                    stackLayout.Children.Add(new Label()
                    {
                        ClassId = rowItem.Text,
                        VerticalOptions = LayoutOptions.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                    }
                    .Text(rowItem.Text)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(magicHelper.DefaultFontSize)
                    .Bold()
                    .TextColor(Color.FromArgb("dd888888")));
                    #endregion
                }

                generateView = stackLayout;
            }
            #endregion

            #region RocDate 國曆日期
            if (rowItem.Type == magicHelper.FormRocDate)
            {
                VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
                if (string.IsNullOrEmpty(rowItem.Title) == false)
                {
                    #region 文字輸入盒 的 前置說明文字
                    verticalStackLayout.Children.Add(new Label()
                    {
                        ClassId = rowItem.Title,
                    }
                    .Text(rowItem.Title)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(magicHelper.DefaultFontSize)
                    .Bold()
                    .TextColor(Color.FromArgb("bb888888")));
                    #endregion
                }

                DatePicker datepicker = new DatePicker()
                {
                    ClassId = rowItem.Name,
                    BackgroundColor = magicHelper.FormViewBackgroundColor
                }
                .Margin(new Thickness(0, 0, 0, 20));

                verticalStackLayout.Children.Add(datepicker);
                generateView = verticalStackLayout;
            }
            #endregion

            #region RocDate 24小時時間
            if (rowItem.Type == magicHelper.FormTime24)
            {
                VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
                if (string.IsNullOrEmpty(rowItem.Title) == false)
                {
                    #region 文字輸入盒 的 前置說明文字
                    verticalStackLayout.Children.Add(new Label()
                    {
                        ClassId = rowItem.Title,
                    }
                    .Text(rowItem.Title)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(magicHelper.DefaultFontSize)
                    .Bold()
                    .TextColor(Color.FromArgb("bb888888")));
                    #endregion
                }

                TimePicker timePicker = new TimePicker()
                {
                    ClassId = rowItem.Name,
                    Format = "HH:mm:ss",
                };

                Grid grid = new Grid()
                {
                    BackgroundColor = magicHelper.FormViewBackgroundColor,
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                }
                .Margin(new Thickness(0, 0, 0, 20));
                grid.Children.Add(timePicker);
                verticalStackLayout.Children.Add(grid);

                generateView = verticalStackLayout;
            }
            #endregion

            #endregion

            #region Grid 網格
            if (rowItem.Type == magicHelper.FormGrid)
            {
                #region Grid 網格定義
                var cellDefinition = rowItem.ColumnsWidth.Split(",");
                Grid grid = new Grid().Margin(new Thickness(0, 0, 0, 20));

                #region 宣告要用到的 Column 的寬度定義
                foreach (var item in cellDefinition)
                {
                    double cellUnits = double.Parse(item);
                    grid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(cellUnits, GridUnitType.Star)
                    });
                }
                grid.RowDefinitions = Rows.Define(Auto);
                #endregion

                int columnIndex = 0;
                foreach (Column columnItem in rowItem.Columns)
                {
                    if (columnItem.ViewItems == null)
                    {
                        columnIndex++;
                        continue;
                    }

                    StackLayout stackLayout = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical
                    };

                    foreach (Viewitem item in columnItem.ViewItems)
                    {
                        #region 從 Cell 中的 ViewItem，產生 Row 物件，接著，產生控制項
                        Row cellRow = new Row();
                        cellRow.Title = item.Title;
                        cellRow.Text = item.Text;
                        cellRow.Name = item.Name;
                        cellRow.Type = item.Type;
                        cellRow.Options = item.Options;
                        #endregion

                        IView cellView = this.GenerateView(cellRow, insideColumnOfGrid = true);
                        stackLayout.Children.Add(cellView);
                    }
                    grid.Add(stackLayout, columnIndex, 0);
                    columnIndex++;
                }

                generateView = grid;
                #endregion
            }
            #endregion

            return generateView;
        }
    }
}
