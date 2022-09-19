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
                await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        OnBuildForms();
                    });
                break;  
            }
            await Task.Delay(WatchTime);
        }
    }

    public void OnBuildForms()
    {
        var form = FormPageViewModel.MobileForm;
        var rows = form.Page.Rows;

        foreach (JsonForm.Models.Row rowItem in rows)
        {
            #region Textbox ¤å¦r¿é¤J²°
            if(rowItem.Type == magicHelper.FormTexBox)
            {

            }
            #endregion
        }
    }
}