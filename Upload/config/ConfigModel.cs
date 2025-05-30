

namespace Upload.Config
{
    public class ConfigModel
    {
        public string TempDir {  get; set; }

        public ConfigModel()
        {
            // Thông tin cài đặt của chương trình
            SftpConfig = new SftpConfig() { 
                Host = "200.166.2.201",
                Port = 4422,
                User = "user",
                Password = "ubnt",
            };
            RemotePath = "/AutoDownload-Store";
            TempDir = "./temp";
            Password = "e10adc3949ba59abbe56e057f20f883e";
        }
        public SftpConfig SftpConfig { get;  set; }

        public string RemotePath { get;  set; }
        public string Password { get; internal set; }
    }
}
