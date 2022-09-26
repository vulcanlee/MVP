using BaoYaoYao.Views;
using Prism.Ioc;

namespace BaoYaoYao;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UsePrism(prism=>
            {
                prism.OnAppStart("NavigationPage/MainPage");

                prism.RegisterTypes(container =>
                {
                    container.RegisterForNavigation<MainPage>()
                                 .RegisterInstance(SemanticScreenReader.Default);
                });
                prism.ConfigureServices(container => { });
                })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
