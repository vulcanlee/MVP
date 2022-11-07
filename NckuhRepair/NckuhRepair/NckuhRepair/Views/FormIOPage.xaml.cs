using NckuhRepair.Helpers;
using NckuhRepair.Models;
using NckuhRepair.ViewModels;

namespace NckuhRepair.Views;

public partial class FormIOPage : ContentPage
{
    public FormIOPageViewModel FormIOPageViewModel { get; set; }
    private readonly MagicHelper magicHelper;
    private readonly FormIOBuilderHelper formIOBuilderHelper;
    private readonly IPageDialogService dialogService;

    public FormIOPage(MagicHelper magicHelper, FormIOBuilderHelper formIOBuilderHelper,
        IPageDialogService dialogService)
    {
        InitializeComponent();

        WatchBindingContextHasObjectAsync();

        this.magicHelper = magicHelper;
        this.formIOBuilderHelper = formIOBuilderHelper;
        this.dialogService = dialogService;
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
                        OnBuildDynamicForm();
                    });
                    break;
                }
            }
            await Task.Delay(WatchTime);
        }
    }

    public void OnBuildDynamicForm()
    {
        FormActionModel formAction = new FormActionModel()
        {
            EditMode = FormIOPageViewModel.FormEditMode,
        };
        var form = FormIOPageViewModel.FormIOModel;

        if(!string.IsNullOrEmpty(form.title))
        {
            this.Title = form.title;
        }

        foreach (Models.Component componentParent in form.components)
        {
            IView view = formIOBuilderHelper.GenerateView(componentParent, formAction, dialogService);
            hostContainer.Children.Add(view);

            if (componentParent.type == magicHelper.FormIOPanel)
            {
                VerticalStackLayout verticalStackLayout = (view as Border).Content as VerticalStackLayout;

                foreach (Models.Component componentChild in componentParent.components)
                {
                    IView viewChild = formIOBuilderHelper.GenerateView(componentChild, formAction, dialogService);
                    if (viewChild != null)
                    {
                        verticalStackLayout.Children.Add(viewChild);
                    }
                }
            }
        }
    }
}