﻿using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Shapes;
using NckuhRepair.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace NckuhRepair.Helpers;

public class FormIOBuilderHelper
{
    private readonly MagicHelper magicHelper;

    public FormIOBuilderHelper(MagicHelper magicHelper)
    {
        this.magicHelper = magicHelper;
    }

    public IView GenerateView(Models.Component component,
        FormActionModel formAction, IPageDialogService dialogService)
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
                Padding = magicHelper.LayoutPadding,
            };

            border.Margin(new Thickness(0, 0, 0, 30));

            VerticalStackLayout verticalStackLayout = new VerticalStackLayout();

            #region 加入面板標題
            verticalStackLayout.Children.Add(new Label()
                .Text(component.title)
                .Margin(new Thickness(0, 0, 0, 0))
                .FontSize(magicHelper.DefaultPanelTitleFontSize)
                .CenterHorizontal()
                .Bold()
                .TextColor(magicHelper.FormPanelTextColor));
            verticalStackLayout.Children.Add(new Border()
            {
                BackgroundColor = magicHelper.FormPanelTextColor,
                Margin = new Thickness(0, 20, 0, 20),
            });
            #endregion

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

            Border border = new Border()
            {
                BackgroundColor = magicHelper.FormViewBackgroundColor,
                StrokeThickness = 2,
                Stroke = Color.FromArgb("7C9B73"),
                StrokeShape = new RoundRectangle()
                {
                    CornerRadius = new CornerRadius(5, 5, 5, 5)
                },
            }
            .Margin(new Thickness(0, 0, 0, 20));

            Entry entry = new Entry()
            {
                ClassId = component.key,
                BackgroundColor = magicHelper.FormViewBackgroundColor,
            }
            .Text(component.Value)
            .Margin(new Thickness(3));

            #region 綁定變更事件
            entry.TextChanged += (s, e) =>
            {
                component.Value = e.NewTextValue;
            };
            #endregion

            #region 查看表單的唯獨模式
            if (formAction.EditMode == false)
            {
                entry.IsEnabled = false;
            }
            #endregion

            border.Content = entry;
            verticalStackLayout.Children.Add(border);
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

            Border border = new Border()
            {
                BackgroundColor = magicHelper.FormViewBackgroundColor,
                StrokeThickness = 2,
                Stroke = Color.FromArgb("7C9B73"),
                StrokeShape = new RoundRectangle()
                {
                    CornerRadius = new CornerRadius(5, 5, 5, 5)
                },
            }
            .Margin(new Thickness(0, 0, 0, 20));

            Entry entry = new Entry()
            {
                ClassId = component.key,
                BackgroundColor = magicHelper.FormViewBackgroundColor,
                IsPassword = true,
            }
            .Text(component.Value)
            .Margin(new Thickness(3));

            #region 綁定變更事件
            entry.TextChanged += (s, e) =>
            {
                component.Value = e.NewTextValue;
            };
            #endregion

            #region 查看表單的唯獨模式
            if (formAction.EditMode == false)
            {
                entry.IsEnabled = false;
            }
            #endregion

            border.Content = entry;
            verticalStackLayout.Children.Add(border);
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

            Border border = new Border()
            {
                BackgroundColor = magicHelper.FormViewBackgroundColor,
                StrokeThickness = 2,
                Stroke = Color.FromArgb("7C9B73"),
                StrokeShape = new RoundRectangle()
                {
                    CornerRadius = new CornerRadius(5, 5, 5, 5)
                },
            }
            .Margin(new Thickness(0, 0, 0, 20));

            Entry entry = new Entry()
            {
                ClassId = component.key,
                BackgroundColor = magicHelper.FormViewBackgroundColor,
                Keyboard = Keyboard.Numeric,
            }
            .Text(component.Value)
            .Margin(new Thickness(3));

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

            #region 查看表單的唯獨模式
            if (formAction.EditMode == false)
            {
                entry.IsEnabled = false;
            }
            #endregion

            border.Content = entry;
            verticalStackLayout.Children.Add(border);
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

            Border border = new Border()
            {
                BackgroundColor = magicHelper.FormViewBackgroundColor,
                StrokeThickness = 2,
                Stroke = Color.FromArgb("7C9B73"),
                StrokeShape = new RoundRectangle()
                {
                    CornerRadius = new CornerRadius(5, 5, 5, 5)
                },
            }
            .Margin(new Thickness(0, 0, 0, 20));

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
            .Margin(new Thickness(3));
            entry.Text = component.Value;

            entry.TextChanged += (s, e) =>
            {
                component.Value = e.NewTextValue;
            };

            #region 查看表單的唯獨模式
            if (formAction.EditMode == false)
            {
                entry.IsEnabled = false;
            }
            #endregion

            border.Content = entry;
            verticalStackLayout.Children.Add(border);
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

            // Todo : 要能顯示上次輸入的內容

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

            #region 查看表單的唯獨模式
            if (formAction.EditMode == false)
            {
                picker.IsEnabled = false;
            }
            #endregion

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

            #region 帶入之前輸入的內容
            bool result = false;
            bool convertResult = bool.TryParse(component.Value, out result);
            if (convertResult)
                checkBox.IsChecked = result;
            #endregion

            checkBox.CheckedChanged += (s, e) =>
            {
                component.Value = e.Value.ToString();
            };
            grid.Add(checkBox, 0, 0);

            #region 查看表單的唯獨模式
            if (formAction.EditMode == false)
            {
                checkBox.IsEnabled = false;
            }
            #endregion

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

                #region 帶入之前輸入的內容
                bool result = false;
                bool convertResult = bool.TryParse(component.Value, out result);
                if (convertResult)
                    checkBox.IsChecked = result;
                #endregion

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

                #region 查看表單的唯獨模式
                if (formAction.EditMode == false)
                {
                    checkBox.IsEnabled = false;
                }
                #endregion

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

                #region 帶入之前輸入的內容
                bool result = false;
                bool convertResult = bool.TryParse(component.Value, out result);
                if (convertResult)
                    radioButton.IsChecked = result;
                #endregion

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

                #region 查看表單的唯獨模式
                if (formAction.EditMode == false)
                {
                    radioButton.IsEnabled = false;
                }
                #endregion

                stackLayout.Children.Add(radioButton);
            }

            generateView = stackLayout;
        }
        #endregion

        #endregion

        #region 進階控制項

        #region day 國曆日期
        if (component.type == magicHelper.FormIODay)
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

            DatePicker datepicker = new DatePicker()
            {
                ClassId = component.key,
                BackgroundColor = magicHelper.FormViewBackgroundColor,
                Date = DateTime.Now.Date,
            }
            .Margin(new Thickness(0, 0, 0, 20));

            #region 帶入之前輸入的內容
            DateTime result = DateTime.Now;
            bool convertResult = DateTime.TryParse(component.Value, out result);
            if (convertResult)
                datepicker.Date = result;
            #endregion

            datepicker.DateSelected += (s, e) =>
            {
                component.Value = datepicker.Date.ToString();
            };

            #region 查看表單的唯獨模式
            if (formAction.EditMode == false)
            {
                datepicker.IsEnabled = false;
            }
            #endregion

            verticalStackLayout.Children.Add(datepicker);
            generateView = verticalStackLayout;
        }
        #endregion

        #region time 24小時時間
        if (component.type == magicHelper.FormIOTime)
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

            TimePicker timePicker = new TimePicker()
            {
                ClassId = component.key,
                Format = "HH:mm:ss",
            };

            #region 帶入之前輸入的內容
            TimeSpan result;
            bool convertResult = TimeSpan.TryParse(component.Value, out result);
            if (convertResult)
                timePicker.Time = result;
            #endregion

            timePicker.PropertyChanged += (s, e) =>
            {
                component.Value = timePicker.Time.ToString();
            };

            #region 查看表單的唯獨模式
            if (formAction.EditMode == false)
            {
                timePicker.IsEnabled = false;
            }
            #endregion

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

        #region 手機專屬控制項
        #region camera 手機拍照
        if (component.type == magicHelper.FormIOCamera)
        {
            VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
            if (string.IsNullOrEmpty(component.tooltip) == false)
            {
                #region 文字輸入盒 的 前置說明文字
                verticalStackLayout.Children.Add(new Label()
                {
                    ClassId = component.tooltip
                }
                .Text(component.tooltip)
                .Margin(new Thickness(0, 0, 0, 0))
                .FontSize(magicHelper.DefaultFontSize)
                .Bold()
                .TextColor(magicHelper.FormEntryBackgroundColor));
                #endregion
            }

            Grid grid = new Grid();
            grid.RowDefinitions = Rows.Define(Auto, Auto);
            grid.ColumnDefinitions = Columns.Define(Stars(1), 50, 50);
            grid.Margin(new Thickness(0, 0, 0, 20));

            #region 該圖片的檔案名稱顯示欄位
            Entry entry = new Entry()
            {
                ClassId = component.key,
                IsEnabled = false,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = magicHelper.DisableColor,
            }
            .Text(component.Value)
            .Margin(new Thickness(0, 0, 0, 20));
            grid.Add(entry, 0, 0);
            #endregion

            // Todo camera 手機拍照 帶入之前輸入的內容

            #region 待顯示圖片
            Image imageTake = new Image()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Aspect = Aspect.AspectFit,
                IsVisible = false,
            };
            grid.Add(imageTake, 0, 1);
            grid.SetColumnSpan(imageTake, 3);
            #endregion

            #region 清除圖片按鈕
            Image imageClear = new Image()
            {
                Source = ImageSource.FromFile(magicHelper.ClearCircleFillImageName),
                HeightRequest = 30,
                WidthRequest = 30,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
            };

            TapGestureRecognizer tapGestureRecognizerClear = new TapGestureRecognizer();
            tapGestureRecognizerClear.Tapped += (s, e) =>
            {
                #region 清除已經選定的照片
                imageTake.Source = null;
                imageTake.IsVisible = false;
                entry.Text = "";
                component.Value = "";
                #endregion
            };

            imageClear.GestureRecognizers.Add(tapGestureRecognizerClear);

            grid.Add(imageClear, 1, 0);
            #endregion

            #region 拍照按鈕
            Image imageCamera = new Image()
            {
                Source = ImageSource.FromFile(magicHelper.CameraImageName),
                HeightRequest = 30,
                WidthRequest = 30,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
            };

            #region 加入點選的手勢操作
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {

                #region 查看表單的唯獨模式
                if (formAction.EditMode == true)
                {

                    if (MediaPicker.Default.IsCaptureSupported)
                    {
                        #region 這台裝置有支援拍照功能
                        FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                        if (photo != null)
                        {
                            #region 將取得的媒體檔案，儲存到快取目錄下
                            string targetFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory,
                                photo.FileName);
                            entry.Text = photo.FileName;
                            using Stream sourceStream = await photo.OpenReadAsync();
                            using FileStream targetStream = File.OpenWrite(targetFilePath);

                            await sourceStream.CopyToAsync(targetStream);
                            sourceStream.Close(); targetStream.Close();

                            component.Value = targetFilePath;
                            #endregion

                            #region 顯示該圖片
                            imageTake.Source = ImageSource.FromFile(targetFilePath);
                            imageTake.IsVisible = true;
                            #endregion

                            #region 轉換成為 base64
                            //component.imageContent = "";
                            //using Stream sourceStream2 = await photo.OpenReadAsync();
                            //using (MemoryStream memory = new MemoryStream())
                            //{
                            //    sourceStream2.CopyTo(memory);
                            //    byte[] bytes = memory.ToArray();
                            //    //如何將 Image Stream 轉換成為 ImageSource
                            //    //image.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                            //    string base64 = System.Convert.ToBase64String(bytes);
                            //    component.imageContent = base64;
                            //}
                            #endregion
                        }
                        #endregion
                    }
                }
                #endregion
            };
            imageCamera.GestureRecognizers.Add(tapGestureRecognizer);

            #endregion

            grid.Add(imageCamera, 2, 0);
            #endregion

            verticalStackLayout.Children.Add(grid);
            generateView = verticalStackLayout;
        }
        #endregion

        #region file 多檔案上傳
        if (component.type == magicHelper.FormIOFile)
        {
            component.fileTypes.Clear();
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

            // Todo 多檔案上傳的 帶入之前輸入的內容

            #region 顯示出多個要上傳的檔案 Bindable Layouts
            StackLayout stackLayout = new StackLayout();
            #endregion

            Button button = new Button()
            {
                ClassId = component.key,
            }
            .Text(component.tooltip)
            .Margin(new Thickness(0, 0, 0, 20));

            #region 綁定點選事件
            button.Clicked += async (s, e) =>
            {
                var result = await FilePicker.PickAsync();
                if (result == null)
                    return;

                #region 檢查該檔案是否已經存在
                var isExistFile = component.fileTypes
                .FirstOrDefault(x => System.IO.Path.GetFileName(x.value) ==
                System.IO.Path.GetFileName(result.FullPath));

                if (isExistFile != null)
                {
                    await dialogService
                    .DisplayAlertAsync("錯誤", $"這個選定檔案，已經存在於上傳清單內。" +
                    $"{Environment.NewLine}{Environment.NewLine}" +
                    $"{result.FileName}", "確定");
                    return;
                }
                #endregion

                FileType fileType = new FileType
                {
                    label = result.ContentType,
                    value = result.FullPath,
                };
                component.fileTypes.Add(fileType);

                #region 建立要顯示該上傳檔案的 Grid
                Grid grid = new Grid()
                {
                    ClassId = fileType.value,
                };
                grid.RowDefinitions = Rows.Define(Auto);
                grid.ColumnDefinitions = Columns.Define(Stars(1), 40);
                grid.Margin(new Thickness(0, 10, 0, 10));
                #endregion

                #region 將相關的內容，填入到 Grid 內
                Label fileName = new Label()
                {
                    Text = fileType.value,
                    FontSize = 14
                };
                grid.Add(fileName, 0, 0);

                Image imageClear = new Image()
                {
                    Source = ImageSource.FromFile(magicHelper.ClearCircleFillImageName),
                    HeightRequest = 30,
                    WidthRequest = 30,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                };

                TapGestureRecognizer tapGestureRecognizerClear = new TapGestureRecognizer();
                tapGestureRecognizerClear.Tapped += (s, e) =>
                {
                    #region 清除已經選定的照片
                    component.fileTypes.Remove(fileType);
                    stackLayout.Children.Remove(grid);
                    #endregion
                };

                imageClear.GestureRecognizers.Add(tapGestureRecognizerClear);

                grid.Add(imageClear, 1, 0);
                stackLayout.Children.Add(grid);
                #endregion
            };
            #endregion

            #region 查看表單的唯獨模式
            if (formAction.EditMode == false)
            {
                button.IsEnabled = false;
            }
            #endregion

            verticalStackLayout.Children.Add(button);
            verticalStackLayout.Children.Add(stackLayout);
            generateView = verticalStackLayout;
        }
        #endregion

        #endregion

        return generateView;
    }
}
