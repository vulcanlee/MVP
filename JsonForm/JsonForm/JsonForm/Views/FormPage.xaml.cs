using JsonForm.ViewModels;

namespace JsonForm.Views;

public partial class FormPage : ContentPage
{
    public FormPageViewModel FormPageViewModel { get; set; }
    public FormPage()
    {
        InitializeComponent();

        WatchBindingContextHasObjectAsync();
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
    //protected override void OnAppearing()
    //{
    //	if(FormPageViewModel != null)
    //	{
    //		FormPageViewModel.BuildFormObject = this.OnBuildForms;
    //       }
    //	base.OnAppearing();
    //}
    public void OnBuildForms()
    {

    }
}