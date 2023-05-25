using MultiplayerServer.GS;
using MultiplayerServer.Utilities;
using Newtonsoft.Json;

namespace MultiplayerServer;

class Program
{
    static void Main()
    {
        // Construct the path to the ServerConfig.json file
        string configPath = Path.Combine("Assets", "Config", "ServerConfig.json");

        Console.WriteLine("Server configpath : " + configPath);
        string configJson = File.ReadAllText(configPath);
        var serverConfig = JsonConvert.DeserializeObject<ServerConfig>(configJson);

        // Retrieve the appropriate IP address based on the current environment
        var environmentConfig = ServerConfig.GetSelectedEnvironmentConfig(serverConfig);

        // Use the IP address for your server setup
        GameServer server = new ();
        server.Start(environmentConfig.IPAddress, environmentConfig.Port);
        // Example usage:
        Console.WriteLine("Server started. Using IP address: " + environmentConfig.IPAddress);

        // Keep the server running until the user presses Enter
        Console.ReadLine();
    }
}