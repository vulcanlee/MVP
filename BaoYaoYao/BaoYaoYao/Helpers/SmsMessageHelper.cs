using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.Helpers
{
    public class SendMessage
    {
        public static string Message(string SB, string MSG, string DEST)
        {

            //string UID = "0902300913";
            //string PWD = "3npu";
            string UID = "0988018635";
            string PWD = "2olxjldu";

            //http://tw.every8d.com/api20/doc/EVERY8D%20HTTP%20API%E6%96%87%E4%BB%B6-v2%201-https.pdf API文件
            string URL = string.Format("https://oms.every8d.com/API21/HTTP/sendSMS.ashx?UID={0}&PWD={1}&SB={2}&MSG={3}&DEST={4}", UID, PWD, WebUtility.UrlEncode(SB), WebUtility.UrlEncode(MSG), WebUtility.UrlEncode(DEST));
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Method = "POST";
            try
            {
                using (WebResponse wr = req.GetResponse())
                using (Stream readStream = wr.GetResponseStream())
                using (StreamReader sr = new StreamReader(readStream, Encoding.GetEncoding("utf-8")))
                {
                    string Strxml = sr.ReadToEnd();
                    return Strxml;
                }
            }
            catch (Exception ee)
            {
                return "Response error";
            }
        }
        public static async Task<string> MessageHttpClient(string SB, string MSG, string DEST)
        {

            //string UID = "0902300913";
            //string PWD = "3npu";
            string UID = "0988018635";
            string PWD = "2olxjldu";

            //http://tw.every8d.com/api20/doc/EVERY8D%20HTTP%20API%E6%96%87%E4%BB%B6-v2%201-https.pdf API文件
            string URL = string.Format("https://oms.every8d.com/API21/HTTP/sendSMS.ashx?UID={0}&PWD={1}&SB={2}&MSG={3}&DEST={4}", UID, PWD, WebUtility.UrlEncode(SB), WebUtility.UrlEncode(MSG), WebUtility.UrlEncode(DEST));

            try
            {
                var client = new HttpClient();
                var responseMessage = await client.PostAsync(URL, new StringContent(SB));
                var result = await responseMessage.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)
            {
                return $"Response error, ({ex.Message})";
            }
        }
    }
}
