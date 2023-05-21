using MultiplayerServer.GS;
using System.Text.Json;

namespace MultiplayerServer;

class ServerConfig
{
    public string? CurrentEnvironment { get; set; }
    public EnvironmentConfig? Development { get; set; }
    public EnvironmentConfig? Production { get; set; }
}

class EnvironmentConfig
{
    public string? IPAddress { get; set; }
    public int Port { get; set; }
}

class Program
{
    static void Main()
    {
        // Read the server configuration from ServerConfig.json
        string configPath = Path.Combine("Assets", "Config", "ServerConfig.json");
        string configJson = File.ReadAllText(configPath);
        var serverConfig = JsonSerializer.Deserialize<ServerConfig>(configJson);

        // Retrieve the appropriate IP address based on the current environment
        var environmentConfig = GetEnvironmentConfig(serverConfig);

        // Use the IP address for your server setup
        GameServer server = new GameServer();
        server.Start(environmentConfig.IPAddress, environmentConfig.Port);
        // Example usage:
        Console.WriteLine("Server started. Using IP address: " + environmentConfig.IPAddress);

        // Keep the server running until the user presses Enter
        Console.ReadLine();
    }

    static EnvironmentConfig GetEnvironmentConfig(ServerConfig serverConfig)
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
}