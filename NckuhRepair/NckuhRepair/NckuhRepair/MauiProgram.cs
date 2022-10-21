using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using NckuhRepair.Helpers;
using NckuhRepair.ViewModels;
using NckuhRepair.Views;

namespace NckuhRepair;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkitMarkup()
            .UseMauiCommunityToolkit()
            .UsePrism(prism =>
            {
                prism.OnAppStart("SplashPage");

                prism.RegisterTypes(containerRegistry =>
                {
                    containerRegistry.RegisterForNavigation<MainPage>()
                    .RegisterInstance(SemanticScreenReader.Default);
                    containerRegistry.RegisterForNavigation<SplashPage, SplashPageViewModel>();
                    containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
                    containerRegistry.RegisterForNavigation<FormGallery, FormGalleryViewModel>();
                    containerRegistry.RegisterForNavigation<NaviPage, NaviPageViewModel>();
                    containerRegistry.RegisterForNavigation<MemberPage, MemberPageViewModel>();
                    containerRegistry.RegisterForNavigation<FormRecord, FormRecordViewModel>();
                    containerRegistry.RegisterForNavigation<FormIOPage, FormIOPageViewModel>();

                    containerRegistry.RegisterSingleton<MagicHelper>();
                    containerRegistry.Register<FormIOVerifyHelper>();
                });
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
