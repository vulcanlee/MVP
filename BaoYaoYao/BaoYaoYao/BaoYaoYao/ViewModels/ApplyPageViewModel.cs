using BaoYaoYao.Helpers;
using BaoYaoYao.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.ViewModels
{
    public partial class ApplyPageViewModel :ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;
        private readonly MagicObjectHelper magicObjectHelper;
        [ObservableProperty]
        FormRecord formRecord = new FormRecord();
        //[ObservableProperty]
        //ImageSource imagePreview = null;

        [ObservableProperty]
        bool isShowQRCodeView = true;

        [ObservableProperty]
        string qRCodeScanResult = "";

        public ApplyPageViewModel(INavigationService navigationService,
            MagicObjectHelper magicObjectHelper)
        {
            this.navigationService = navigationService;
            this.magicObjectHelper = magicObjectHelper;
            TestCommand = new RelayCommand(async() =>
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
                            sourceStream.Seek(0, SeekOrigin.Begin);
                            FormRecord.Photo = ImageSource.FromFile(localFilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                }
            });

        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(magicObjectHelper.FormRecordName))
            {
                FormRecord = parameters.GetValue<FormRecord>(magicObjectHelper.FormRecordName);
            }
        }

        public RelayCommand TestCommand { get; set; }

        [RelayCommand]
        public void ShowQRCodeView()
        {
            IsShowQRCodeView = true;

            NavigationParameters para = new NavigationParameters();
            para.Add(magicObjectHelper.FormRecordName, formRecord);

            navigationService.NavigateAsync("/BarCodeScanPage", para);
        }
        //[RelayCommand]
        //public async Task TakePhoto()
        //{
        //    if (MediaPicker.Default.IsCaptureSupported)
        //    {
        //        try
        //        {

        //            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

        //            if (photo != null)
        //            {
        //                // save the file into local storage
        //                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

        //                using Stream sourceStream = await photo.OpenReadAsync();
        //                using FileStream localFileStream = File.OpenWrite(localFilePath);

        //                await sourceStream.CopyToAsync(localFileStream);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"{ex.Message}");
        //        }
        //    }
        //}
    }
}
