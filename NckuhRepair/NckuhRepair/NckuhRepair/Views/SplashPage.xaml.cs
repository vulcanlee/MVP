using CommunityToolkit.Maui.Core.Platform;
using NckuhRepair.Helpers;

namespace NckuhRepair.Views;

public partial class SplashPage : ContentPage
{
	private readonly MagicHelper magicObjectHelper;

	public SplashPage(MagicHelper magicObjectHelper)
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