using ZXing.Net.Maui.Controls;
using ZXing.Net.Maui;
using BaoYaoYao.ViewModels;

namespace BaoYaoYao.Views;

public partial class BarCodeScanPage : ContentPage
{
    BarCodeScanPageViewModel barCodeScanPageViewModel;
    public BarCodeScanPage()
    {
		InitializeComponent();

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions()
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = false,
        };

        this.Appearing += (s, e) =>
        {
            barCodeScanPageViewModel = this.BindingContext as BarCodeScanPageViewModel;
        };
    }

    private void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (barCodeScanPageViewModel != null)
        {
            if (e.Results.Length > 0)
            {
                BarcodeResult barcodeResult = e.Results[0];
                barCodeScanPageViewModel.QRCodeScanResult = barcodeResult.Value;
                barCodeScanPageViewModel.FormRecord.QRCode = barcodeResult.Value;
                barCodeScanPageViewModel.GoBackApplyPage();
            }
        }

    }
}