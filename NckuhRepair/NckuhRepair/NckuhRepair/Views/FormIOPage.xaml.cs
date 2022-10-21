using NckuhRepair.Helpers;
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
    /// �I���d�ݬO�_ ViewModel �w�g�Q�]�w�� BindingContext ���F
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

        if(!string.IsNullOrEmpty(form.title))
        {
            this.Title = form.title;
        }

        foreach (Models.Component componentParent in form.components)
        {
            IView view = formIOBuilderHelper.GenerateView(componentParent, dialogService);
            hostContainer.Children.Add(view);

            if (componentParent.type == magicHelper.FormIOPanel)
            {
                VerticalStackLayout verticalStackLayout = (view as Border).Content as VerticalStackLayout;

                foreach (Models.Component componentChild in componentParent.components)
                {
                    IView viewChild = formIOBuilderHelper.GenerateView(componentChild, dialogService);
                    if (viewChild != null)
                    {
                        verticalStackLayout.Children.Add(viewChild);
                    }
                }
            }
        }
    }
}