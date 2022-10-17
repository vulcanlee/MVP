using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
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
                prism.OnAppStart("MainPage");

                prism.RegisterTypes(containerRegistry =>
                {
                    containerRegistry.RegisterForNavigation<MainPage>()
                    .RegisterInstance(SemanticScreenReader.Default);

                });

                //prism.ConfigureServices(container =>
                //{
                //    container.RegisterForNavigation<MainPage>();
                //});
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
