namespace LaunchPacs.Models
{
    public class G3LauncherModel
    {
        // 實際下達的命令
        // C:\INFINITT\viewer\G3Launcher.exe http://10.1.1.142+admin+nimda++S+RU799OR39MJ3BCF1+BET0001

        public string ViewerPath { get; set; } = "";
        public string Iis { get; set; } = "";
        public string LID { get; set; } = "";
        public string LPW { get; set; } = "";
        public string AN { get; set; } = "";
        public string PID { get; set; } = ""; 
        public string MaxNo { get; set; } = "";
        public string Suid { get; set; } = "";
    }
}
