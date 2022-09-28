﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.ViewModels
{
    public class ApplyPageViewModel :INavigatedAware
    {
        private readonly INavigationService navigationService;

        ImageSource imagePreview = null;

        public ApplyPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

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
        }
        public RelayCommand TestCommand { get; set; }
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