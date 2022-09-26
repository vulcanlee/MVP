using System.Collections.Generic;

namespace SimpleIdea
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Console.WriteLine($"{((e.ExceptionObject) as Exception).Message}");
                Console.WriteLine($"{((e.ExceptionObject) as Exception).StackTrace}");
                e.
            };

            throw new NotImplementedException();


            return;
            // 在這裡指定表單內使用的變數，決定是否要顯示文字
            // 或者直接要顯示該變數文字內容
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("Radio1", "true");
            map.Add("Checkbox1", "false");
            map.Add("Label1", "--這裡的文字內容來自於變數內--");
            string ReportSource =
                "這是報告{單選結果1:Radio:Radio1}，這裡帶入純文字變數{:Label:Label1}，有選擇的狀態{與檢查盒:Checkbox:Checkbox1}";

            GenerateReport(ReportSource, map);
        }

        private static void GenerateReport(string reportSource, Dictionary<string, string> map)
        {
            var resultReport = ReplaceAllVariable(reportSource, map);
            Console.WriteLine(resultReport);
        }

        private static string ReplaceAllVariable(string reportSource, Dictionary<string, string> map)
        {
            string resultReport = reportSource;

            #region 
            string leftEscape = "{";
            string rightEscape = "}";
            string variableEscape = ":";
            while (true)
            {
                var leftIndex = resultReport.IndexOf(leftEscape);
                var rightIndex = resultReport.IndexOf(rightEscape);
                if (leftIndex == -1 || rightIndex == -1)
                    break;

                var parseToken = resultReport
                    .Substring(leftIndex, rightIndex - leftIndex + 1);
                var parseTokenSource = parseToken;

                parseToken = parseToken.Replace(leftEscape, "");
                parseToken = parseToken.Replace(rightEscape, "");
                parseToken = parseToken.Trim();
                var variables = parseToken.Split(variableEscape);
                if (variables.Length != 3)
                    break;

                string constantPrefix = variables[0];
                string variableType = variables[1];
                string variableName = variables[2];
                string replaceWord = "";
                if (variableType == "Radio")
                {
                    if (map.ContainsKey(variableName))
                    {
                        var variableValue = map[variableName];
                        if (variableValue == "true")
                        {
                            replaceWord = parseToken.Substring(0, constantPrefix.Length);
                        }
                        resultReport = resultReport.Replace(parseTokenSource, replaceWord);
                    }
                    else
                        break;
                }
                else if (variableType == "Checkbox")
                {
                    if (map.ContainsKey(variableName))
                    {
                        var variableValue = map[variableName];
                        if (variableValue == "true")
                        {
                            replaceWord = parseToken.Substring(0, constantPrefix.Length);
                        }
                        resultReport = resultReport.Replace(parseTokenSource, replaceWord);
                    }
                    else
                        break;
                }
                else if (variableType == "Label")
                {
                    if (map.ContainsKey(variableName))
                    {
                        var variableValue = map[variableName];
                        resultReport = resultReport.Replace(parseTokenSource, variableValue);
                    }
                    else
                        break;
                }
                else
                    break;

            }
            #endregion

            return resultReport;
        }
    }
}