using PrismApp2.Views;

namespace PrismApp2;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
                //.OnAppStart("NavigationPage/MainPage");
                .OnAppStart("SplashPage");
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<SplashPage>()
                     .RegisterInstance(SemanticScreenReader.Default);
        containerRegistry.RegisterForNavigation<MainPage>()
                     .RegisterInstance(SemanticScreenReader.Default);
        containerRegistry.RegisterForNavigation<NextPage>()
                     .RegisterInstance(SemanticScreenReader.Default);
    }
}
