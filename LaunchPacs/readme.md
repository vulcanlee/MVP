# G3Launch Gateway 說明

# 安裝與設定說明

sc create "PACS_Viewer" binPath="C:\Vulcan\Github\MVP\LaunchPacs\LaunchPacs\bin\Release\net7.0\publish\LaunchPacs.exe"

sc start "PACS_Viewer"

sc stop "PACS_Viewer"

sc.exe delete "PACS_Viewer"

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

# 安裝與設定說明


