﻿using BaoYaoYao.Helpers;
using BaoYaoYao.Controls;
using BaoYaoYao.ViewModels;
using BaoYaoYao.Views;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Prism.Ioc;
using ZXing.Net.Maui;

namespace BaoYaoYao;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            //.UsePrismApp<App>(PrismStartup.Configure)

            .UseMauiApp<App>()
            .UseBarcodeReader()
            .UsePrism(prism =>
            {
                prism.OnAppStart("SplashPage");

                prism.RegisterTypes(container =>
                {
                    container.RegisterForNavigation<MainPage>()
                                 .RegisterInstance(SemanticScreenReader.Default);
                    container.RegisterForNavigation<SplashPage, SplashPageViewModel>();
                    container.RegisterForNavigation<LoginPage, LoginPageViewModel>();
                    container.RegisterForNavigation<RegistrationPage, RegistrationPageViewModel>();
                    container.RegisterForNavigation<NaviPage, NaviPageViewModel>();
                    container.RegisterForNavigation<VerifyPhonePage, VerifyPhonePageViewModel>();
                    container.RegisterForNavigation<ConnectPharmacyPage, ConnectPharmacyPageViewModel>();
                    container.RegisterForNavigation<ApplyPage, ApplyPageViewModel>();
                    container.RegisterForNavigation<ApplyHistoryPage, ApplyHistoryPageViewModel>();
                    container.RegisterForNavigation<BarCodeScanPage, BarCodeScanPageViewModel>();

                    container.Register<ProcessingPage>();

                    container.RegisterSingleton<MagicObjectHelper>();
                    container.RegisterSingleton<DataCheckVerifyHelper>();
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
