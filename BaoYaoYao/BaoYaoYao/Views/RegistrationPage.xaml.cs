using BaoYaoYao.Events;
using BaoYaoYao.Helpers;
using BaoYaoYao.Controls;
using CommunityToolkit.Maui.Core.Platform;
using CommunityToolkit.Maui.Views;

namespace BaoYaoYao.Views;

public partial class RegistrationPage : ContentPage
{
    private readonly MagicObjectHelper magicObjectHelper;
    private readonly ProcessingPage processingPage;
    private readonly IEventAggregator eventAggregator;

    public RegistrationPage(MagicObjectHelper magicObjectHelper,
        ProcessingPage processingPage, IEventAggregator eventAggregator)
    {
        InitializeComponent();
        this.magicObjectHelper = magicObjectHelper;
        this.processingPage = processingPage;
        this.eventAggregator = eventAggregator;

        this.eventAggregator.GetEvent<ShowPopupEvent>().Subscribe(x =>
        {
            if (x.IsShow == true && x.Target == magicObjectHelper.PageRegistration)
                this.ShowPopup(processingPage);
        });
    }

    protected override void OnAppearing()
    {
#if ANDROID || IOS
        StatusBar.SetColor(magicObjectHelper.StatusBarBackgroundColor);
#endif
    }
}