using CommunityToolkit.Mvvm.ComponentModel;
using JsonForm.Models;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonForm.ViewModels
{
    public partial class FormPageViewModel:ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;
        public FormPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public MobileForm MobileForm { get; set; } = null;
        public string Json { get; set; }
        public Action BuildFormObject { get; set; }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            MobileForm = parameters.GetValue<MobileForm>("MobileForm");
            Json = parameters.GetValue<string>("JSON");

        }
    }
}
