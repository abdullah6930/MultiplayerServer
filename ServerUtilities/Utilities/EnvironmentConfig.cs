using System;

namespace MultiplayerServer.Utilities
{
    [Serializable]
    public class EnvironmentConfig
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
    }
}
