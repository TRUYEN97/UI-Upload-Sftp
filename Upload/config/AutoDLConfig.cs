using System;
using System.IO;
using Newtonsoft.Json;

namespace Upload.Config
{
    public class AutoDLConfig
    {
        private static readonly Lazy<AutoDLConfig> _instance = new Lazy<AutoDLConfig>(() => new AutoDLConfig());
        private static readonly string cfPath = "./AutoDL_Conig.json";
        private ConfigModel _configModel;
        private AutoDLConfig() {
            if (!Init(cfPath))
            {
                _configModel = new ConfigModel();
            }
            UpdateCf();
        }

        private bool Init(string cfPath)
        {
            try
            {
                if (!File.Exists(cfPath))
                { 
                    return false; 
                }
                string configText = File.ReadAllText(cfPath);
                _configModel = JsonConvert.DeserializeObject<ConfigModel>(configText);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static ConfigModel ConfigModel => Instance._configModel;

        public static bool UpdateConfig()
        {
            return Instance.UpdateCf();
        }

        public bool UpdateCf()
        {
            try
            {
                string cfJson = JsonConvert.SerializeObject(_configModel, Formatting.Indented);
                File.WriteAllText(cfPath, cfJson);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static AutoDLConfig Instance => _instance.Value;
    }
}
