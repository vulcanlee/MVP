using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using JsonForm.Helps;
using JsonForm.ViewModels;
using JsonForm.Views;
using Prism.Ioc;

namespace JsonForm;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UsePrism(prism =>
            {
                prism.OnAppStart("NavigationPage/MainPage");

                prism.RegisterTypes(containerRegistry =>
                {
                    containerRegistry.RegisterForNavigation<MainPage>()
                    .RegisterInstance(SemanticScreenReader.Default);

                    containerRegistry.RegisterForNavigation<FormPage, FormPageViewModel>();
                    containerRegistry.RegisterForNavigation<FormIOPage, FormIOPageViewModel>();

                    containerRegistry.RegisterSingleton<MagicHelper>();
                    containerRegistry.RegisterSingleton<FormBuilderHelper>();
                    containerRegistry.RegisterSingleton<FormIOBuilderHelper>();
                });

                //prism.ConfigureServices(container =>
                //{
                //    container.RegisterForNavigation<MainPage>();
                //});
            })
            .UseMauiCommunityToolkitMarkup()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
