using BaoYaoYao.ViewModels;
using BaoYaoYao.Views;

namespace BaoYaoYao;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
                .OnAppStart("SplashPage");
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainPage>()
            .RegisterInstance(SemanticScreenReader.Default);
        containerRegistry.RegisterForNavigation<SplashPage, SplashPageViewModel>();
        containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        containerRegistry.RegisterForNavigation<RegistrationPage, RegistrationPageViewModel>();
        containerRegistry.RegisterForNavigation<NaviPage, NaviPageViewModel>();
        containerRegistry.RegisterForNavigation<VerifyPhonePage, VerifyPhonePageViewModel>();
        containerRegistry.RegisterForNavigation<ConnectPharmacyPage, ConnectPharmacyPageViewModel>();
        containerRegistry.RegisterForNavigation<ApplyPage, ApplyPageViewModel>();
        containerRegistry.RegisterForNavigation<ApplyHistoryPage, ApplyHistoryPageViewModel>();
    }
}
