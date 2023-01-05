# 曜瑄電子表單輔助支援工具 Ver 1.0.4

本工具開發目的共有兩個：
* 提供壓力測試，用來評估在有安裝電子表單的主機上，可以承受與提供多少表單操作數量，與觀察每次表單操作下的反應時間是多少。
* 可以用於將每個表單的 .dll 檔案，強制載入到應用程式及區內的記憶體內，這樣做的目的，有兩個；第一個就是該電子表單若尚未在系統中產生相對應的 .dll 檔案(每次部署新版本的表單系統後，將會造成在系統內的快取 .dll 檔案被清除)，第二個就是若這些表單使用的 .dll 檔案尚未讀入記憶體內，透過個支援工具協助，使用模擬打開電子表單的作法，讓電子表單使用的相對應 .dll 將會存在於應用程式集區記憶體內。這樣將會造成一個效果，那就是下次當實際要使用某個電子表單的時候，將會提供比較好的反應時間。

# 使用說明

## 下載與啟動

請透過 [https://www.asuswebstorage.com/navigate/a/#/s/5FBB961ADF1E4F3C97188FC18FC4DA07Y](https://www.asuswebstorage.com/navigate/a/#/s/5FBB961ADF1E4F3C97188FC18FC4DA07Y) 連結下載此表單支援工具的壓縮檔案到本機電腦上。

下載後的壓縮檔案名稱為 FormTesting.zip ，請將此壓縮檔案解壓縮在本機電腦內的任一目錄內，接著，打開命令提示字元視窗，切換到此目錄下，最後輸入 **FormTesting.exe** 便可以啟動此表單支援工具。

## 工具運作類型說明

這個表單支援工具共支援三種運行模式，要切換這些運作模式，請修改 appsetting.json 設定檔案

### PerformanceTesting 壓力測試模式

在此模式下，將會大量開啟指定數量的表單，觀察系統是否可以承受在瞬間處理這些大量的表單存取動作

### WarmingUp 電子表單暖機模式

透過預設的電子表單 ID，將會模擬使用者開啟電子表單動作，逐一開啟這些電子表單；若該電子表單尚未在此系統開啟使用過，將會自動檢查是否要產生相對應的 .dll 檔案，並且將其載入到應用程式集區的記憶體內，如此，將會加速使用者操作電子表單的使用體驗與反應速度。

### DistributionTesting 30秒內可以提供表單吞吐量

在這個模式下，將會把指定要開啟的電子表單數量，隨機在 30 秒內的任何時間來開啟，如此，便可以觀察後端伺服器運作情況與效能指標數據，了解到這個系統可以提供的最大表單操作動作數量是多少。

## 設定說明

想要進行這個工具的執行參數調整，請打開 appsetting.json 檔案

在這個設定檔案內，將會有 [Target] & [TestingNodes] 兩個區段

### Target

這個區段將會用來宣告，這個支援工具將會使用哪個設定節點參數來執行，其中， [TestingNode] 屬性將會用來宣告要執行的設定節點名稱；在底下的例子將表示這個支援工具將會取得 "ExentricSitePerformanceTest" 這個節點設定值來運行，而 [TestingNode] 屬性可以使用的名稱，將會定義於 [TestingNodes] > [Mode] 屬性內

```json
 "Target": {
    "TestingNode": "ExentricSitePerformanceTest"
  }
```

### TestingNodes

這裡將會定義這個支援工具有哪些設定節點參數可以來執行 [TestingNodes] 這個屬性值表示為一個陣列，會有多個節點可以來宣告，每個節點內會有底下的屬性存在。

* Mode
  
  字串，可以指定這些字串名稱 PerformanceTesting , WarmingUp , DistributionTesting  
  > 分別表示為 壓力測試模式、電子表單暖機模式、30秒內可以提供表單吞吐量
* Title

  字串，這個設定節點的名稱，該名稱將可以用於 [Target] > [TestingNode] 的屬性值，指定此次工具執行時候，將會採用這個節點的設定值來運作。

* Description

  字串，備註文字，用來說明這個節點的用途與目的

* Host

  物件，這個節點內，共用三個屬性要設定，分別為 Account 登入系統用使用的帳號、Password 登入系統要使用的密碼、ConnectHost 連線到此表單的通訊協定與主機名稱，例如為 http://fmsap1p.kmuh.gov.tw ，請注意，這裡不要在最後面加入 **/** 文字

* NumberOfRequests

  整數，要開啟表單的請求次數
* MaxHttpClients

  整數，預先準備的 HttpClient 數量
* ForceSleepMilliSecond

  整數，模擬使用者登入完成系統之後，需要暫時休息的時間，單位為 1/1000 秒
* RecordRawHtmlContent
  
  布林值，是否要將抓取到的 HTML 表單原始標記內容，寫入到 Log 目錄下
* RecordToFileHttpClientPerformanceMeasure

  布林值，是否要將 HttpClient 量測到的效能數據，同時也要寫入到 Report 目錄下
* RecordToFileRemotePerformanceMeasure

  布林值，是否要將遠端伺服器上量測到的效能數據，同時也要寫入到 Report 目錄下
* HttpClientPerformanceMeasure

  布林值，是否要顯示 HttpClient 的運作效能統計報表
* RemotePerformanceMeasure

  布林值，是否要使用遠端伺服器蒐集之效能數據(若該表單系統不支援此功能，請勿開啟)
* RemotePerformanceMaxLatencyAnalysis

  布林值，顯示出在後端伺服器程式碼，在執行哪個區塊段落的時候，將會耗費最多的時間與要執行該區段程式碼前延遲了多久？可以使用這些資訊來判斷出後端程式碼是哪個區塊執行最慢與延誤最久才能開始執行，透過這些數據將會有助於系統效能分析與除錯。

  底下為開啟此設定值的範例輸出結果

```
http://www.posly.cc/Forms/CustomForm?FormId=0404eb92-17ec-4ee4-ba49-775818a89f0e&Patno=%25pi&Navbar=hide
Request Estimated : 547
Max Node Estimated : 125 - _CustomFormLayout.cshtml
Max Latency : 16 - Application_BeginRequest  > CustomForm 全部程式碼需要時間

http://www.posly.cc/Forms/CustomForm?FormId=0253b745-d3f0-4ae2-acbb-21f5fd4c9d31d&Patno=%25pi&Navbar=hide
Request Estimated : 547
Max Node Estimated : 125 - _CustomFormLayout.cshtml
Max Latency : 16 - Application_BeginRequest  > CustomForm 全部程式碼需要時間

http://www.posly.cc/Forms/CustomForm?FormId=03f3fb5a-2bcd-472c-a1f0-1bccd49078b5&Patno=%25pi&Navbar=hide
Request Estimated : 531
Max Node Estimated : 94 - CustomForm 全部程式碼需要時間
Max Latency : 109 - _ViewStart.cshtml > _CustomFormLayout.cshtml
```

* RemotePerformanceOutputDetail

  布林值，顯示每個後端程式碼區段的總共執行耗時多久與延誤多久明細

  底下為開啟此設定值的範例輸出結果

```
http://www.posly.cc/Forms/CustomForm?FormId=0135378c-b579-4139-bb6b-712c15bab16e&Patno=%25pi&Navbar=hide
【Application_BeginRequest  (00:00:00.6093745) > [0] 】
【CustomForm 全部程式碼需要時間 (00:00:00.3906210) > [16] 】
【_ViewStart.cshtml (00:00:00) > [0] 】
【_CustomFormLayout.cshtml (00:00:00.0937575) > [62] 】
【_Layout2.cshtml (00:00:00.0468713) > [0] 】

http://www.posly.cc/Forms/CustomForm?FormId=01d335ed-b3ad-454b-97b2-c5c5cf4a5e24&Patno=%25pi&Navbar=hide
【Application_BeginRequest  (00:00:00.4999981) > [0] 】
【CustomForm 全部程式碼需要時間 (00:00:00.3281286) > [31] 】
【_ViewStart.cshtml (00:00:00) > [0] 】
【_CustomFormLayout.cshtml (00:00:00.0156251) > [16] 】
【_Layout2.cshtml (00:00:00.0937467) > [0] 】

http://www.posly.cc/Forms/CustomForm?FormId=04ad90ae-fc5c-49fd-aa27-956413f3baf0&Patno=%25pi&Navbar=hide
【Application_BeginRequest  (00:00:00.2031280) > [0] 】
【CustomForm 全部程式碼需要時間 (00:00:00.0938634) > [16] 】
【_ViewStart.cshtml (00:00:00) > [0] 】
【_CustomFormLayout.cshtml (00:00:00.0780130) > [0] 】
【_Layout2.cshtml (00:00:00.0156238) > [0] 】
```
* RemotePerformanceOutputNodeDetail

  布林值，另外一種模式來顯示每個後端程式碼區段的總共執行耗時多久與延誤多久明細

  底下為開啟此設定值的範例輸出結果

```
http://www.posly.cc/Forms/CustomForm?FormId=03e73ba7-6c33-4726-b77e-74f4dc98ba1e&Patno=%25pi&Navbar=hide
      Application_BeginRequest B:03:39.6984 F:03:40.1516 E:00:00:00.4531175
                    CustomForm B:03:39.7141 F:03:39.9953 E:00:00:00.2812485
             _ViewStart.cshtml B:03:39.9953 F:03:39.9953 E:00:00:00
      _CustomFormLayout.cshtml B:03:39.9953 F:03:40.0266 E:00:00:00.0313410
               _Layout2.cshtml B:03:40.0266 F:03:40.1359 E:00:00:00.1092815

http://www.posly.cc/Forms/CustomForm?FormId=039de4b7-a813-43de-a270-6270a4283eeb&Patno=%25pi&Navbar=hide
      Application_BeginRequest B:03:39.6984 F:03:40.1516 E:00:00:00.4531175
                    CustomForm B:03:39.7141 F:03:39.9641 E:00:00:00.2500123
             _ViewStart.cshtml B:03:39.9641 F:03:39.9641 E:00:00:00
      _CustomFormLayout.cshtml B:03:39.9641 F:03:39.9953 E:00:00:00.0312362
               _Layout2.cshtml B:03:39.9953 F:03:40.0734 E:00:00:00.0781266

http://www.posly.cc/Forms/CustomForm?FormId=0244d858-c493-4529-9aa6-1c4b07d7e08c&Patno=%25pi&Navbar=hide
      Application_BeginRequest B:03:39.6984 F:03:40.1516 E:00:00:00.4531175
                    CustomForm B:03:39.7141 F:03:39.9953 E:00:00:00.2812485
             _ViewStart.cshtml B:03:39.9953 F:03:39.9953 E:00:00:00
      _CustomFormLayout.cshtml B:03:39.9953 F:03:40.0266 E:00:00:00.0313410
               _Layout2.cshtml B:03:40.0266 F:03:40.1359 E:00:00:00.1092815
```
* ResetRemotePerformanceMeasureEndpoint

  字串，宣告要清除伺服器端效能量測數據的服務端點
  
* GetRemotePerformanceMeasureEndpoint

  字串，宣告要取得伺服器端效能量測數據的服務端點
* FormEndpointPrefix

  字串，模擬開啟表單的 URL的前半段內容，也就是在 Form Id 前的 URL 文字。例如: "/Forms/CustomForm?FormId="
* FormEndpointPost

  字串，模擬開啟表單的 URL的後半段內容，也就是在 Form Id 前的 URL 文字。例如: "&Patno=123&Navbar=hide"
* FormIds

  字串陣列，這裡將會列出可以用的表單 Form Id 清單集合

