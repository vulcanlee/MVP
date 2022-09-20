using CommunityToolkit.Maui.Markup;
using JsonForm.Helps;
using JsonForm.Models;
using JsonForm.ViewModels;
using Newtonsoft.Json;

namespace JsonForm.Views;

public partial class FormPage : ContentPage
{
    public FormPageViewModel FormPageViewModel { get; set; }
    private readonly MagicHelper magicHelper;
    private readonly FormBuilderHelper formBuilderHelper;

    public FormPage(MagicHelper magicHelper, FormBuilderHelper formBuilderHelper)
    {
        InitializeComponent();

        WatchBindingContextHasObjectAsync();
        this.magicHelper = magicHelper;
        this.formBuilderHelper = formBuilderHelper;
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
            hostContainer.Children.Add(formBuilderHelper.GenerateView(rowItem));
        }
    }
}
