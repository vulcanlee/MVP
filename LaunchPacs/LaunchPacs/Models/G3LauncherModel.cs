namespace LaunchPacs.Models
{
    public class G3LauncherModel
    {
        // 實際下達的命令
        // C:\INFINITT\viewer\G3Launcher.exe http://10.1.1.142+admin+nimda++S+RU799OR39MJ3BCF1+BET0001

        public string ViewerPath { get; set; } = @"C:\INFINITT\viewer\G3Launcher.exe";
        public string Iis { get; set; } = "";
        public string UserId { get; set; } = "";
        public string UserPassword { get; set; } = "";
        public string AccessionNo { get; set; } = "";
        public string PatientId { get; set; } = ""; 
        public string MaxNo { get; set; } = "";
        public string Suid { get; set; } = "";
    }
}
