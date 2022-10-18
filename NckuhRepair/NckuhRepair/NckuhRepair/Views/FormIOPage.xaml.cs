using NckuhRepair.Helpers;
using NckuhRepair.ViewModels;

namespace NckuhRepair.Views;

public partial class FormIOPage : ContentPage
{
    public FormIOPageViewModel FormIOPageViewModel { get; set; }
    private readonly MagicHelper magicHelper;
    private readonly FormIOBuilderHelper formIOBuilderHelper;

    public FormIOPage(MagicHelper magicHelper, FormIOBuilderHelper formIOBuilderHelper)
    {
        InitializeComponent();

        WatchBindingContextHasObjectAsync();

        this.magicHelper = magicHelper;
        this.formIOBuilderHelper = formIOBuilderHelper;
    }

    /// <summary>
    /// 背景查看是否 ViewModel 已經被設定到 BindingContext 內了
    /// </summary>
    /// <returns></returns>
    async Task WatchBindingContextHasObjectAsync()
    {
        int WatchTime = 100;
        while (true)
        {
            if (this.BindingContext != null)
            {
                FormIOPageViewModel = this.BindingContext as FormIOPageViewModel;
                if (FormIOPageViewModel.ReadSuccessful == true)
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
        var form = FormIOPageViewModel.FormIOModel;

        foreach (Models.Component componentParent in form.components)
        {
            IView view = formIOBuilderHelper.GenerateView(componentParent);
            hostContainer.Children.Add(view);

            if (componentParent.type == magicHelper.FormIOPanel)
            {
                VerticalStackLayout verticalStackLayout = (view as Border).Content as VerticalStackLayout;

                foreach (Models.Component componentChild in componentParent.components)
                {
                    IView viewChild = formIOBuilderHelper.GenerateView(componentChild);
                    if (viewChild != null)
                    {
                        verticalStackLayout.Children.Add(viewChild);
                    }
                }
            }
        }
    }
}