using BaoYaoYao.Helpers;
using CommunityToolkit.Maui.Core.Platform;

namespace BaoYaoYao.Views;

public partial class NaviPage : NavigationPage
{
    private readonly MagicObjectHelper magicObjectHelper;

    public NaviPage(MagicObjectHelper magicObjectHelper)
	{
		InitializeComponent();
        this.magicObjectHelper = magicObjectHelper;
    }

    protected override void OnAppearing()
    {
#if ANDROID || IOS
        StatusBar.SetColor(magicObjectHelper.StatusBarBackgroundColor);
#endif
    }

}
