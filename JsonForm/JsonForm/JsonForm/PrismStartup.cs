using JsonForm.Helps;
using JsonForm.ViewModels;
using JsonForm.Views;

namespace JsonForm;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
                .OnAppStart("NavigationPage/MainPage");
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainPage>()
                     .RegisterInstance(SemanticScreenReader.Default);

        containerRegistry.RegisterForNavigation<FormPage, FormPageViewModel>();
        containerRegistry.RegisterSingleton<MagicHelper>();
    }
}
