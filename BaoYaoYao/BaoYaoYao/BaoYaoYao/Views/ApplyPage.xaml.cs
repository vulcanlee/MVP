using BaoYaoYao.ViewModels;
using ZXing.Net.Maui;

namespace BaoYaoYao.Views;

public partial class ApplyPage : ContentPage
{
    ApplyPageViewModel applyPageViewModel = null;
    public ApplyPage()
    {
        InitializeComponent();

        //cameraBarcodeReaderView.Options = new BarcodeReaderOptions()
        //{
        //    Formats = BarcodeFormats.All,
        //    AutoRotate = true,
        //    Multiple = false,
        //};

        this.Appearing += (s, e) =>
        {
            applyPageViewModel = this.BindingContext as ApplyPageViewModel;
        };

    }

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        if (applyPageViewModel != null)
        {
            if (e.Results.Length > 0)
            {
                BarcodeResult barcodeResult = e.Results[0];
                applyPageViewModel.QRCodeScanResult = barcodeResult.Value;
                applyPageViewModel.IsShowQRCodeView = false;
            }
        }
    }
}