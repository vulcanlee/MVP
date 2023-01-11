# G3Launch Gateway 說明

# 測試效果

* 下載桌面應用程式，請透過底下 URL 來下載

https://www.asuswebstorage.com/navigate/a/#/s/CD6A98FE0F6F4EE988153AED5F38E149Y

* 將此壓縮檔案解壓縮到本機電腦任何地方
* 在解壓縮目錄下將會看到 LaunchPacs.exe 檔案
* 使用滑鼠雙擊此程式，或者開啟命令提示字元視窗，執行這個檔案
* 在壓縮檔案內查看到 test.html 檔案，並請使用任一瀏覽器打開這個檔案
* 在網頁中將會看到兩個按鈕，點選這兩個按鈕，將會看到不同 PACS 內的圖片

# 呼叫端點參數說明

若想要使用底下方式來開啟 PACS Viewer 

C:\INFINITT\viewer\G3Launcher.exe http://10.1.1.142+admin+nimda++S+RU799OR39MJ3BCF1+BET0001

這個時候，需要使用底下端點 URL 來呼叫

其中，每個引數的字串值必須使用 URL Encoder 來進行編碼

http://localhost:14928/Launch?ViewerPath=C%3A%5CINFINITT%5Cviewer%5CG3Launcher.exe&IIS=http%3A%2F%2F10.1.1.142&UserId=admin&UserPassword=nimda&AccessionNo=RU799OR39MJ3BCF1&PatientId=BET0001

http://localhost:14928/Launch?ViewerPath=C%3A%5CINFINITT%5Cviewer%5CG3Launcher.exe&IIS=http%3A%2F%2F10.1.1.142&UserId=admin&UserPassword=nimda&AccessionNo=S21B2R1908569&PatientId=01658998

## 參數說明

* ViewerPath

  PACS Viewer 的完整路徑

* IIS

  指向 PACS Web 主機的網址

* UserId

  帳號

* UserPassword

  密碼

* AccessionNo

  AccessionNo

* PatientId

  PatientId
  
# 測試方式

參考 test.html 網站，並請使用任一瀏覽器打開該網址，實際操作與驗證


