using System.Net;
using System.Net.Sockets;

namespace MultiplayerServer.GS;

public class GameServer
{
    private Socket? serverSocket;
    private readonly byte[] buffer = new byte[1024]; // Buffer for receiving data

    public void Start(string ipAddressString, int port)
    {
        // Create the server socket
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Bind the server socket to a specific IP address and port
        IPAddress ipAddress = IPAddress.Parse(ipAddressString);
        IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
        serverSocket.Bind(endPoint);

        // Start listening for incoming client connections
        serverSocket.Listen(5); // Maximum number of queued connections

        Console.WriteLine("Server started. Waiting for client connections...");

        // Start accepting client connections in a separate thread
        Thread acceptThread = new (AcceptConnections);
        acceptThread.Start();
    }

    private void AcceptConnections()
    {
        while (true)
        {
            // Accept an incoming client connection
            Socket clientSocket = serverSocket.Accept();

            Console.WriteLine("Client connected: " + clientSocket.RemoteEndPoint);

            // Start a new thread to handle the client connection
            Thread clientThread = new (HandleClient);
            clientThread.Start(clientSocket);
        }
    }

    private void HandleClient(object clientSocketObj)
    {
        Socket clientSocket = (Socket)clientSocketObj;

        // Handle the client connection
        try
        {
            while (true)
            {
                // Receive data from the client
                int bytesRead = clientSocket.Receive(buffer);
                string message = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received from client: " + message);

                // Process the received data or perform any necessary actions

                // Example: Send a response back to the client
                string response = "Hello from server!";
                byte[] responseBytes = System.Text.Encoding.ASCII.GetBytes(response);
                clientSocket.Send(responseBytes);
            }
        }
        catch (SocketException ex)
        {
            Console.WriteLine("Client disconnected: " + ex.Message);
        }
        finally
        {
            // Close the client socket
            clientSocket.Close();
        }
    }
}