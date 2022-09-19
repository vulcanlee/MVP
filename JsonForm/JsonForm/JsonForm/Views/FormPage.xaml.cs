using CommunityToolkit.Maui.Markup;
using JsonForm.Helps;
using JsonForm.Models;
using JsonForm.ViewModels;
using Newtonsoft.Json;

namespace JsonForm.Views;

public partial class FormPage : ContentPage
{
    private readonly MagicHelper magicHelper;

    public FormPageViewModel FormPageViewModel { get; set; }
    public FormPage(MagicHelper magicHelper)
    {
        InitializeComponent();

        WatchBindingContextHasObjectAsync();
        this.magicHelper = magicHelper;
    }

    async Task WatchBindingContextHasObjectAsync()
    {
        int WatchTime = 100;
        while (true)
        {
            if (this.BindingContext != null)
            {
                FormPageViewModel = this.BindingContext as FormPageViewModel;
                if (FormPageViewModel.ReadSuccessful == true)
                {
                    await MainThread.InvokeOnMainThreadAsync(() =>
                        {
                            OnBuildForms();
                        });
                    break;
                }
            }
            await Task.Delay(WatchTime);
        }
    }

    public void OnBuildForms()
    {
        var form = FormPageViewModel.MobileForm;
        var rows = form.Page.Rows;

        foreach (Row rowItem in rows)
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

            #region Textbox 文字輸入盒
            if (rowItem.Type == magicHelper.FormTexBox)
            {
                if (string.IsNullOrEmpty(rowItem.Title) == false)
                {
                    #region 文字輸入盒 的 前置說明文字
                    hostContainer.Children.Add(new Label()
                    {
                        ClassId = rowItem.Title,
                    }
                    .Text(rowItem.Title)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(12)
                    .Bold()
                    .TextColor(Color.FromArgb("bb888888")));
                    #endregion
                }
                //"Title": "姓名",
                //"Required": "true",
                //"Width": "24",
                //"name": "textbox001"
                //        },
                Entry entry = new Entry()
                {
                    ClassId = rowItem.Name,
                    BackgroundColor = magicHelper.FormViewBackgroundColor
                }
                .Margin(new Thickness(0, 0, 0, 20));

                hostContainer.Children.Add(entry);
            }
            #endregion

            #region RocDate 國曆日期
            if (rowItem.Type == magicHelper.FormRocDate)
            {
                if (string.IsNullOrEmpty(rowItem.Title) == false)
                {
                    #region 文字輸入盒 的 前置說明文字
                    hostContainer.Children.Add(new Label()
                    {
                        ClassId = rowItem.Title,
                    }
                    .Text(rowItem.Title)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(12)
                    .Bold()
                    .TextColor(Color.FromArgb("bb888888")));
                    #endregion
                }

                DatePicker datepicker = new DatePicker()
                {
                    ClassId = rowItem.Name,
                    BackgroundColor=magicHelper.FormViewBackgroundColor
                }
                .Margin(new Thickness(0, 0, 0, 20));

                hostContainer.Children.Add(datepicker);
            }
            #endregion

            #region RocDate 24小時時間
            if (rowItem.Type == magicHelper.FormTime24)
            {
                if (string.IsNullOrEmpty(rowItem.Title) == false)
                {
                    #region 文字輸入盒 的 前置說明文字
                    hostContainer.Children.Add(new Label()
                    {
                        ClassId = rowItem.Title,
                    }
                    .Text(rowItem.Title)
                    .Margin(new Thickness(0, 0, 0, 0))
                    .FontSize(12)
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
                .Margin(new Thickness(0,0,0,20));
                grid.Children.Add(timePicker);

                hostContainer.Children.Add(grid);
            }
            #endregion
        }
    }
}
