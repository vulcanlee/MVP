using CommunityToolkit.Maui.Core.Platform;
using NckuhRepair.Helpers;

namespace NckuhRepair.Views;

public partial class SplashPage : ContentPage
{
	private readonly MagicObjectHelper magicObjectHelper;

	public SplashPage(MagicObjectHelper magicObjectHelper)
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