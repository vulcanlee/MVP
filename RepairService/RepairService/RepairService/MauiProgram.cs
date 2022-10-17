﻿using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using RepairService.Views;

namespace RepairService;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup()
            .UsePrism(prism =>
            {
                prism.OnAppStart("SplashPage");

                prism.RegisterTypes(containerRegistry =>
                {
                    containerRegistry.RegisterForNavigation<MainPage>()
                    .RegisterInstance(SemanticScreenReader.Default);
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
