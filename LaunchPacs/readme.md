# G3Launch Gateway 說明

# 測試效果

* 下載桌面應用程式，請透過底下 URL 來下載

https://www.asuswebstorage.com/navigate/a/#/s/CD6A98FE0F6F4EE988153AED5F38E149Y

* 將此壓縮檔案解壓縮到本機電腦任何地方
* 在解壓縮目錄下將會看到 LaunchPacs.exe 檔案
* 使用滑鼠雙擊此程式，或者開啟命令提示字元視窗，執行這個檔案
* 在壓縮檔案內查看到 test.html 檔案，並請使用任一瀏覽器打開這個檔案
* 在網頁中將會看到兩個按鈕，點選這兩個按鈕，將會看到不同 PACS 內的圖片

# 查看版本資訊

http://localhost:14928/Launch/Version

# 呼叫端點參數說明

若想要使用底下方式來開啟 PACS Viewer 

C:\INFINITT\viewer\G3Launcher.exe http://10.1.1.142+admin+nimda++S+RU799OR39MJ3BCF1+BET0001

這個時候，需要使用底下端點 URL 來呼叫

其中，每個引數的字串值必須使用 URL Encoder 來進行編碼

http://localhost:14928/Launch?LID=admin&LPW=nimda&AN=RU799OR39MJ3BCF1&PID=BET0001

http://localhost:14928/Launch?LID=admin&LPW=nimda&AN=S21B2R1908569&PID=01658998

## 參數說明

* LID

  帳號

* LPW

  密碼

* AN

  Accession No

* PID

  Patient Id
  
# PACS Viewer 程式位置 與 主機 URL 參數設定

在壓縮檔案內，將會找到 appsettings.json 檔案，打該這個檔案，將會看到如下內容

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:14928"
      }
    }
  },
  "PACS": {
    "HiddenWindown": true,
    "PacsProgramPath": "C:\\INFINITT\\viewer\\G3Launcher.exe",
    "IisUrl": "http://10.1.1.142"
  }
}
```

* HiddenWindown

  這個屬性將會宣告這個命令提示字元視窗啟動之後，是否需要隱藏

* PacsProgramPath

  這個屬性將會宣告為本機 PACS Viewer 所在的位置

* IisUrl

  這個屬性將會宣告為指向 PACS 主機的 URL

# 啟動引數

## quit

若在啟動此程式的時候，有加入 `quit` 引數，則表示該程式會於啟動之後，會將已經啟動過的同樣程式先予以終止執行，接著，會退出此次程式的執行。

底下為使用範例

LaunchPacs.exe quit

## hide

若在啟動此程式的時候，有加入 `hide` 引數，則表示該程式會使用隱藏式窗模式來執行。
>礙於 Windows 11 的問題，當加入 hide 參數之後，程式將會使用 ProcessStartInfo.CreateNoWindow 宣告使用沒有視窗模式來運行，並且使用 Process.Start 重新啟動這隻程式，而當前的程式則會立即結束執行，也就是另外產生一個新的 Process

底下為使用範例

LaunchPacs.exe quit

