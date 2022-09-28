using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;

        public LoginPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        #region 綁定命令使用
        [RelayCommand]
        public async Task Register()
        {
            NavigationParameters para = new NavigationParameters();
            para.Add(KnownNavigationParameters.Animated, false);

            await navigationService.NavigateAsync("/NaviPage/RegistrationPage", para);

            //await navigationService.CreateBuilder()
            //    .UseAbsoluteNavigation()
            //    .AddSegment<NaviPageViewModel>()
            //    .AddSegment<RegistrationPageViewModel>()
            //    .NavigateAsync();
        }
        [RelayCommand]
        public async Task Login()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                try
                {

                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                    if (photo != null)
                    {
                        // save the file into local storage
                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                        using Stream sourceStream = await photo.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);

                        await sourceStream.CopyToAsync(localFileStream);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
            //await navigationService.NavigateAsync("/NaviPage/ConnectPharmacyPage?Animated=false");
        }
        #endregion
    }
}
