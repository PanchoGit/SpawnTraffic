namespace SpawnTraffic.Logger
{
    public class LoggerSetting
    {
        public static string LogConnectionString { get; set; }

        public string LogConnection
        {
            get => LogConnectionString;
            set => LogConnectionString = value;
        }

        public string PluginFolder { get; set; }

        public string Log4NetConfig { get; set; }
    }
}
