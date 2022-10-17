using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NckuhRepair.ViewModels
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

        #region ViewModel 綁定命令
        [RelayCommand]
        public async Task Login()
        {
            await Task.Yield();
            await navigationService.CreateBuilder()
                .AddSegment<FormGalleryViewModel>()
                .NavigateAsync();
        }
        #endregion
    }
}
