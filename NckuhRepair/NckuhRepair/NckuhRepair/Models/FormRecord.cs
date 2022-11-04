using CommunityToolkit.Mvvm.ComponentModel;

namespace NckuhRepair.Models;

/// <summary>
/// 用於表單紀錄頁面要顯示的清單內的個別紀錄
/// </summary>
public partial class FormRecordItem : ObservableObject
{
    [ObservableProperty]
    string title;
    [ObservableProperty]
    DateTime createAt;
    [ObservableProperty]
    FormIOModel form = new FormIOModel();
}
