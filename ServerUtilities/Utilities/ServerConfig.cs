using System;

namespace MultiplayerServer.Utilities
{
    [Serializable]
    public class ServerConfig
    {
        public string CurrentEnvironment { get; set; }
        public EnvironmentConfig Development { get; set; }
        public EnvironmentConfig Production { get; set; }

        public enum Environment
        {
            Development,
            Production
        }

        public static EnvironmentConfig GetSelectedEnvironmentConfig(ServerConfig serverConfig)
        {
            // Retrieve the appropriate IP address based on the current environment
            switch (serverConfig.CurrentEnvironment.ToLower())
            {
                case "development":
                    return serverConfig.Development;
                case "production":
                    return serverConfig.Production;
                default:
                    throw new ArgumentException("Invalid CurrentEnvironment value in the configuration file.");
            }
        }

        public static EnvironmentConfig GetEnvironmentConfig(ServerConfig serverConfig, Environment environment)
        {
            switch(environment)
            {
                case Environment.Development:
                    return serverConfig.Development;
                case Environment.Production:
                    return serverConfig.Production;
            }
            return serverConfig.Development;
        }
    }
}
